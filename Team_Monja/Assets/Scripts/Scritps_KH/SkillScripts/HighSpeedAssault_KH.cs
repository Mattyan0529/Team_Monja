using UnityEngine;

public class HighSpeedAssault_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _residentScript;

    private WriteHitPoint_KH _writeHitPoint = default;
    private MonsterRandomWalk_KH _monsterRandomWalk = default;
    private PlayerMove_MT _playerMove = default;
    private Rigidbody _rigidbody = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private AudioSource _audioSource = default;

    private StatusManager_MT _myStatusManager = default;        // �����̃X�e�[�^�X
    private StatusManager_MT _targetStatusManager = default;    // �U������̃X�e�[�^�X

    private bool _isSpeedUp = false;

    private float _addForce = 20f;

    private float _updateTime = 1f;
    private float _elapsedTime = 0f;

    void Start()
    {
        _writeHitPoint = _residentScript.GetComponent<WriteHitPoint_KH>();
        _soundEffectManagement = _residentScript.GetComponent<SoundEffectManagement_KH>();
        _rigidbody = GetComponent<Rigidbody>();

        if (gameObject.tag == "Enemy")
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
        if (_isSpeedUp) return;
        if (gameObject.CompareTag("Enemy") && _monsterRandomWalk.enabled) return;

        if (gameObject.CompareTag("Enemy"))
        {
            _monsterRandomWalk.enabled = false;     // �����]�����I�t
            _rigidbody.AddForce(transform.forward * _addForce, ForceMode.Impulse);

            if (_audioSource == null)
            {
                _audioSource = GetComponentInChildren<AudioSource>();
            }

            _soundEffectManagement.PlaySlashAttackSound(_audioSource);
        }
        else if (gameObject.CompareTag("Player"))
        {
            _playerMove.enabled = false;        // �����]�����I�t
            _rigidbody.AddForce(transform.forward * _addForce, ForceMode.Impulse);

            if (_audioSource == null)
            {
                _audioSource = GetComponentInChildren<AudioSource>();
            }

            _soundEffectManagement.PlayWindSound(_audioSource);
        }

        _isSpeedUp = true;
    }

    /// <summary>
    /// �ˌ��I��
    /// </summary>
    private void SpeedDown()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            _monsterRandomWalk.enabled = true;     // �����]�����I��
        }
        else if (gameObject.CompareTag("Player"))
        {
            _playerMove.enabled = true;        // �����]�����I��
        }

        _isSpeedUp = false;
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
    /// HP�̌v�Z
    /// </summary>
    public void HitPointCalculation(StatusManager_MT myStatus, StatusManager_MT targetStatus)
    {
        int myAttackPower = myStatus.Strength;        // �����̍U���͂�get���Ă���
        int targetDefensePower = targetStatus.Defense;        // ����̖h��͂�get���Ă���
        int targetHitPoint = targetStatus.HP;        // �����HP��get���Ă���

        if (myAttackPower < targetDefensePower) return;        // �h��͂̂ق�������������0�_���[�W

        int damage = targetHitPoint - (myAttackPower - targetDefensePower);
        _writeHitPoint.UpdateHitPoint(targetStatus, damage);      // targetStatuses��HP���X�V
        SpeedDown();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isSpeedUp) return;        // �����ˌ����łȂ���Ώ������s��Ȃ�
        if (!collision.gameObject.GetComponent<PlayerGuard_KH>()) return;
        if (!collision.gameObject.GetComponent<PlayerGuard_KH>().IsGuard) return;       // �K�[�h���ł���΍U������

        if (gameObject.CompareTag("Enemy") && collision.gameObject.CompareTag("Player"))       // �����������X�^�[�ő��肪�v���C���[�������ꍇ
        {
            _soundEffectManagement.StopSound(_audioSource);
            _soundEffectManagement.PlaySlowPunchSound(_audioSource);

            _myStatusManager = GetComponent<StatusManager_MT>();
            _targetStatusManager = collision.gameObject.GetComponent<StatusManager_MT>();
            HitPointCalculation(_myStatusManager, _targetStatusManager);
        }
        else if (gameObject.CompareTag("Player") && collision.gameObject.CompareTag("Enemy"))       // �������v���C���[�ő��肪�����X�^�[�������ꍇ
        {
            _soundEffectManagement.StopSound(_audioSource);
            _soundEffectManagement.PlaySlowPunchSound(_audioSource);

            _myStatusManager = GetComponent<StatusManager_MT>();
            _targetStatusManager = collision.gameObject.GetComponent<StatusManager_MT>();
            HitPointCalculation(_myStatusManager, _targetStatusManager);
        }
    }
}
