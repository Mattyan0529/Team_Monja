using UnityEngine;

public class PlayerSkill_KH : MonoBehaviour
{
    private MonsterSkill_KH _myMonsterSkill = default;
    private PlayerMove_MT _playerMove = default;

    private HighSpeedAssault_KH _highSpeedAssault = default;
    private WeaponAttack_KH _weaponAttack = default;
    private LongDistanceAttack_KH _longDistanceAttack = default;
    private FlySkill_KH _flySkill = default;
    private Petrification_KH _petrification = default;

    private NormalAttack_KH _normalAttack = default;
    private PlayerGuard_KH _playerGuard = default;

    private int _skillNum;

    private void Awake()
    {
        _myMonsterSkill = GetComponent<MonsterSkill_KH>();
        _playerMove = GetComponent<PlayerMove_MT>();
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
        if (Input.GetMouseButtonDown(1))
        {
            switch (_skillNum)
            {
                case (int)MonsterSkill_KH.SkillType.HighSpeedAssault:      // �����ˌ��̏ꍇ
                    _highSpeedAssault.SpeedUp();
                    return;

                case (int)MonsterSkill_KH.SkillType.WeaponAttack:          // ������g�����U���Ȃǂ̏ꍇ
                    _weaponAttack.Attack();
                    return;

                case (int)MonsterSkill_KH.SkillType.LongDistanceAttack:        // �������U���̏ꍇ
                    _longDistanceAttack.GenerateBullet();
                    return;

                case (int)MonsterSkill_KH.SkillType.Fly:                   // ��ԃX�L���̏ꍇ
                    _flySkill.PlayerFlyManager();
                    return;

                case (int)MonsterSkill_KH.SkillType.Petrification:         // �Ή��̏ꍇ
                    _petrification.CreatePetrificationArea();
                    return;
            }
        }
    }
}
