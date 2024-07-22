using UnityEngine;
using static MonsterSkill_KH;

public class PlayerSkill_KH : MonoBehaviour
{
    [SerializeField]
    GameObject _coolTimeUIObj = default;

    private MonsterSkill_KH _myMonsterSkill = default;
    private EnemyMove _enemyMove = default;
    private AttackAreaJudge _attackAreaJudge = default;
    private CoolTimeUI _coolTimeUI = default;

    private IDamagable _skillInterface = default;

    private NormalAttack_KH _normalAttack = default;
    private PlayerGuard_KH _playerGuard = default;

    //松本
    private CharacterAnim_MT _characterAnim = default;

    private float _coolTime = 2f;    // スキルを発動してから次に発動できるようになるまでの時間
    private float _elapsedTime = 0f;

    private int _skillNum;
    private bool _canUseSkill = true;
    private bool _isUseSkill = false;

    private float lefttrigger;

    public bool IsUseSkill
    {
        get { return _isUseSkill; }
        set { _isUseSkill = value; }
    }

    private void Awake()
    {
        _myMonsterSkill = GetComponent<MonsterSkill_KH>();
        _enemyMove = GetComponent<EnemyMove>();
        _attackAreaJudge = GetComponent<AttackAreaJudge>();
        _characterAnim = GetComponent<CharacterAnim_MT>();
    }

    void Start()
    {
        _skillNum = _myMonsterSkill.SkillTypeNum;

        if (GetComponent<NormalAttack_KH>())
        {
            _normalAttack = GetComponent<NormalAttack_KH>();
        }
        else if (GetComponent<PlayerGuard_KH>())
        {
            _playerGuard = GetComponent<PlayerGuard_KH>();
        }

        _coolTimeUI = _coolTimeUIObj.GetComponent<CoolTimeUI>();
        _skillInterface = GetComponent<IDamagable>();

        float lefttrigger = Input.GetAxis("skill");


        GameobjectTagJudge();
    }

    void Update()
    {
        CallSkill();

        // 乗り移りが発生したらタグを変更（Mが押されたが乗り移りが発生しなかったときも処理が走ってしまう）
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameobjectTagJudge();
        }

        if (!_canUseSkill)
        {
            UpdateTime();
        }
    }

    /// <summary>
    /// 始まった時点でEnemyだったら自動でEnemy状態にする
    /// </summary>
    public void GameobjectTagJudge()
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
        /*if (((Input.GetMouseButtonDown(0) || lefttrigger > 0.3f) && _skillNum == (int)MonsterSkill_KH.SkillType.Fly && _flySkill.IsFlying))
        {
            _isUseSkill = false;
            _flySkill.StopFly();
        }*/
        if (((Input.GetMouseButtonDown(0) || lefttrigger > 0.3f) && _canUseSkill))
        {
            if (_normalAttack != null && _normalAttack.IsAttack) return;
            if (_playerGuard != null && _playerGuard.IsGuard) return;

            _isUseSkill = true;
            _skillInterface.SpecialAttack();
            _canUseSkill = false;
            _coolTimeUI.StartCoolTime();
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
