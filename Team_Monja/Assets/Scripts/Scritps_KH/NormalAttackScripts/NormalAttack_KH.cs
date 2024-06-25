using UnityEngine;

public class NormalAttack_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _residentScript;

    [SerializeField]
    GameObject _coolTimeUIObj = default;

    private float _radius = 4f;      // 攻撃範囲として生み出すSphereの半径
    private Vector3 _position = Vector3.zero;       // Sphereの位置

    private float _deleteTime = 0.2f;
    private float _elapsedTime = 0f;

    private float _coolTime = 0.5f;    // 通常攻撃を発動してから次に発動できるようになるまでの時間
    private float _coolTimeElapsedTime = 0f;

    private GameObject _attackArea;
    private bool _isAttack = false;

    private WriteHitPoint_KH _writeHitPoint = default;
    private CoolTimeUI _coolTimeUI = default;
    //松本
    private CharacterAnim_MT _characterAnim = default;

    void Start()
    {
        _writeHitPoint = _residentScript.GetComponent<WriteHitPoint_KH>();
        _characterAnim = GetComponent<CharacterAnim_MT>();
        _coolTimeUI = _coolTimeUIObj.GetComponent<CoolTimeUI>();
    }

    void Update()
    {
        UpdateTime();
        AttackInputManager();
    }

    private void AttackInputManager()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    /// <summary>
    /// 攻撃範囲のSphereを生成
    /// </summary>
    private void Attack()
    {
        //松本
        _characterAnim.NowAnim = "Attack";


        _isAttack = true;

        if (_attackArea != null)        // Sphereがすでにあるときは作成しない
        {
            _attackArea.SetActive(true);
            return;
        }

        _attackArea = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        // Sphereをこのスクリプトがアタッチされているオブジェクトの子に設定
        _attackArea.transform.parent = transform;

        // SphereのTransformを設定
        _attackArea.transform.localPosition = _position;
        _attackArea.transform.localScale = new Vector3(_radius, _radius, _radius);

        // Sphereのマテリアルを設定（透明に）
        Renderer renderer = _attackArea.GetComponent<Renderer>();
        renderer.enabled = false;

        _attackArea.GetComponent<SphereCollider>().isTrigger = true;

        _attackArea.AddComponent<NormalAttackHitDecision_KH>();

        _coolTimeUI.StartCoolTime();
    }

    /// <summary>
    /// 当たった相手を取得
    /// </summary>
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

        if (myAttackPower < targetDefensePower) return;        // 防御力のほうが高かったら0ダメージ

        int damage = targetHitPoint - (myAttackPower - targetDefensePower);
        _writeHitPoint.UpdateHitPoint(targetStatus, damage);      // targetStatusのHPを更新
    }

    /// <summary>
    /// 一定時間後攻撃範囲を削除する
    /// </summary>
    private void UpdateTime()
    {
        if (!_isAttack) return;     // 攻撃中以外は処理を行わない

        // 時間加算
        _elapsedTime += Time.deltaTime;

        // 規定時間に達していた場合
        if (_elapsedTime > _deleteTime)
        {
            _attackArea.SetActive(false);
            _elapsedTime = 0f;
            _isAttack = false;
        }
    }
}
