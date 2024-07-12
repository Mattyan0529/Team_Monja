using UnityEngine;

public class NormalAttack_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _residentScript;

    [SerializeField]
    private GameObject _coolTimeUIObj = default;

    [SerializeField]
    private EffectManager _effectManager; // EffectManagerの参照を追加

    private int _attackCount = 0; //攻撃回数をカウント

    private float _deleteTime = 0.2f;
    private float _elapsedTime = 0f;

    private float _coolTime = 0.5f; // 通常攻撃を発動してから次に発動できるようになるまでの時間
    private float _coolTimeElapsedTime = 0f;

    [SerializeField]
    private GameObject _attackArea;
    private bool _isAttack = false;
    private bool _canUseNormalAttack = true;

    private WriteHitPoint_KH _writeHitPoint = default;
    private CoolTimeUI _coolTimeUI = default;
    private CreateDamageImage_KH _createDamageImage = default;
    //松本
    private CharacterAnim_MT _characterAnim = default;
    private Animator _animator;

    public bool IsAttack
    {
        get { return _isAttack; }
    }

    void Start()
    {
        _writeHitPoint = _residentScript.GetComponent<WriteHitPoint_KH>();
        _characterAnim = GetComponent<CharacterAnim_MT>();
        _coolTimeUI = _coolTimeUIObj.GetComponent<CoolTimeUI>();
        _createDamageImage = _residentScript.GetComponent<CreateDamageImage_KH>();
        _animator = GetComponent<Animator>(); // Animatorコンポーネントを取得
        if (_animator == null)
        {
            Debug.LogError("Animator component is not attached to the GameObject.");
        }
    }

    void Update()
    {
        UpdateCoolTime();
        AttackInputManager();
    }

    /// <summary>
    /// クールタイム後通常攻撃を使えるようにする
    /// </summary>
    private void UpdateCoolTime()
    {
        if (_canUseNormalAttack) return; // 攻撃中以外は処理を行わない

        // 時間加算
        _coolTimeElapsedTime += Time.deltaTime;

        // 規定時間に達していた場合
        if (_coolTimeElapsedTime > _coolTime)
        {
            _coolTimeElapsedTime = 0f;
            _canUseNormalAttack = true;
        }
    }

    private void AttackInputManager()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetButtonDown("attack"))
        {
            if (!_canUseNormalAttack) return;
            if (_attackCount >= 2) return;
            //初期化
            ResetAttack();

            if (!_isAttack)
            {
                //攻撃はアニメーションから呼び出す
                _characterAnim.NowAnim = "Attack";
            }
            else //連続攻撃する場合
            {
                // アニメーションを停止して最初に戻す
                _animator.Play("Attack", -1, 0f);
            }

            //攻撃回数のカウントを増やす
            _attackCount++;
        }
    }

    /// <summary>
    /// 攻撃範囲のCubeをアニメーションから生成
    /// </summary>
    public void NormalAttack()
    {
        _attackArea.SetActive(true);
        _isAttack = true;

        // 通常攻撃エフェクトを表示
        _effectManager.ShowNormalAttackEffect(transform);
    }

    //松本
    /// <summary>
    /// 攻撃状態をリセット
    /// </summary>
    private void ResetAttack()
    {
        _attackArea.SetActive(false);
    }

    /// <summary>
    /// 連続攻撃の終了、アニメーションから呼び出す
    /// </summary>
    public void EndAttack()
    {
        _attackArea.SetActive(false);
        _isAttack = false;
        _canUseNormalAttack = false;
        _attackCount = 0;
        //クールタイム開始
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
        int myAttackPower = myStatus.Strength; // 自分の攻撃力をgetしてくる
        int targetDefensePower = targetStatus.Defense; // 相手の防御力をgetしてくる
        int targetHitPoint = targetStatus.HP; // 相手のHPをgetしてくる

        if (myAttackPower < targetDefensePower) return; // 防御力のほうが高かったら0ダメージ

        int damage = targetHitPoint - (myAttackPower - targetDefensePower);
        _createDamageImage.InstantiateDamageImage(gameObject, targetStatus.gameObject, myAttackPower - targetDefensePower);
        _writeHitPoint.UpdateHitPoint(targetStatus, damage); // targetStatusのHPを更新
    }
}
