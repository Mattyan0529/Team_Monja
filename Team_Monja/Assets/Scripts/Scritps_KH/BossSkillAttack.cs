using UnityEngine;

public class BossSkillAttack : MonoBehaviour,IDamagable
{
    //¼–{
    private CharacterAnim_MT _characterAnim = default;

    // ‚»‚ê‚¼‚ê‚ÌUŒ‚‚ğÀs‚·‚éŠ„‡
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
    private float _addSpownPos = 1f;     // ï¿½eï¿½ğ¶ï¿½ï¿½ï¿½ï¿½ï¿½Æ‚ï¿½ï¿½ï¿½yï¿½É‘ï¿½ï¿½ï¿½ï¿½l

    #endregion

    #region HitAttack

    [SerializeField]
    private EffectManager _effectManager; // EffectManagerï¿½ÌQï¿½Æ‚ï¿½Ç‰ï¿½

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

        // ï¿½qï¿½Iï¿½uï¿½Wï¿½Fï¿½Nï¿½gï¿½ï¿½ï¿½ï¿½Bulletï¿½ï¿½æ“¾
        _bullet = transform.Find("Bullet").gameObject;
        _bulletHitDecision = _bullet.GetComponent<BulletHitDecision_KH>();

        #endregion

        #region HitAttack

        

        // ï¿½qï¿½Iï¿½uï¿½Wï¿½Fï¿½Nï¿½gï¿½Ì’ï¿½ï¿½ï¿½ï¿½ï¿½AttackAreaï¿½ï¿½æ“¾
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
        // 1‚©‚ç10‚Ü‚Å‚Ì—”
        int skillNum = Random.Range(0, _fireSphereProbability + _hitProbability + _biteProbability);

        switch (skillNum)
        {
            case < _fireSphereProbability:        // ‰Î‹…
                FireSphere();
                _characterAnim.NowAnim = "Skill";
                break;
            case < _fireSphereProbability + _hitProbability:        // ‰£‚é
                HitAttack();
                _characterAnim.NowAnim = "Attack";
                break;
            case < _fireSphereProbability + _hitProbability + _biteProbability:       // Šš‚Ş
                BiteAttack();
                _characterAnim.NowAnim = "Attack2";
                break;
        }
    }

    private void FireSphere()
    {
        if (_isShot) return;      // ï¿½dï¿½ï¿½ï¿½ÅUï¿½ï¿½ï¿½Í‚ï¿½ï¿½È‚ï¿½

        // ï¿½ï¿½ï¿½xï¿½ï¿½tï¿½ï¿½ï¿½ï¿½
        _bullet.transform.position = new Vector3(transform.position.x, transform.position.y + _addSpownPos, transform.position.z);
        Rigidbody rigidbody = _bullet.GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * _bulletSpeed;
        _bullet.transform.SetParent(null);
        _bulletHitDecision.ActivateBullet();

        if (_audioSource == null)
        {
            _audioSource = GetComponentInChildren<AudioSource>();
        }
        // SEï¿½ï¿½Â‚ç‚·
        _soundEffectManagement.PlayLongDistanceAttackSound(_audioSource);

        _isShot = true;
    }

    private void HitAttack()
    {
        if (_monsterRandomWalk.enabled) return;     // ï¿½ï¿½ï¿½ï¿½ï¿½_ï¿½ï¿½ï¿½Ú“ï¿½ï¿½ï¿½ï¿½iï¿½vï¿½ï¿½ï¿½Cï¿½ï¿½ï¿½[ï¿½ï¿½ï¿½Uï¿½ï¿½ï¿½ÍˆÍŠOï¿½jï¿½Íï¿½ï¿½ï¿½ï¿½ï¿½ï¿½È‚ï¿½

        //ï¿½Xï¿½Lï¿½ï¿½ï¿½Gï¿½tï¿½Fï¿½Nï¿½g
        if (_effectManager != null)
        {
            _effectManager.ShowSpecialAttackEffect(transform);
        }

        // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½~ï¿½ß‚ï¿½
        if (gameObject.CompareTag("Enemy") || gameObject.CompareTag("Boss"))
        {
            _playerRangeInJudge.enabled = false;
        }

        _soundEffectManagement.PlayStrongPunchSound(_audioSource);
    }

    private void BiteAttack()
    {
        if (_monsterRandomWalk.enabled) return;     // ï¿½ï¿½ï¿½ï¿½ï¿½_ï¿½ï¿½ï¿½Ú“ï¿½ï¿½ï¿½ï¿½iï¿½vï¿½ï¿½ï¿½Cï¿½ï¿½ï¿½[ï¿½ï¿½ï¿½Uï¿½ï¿½ï¿½ÍˆÍŠOï¿½jï¿½Íï¿½ï¿½ï¿½ï¿½ï¿½ï¿½È‚ï¿½

        //ï¿½Xï¿½Lï¿½ï¿½ï¿½Gï¿½tï¿½Fï¿½Nï¿½g
        if (_effectManager != null)
        {
            _effectManager.ShowSpecialAttackEffect(transform);
        }

        // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½~ï¿½ß‚ï¿½
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
        // ‘Šè‚Æ©•ª‚ÌStatusManager‚ª—¼•û•K—v
        StatusManager_MT targetStatusManager = hitObj.gameObject.GetComponent<StatusManager_MT>();
        StatusManager_MT myStatusManager = GetComponent<StatusManager_MT>();

        HitPointCalculation(myStatusManager, targetStatusManager);
    }

    /// <summary>
    /// ƒ_ƒ[ƒWŒvZ
    /// </summary>
    public void HitPointCalculation(StatusManager_MT myStatus, StatusManager_MT targetStatus)
    {
        int myAttackPower = myStatus.Strength;        // ©•ª‚ÌUŒ‚—Í‚ğget‚µ‚Ä‚­‚é
        int targetDefensePower = targetStatus.Defense;        // ‘Šè‚Ì–hŒä—Í‚ğget‚µ‚Ä‚­‚é
        int targetHitPoint = targetStatus.HP;        // ‘Šè‚ÌHP‚ğget‚µ‚Ä‚­‚é

        if (myAttackPower < targetDefensePower) return;        // –hŒä—Í‚Ì‚Ù‚¤‚ª‚‚©‚Á‚½‚ç0ƒ_ƒ[ƒW

        int damage = targetHitPoint - (myAttackPower - targetDefensePower);
        //_createDamageImage.InstantiateDamageImage(gameObject, targetStatus.gameObject, myAttackPower - targetDefensePower);
        //_writeHitPoint.UpdateHitPoint(targetStatus, damage);      // targetStatus‚ÌHP‚ğXV
    }
}

