using UnityEngine;

public class HighSpeedAssault_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _residentScript;

    [SerializeField]
    private EffectManager _effectManager; // EffectManager�̎Q�Ƃ�ǉ�


    // �G�t�F�N�g�̃C���X�^���X
    private GameObject _speedUpEffectInstance;

    private WriteHitPoint_KH _writeHitPoint = default;
    private MonsterRandomWalk_KH _monsterRandomWalk = default;
    private PlayerMove_MT _playerMove = default;
    private Rigidbody _rigidbody = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private AudioSource _audioSource = default;
    private CreateDamageImage_KH _createDamageImage = default;
    private PlayerSkill_KH _playerSkill = default;

    private StatusManager_MT _myStatusManager = default;        // �����̃X�e�[�^�X
    private StatusManager_MT _targetStatusManager = default;    // �U������̃X�e�[�^�X

    private bool _isSpeedUp = false;

    private float _addForce = 20f;

    private float _updateTime = 1f;
    private float _elapsedTime = 0f;

    void Start()
    {
        _writeHitPoint = _residentScript.GetComponent<WriteHitPoint_KH>();
        _createDamageImage = _residentScript.GetComponent<CreateDamageImage_KH>();
        _soundEffectManagement = _residentScript.GetComponent<SoundEffectManagement_KH>();
        _rigidbody = GetComponent<Rigidbody>();
        _playerSkill = GetComponent<PlayerSkill_KH>();

        if (gameObject.tag == "Enemy" || gameObject.tag == "Boss")
        {
            _monsterRandomWalk = GetComponent<MonsterRandomWalk_KH>();
        }
        else if (gameObject.tag == "Player")
        {
            _playerMove = GetComponent<PlayerMove_MT>();
        }
    }

    void Update()
    {
        UpdateTime();       // ��莞�Ԍo�ߌ�ɑ��x��������
    }

    /// <summary>
    /// �ˌ��J�n
    /// </summary>
    public void SpeedUp()
    {
        // ���łɉ������̂Ƃ��ƁA�����_���ړ����͉������Ȃ�
        if (_isSpeedUp) return;
        if ((gameObject.CompareTag("Enemy") || gameObject.CompareTag("Boss")) && _monsterRandomWalk.enabled) return;

        if (gameObject.CompareTag("Enemy") || gameObject.CompareTag("Boss"))
        {
            _monsterRandomWalk.enabled = false;     // �����]�����I�t
            _rigidbody.AddForce(transform.forward * _addForce, ForceMode.Impulse);

            if (_audioSource == null)
            {
                _audioSource = GetComponentInChildren<AudioSource>();
            }

            // SE����
            _soundEffectManagement.PlaySlashAttackSound(_audioSource);

        }
        else if (gameObject.CompareTag("Player"))
        {
            if (_playerMove == null)
            {
                _playerMove = GetComponentInChildren<PlayerMove_MT>();
            }

            _playerMove.enabled = false;        // �����]�����I�t
            _rigidbody.AddForce(transform.forward * _addForce, ForceMode.Impulse);

            if (_audioSource == null)
            {
                _audioSource = GetComponentInChildren<AudioSource>();
            }
            // SE��炷
            _soundEffectManagement.PlaySlashAttackSound(_audioSource);
        }

        //�X�L���G�t�F�N�g
        if (_effectManager != null)
        {
            _effectManager.ShowSpecialAttackEffect(transform);
        }
        else
        {
            Debug.LogError("EffectManager component is not found.");
        }

        _isSpeedUp = true;
    }

    /// <summary>
    /// �ˌ��I��
    /// </summary>
    private void SpeedDown()
    {
        _isSpeedUp = false;
        if (gameObject.CompareTag("Enemy") || gameObject.CompareTag("Boss"))
        {
            _monsterRandomWalk.enabled = true;     // �����]�����I��
        }
        else if (gameObject.CompareTag("Player"))
        {
            _playerMove.enabled = true;        // �����]�����I��
        }

        _soundEffectManagement.StopSound(_audioSource);

        // �G�t�F�N�g�𖳌��ɂ���
        if (_speedUpEffectInstance != null)
        {
            Destroy(_speedUpEffectInstance);
            _speedUpEffectInstance = null;
        }

        _playerSkill.IsUseSkill = false;
    }

    /// <summary>
    /// �X�L�����������莞�Ԍo�ߌ�����]����������
    /// </summary>
    private void UpdateTime()
    {
        if (!_isSpeedUp)
        {
            _elapsedTime = 0f;
            return;
        }

        // ���ԉ��Z
        _elapsedTime += Time.deltaTime;

        // �K�莞�ԂɒB���Ă����ꍇ
        if (_elapsedTime > _updateTime)
        {
            SpeedDown();
            _elapsedTime = 0f;
        }
    }

    /// <summary>
    /// ���炷HP�̌v�Z
    /// </summary>
    public void HitPointCalculation(StatusManager_MT myStatus, StatusManager_MT targetStatus)
    {
        int myAttackPower = myStatus.Strength;        // �����̍U���͂�get���Ă���
        int targetDefensePower = targetStatus.Defense;        // ����̖h��͂�get���Ă���
        int targetHitPoint = targetStatus.HP;        // �����HP��get���Ă���

        if (myAttackPower < targetDefensePower) return;        // �h��͂̂ق�������������0�_���[�W

        int damage = targetHitPoint - (myAttackPower - targetDefensePower);
        _createDamageImage.InstantiateDamageImage(gameObject, targetStatus.gameObject, myAttackPower - targetDefensePower);
        _writeHitPoint.UpdateHitPoint(targetStatus, damage);      // targetStatuses��HP���X�V

        SpeedDown();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isSpeedUp) return;        // �����ˌ����łȂ���Ώ������s��Ȃ�
        if (collision.gameObject.GetComponent<PlayerGuard_KH>() &&
            !collision.gameObject.GetComponent<PlayerGuard_KH>().IsGuard) return;       // �K�[�h���ł���΍U������

        if ((gameObject.CompareTag("Enemy") || gameObject.CompareTag("Boss")) && collision.gameObject.CompareTag("Player"))       // �����������X�^�[�ő��肪�v���C���[�������ꍇ
        {
            _soundEffectManagement.StopSound(_audioSource);
            _soundEffectManagement.PlaySlowPunchSound(_audioSource);

            _myStatusManager = GetComponent<StatusManager_MT>();
            _targetStatusManager = collision.gameObject.GetComponent<StatusManager_MT>();
            HitPointCalculation(_myStatusManager, _targetStatusManager);
        }
        else if (gameObject.CompareTag("Player") && (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss")))       // �������v���C���[�ő��肪�����X�^�[�������ꍇ
        {
            _soundEffectManagement.StopSound(_audioSource);
            _soundEffectManagement.PlaySlowPunchSound(_audioSource);

            _myStatusManager = GetComponent<StatusManager_MT>();
            _targetStatusManager = collision.gameObject.GetComponent<StatusManager_MT>();
            HitPointCalculation(_myStatusManager, _targetStatusManager);
        }
    }
}
