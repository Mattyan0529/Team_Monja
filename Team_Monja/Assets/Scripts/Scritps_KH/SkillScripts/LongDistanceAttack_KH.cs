using UnityEngine;

public class LongDistanceAttack_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _residentScript;

    private float _bulletSpeed = 20f;
    private float _radius = 0.5f;

    private float _addSpownPos;     // 弾を生成するときにyに足す値

    private GameObject _bullet;
    private WriteHitPoint_KH _writeHitPoint;

    private float _deleteTime = 3f;
    private float _elapsedTime = 0f;

    private bool _isShot = false;

    // Start is called before the first frame update
    void Start()
    {
        _writeHitPoint = _residentScript.GetComponent<WriteHitPoint_KH>();
    }

    // Update is called once per frame
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

        _bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        // SphereのTransformを設定
        _bullet.transform.position = new Vector3(transform.position.x, transform.position.y + _addSpownPos, transform.position.z);
        _bullet.transform.localScale = new Vector3(_radius, _radius, _radius);

        _bullet.GetComponent<SphereCollider>().isTrigger = true;

        // Sphereのマテリアルを設定（透明に）
        Renderer renderer = _bullet.GetComponent<Renderer>();
        //renderer.enabled = false;

        // 速度を付ける
        _bullet.AddComponent<Rigidbody>();
        Rigidbody rigidbody = _bullet.GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.velocity = transform.forward * _bulletSpeed;

        _bullet.AddComponent<BulletHitDecision_KH>();

        _isShot = true;

        _bullet.GetComponent<BulletHitDecision_KH>().GetParent(gameObject);
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
            Destroy(_bullet);
            _elapsedTime = 0f;
            _isShot = false;
        }
    }
}
