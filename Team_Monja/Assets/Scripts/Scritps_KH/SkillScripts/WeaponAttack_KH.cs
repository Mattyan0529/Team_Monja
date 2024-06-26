using UnityEngine;

public class WeaponAttack_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _residentScript;

    [SerializeField]
    private EffectManager _effectManager; // EffectManagerの参照を追加


    private float _deleteTime = 0.5f;
    private float _elapsedTime = 0f;

    private GameObject _attackArea;

    private WriteHitPoint_KH _writeHitPoint = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private AudioSource _audioSource = default;
    private MonsterRandomWalk_KH _monsterRandomWalk = default;
    private PlayerRangeInJudge_KH _playerRangeInJudge = default;
    private PlayerMove_MT _playerMove = default;
    private CreateDamageImage_KH _createDamageImage = default;

    private bool _isAttack = false;

    //松本
    private CharacterAnim_MT _characterAnim = default;

    private void Awake()
    {
        _monsterRandomWalk = GetComponent<MonsterRandomWalk_KH>();
        _playerRangeInJudge = GetComponent<PlayerRangeInJudge_KH>();
        _playerMove = GetComponent<PlayerMove_MT>();
    }

    void Start()
    {
        _writeHitPoint = _residentScript.GetComponent<WriteHitPoint_KH>();
        _soundEffectManagement = _residentScript.GetComponent<SoundEffectManagement_KH>();
        _createDamageImage = _residentScript.GetComponent<CreateDamageImage_KH>();
        _characterAnim = GetComponent<CharacterAnim_MT>();
        _audioSource = GetComponent<AudioSource>();

        // 子オブジェクトの中からAttackAreaを取得
        _attackArea = transform.Find("AttackArea").gameObject;
        _attackArea.SetActive(false);
    }

    void Update()
    {
        UpdateTime();
    }

    /// <summary>
    /// 攻撃をする
    /// </summary>
    public void Attack()
    {
        if (_monsterRandomWalk.enabled) return;     // ランダム移動中（プレイヤーが攻撃範囲外）は処理しない

        //松本
        _characterAnim.NowAnim = "Skill";

        //スキルエフェクト
        if (_effectManager != null)
        {
            _effectManager.ShowSpecialAttackEffect(transform);
        }
        else
        {
            Debug.LogError("EffectManager component is not found.");
        }

        // 動きを止める
        if (gameObject.CompareTag("Enemy") || gameObject.CompareTag("Boss"))
        {
            _playerRangeInJudge.enabled = false;
        }
        if (gameObject.CompareTag("Player"))
        {
            _playerMove.enabled = false;
        }

        _isAttack = true;
        _attackArea.SetActive(true);

        _soundEffectManagement.PlayStrongPunchSound(_audioSource);
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
        _createDamageImage.InstantiateDamageImage(gameObject, targetStatus.gameObject, myAttackPower - targetDefensePower);
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
            // 動きを再開する
            if (gameObject.CompareTag("Enemy") || gameObject.CompareTag("Boss"))
            {
                _playerRangeInJudge.enabled = true;
            }
            if (gameObject.CompareTag("Player"))
            {
                _playerMove.enabled = true;
            }

            _attackArea.SetActive(false);
            _elapsedTime = 0f;
            _isAttack = false;
        }
    }
}
