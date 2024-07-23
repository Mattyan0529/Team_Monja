using UnityEngine;

public class BossSkillAttack : MonoBehaviour, IDamagable
{
    //松本
    private CharacterAnim_MT _characterAnim = default;

    // それぞれの攻撃を実行する割合
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
    private float _addSpownPos = 1f;     // 弾を生成するときにyに足す値

    #endregion

    #region HitAttack

    [SerializeField]
    private EffectManager _effectManager; // EffectManagerの参照を追加

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

        // 子オブジェクトからBulletを取得
        _bullet = transform.Find("Bullet").gameObject;
        _bulletHitDecision = _bullet.GetComponent<BulletHitDecision_KH>();

        #endregion

        #region HitAttack



        // 子オブジェクトの中からAttackAreaを取得
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
        // 1から10までの乱数
        int skillNum = Random.Range(0, _fireSphereProbability + _hitProbability + _biteProbability);

        switch (skillNum)
        {
            case < _fireSphereProbability:        // 火球
                FireSphere();
                _characterAnim.NowAnim = "Skill";
                break;
            case < _fireSphereProbability + _hitProbability:        // 殴る
                HitAttack();
                _characterAnim.NowAnim = "Attack";
                break;
            case < _fireSphereProbability + _hitProbability + _biteProbability:       // 噛む
                BiteAttack();
                _characterAnim.NowAnim = "Attack2";
                break;
        }
    }

    private void FireSphere()
    {
        if (_isShot) return;      // 重複で攻撃はしない

        // 速度を付ける
        _bullet.transform.position = new Vector3(transform.position.x, transform.position.y + _addSpownPos, transform.position.z);
        Rigidbody rigidbody = _bullet.GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * _bulletSpeed;
        _bullet.transform.SetParent(null);
        _bulletHitDecision.ActivateBullet();

        if (_audioSource == null)
        {
            _audioSource = GetComponentInChildren<AudioSource>();
        }
        // SEを鳴らす
        _soundEffectManagement.PlayLongDistanceAttackSound(_audioSource);

        _isShot = true;
    }

    private void HitAttack()
    {
        if (_monsterRandomWalk.enabled) return;     // ランダム移動中（プレイヤーが攻撃範囲外）は処理しない

        //スキルエフェクト
        if (_effectManager != null)
        {
            _effectManager.ShowSpecialAttackEffect(transform);
        }

        // 動きを止める
        if (gameObject.CompareTag("Enemy") || gameObject.CompareTag("Boss"))
        {
            _playerRangeInJudge.enabled = false;
        }

        _soundEffectManagement.PlayStrongPunchSound(_audioSource);
    }

    private void BiteAttack()
    {
        if (_monsterRandomWalk.enabled) return;     // ランダム移動中（プレイヤーが攻撃範囲外）は処理しない

        //スキルエフェクト
        if (_effectManager != null)
        {
            _effectManager.ShowSpecialAttackEffect(transform);
        }

        // 動きを止める
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
        // 相手と自分のStatusManagerが両方必要
        StatusManager_MT targetStatusManager = hitObj.gameObject.GetComponent<StatusManager_MT>();
        StatusManager_MT myStatusManager = GetComponent<StatusManager_MT>();

        HitPointCalculation(myStatusManager, targetStatusManager);
    }

    /// <summary>
    /// ダメージ計算
    /// </summary>
    public void HitPointCalculation(StatusManager_MT myStatus, StatusManager_MT targetStatus)
    {
        int myAttackPower = myStatus.Strength;        // 自分の攻撃力をgetしてくる
        int targetDefensePower = targetStatus.Defense;        // 相手の防御力をgetしてくる
        int targetHitPoint = targetStatus.HP;        // 相手のHPをgetしてくる

        int damage = myAttackPower - targetDefensePower;

        if (myAttackPower < targetDefensePower)
        {
            // 防御力のほうが高い場合はダメージを1とする
            int smallestDamage = 1;
            damage = smallestDamage;
        }

        int hitPointAfterDamage = targetHitPoint - damage;

        //_createDamageImage.InstantiateDamageImage(gameObject, targetStatus.gameObject, damage);
        //_writeHitPoint.UpdateHitPoint(targetStatus, hitPointAfterDamage);      // targetStatusのHPを更新
    }

    /// <summary>
    /// 一定時間後弾を削除する
    /// </summary>
    private void UpdateSphereTime()
    {
        if (!_isShot) return;     // 攻撃中以外は処理を行わない
        // 時間加算
        _elapsedTime += Time.deltaTime;

        // 規定時間に達していた場合
        if (_elapsedTime > _sphereDeleteTime)
        {
            _bulletHitDecision.DisableBullet();
            _bullet.transform.SetParent(gameObject.transform);
            _elapsedTime = 0f;
            _isShot = false;
        }
    }

    /// <summary>
    /// 一定時間後攻撃範囲を削除する
    /// </summary>
    private void UpdateHitTime()
    {
        if (!_isAttack) return;     // 攻撃中以外は処理を行わない

        // 時間加算
        _elapsedTime += Time.deltaTime;

        // 規定時間に達していた場合
        if (_elapsedTime > _hitAttackDeleteTime)
        {
            // 動きを再開する
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