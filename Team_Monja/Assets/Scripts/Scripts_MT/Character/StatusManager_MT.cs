using UnityEngine;

public class StatusManager_MT : MonoBehaviour
{
    // ��b�l
    [SerializeField] private int _baseHP = 10; // �C���X�y�N�^�[�Őݒ�\
    [SerializeField] private int _baseStrength = 5; // �C���X�y�N�^�[�Őݒ�\
    [SerializeField] private int _baseDefense = 3; // �C���X�y�N�^�[�Őݒ�\

    // �{��
    [SerializeField] private float healthMultiplier = 1.0f;  // �C���X�y�N�^�[�Őݒ�\
    [SerializeField] private float strengthMultiplier = 1.0f;  // �C���X�y�N�^�[�Őݒ�\
    [SerializeField] private float defenseMultiplier = 1.0f;  // �C���X�y�N�^�[�Őݒ�\

    // �v���X�̒l
    [SerializeField] private int _plusStatHP = 0; // �C���X�y�N�^�[�Őݒ�\
    [SerializeField] private int _plusStatStrength = 0; // �C���X�y�N�^�[�Őݒ�\
    [SerializeField] private int _plusStatDefense = 0; // �C���X�y�N�^�[�Őݒ�\

    private int _maxHP = 0;
    private int _hp = 0;
    private int _str = 0;
    private int _def = 0;

    //�R���|�[�l���g
    MoveSlider_MT _moveSlider;
    StrengthStatusUI_MT _strengthStatusUI;
    DefenseStatusUI_MT _defenseStatusUI;
    HPStatusUI_MT _hpStatusUI;

    //canvas
    [SerializeField] private GameObject canvasObj;

    // �X�e�[�^�X�̃v���p�e�B
    public int MaxHP { get { return _maxHP; } private set { _maxHP = value; } }
    public int HP { get { return _hp; } set { _hp = value; } }
    public int Strength { get { return _str; } private set { _str = value; } }
    public int Defense { get { return _def; } private set { _def = value; } }
    public int PlusHP { get { return _plusStatHP; } set { _plusStatHP = value; } }
    public int PlusStrength { get { return _plusStatStrength; } set { _plusStatStrength = value; } }
    public int PlusDefense { get { return _plusStatDefense; } set { _plusStatDefense = value; } }

    private void Awake()
    {
        // �v���C���[�̏����X�e�[�^�X��ݒ�
        InitializeStatus();
    }

    private void Start()
    {
        _moveSlider = canvasObj.GetComponentInChildren<MoveSlider_MT>();
        _strengthStatusUI = canvasObj.GetComponentInChildren<StrengthStatusUI_MT>();
        _defenseStatusUI = canvasObj.GetComponentInChildren<DefenseStatusUI_MT>();
        _hpStatusUI = canvasObj.GetComponentInChildren<HPStatusUI_MT>();


        // �{����K�p
        ApplyMultipliers();

        // ���݂�HP���ő�HP�ɐݒ�
        HP = MaxHP;

        //HP�o�[�X�V
        if (CompareTag("Player"))
        {
            _moveSlider.SetMaxHP(MaxHP);
            _moveSlider.SetCurrentHP(HP);
        }

    }

    private void Update()
    {
        if(CompareTag("Player"))
        {
            _strengthStatusUI.ChangeText(Strength);
            _defenseStatusUI.ChangeText(Defense);
            _hpStatusUI.ChangeText(HP, MaxHP);
            _moveSlider.SetMaxHP(MaxHP);
            _moveSlider.SetCurrentHP(HP);
        }
    }


    // �X�e�[�^�X�̏�����
    private void InitializeStatus()
    {
        MaxHP = _baseHP + PlusHP;
        Strength = _baseStrength + PlusStrength;
        Defense = _baseDefense + PlusDefense;
    }

    // �{����������
    public void ApplyMultipliers()
    {
        // ���݂̃X�e�[�^�X�ɔ{����K�p
        MaxHP = Mathf.FloorToInt((_baseHP + PlusHP) * healthMultiplier);
        Strength = Mathf.FloorToInt((_baseStrength + PlusStrength) * strengthMultiplier);
        Defense = Mathf.FloorToInt((_baseDefense + PlusDefense) * defenseMultiplier);

        // ���݂�HP���X�V�i�K�v�ɉ����Ē����j
        HP = Mathf.Clamp(HP, 0, MaxHP);

    
        Debug.Log($"�{���K�p��: MaxHP = {MaxHP}, Strength = {Strength}, Defense = {Defense}, HP = {HP}");
    }

    // �X�e�[�^�X�̍X�V�����Z�b�g���郁�\�b�h
    public void ResetMultipliers()
    {
        // ���݂̃X�e�[�^�X����{������菜��
        MaxHP = Mathf.FloorToInt(MaxHP / healthMultiplier);
        Strength = Mathf.FloorToInt(Strength / strengthMultiplier);
        Defense = Mathf.FloorToInt(Defense / defenseMultiplier);


        Debug.Log($"�{�����Z�b�g��: MaxHP = {MaxHP}, Strength = {Strength}, Defense = {Defense}, HP = {HP}");
    }

    // ��
    public void HealHP(int healAmount)
    {
        HP += healAmount;
        if (HP > MaxHP)
        {
            HP = MaxHP;
        }

        Debug.Log($"�񕜌�: HP = {HP}");
    }
}
