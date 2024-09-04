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

    private float _updateTime = 0f;    // ���b�����ɃX�L�����Ăяo����
    private float _elapsedTime = 3f;   // �ŏ��̈��͂����ɍU�����邽�߂�_maxTimeSpacing���傫�Ȓl�Ƃ���

    [SerializeField]
    private float _moveStopTime = 2f;  // �X�L�����Ă񂾌㉽�b�ԓ������~�߂Ă�����
    private float _moveStopElapsedTime = 0f;
    private bool _isStop = false;

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
    //���{
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
        //���{
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
        if (_isStop)
        {
            RestartMoveInTime();
        }
    }

    /// <summary>
    /// �����X�^�[����v���C���[�ɕς������X�N���v�g�؂�ւ�
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
    /// _skillNum���Q�b�g
    /// </summary>
    public int SkillTypeNum
    {
        get { return _skillNum; }
    }

    /// <summary>
    /// ����I�ɃX�L���𔭓�
    /// </summary>
    private void UpdateTime()
    {
        if (_changeEnemyMoveType.NowState != ChangeEnemyMoveType_KH.EnemyMoveState.InAttack) return;

        // ���ԉ��Z
        _elapsedTime += Time.deltaTime;

        // �K�莞�ԂɒB���Ă����ꍇ
        if (_elapsedTime > _updateTime)
        {
            _enemyMove.enabled = false;
            _isStop = true;

            RandomCallSkill();      // �X�L������  
            _elapsedTime = 0f;
            //MoveStop();
        }
    }

    /// <summary>
    /// �����X�^�[�����b�����ɃX�L�����g��
    /// </summary>
    private void RandomCallSkill()
    {
        _skillInterface.SpecialAttack();
    }

    /// <summary>
    /// �X�L�����g���Ă���Ԏ~�߂�A����т��̉����̏���
    /// </summary>
    /// �X�L�����g���I��������~����
    private void /*MoveStop*/RestartMoveInTime()
    {
        //float skillTime = _updateTime;
        //���ԉ��Z
        //_elapsedTime += Time.deltaTime;
        _moveStopElapsedTime += Time.deltaTime;


        /*if (_elapsedTime > skillTime)
        {
            _enemyMove.enabled = true;
        }
        else return;*/

        if(_moveStopElapsedTime > _moveStopTime)
        {
            _moveStopElapsedTime = 0f;
            _enemyMove.enabled = true;
            _isStop = false;
        }
    }
}
