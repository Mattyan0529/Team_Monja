using UnityEngine;

public class EvillMageSkillAttack_KH : MonoBehaviour, IDamagable_KH
{
    [SerializeField]
    private GameObject _residentScript;



    private float _bulletSpeed = 50f;

    private float _addSpownPos = 1f;     // �e�𐶐�����Ƃ���y�ɑ����l

    private GameObject _bullet = default;
    private WriteHitPoint_KH _writeHitPoint = default;
    private BulletHitDecision_KH _bulletHitDecision = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private AudioSource _audioSource = default;
    private CreateDamageImage_KH _createDamageImage = default;
    private PlayerSkill_KH _playerSkill = default;
    private CharacterAnim_MT _characterAnim = default;
    private ChangeEnemyMoveType_KH _changeEnemyMoveType = default;

    private float _deleteTime = 2f;
    private float _elapsedTime = 0f;

    private bool _isShot = false;

    void Start()
    {
        _writeHitPoint = _residentScript.GetComponent<WriteHitPoint_KH>();
        _createDamageImage = _residentScript.GetComponent<CreateDamageImage_KH>();
        _soundEffectManagement = _residentScript.GetComponent<SoundEffectManagement_KH>();
        _playerSkill = GetComponent<PlayerSkill_KH>();
        _characterAnim = GetComponent<CharacterAnim_MT>();
        _audioSource = GetComponent<AudioSource>();
        _changeEnemyMoveType = GetComponent<ChangeEnemyMoveType_KH>();

        // �q�I�u�W�F�N�g����Bullet���擾
        _bullet = transform.Find("Bullet").gameObject;
        _bulletHitDecision = _bullet.GetComponent<BulletHitDecision_KH>();
    }

    void Update()
    {
        UpdateTime();
    }

    /// <summary>
    /// �������̒e�𐶐�����
    /// </summary>
    public void SpecialAttack()
    {
        if (_isShot) return;      // �d���ōU���͂��Ȃ�

        //���{
        _characterAnim.NowAnim = "Skill";


        // ���x��t����
        _bullet.transform.position = new Vector3(transform.position.x, transform.position.y + _addSpownPos, transform.position.z);
        Rigidbody rigidbody = _bullet.GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * _bulletSpeed;
        _bullet.transform.SetParent(null);
        _bulletHitDecision.ActivateBullet();

        _changeEnemyMoveType.IsMove = false;

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
        _bullet.transform.SetParent(gameObject.transform);

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

        int damage = myAttackPower - targetDefensePower;

        if (myAttackPower <= targetDefensePower)
        {
            // �h��͂̂ق��������ꍇ�̓_���[�W��1�Ƃ���
            int smallestDamage = 1;
            damage = smallestDamage;
        }

        int hitPointAfterDamage = targetHitPoint - damage;

        _createDamageImage.InstantiateDamageImage(gameObject, targetStatus.gameObject, damage);
        _writeHitPoint.UpdateHitPoint(targetStatus, hitPointAfterDamage);      // targetStatus��HP���X�V
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
            _characterAnim.NowAnim = "Idle";
            _bulletHitDecision.DisableBullet();
            _bullet.transform.SetParent(gameObject.transform);
            _elapsedTime = 0f;
            _changeEnemyMoveType.IsMove = true;
            _isShot = false;
            _playerSkill.IsUseSkill = false;
        }
    }

    private void OnDisable()
    {
        if (_bulletHitDecision != null)
        {
            _bulletHitDecision.DisableBullet();
        }
        _isShot = false;
    }
}
