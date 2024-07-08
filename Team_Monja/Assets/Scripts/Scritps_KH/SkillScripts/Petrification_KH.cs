using UnityEngine;

public class Petrification_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _residentScript = default;

    [SerializeField]
    private EffectManager _effectManager; // EffectManager�̎Q�Ƃ�ǉ�

    private PlayerMove_MT _playerMove = default;
    private MonsterRandomWalk_KH _monsterRandomWalk = default;
    private PlayerRangeInJudge_KH _playerRangeInJudge = default;
    private Rigidbody _rigidbody = default;
    private AudioSource _audioSource = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private PlayerSkill_KH _playerSkill = default;
    private PlayerSkill_KH _myPlayerSkill = default;
    private MonsterSkill_KH _monsterSkill = default;
    private NormalAttack_KH _normalAttack = default;
    private PlayerGuard_KH _playerGuard = default;

    private GameObject _petrificationArea;


    private float _deleteTime = 1f;
    private float _elapsedTime = 0f;

    private bool _isPetrification = false;
    private bool _isSphereExists = false;

    void Start()
    {
        // �q�I�u�W�F�N�g�̒�����PetrificationArea���擾
        _petrificationArea = transform.Find("PetrificationArea").gameObject;
        _soundEffectManagement = _residentScript.GetComponent<SoundEffectManagement_KH>();
        _myPlayerSkill = GetComponent<PlayerSkill_KH>();

    }

    void Update()
    {
        UpdateTime();
    }

    /// <summary>
    /// �Ή��͈͂�Sphere��L����
    /// </summary>
    public void CreatePetrificationArea()
    {
        _isSphereExists = true;
        _petrificationArea.SetActive(true);

        if (_audioSource == null)
        {
            _audioSource = GetComponentInChildren<AudioSource>();
        }
        // SE��炷
        _soundEffectManagement.PlayMagicSound(_audioSource);

        //�X�L���G�t�F�N�g
        if (_effectManager != null)
        {
            _effectManager.ShowSpecialAttackEffect(transform);
        }
        else
        {
            Debug.LogError("EffectManager component is not found.");
        }
    }

    /// <summary>
    /// �ړ��n�X�N���v�g�ƍU���n�X�N���v�g�𖳌�������
    /// </summary>
    /// <param name="petrificationTarget">�Ή��Ώ�</param>
    public void Petrification(GameObject petrificationTarget)
    {
        if ((petrificationTarget.CompareTag("Enemy") || petrificationTarget.CompareTag("Boss")) && gameObject.CompareTag("Player"))     // �G��Ή�����ꍇ
        {
            _monsterRandomWalk = petrificationTarget.GetComponent<MonsterRandomWalk_KH>();
            _playerRangeInJudge = petrificationTarget.GetComponent<PlayerRangeInJudge_KH>();
            _rigidbody = petrificationTarget.GetComponent<Rigidbody>();
            _monsterSkill = petrificationTarget.GetComponent<MonsterSkill_KH>();

            // �ړ��n�̃X�N���v�g�𖳌���
            _monsterRandomWalk.enabled = false;
            _playerRangeInJudge.enabled = false;
            _rigidbody.velocity = Vector3.zero;

            // �U���n�̃X�N���v�g�𖳌���
            _monsterSkill.enabled = false;

            _isPetrification = true;
        }
        else if (petrificationTarget.CompareTag("Player") &&( gameObject.CompareTag("Enemy") || gameObject.CompareTag("Boss")))       // �v���C���[��Ή�����ꍇ
        {
            _playerMove = petrificationTarget.GetComponent<PlayerMove_MT>();
            _playerSkill = petrificationTarget.GetComponent<PlayerSkill_KH>();
            petrificationTarget.TryGetComponent<NormalAttack_KH>(out _normalAttack);
            petrificationTarget.TryGetComponent<PlayerGuard_KH>(out _playerGuard);
            _rigidbody = petrificationTarget.GetComponent<Rigidbody>();

            // �ړ��n�̃X�N���v�g�𖳌���
            _playerMove.enabled = false;
            _rigidbody.velocity = Vector3.zero;

            // �U���n�̃X�N���v�g�𖳌���
            _playerSkill.enabled = false;
            if (_normalAttack != null)
            {
                _normalAttack.enabled = false;
            }
            if (_playerGuard != null)
            {
                _playerGuard.enabled = false;
            }

            _isPetrification = true;
        }
    }

    /// <summary>
    /// �ړ��n�X�N���v�g�ƍU���n�X�N���v�g��L��������
    /// </summary>
    private void PetrificationCancellation()
    {
        if (!_isPetrification) return;

        if (gameObject.CompareTag("Player"))     // �G�̐Ή�����������
        {
            if (_monsterRandomWalk != null)
            {
                _monsterRandomWalk.enabled = true;
            }
            else if (_playerRangeInJudge != null)
            {
                _playerRangeInJudge.enabled = true;
            }

            _monsterSkill.enabled = true;

            _isPetrification = false;
        }
        else if (gameObject.CompareTag("Enemy") || gameObject.CompareTag("Boss"))       // �v���C���[�̐Ή�����������ꍇ
        {
            _playerMove.enabled = true;

            _playerSkill.enabled = true;
            if (_normalAttack != null)
            {
                _normalAttack.enabled = true;
            }
            if (_playerGuard != null)
            {
                _playerGuard.enabled = true;
            }

            _isPetrification = false;
        }
    }

    /// <summary>
    /// ��莞�Ԍ�Ή��͈͂��폜����
    /// </summary>
    private void UpdateTime()
    {
        if (!_isSphereExists) return;     // Sphere������Ƃ��ȊO�͏������s��Ȃ�

        // ���ԉ��Z
        _elapsedTime += Time.deltaTime;

        // �K�莞�ԂɒB���Ă����ꍇ
        if (_elapsedTime > _deleteTime)
        {
            AreaDestroy();
        }
    }

    private void AreaDestroy()
    {
        _petrificationArea.SetActive(false);
        PetrificationCancellation();
        _elapsedTime = 0f;
        _isSphereExists = false;
        _myPlayerSkill.IsUseSkill = false;
    }

    private void OnDisable()
    {
        AreaDestroy();
        PetrificationCancellation();
    }

    private void OnDestroy()
    {
        AreaDestroy();
        PetrificationCancellation();
    }
}
