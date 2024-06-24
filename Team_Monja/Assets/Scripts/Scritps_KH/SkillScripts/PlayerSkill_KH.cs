using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerSkill_KH : MonoBehaviour
{
    [SerializeField]
    GameObject _coolTimeUIObj = default;

    private MonsterSkill_KH _myMonsterSkill = default;
    private PlayerMove_MT _playerMove = default;
    private CoolTimeUI _coolTimeUI = default;

    private HighSpeedAssault_KH _highSpeedAssault = default;
    private WeaponAttack_KH _weaponAttack = default;
    private LongDistanceAttack_KH _longDistanceAttack = default;
    private FlySkill_KH _flySkill = default;
    private Petrification_KH _petrification = default;

    private NormalAttack_KH _normalAttack = default;
    private PlayerGuard_KH _playerGuard = default;

    //松本
    private CharacterAnim_MT _characterAnim = default;

    private float _coolTime = 2f;    // スキルを発動してから次に発動できるようになるまでの時間
    private float _elapsedTime = 0f;

    private int _skillNum;
    private bool _canUseSkill = true;

    private void Awake()
    {
        _myMonsterSkill = GetComponent<MonsterSkill_KH>();
        _playerMove = GetComponent<PlayerMove_MT>();
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

        SkillJudge();
        GameobjectTagJudge();
    }

    void Update()
    {
        CallSkill();

        // 乗り移りが発生したらタグを変更（Mが押されたが乗り移りが発生しなかったときも処理が走ってしまう）
        if (Input.GetKeyDown(KeyCode.M))
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
    private void GameobjectTagJudge()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            _myMonsterSkill.enabled = true;
            _playerMove.enabled = false;

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
    /// どのスキルを持っているか判定
    /// </summary>
    private void SkillJudge()
    {
        switch (_skillNum)
        {
            case (int)MonsterSkill_KH.SkillType.HighSpeedAssault:      // 高速突撃の場合
                _highSpeedAssault = GetComponent<HighSpeedAssault_KH>();
                return;

            case (int)MonsterSkill_KH.SkillType.WeaponAttack:          // 武器を使った攻撃などの場合
                _weaponAttack = GetComponent<WeaponAttack_KH>();
                return;

            case (int)MonsterSkill_KH.SkillType.LongDistanceAttack:        // 遠距離攻撃の場合
                _longDistanceAttack = GetComponent<LongDistanceAttack_KH>();
                return;

            case (int)MonsterSkill_KH.SkillType.Fly:                   // 飛ぶスキルの場合
                _flySkill = GetComponent<FlySkill_KH>();
                return;

            case (int)MonsterSkill_KH.SkillType.Petrification:         // 石化の場合
                _petrification = GetComponent<Petrification_KH>();
                return;
        }
    }

    /// <summary>
    /// ボタンを押したらスキル発動
    /// </summary>
    private void CallSkill()
    {
        if (Input.GetMouseButtonDown(1) && _canUseSkill)
        {
            _canUseSkill = false;

            switch (_skillNum)
            {
                case (int)MonsterSkill_KH.SkillType.HighSpeedAssault:      // 高速突撃の場合
                    _highSpeedAssault.SpeedUp();
                    _characterAnim.NowAnim = "Skill";
                    break;

                case (int)MonsterSkill_KH.SkillType.WeaponAttack:          // 武器を使った攻撃などの場合
                    _weaponAttack.Attack();
                    _characterAnim.NowAnim = "Skill";
                    break;

                case (int)MonsterSkill_KH.SkillType.LongDistanceAttack:        // 遠距離攻撃の場合
                    _longDistanceAttack.GenerateBullet();
                    _characterAnim.NowAnim = "Skill";
                    break;

                case (int)MonsterSkill_KH.SkillType.Fly:                   // 飛ぶスキルの場合
                    if (_flySkill.IsFlying) return;
                    _flySkill.PlayerFlyManager();
                    _characterAnim.NowAnim = "Skill";
                    break;

                case (int)MonsterSkill_KH.SkillType.Petrification:         // 石化の場合
                    _petrification.CreatePetrificationArea();
                    _characterAnim.NowAnim = "Skill";
                    break;
            }

            _coolTimeUI.StartCoolTime();
        }
    }

    /// <summary>
    /// 定期的にスキルを発動
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
