using UnityEngine;

public class Petrification_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _residentScript = default;

    private PlayerMove_MT _playerMove = default;
    private MonsterRandomWalk_KH _monsterRandomWalk = default;
    private PlayerRangeInJudge_KH _playerRangeInJudge = default;
    private Rigidbody _rigidbody = default;
    private AudioSource _audioSource = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;

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
    }

    /// <summary>
    /// �ړ��n�X�N���v�g�𖳌�������
    /// </summary>
    /// <param name="petrificationTarget">�Ή��Ώ�</param>
    public void Petrification(GameObject petrificationTarget)
    {
        if (petrificationTarget.CompareTag("Enemy") && gameObject.CompareTag("Player"))     // �G��Ή�����ꍇ
        {
            _monsterRandomWalk = petrificationTarget.GetComponent<MonsterRandomWalk_KH>();
            _playerRangeInJudge = petrificationTarget.GetComponent<PlayerRangeInJudge_KH>();
            _rigidbody = petrificationTarget.GetComponent<Rigidbody>();

            // �ړ��n�̃X�N���v�g�𖳌���
            _monsterRandomWalk.enabled = false;
            _playerRangeInJudge.enabled = false;
            _rigidbody.velocity = Vector3.zero;

            _isPetrification = true;
        }
        else if (petrificationTarget.CompareTag("Player") && gameObject.CompareTag("Enemy"))       // �v���C���[��Ή�����ꍇ
        {
            _playerMove = petrificationTarget.GetComponent<PlayerMove_MT>();
            _rigidbody = petrificationTarget.GetComponent<Rigidbody>();

            // �ړ��n�̃X�N���v�g�𖳌���
            _playerMove.enabled = false;
            _rigidbody.velocity = Vector3.zero;

            _isPetrification = true;
        }
    }

    /// <summary>
    /// �ړ��n�X�N���v�g��L��������
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

            _isPetrification = false;
        }
        else if (gameObject.CompareTag("Enemy"))       // �v���C���[�̐Ή�����������ꍇ
        {
            _playerMove.enabled = true;

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
            _petrificationArea.SetActive(false);
            PetrificationCancellation();
            _elapsedTime = 0f;
            _isSphereExists = false;
        }
    }

    private void OnDisable()
    {
        PetrificationCancellation();
    }
}
