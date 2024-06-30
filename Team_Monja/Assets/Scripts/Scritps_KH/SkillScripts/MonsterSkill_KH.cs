using UnityEngine;

public class MonsterSkill_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _residentScript;
    [SerializeField]
    private GameObject _followArea;
    [SerializeField]
    private Sprite _skillIcon = default;
    [SerializeField]
    private GameObject _skillSpriteObj = default;
    [SerializeField]
    private Sprite _normalIcon = default;
    [SerializeField]
    private GameObject _normalAttackSpriteObj = default;

    private float _updateTime = 0f;    // 何秒おきにスキルを呼び出すか
    private float _elapsedTime = default;

    private float _maxTimeSpacing = 4f;
    private float _minTimeSpacing = 2f;

    private float _followAreaSize = 15f;

    private PlayerSkill_KH _playerSkill = default;
    private PlayerManager_KH _playerManager = default;
    private MonsterRandomWalk_KH _monsterRandomWalk = default;
    private SkillSpriteChange_KH _skillSpriteChange = default;
    private SkillSpriteChange_KH _normalAttackSpriteChange = default;

    private HighSpeedAssault_KH _highSpeedAssault = default;
    private WeaponAttack_KH _weaponAttack = default;
    private LongDistanceAttack_KH _longDistanceAttack = default;
    private FlySkill_KH _flySkill = default;
    private Petrification_KH _petrification = default;
    private BossSkill_KH _bossSkill = default;
    //松本
    private CharacterAnim_MT _characterAnim;


    private NormalAttack_KH _normalAttack = default;
    private PlayerGuard_KH _playerGuard = default;

    private int _skillNum = 6;

    /// <summary>
    /// スキルの大まかなタイプ
    /// このタイプごとにスクリプトにまとめてるよ
    /// </summary>
    public enum SkillType
    {
        HighSpeedAssault,
        WeaponAttack,
        LongDistanceAttack,
        Fly,
        Petrification,
        Boss
    }

    void Awake()
    {
        _playerSkill = GetComponent<PlayerSkill_KH>();
        _monsterRandomWalk = GetComponent<MonsterRandomWalk_KH>();
        _playerManager = _residentScript.GetComponent<PlayerManager_KH>();

        if (GetComponent<NormalAttack_KH>())
        {
            _normalAttack = GetComponent<NormalAttack_KH>();
        }
        else if (GetComponent<PlayerGuard_KH>())
        {
            _playerGuard = GetComponent<PlayerGuard_KH>();
        }

        SkillJudge();
    }

    void Start()
    {
        //松本
        _characterAnim = GetComponent<CharacterAnim_MT>();

        _skillSpriteChange = _skillSpriteObj.GetComponent<SkillSpriteChange_KH>();
        _normalAttackSpriteChange = _normalAttackSpriteObj.GetComponent<SkillSpriteChange_KH>();
        GameobjectTagJudge();
        _updateTime = Random.Range(_minTimeSpacing, _maxTimeSpacing);
    }

    void Update()
    {
        UpdateTime();

        // 乗り移りが発生したらタグを変更（Mが押されたが乗り移りが発生しなかったときも処理が走ってしまう）
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameobjectTagJudge();
        }
    }

    /// <summary>
    /// モンスターからプレイヤーに変わったらスクリプト切り替え
    /// </summary>
    public void GameobjectTagJudge()
    {
        if (gameObject.CompareTag("Player"))
        {
            _playerManager.Player = gameObject;     // プレイヤー更新
            _followArea.transform.localScale = gameObject.transform.localScale * _followAreaSize;
            _followArea.transform.SetParent(gameObject.transform);
            _playerSkill.enabled = true;
            _skillSpriteChange.ChangeSprite(_skillIcon);
            _normalAttackSpriteChange.ChangeSprite(_normalIcon);

            if (GetComponent<NormalAttack_KH>())
            {
                _normalAttack.enabled = true;
            }
            else if (GetComponent<PlayerGuard_KH>())
            {
                _playerGuard.enabled = true;
            }

            _monsterRandomWalk.enabled = false;
            this.enabled = false;
        }
    }

    /// <summary>
    /// このキャラクターが持っているのはどのスキルか判定
    /// </summary>
    private void SkillJudge()
    {
        if (GetComponent<BossSkill_KH>())      // ボス
        {
            _skillNum = (int)SkillType.Boss;
            _bossSkill = GetComponent<BossSkill_KH>();
        }
        else if (GetComponent<HighSpeedAssault_KH>())       // スキル：高速突撃
        {
            _skillNum = (int)SkillType.HighSpeedAssault;
            _highSpeedAssault = GetComponent<HighSpeedAssault_KH>();
        }
        else if (GetComponent<WeaponAttack_KH>())      // スキル：武器を使った攻撃
        {
            _skillNum = (int)SkillType.WeaponAttack;
            _weaponAttack = GetComponent<WeaponAttack_KH>();
        }
        else if (GetComponent<LongDistanceAttack_KH>())       // スキル：遠距離攻撃
        {
            _skillNum = (int)SkillType.LongDistanceAttack;
            _longDistanceAttack = GetComponent<LongDistanceAttack_KH>();
        }
        else if (GetComponent<FlySkill_KH>())       // スキル：飛ぶ
        {
            _skillNum = (int)SkillType.Fly;
            _flySkill = GetComponent<FlySkill_KH>();
        }
        else if (GetComponent<Petrification_KH>())       // スキル：石化
        {
            _skillNum = (int)SkillType.Petrification;
            _petrification = GetComponent<Petrification_KH>();
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
        // 時間加算
        _elapsedTime += Time.deltaTime;

        // 規定時間に達していた場合
        if (_elapsedTime > _updateTime)
        {
            RandomCallSkill();      // スキル発動
            _characterAnim.NowAnim = "Skill";   
            _elapsedTime = 0f;
        }
    }

    /// <summary>
    /// モンスターが数秒おきにスキルを使う
    /// </summary>
    private void RandomCallSkill()
    {
        switch (SkillTypeNum)
        {
            case (int)SkillType.HighSpeedAssault:      // 高速突撃の場合

                _highSpeedAssault.SpeedUp();
                return;

            case (int)SkillType.WeaponAttack:          // 武器を使った攻撃などの場合
                                                       // ランダム移動中（プレイヤーが攻撃範囲外）は処理しないが、その判断はAttack内でしてる
                _weaponAttack.Attack();
                return;

            case (int)SkillType.LongDistanceAttack:        // 遠距離攻撃の場合

                _longDistanceAttack.GenerateBullet();
                return;

            case (int)SkillType.Fly:                   // 飛ぶスキルの場合

                _flySkill.MonsterStartFly();
                return;

            case (int)SkillType.Petrification:         // 石化の場合

                if (_monsterRandomWalk.enabled) return;     // ランダム移動中（プレイヤーが攻撃範囲外）は処理しない
                _petrification.CreatePetrificationArea();
                return;

            case (int)SkillType.Boss:       // ボスのスキルの場合

                _bossSkill.RandomSkillCall();
                return;
        }

    }

    private void OnDisable()
    {
        if(gameObject.CompareTag("Player"))return;

        switch (SkillTypeNum)
        {
            case (int)SkillType.HighSpeedAssault:      // 高速突撃の場合

                _highSpeedAssault.enabled = false;
                return;

            case (int)SkillType.WeaponAttack:          // 武器を使った攻撃などの場合
                                                       
                _weaponAttack.enabled = false;
                return;

            case (int)SkillType.LongDistanceAttack:        // 遠距離攻撃の場合

                _longDistanceAttack.enabled = false;
                return;

            case (int)SkillType.Fly:                   // 飛ぶスキルの場合

                _flySkill.enabled = false;
                return;

            case (int)SkillType.Petrification:         // 石化の場合

                _petrification.enabled = false;
                return;

            case (int)SkillType.Boss:       // ボスのスキルの場合

                _bossSkill.enabled = false;
                return;
        }
    }
}
