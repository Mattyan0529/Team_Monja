using UnityEngine;

public class EvillMageSkillAttack : MonoBehaviour,IDamagable
{
    [SerializeField]
    private GameObject _residentScript;



    private float _bulletSpeed = 50f;

    private float _addSpownPos = 1f;     // 弾を生成するときにyに足す値

    private GameObject _bullet = default;
    private WriteHitPoint_KH _writeHitPoint = default;
    private BulletHitDecision_KH _bulletHitDecision = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private AudioSource _audioSource = default;
    private CreateDamageImage_KH _createDamageImage = default;
    private PlayerSkill_KH _playerSkill = default;

    private float _deleteTime = 2f;
    private float _elapsedTime = 0f;

    private bool _isShot = false;

    void Start()
    {
        _writeHitPoint = _residentScript.GetComponent<WriteHitPoint_KH>();
        _createDamageImage = _residentScript.GetComponent<CreateDamageImage_KH>();
        _soundEffectManagement = _residentScript.GetComponent<SoundEffectManagement_KH>();
        _playerSkill = GetComponent<PlayerSkill_KH>();

        // 子オブジェクトからBulletを取得
        _bullet = transform.Find("Bullet").gameObject;
        _bulletHitDecision = _bullet.GetComponent<BulletHitDecision_KH>();
    }

    void Update()
    {
        UpdateTime();
    }

    /// <summary>
    /// 遠距離の弾を生成する
    /// </summary>
    public void SpecialAttack()
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

    /// <summary>
    /// 当たった相手を取得
    /// </summary>
    public void HitDecision(GameObject hitObj)
    {
        _isShot = false;
        _bullet.transform.SetParent(gameObject.transform);

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

        if (myAttackPower < targetDefensePower) return;        // 防御力のほうが高かったら0ダメージ

        int damage = targetHitPoint - (myAttackPower - targetDefensePower);
        _createDamageImage.InstantiateDamageImage(gameObject, targetStatus.gameObject, myAttackPower - targetDefensePower);
        _writeHitPoint.UpdateHitPoint(targetStatus, damage);      // targetStatusのHPを更新
    }

    /// <summary>
    /// 一定時間後弾を削除する
    /// </summary>
    private void UpdateTime()
    {
        if (!_isShot) return;     // 攻撃中以外は処理を行わない
        // 時間加算
        _elapsedTime += Time.deltaTime;

        // 規定時間に達していた場合
        if (_elapsedTime > _deleteTime)
        {
            _bulletHitDecision.DisableBullet();
            _bullet.transform.SetParent(gameObject.transform);
            _elapsedTime = 0f;
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

