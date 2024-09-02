using UnityEngine;

public class MonsterSkill_KH : MonoBehaviour
{
 
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
    private float _elapsedTime = 3f;

    private float _maxTimeSpacing = 2f;
    private float _minTimeSpacing = 1f;

    private float _nearPlayerAreaSize = 120f;

    private PlayerSkill_KH _playerSkill = default;
    private EnemyMove_KH _enemyMove = default;
    private AttackAreaJudge_KH _attackAreaJudge = default;
    private SkillSpriteChange_KH _skillSpriteChange = default;
    private SkillSpriteChange_KH _normalAttackSpriteChange = default;
    private ChangeEnemyMoveType_KH _changeEnemyMoveType = default;
    private NearPlayerWayPointManager_KH _nearPlayerWayPointManager = default;
    private IDamagable_KH _skillInterface = default;
    //松本
    private CharacterAnim_MT _characterAnim;


    private NormalAttack_KH _normalAttack = default;
    private PlayerGuard_KH _playerGuard = default;

    private int _skillNum = 6;


    void Awake()
    {
        _playerSkill = GetComponent<PlayerSkill_KH>();
        _enemyMove = GetComponent<EnemyMove_KH>();
        _attackAreaJudge = GetComponent<AttackAreaJudge_KH>();
        _changeEnemyMoveType = GetComponent<ChangeEnemyMoveType_KH>();

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
        _nearPlayerWayPointManager = _nearPlayerArea.GetComponent<NearPlayerWayPointManager_KH>();
        GameobjectTagJudge();
        _updateTime = Random.Range(_minTimeSpacing, _maxTimeSpacing);
        _skillInterface = GetComponent<IDamagable_KH>();
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
            _nearPlayerWayPointManager.ClearNearPlayerWayPoint();
            _nearPlayerArea.transform.localScale = gameObject.transform.localScale * _nearPlayerAreaSize;
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
        if (_changeEnemyMoveType.NowState != ChangeEnemyMoveType_KH.EnemyMoveState.InAttack) return;

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
        _skillInterface.SpecialAttack();
    }
}
