using UnityEngine;

public class StatusManager_MT : MonoBehaviour
{
    // ��b�l
    [SerializeField] private int _baseHP = 0; // �C���X�y�N�^�[�Őݒ�\
    [SerializeField] private int _baseStrength = 0; // �C���X�y�N�^�[�Őݒ�\
    [SerializeField] private int _baseDefense = 0; // �C���X�y�N�^�[�Őݒ�\

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
    MoveSlider_MT moveSlider;

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
        // �v���C���[�̏����X�e�[�^�X��ݒ肵�A�{����K�p
        InitializeStatus();
        ApplyMultipliers();
    }

    private void Start()
    {
        //GetComponent
        moveSlider = canvasObj.GetComponent<MoveSlider_MT>();

        // ���݂�HP���ő�HP�ɐݒ�
        HP = MaxHP;
        //HP�o�[�X�V
        moveSlider.NewValue(MaxHP);
        moveSlider.NowValue(HP);
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
        //HP�o�[�X�V
        moveSlider.NowValue(HP);
    }

    // �X�e�[�^�X�̍X�V�����Z�b�g���郁�\�b�h
    public void ResetMultipliers()
    {
        // ���݂̃X�e�[�^�X����{������菜��
        MaxHP = Mathf.FloorToInt(MaxHP / healthMultiplier);
        Strength = Mathf.FloorToInt(Strength / strengthMultiplier);
        Defense = Mathf.FloorToInt(Defense / defenseMultiplier);
    }

  
    //�L�����N�^�[��HP���X�V����i���炷�j
    public void UpdateHitPoint(StatusManager_MT attackdStatus, int afterAttackedHitPoint)
    {
        attackdStatus.HP = afterAttackedHitPoint;

    }

    // ��
    public void HealHP(int healAmount)
    {
        HP += healAmount;
        if (HP > MaxHP)
        {
            HP = MaxHP;
        }
        //HP�o�[�X�V
        moveSlider.NowValue(HP);
    }
}
