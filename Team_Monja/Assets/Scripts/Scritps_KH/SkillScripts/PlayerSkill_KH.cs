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

    //���{
    private CharacterAnim_MT _characterAnim = default;

    private float _coolTime = 2f;    // �X�L���𔭓����Ă��玟�ɔ����ł���悤�ɂȂ�܂ł̎���
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

        // ���ڂ肪����������^�O��ύX�iM�������ꂽ�����ڂ肪�������Ȃ������Ƃ��������������Ă��܂��j
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
    /// �n�܂������_��Enemy�������玩����Enemy��Ԃɂ���
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
    /// �ǂ̃X�L���������Ă��邩����
    /// </summary>
    private void SkillJudge()
    {
        switch (_skillNum)
        {
            case (int)MonsterSkill_KH.SkillType.HighSpeedAssault:      // �����ˌ��̏ꍇ
                _highSpeedAssault = GetComponent<HighSpeedAssault_KH>();
                return;

            case (int)MonsterSkill_KH.SkillType.WeaponAttack:          // ������g�����U���Ȃǂ̏ꍇ
                _weaponAttack = GetComponent<WeaponAttack_KH>();
                return;

            case (int)MonsterSkill_KH.SkillType.LongDistanceAttack:        // �������U���̏ꍇ
                _longDistanceAttack = GetComponent<LongDistanceAttack_KH>();
                return;

            case (int)MonsterSkill_KH.SkillType.Fly:                   // ��ԃX�L���̏ꍇ
                _flySkill = GetComponent<FlySkill_KH>();
                return;

            case (int)MonsterSkill_KH.SkillType.Petrification:         // �Ή��̏ꍇ
                _petrification = GetComponent<Petrification_KH>();
                return;
        }
    }

    /// <summary>
    /// �{�^������������X�L������
    /// </summary>
    private void CallSkill()
    {
        if (Input.GetMouseButtonDown(1) && _canUseSkill)
        {
            _canUseSkill = false;

            switch (_skillNum)
            {
                case (int)MonsterSkill_KH.SkillType.HighSpeedAssault:      // �����ˌ��̏ꍇ
                    _highSpeedAssault.SpeedUp();
                    _characterAnim.NowAnim = "Skill";
                    break;

                case (int)MonsterSkill_KH.SkillType.WeaponAttack:          // ������g�����U���Ȃǂ̏ꍇ
                    _weaponAttack.Attack();
                    _characterAnim.NowAnim = "Skill";
                    break;

                case (int)MonsterSkill_KH.SkillType.LongDistanceAttack:        // �������U���̏ꍇ
                    _longDistanceAttack.GenerateBullet();
                    _characterAnim.NowAnim = "Skill";
                    break;

                case (int)MonsterSkill_KH.SkillType.Fly:                   // ��ԃX�L���̏ꍇ
                    if (_flySkill.IsFlying) return;
                    _flySkill.PlayerFlyManager();
                    _characterAnim.NowAnim = "Skill";
                    break;

                case (int)MonsterSkill_KH.SkillType.Petrification:         // �Ή��̏ꍇ
                    _petrification.CreatePetrificationArea();
                    _characterAnim.NowAnim = "Skill";
                    break;
            }

            _coolTimeUI.StartCoolTime();
        }
    }

    /// <summary>
    /// ����I�ɃX�L���𔭓�
    /// </summary>
    private void UpdateTime()
    {
        // ���ԉ��Z
        _elapsedTime += Time.deltaTime;

        // �K�莞�ԂɒB���Ă����ꍇ
        if (_elapsedTime > _coolTime)
        {
            _elapsedTime = 0f;
            _canUseSkill = true;
        }
    }
}
