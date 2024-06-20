using UnityEngine;

public class LongDistanceAttack_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _residentScript;

    private float _bulletSpeed = 50f;

    private float _addSpownPos;     // 弾を生成するときにyに足す値

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

        // 子オブジェクトからBulletを取得
        _bullet = transform.Find("Bullet").gameObject;
        _bulletHitDecision = _bullet.GetComponent<BulletHitDecision_KH>();
        _bulletHitDecision.DisableBullet();
    }

    void Update()
    {
        UpdateTime();
    }

    /// <summary>
    /// 遠距離の弾を生成する
    /// </summary>
    public void GenerateBullet()
    {
        if (_isShot) return;      // 重複で攻撃はしない

        // 速度を付ける
        _bullet.transform.position = new Vector3(transform.position.x, transform.position.y + _addSpownPos, transform.position.z);
        Rigidbody rigidbody = _bullet.GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * _bulletSpeed;
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

        statusManager.UpdateHitPoint(targetStatus, damage);      // targetStatusのHPを更新
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
            _elapsedTime = 0f;
            _isShot = false;
        }
    }
}
