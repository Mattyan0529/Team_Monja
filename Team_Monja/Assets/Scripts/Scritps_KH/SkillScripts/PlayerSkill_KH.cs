using UnityEngine;
using static MonsterSkill_KH;

public class PlayerSkill_KH : MonoBehaviour
{
    [SerializeField]
    GameObject _coolTimeUIObj = default;

    private PlayerMove_MT _playerMove = default;
    private MonsterSkill_KH _myMonsterSkill = default;
    private EnemyMove_KH _enemyMove = default;
    private AttackAreaJudge_KH _attackAreaJudge = default;
    private CoolTimeUI_KH _coolTimeUI = default;

    private IDamagable_KH _skillInterface = default;

    private NormalAttack_KH _normalAttack = default;
    private PlayerGuard_KH _playerGuard = default;

    private float _coolTime = 2f;    // スキルを発動してから次に発動できるようになるまでの時間
    private float _elapsedTime = 0f;

    private bool _canUseSkill = true;
    private bool _isUseSkill = false;

    public bool IsUseSkill
    {
        get { return _isUseSkill; }
        set { _isUseSkill = value; }
    }

    private void Awake()
    {
        _myMonsterSkill = GetComponent<MonsterSkill_KH>();
        _enemyMove = GetComponent<EnemyMove_KH>();
        _attackAreaJudge = GetComponent<AttackAreaJudge_KH>();
    }

    void Start()
    {
        if (GameObject.FindWithTag("PlayerManager").GetComponent<PlayerMove_MT>())
        {
            _playerMove = GameObject.FindWithTag("PlayerManager").GetComponent<PlayerMove_MT>();
        }

        if (GetComponent<NormalAttack_KH>())
        {
            _normalAttack = GetComponent<NormalAttack_KH>();
        }
        else if (GetComponent<PlayerGuard_KH>())
        {
            _playerGuard = GetComponent<PlayerGuard_KH>();
        }

        _coolTimeUI = _coolTimeUIObj.GetComponent<CoolTimeUI_KH>();
        _skillInterface = GetComponent<IDamagable_KH>();

        GameObjectTagJudge();
    }

    void Update()
    {
        CallSkill();

        if (!_canUseSkill)
        {
            UpdateTime();
        }
    }

    /// <summary>
    /// 始まった時点でEnemyだったら自動でEnemy状態にする
    /// </summary>
    public void GameObjectTagJudge()
    {
        if (gameObject.CompareTag("Enemy") || gameObject.CompareTag("Boss"))
        {
            _myMonsterSkill.enabled = true;
            _enemyMove.enabled = true;
            _attackAreaJudge.enabled = true;

            if (GetComponent<NormalAttack_KH>())
            {
                _normalAttack.enabled = false;
            }
            else if (GetComponent<PlayerGuard_KH>())
            {
                _playerGuard.enabled = false;
            }

            this.enabled = false;
        }
    }

    /// <summary>
    /// ボタンを押したらスキル発動
    /// </summary>
    private void CallSkill()
    {
        // スキルが使える状態かどうか、既にスキルを使用していないか確認
        if (_canUseSkill && !_isUseSkill && (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Cancel")))
        {
            // スキルが使えなくなる状態に設定
            _canUseSkill = false;
            _isUseSkill = true;  // スキル使用中を表すフラグをtrueに

            // クールタイムUIを開始
            _coolTimeUI.StartCoolTime();

            // プレイヤーの移動を一時的に無効にする
            if (_playerMove != null)
            {
                _playerMove.enabled = false;
            }

            // 通常攻撃やガードが実行中ならスキルを発動しない
            if (_normalAttack != null && _normalAttack.IsAttack) return;
            if (_playerGuard != null && _playerGuard.IsGuard) return;

            // スキル発動
            _skillInterface.SpecialAttack();

            // スキル発動後、再び移動を有効にする
            if (_playerMove != null)
            {
                _playerMove.enabled = true;
            }

            // スキル終了処理（例: スキルアニメーションの終了を待つ場合などはここに処理を追加する）
            _isUseSkill = false;  // スキルが終了したら再び使用できるようにフラグをfalseに
        }
    }


    /// <summary>
    /// スキルを再度使えるようにする
    /// </summary>
    private void UpdateTime()
    {
        // 時間加算
        _elapsedTime += Time.deltaTime;

        // 規定時間に達していた場合
        if (_elapsedTime > _coolTime)
        {
            _elapsedTime = 0f;
            _canUseSkill = true;
        }
    }
}
