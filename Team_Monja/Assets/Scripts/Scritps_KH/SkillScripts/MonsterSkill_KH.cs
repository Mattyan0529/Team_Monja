using UnityEngine;

public class MonsterSkill_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _residentScript;
    [SerializeField]
    private GameObject _nearPlayerArea;
    [SerializeField]
    private Sprite _skillIcon = default;
    [SerializeField]
    private GameObject _skillSpriteObj = default;
    [SerializeField]
    private Sprite _normalAttackIcon = default;
    [SerializeField]
    private GameObject _normalAttackSpriteObj = default;

    private float _updateTime = 0f;    // 何秒おきにスキルを呼び出すか
    private float _elapsedTime = default;

    private float _maxTimeSpacing = 3f;
    private float _minTimeSpacing = 2f;

    private float _nearPlayerAreaSize = 70f;

    private PlayerSkill_KH _playerSkill = default;
    private PlayerManager_KH _playerManager = default;
    private EnemyMove _enemyMove = default;
    private AttackAreaJudge _attackAreaJudge = default;
    private SkillSpriteChange_KH _skillSpriteChange = default;
    private SkillSpriteChange_KH _normalAttackSpriteChange = default;
    private ChangeEnemyMoveType _changeEnemyMoveType = default;
    private NearPlayerWayPointManager _nearPlayerWayPointManager = default;
    private IDamagable _skillInterface = default;
    //松本
    private CharacterAnim_MT _characterAnim;


    private NormalAttack_KH _normalAttack = default;
    private PlayerGuard_KH _playerGuard = default;

    private int _skillNum = 6;


    void Awake()
    {
        _playerSkill = GetComponent<PlayerSkill_KH>();
        _enemyMove = GetComponent<EnemyMove>();
        _attackAreaJudge = GetComponent<AttackAreaJudge>();
        _playerManager = _residentScript.GetComponent<PlayerManager_KH>();
        _changeEnemyMoveType = GetComponent<ChangeEnemyMoveType>();

        if (GetComponent<NormalAttack_KH>())
        {
            _normalAttack = GetComponent<NormalAttack_KH>();
        }
        else if (GetComponent<PlayerGuard_KH>())
        {
            _playerGuard = GetComponent<PlayerGuard_KH>();
        }
    }

    void Start()
    {
        //松本
        _characterAnim = GetComponent<CharacterAnim_MT>();

        _skillSpriteChange = _skillSpriteObj.GetComponent<SkillSpriteChange_KH>();
        _normalAttackSpriteChange = _normalAttackSpriteObj.GetComponent<SkillSpriteChange_KH>();
        _nearPlayerWayPointManager = _nearPlayerArea.GetComponent<NearPlayerWayPointManager>();
        GameobjectTagJudge();
        _updateTime = Random.Range(_minTimeSpacing, _maxTimeSpacing);
        _skillInterface = GetComponent<IDamagable>();
    }

    void Update()
    {
        GameobjectTagJudge();
        UpdateTime();
    }

    /// <summary>
    /// モンスターからプレイヤーに変わったらスクリプト切り替え
    /// </summary>
    public void GameobjectTagJudge()
    {
        if (gameObject.CompareTag("Player"))
        {
            _playerManager.Player = gameObject;     // プレイヤー更新
            _nearPlayerWayPointManager.ClearNearPlayerWayPoint();
            _nearPlayerArea.transform.localScale = gameObject.transform.localScale * _nearPlayerAreaSize;
            _nearPlayerArea.transform.SetParent(gameObject.transform);
            _playerSkill.enabled = true;
            _skillSpriteChange.ChangeSprite(_skillIcon);
            _normalAttackSpriteChange.ChangeSprite(_normalAttackIcon);

            if (GetComponent<NormalAttack_KH>())
            {
                _normalAttack.enabled = true;
            }
            else if (GetComponent<PlayerGuard_KH>())
            {
                _playerGuard.enabled = true;
            }

            _enemyMove.enabled = false;
            _attackAreaJudge.enabled = false;
            this.enabled = false;
        }
    }

    /// <summary>
    /// _skillNumをゲット
    /// </summary>
    public int SkillTypeNum
    {
        get { return _skillNum; }
    }

    /// <summary>
    /// 定期的にスキルを発動
    /// </summary>
    private void UpdateTime()
    {
        if (_changeEnemyMoveType.NowState != ChangeEnemyMoveType.EnemyMoveState.InAttack) return;

        // 時間加算
        _elapsedTime += Time.deltaTime;

        // 規定時間に達していた場合
        if (_elapsedTime > _updateTime)
        {
            RandomCallSkill();      // スキル発動  
            _elapsedTime = 0f;
        }
    }

    /// <summary>
    /// モンスターが数秒おきにスキルを使う
    /// </summary>
    private void RandomCallSkill()
    {
        //松本
        _characterAnim.NowAnim = "Skill";

        _skillInterface.SpecialAttack();
    }
}
