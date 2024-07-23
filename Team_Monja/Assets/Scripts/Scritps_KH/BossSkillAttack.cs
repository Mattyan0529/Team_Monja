using UnityEngine;

public class BossSkillAttack : MonoBehaviour, IDamagable
{
    //���{
    private CharacterAnim_MT _characterAnim = default;

    // ���ꂼ��̍U�������s���銄��
    private const int _fireSphereProbability = 1;
    private const int _hitProbability = 4;
    private const int _biteProbability = 5;

    private float _sphereDeleteTime = 2f;
    private float _hitAttackDeleteTime = 0.5f;
    private float _elapsedTime = 0f;

    private AudioSource _audioSource = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private WriteHitPoint_KH _writeHitPoint = default;
    private CreateDamageImage_KH _createDamageImage = default;

    private GameObject _residentScript;

    #region FireSphere

    private GameObject _bullet = default;
    private BulletHitDecision_KH _bulletHitDecision = default;
    private bool _isShot = false;

    private float _bulletSpeed = 50f;
    private float _addSpownPos = 1f;     // �e�𐶐�����Ƃ���y�ɑ����l

    #endregion

    #region HitAttack

    [SerializeField]
    private EffectManager _effectManager; // EffectManager�̎Q�Ƃ�ǉ�

    private MonsterRandomWalk_KH _monsterRandomWalk = default;
    private PlayerRangeInJudge_KH _playerRangeInJudge = default;

    private bool _isAttack = false;
    private GameObject _attackArea;

    #endregion

    private void Awake()
    {
        _monsterRandomWalk = GetComponent<MonsterRandomWalk_KH>();
        _playerRangeInJudge = GetComponent<PlayerRangeInJudge_KH>();
    }

    void Start()
    {
        _characterAnim = GetComponent<CharacterAnim_MT>();

        _residentScript = GameObject.Find("ResidentScripts");
        _writeHitPoint = _residentScript.GetComponent<WriteHitPoint_KH>();
        _createDamageImage = _residentScript.GetComponent<CreateDamageImage_KH>();
        _soundEffectManagement = _residentScript.GetComponent<SoundEffectManagement_KH>();

        #region FireSphere

        // �q�I�u�W�F�N�g����Bullet���擾
        _bullet = transform.Find("Bullet").gameObject;
        _bulletHitDecision = _bullet.GetComponent<BulletHitDecision_KH>();

        #endregion

        #region HitAttack



        // �q�I�u�W�F�N�g�̒�����AttackArea���擾
        _attackArea = transform.Find("AttackArea").gameObject;
        _attackArea.SetActive(false);

        #endregion
    }

    private void Update()
    {
        UpdateSphereTime();
        UpdateHitTime();

    }

    public void SpecialAttack()
    {
        // 1����10�܂ł̗���
        int skillNum = Random.Range(0, _fireSphereProbability + _hitProbability + _biteProbability);

        switch (skillNum)
        {
            case < _fireSphereProbability:        // �΋�
                FireSphere();
                _characterAnim.NowAnim = "Skill";
                break;
            case < _fireSphereProbability + _hitProbability:        // ����
                HitAttack();
                _characterAnim.NowAnim = "Attack";
                break;
            case < _fireSphereProbability + _hitProbability + _biteProbability:       // ����
                BiteAttack();
                _characterAnim.NowAnim = "Attack2";
                break;
        }
    }

    private void FireSphere()
    {
        if (_isShot) return;      // �d���ōU���͂��Ȃ�

        // ���x��t����
        _bullet.transform.position = new Vector3(transform.position.x, transform.position.y + _addSpownPos, transform.position.z);
        Rigidbody rigidbody = _bullet.GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * _bulletSpeed;
        _bullet.transform.SetParent(null);
        _bulletHitDecision.ActivateBullet();

        if (_audioSource == null)
        {
            _audioSource = GetComponentInChildren<AudioSource>();
        }
        // SE��炷
        _soundEffectManagement.PlayLongDistanceAttackSound(_audioSource);

        _isShot = true;
    }

    private void HitAttack()
    {
        if (_monsterRandomWalk.enabled) return;     // �����_���ړ����i�v���C���[���U���͈͊O�j�͏������Ȃ�

        //�X�L���G�t�F�N�g
        if (_effectManager != null)
        {
            _effectManager.ShowSpecialAttackEffect(transform);
        }

        // �������~�߂�
        if (gameObject.CompareTag("Enemy") || gameObject.CompareTag("Boss"))
        {
            _playerRangeInJudge.enabled = false;
        }

        _soundEffectManagement.PlayStrongPunchSound(_audioSource);
    }

    private void BiteAttack()
    {
        if (_monsterRandomWalk.enabled) return;     // �����_���ړ����i�v���C���[���U���͈͊O�j�͏������Ȃ�

        //�X�L���G�t�F�N�g
        if (_effectManager != null)
        {
            _effectManager.ShowSpecialAttackEffect(transform);
        }

        // �������~�߂�
        if (gameObject.CompareTag("Enemy") || gameObject.CompareTag("Boss"))
        {
            _playerRangeInJudge.enabled = false;
        }

        _soundEffectManagement.PlayStrongPunchSound(_audioSource);
    }

    private void CreateBossAttackArea()
    {
        _isAttack = true;
        _attackArea.SetActive(true);
    }

    public void FireSphereCancel()
    {
        _isShot = false;
        _bullet.transform.SetParent(gameObject.transform);
    }

    public void HitDecision(GameObject hitObj)
    {
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

        if (myAttackPower < targetDefensePower)
        {
            // �h��͂̂ق��������ꍇ�̓_���[�W��1�Ƃ���
            int smallestDamage = 1;
            damage = smallestDamage;
        }

        int hitPointAfterDamage = targetHitPoint - damage;

        //_createDamageImage.InstantiateDamageImage(gameObject, targetStatus.gameObject, damage);
        //_writeHitPoint.UpdateHitPoint(targetStatus, hitPointAfterDamage);      // targetStatus��HP���X�V
    }

    /// <summary>
    /// ��莞�Ԍ�e���폜����
    /// </summary>
    private void UpdateSphereTime()
    {
        if (!_isShot) return;     // �U�����ȊO�͏������s��Ȃ�
        // ���ԉ��Z
        _elapsedTime += Time.deltaTime;

        // �K�莞�ԂɒB���Ă����ꍇ
        if (_elapsedTime > _sphereDeleteTime)
        {
            _bulletHitDecision.DisableBullet();
            _bullet.transform.SetParent(gameObject.transform);
            _elapsedTime = 0f;
            _isShot = false;
        }
    }

    /// <summary>
    /// ��莞�Ԍ�U���͈͂��폜����
    /// </summary>
    private void UpdateHitTime()
    {
        if (!_isAttack) return;     // �U�����ȊO�͏������s��Ȃ�

        // ���ԉ��Z
        _elapsedTime += Time.deltaTime;

        // �K�莞�ԂɒB���Ă����ꍇ
        if (_elapsedTime > _hitAttackDeleteTime)
        {
            // �������ĊJ����
            if (gameObject.CompareTag("Enemy") || gameObject.CompareTag("Boss"))
            {
                _playerRangeInJudge.enabled = true;
            }

            _attackArea.SetActive(false);
            _elapsedTime = 0f;
            _isAttack = false;
        }
    }
}