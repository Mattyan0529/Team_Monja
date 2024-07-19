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

    private float _updateTime = 0f;    // ���b�����ɃX�L�����Ăяo����
    private float _elapsedTime = default;

    private float _maxTimeSpacing = 4f;
    private float _minTimeSpacing = 2f;

    private float _nearPlayerAreaSize = 70f;

    private PlayerSkill_KH _playerSkill = default;
    private PlayerManager_KH _playerManager = default;
    private MonsterRandomWalk_KH _monsterRandomWalk = default;
    private EnemyMove _enemyMove = default;
    private SkillSpriteChange_KH _skillSpriteChange = default;
    private SkillSpriteChange_KH _normalAttackSpriteChange = default;
    private IDamagable _skillInterface = default;
    //���{
    private CharacterAnim_MT _characterAnim;


    private NormalAttack_KH _normalAttack = default;
    private PlayerGuard_KH _playerGuard = default;

    private int _skillNum = 6;


    void Awake()
    {
        _playerSkill = GetComponent<PlayerSkill_KH>();
        _monsterRandomWalk = GetComponent<MonsterRandomWalk_KH>();
        _enemyMove = GetComponent<EnemyMove>();
        _playerManager = _residentScript.GetComponent<PlayerManager_KH>();

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
        GameobjectTagJudge();
        _updateTime = Random.Range(_minTimeSpacing, _maxTimeSpacing);
        _skillInterface = GetComponent<IDamagable>();
    }

    void Update()
    {
        GameobjectTagJudge();
        UpdateTime();

        // ���ڂ肪����������^�O��ύX�iM�������ꂽ�����ڂ肪�������Ȃ������Ƃ��������������Ă��܂��j
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameobjectTagJudge();
        }
    }

    /// <summary>
    /// �����X�^�[����v���C���[�ɕς������X�N���v�g�؂�ւ�
    /// </summary>
    public void GameobjectTagJudge()
    {
        if (gameObject.CompareTag("Player"))
        {
            _playerManager.Player = gameObject;     // �v���C���[�X�V
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

            _monsterRandomWalk.enabled = false;
            _enemyMove.enabled = false;
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
        // ���ԉ��Z
        _elapsedTime += Time.deltaTime;

        // �K�莞�ԂɒB���Ă����ꍇ
        if (_elapsedTime > _updateTime)
        {
            RandomCallSkill();      // �X�L������  
            _elapsedTime = 0f;
        }
    }

    /// <summary>
    /// �����X�^�[�����b�����ɃX�L�����g��
    /// </summary>
    private void RandomCallSkill()
    {
        _skillInterface.SpecialAttack();
    }
}
