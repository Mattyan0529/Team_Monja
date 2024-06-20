using UnityEngine;

public class LongDistanceAttack_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _residentScript;

    private float _bulletSpeed = 50f;

    private float _addSpownPos;     // �e�𐶐�����Ƃ���y�ɑ����l

    private GameObject _bullet = default;
    private StatusManager_MT statusManager = default;
    private BulletHitDecision_KH _bulletHitDecision = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private AudioSource _audioSource = default;

    private float _deleteTime = 3f;
    private float _elapsedTime = 0f;

    private bool _isShot = false;

    void Start()
    {
        statusManager = _residentScript.GetComponent<StatusManager_MT>();
        _soundEffectManagement = _residentScript.GetComponent<SoundEffectManagement_KH>();

        // �q�I�u�W�F�N�g����Bullet���擾
        _bullet = transform.Find("Bullet").gameObject;
        _bulletHitDecision = _bullet.GetComponent<BulletHitDecision_KH>();
        _bulletHitDecision.DisableBullet();
    }

    void Update()
    {
        UpdateTime();
    }

    /// <summary>
    /// �������̒e�𐶐�����
    /// </summary>
    public void GenerateBullet()
    {
        if (_isShot) return;      // �d���ōU���͂��Ȃ�

        // ���x��t����
        _bullet.transform.position = new Vector3(transform.position.x, transform.position.y + _addSpownPos, transform.position.z);
        Rigidbody rigidbody = _bullet.GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * _bulletSpeed;
        _bulletHitDecision.ActivateBullet();

        if (_audioSource == null)
        {
            _audioSource = GetComponentInChildren<AudioSource>();
        }
        // SE��炷
        _soundEffectManagement.PlayLongDistanceAttackSound(_audioSource);

        _isShot = true;
    }

    /// <summary>
    /// ��������������擾
    /// </summary>
    public void HitDecision(GameObject hitObj)
    {
        _isShot = false;

        // ����Ǝ�����StatusManager�������K�v
        StatusManager_MT targetStatusManager = hitObj.gameObject.GetComponent<StatusManager_MT>();
        StatusManager_MT myStatusManager = GetComponent<StatusManager_MT>();

        HitPointCalculation(myStatusManager, targetStatusManager);
    }

    /// <summary>
    /// �_���[�W�v�Z
    /// </summary>
    public void HitPointCalculation(StatusManager_MT myStatus, StatusManager_MT targetStatus)
    {
        int myAttackPower = myStatus.Strength;        // �����̍U���͂�get���Ă���
        int targetDefensePower = targetStatus.Defense;        // ����̖h��͂�get���Ă���
        int targetHitPoint = targetStatus.HP;        // �����HP��get���Ă���

        if (myAttackPower < targetDefensePower) return;        // �h��͂̂ق�������������0�_���[�W

        int damage = targetHitPoint - (myAttackPower - targetDefensePower);

        statusManager.UpdateHitPoint(targetStatus, damage);      // targetStatus��HP���X�V
    }

    /// <summary>
    /// ��莞�Ԍ�e���폜����
    /// </summary>
    private void UpdateTime()
    {
        if (!_isShot) return;     // �U�����ȊO�͏������s��Ȃ�

        // ���ԉ��Z
        _elapsedTime += Time.deltaTime;

        // �K�莞�ԂɒB���Ă����ꍇ
        if (_elapsedTime > _deleteTime)
        {
            _bulletHitDecision.DisableBullet();
            _elapsedTime = 0f;
            _isShot = false;
        }
    }
}
