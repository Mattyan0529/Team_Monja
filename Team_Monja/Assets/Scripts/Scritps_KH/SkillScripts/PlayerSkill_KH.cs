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

    //���{
    private CharacterAnim_MT _characterAnim = default;

    private float _coolTime = 2f;    // �X�L���𔭓����Ă��玟�ɔ����ł���悤�ɂȂ�܂ł̎���
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
        _enemyMove = GetComponent<EnemyMove_KH>();
        _attackAreaJudge = GetComponent<AttackAreaJudge_KH>();
        _characterAnim = GetComponent<CharacterAnim_MT>();
    }

    void Start()
    {
        _skillNum = _myMonsterSkill.SkillTypeNum;

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

        lefttrigger = Input.GetAxis("skill");

        if (!_canUseSkill)
        {
            UpdateTime();
        }
    }

    /// <summary>
    /// �n�܂������_��Enemy�������玩����Enemy��Ԃɂ���
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
    /// �{�^������������X�L������
    /// </summary>
    private void CallSkill()
    {
        
        if (_canUseSkill && (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Cancel")))
        {
            _coolTimeUI.StartCoolTime();

            if (_playerMove != null)
            {
                _playerMove.enabled = false;
            }

            if (_normalAttack != null && _normalAttack.IsAttack) return;
            if (_playerGuard != null && _playerGuard.IsGuard) return;

            _isUseSkill = true;
            _skillInterface.SpecialAttack();
            _canUseSkill = false;
          
            if (_playerMove != null)
            {
                _playerMove.enabled = true;
            }
        }
    }

    /// <summary>
    /// �X�L�����ēx�g����悤�ɂ���
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
