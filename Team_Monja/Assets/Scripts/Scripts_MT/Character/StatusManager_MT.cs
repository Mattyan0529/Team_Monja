using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;
public class StatusManager_MT : MonoBehaviour
{
    [Header("���O��I�����Ă�������")]
    public NameList _name;  //�񋓌^�̒l���i�[����ϐ�
    
    [Space]
  

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

    [SerializeField] private Image _damageImage = default;

    private int _maxHP = 0;
    private int _hp = 0;
    private int _str = 0;
    private int _def = 0;

    //HP���r����悤
    private int _currentHP = 0;
    //_damageImage�𓧉߂���X�s�[�h
    private float _fadeSpeed = 0.1f;

    //�R���|�[�l���g
    private MoveSlider_MT _moveSliderPlayer;
    private MoveSlider_MT _moveSliderBoss;
    private StrengthStatusUI_MT _strengthStatusUI;
    private DefenseStatusUI_MT _defenseStatusUI;
    private HPStatusUI_MT _hpStatusUI;
    private BossStatusHP_MT _bossStatusHP;



    //canvas
    [SerializeField] private GameObject canvasObjPlayer;
    [SerializeField] private GameObject canvasObjBoss   ;

    // �X�e�[�^�X�̃C���X�^���X
    public int MaxHP { get { return _maxHP; } private set { _maxHP = value; } }
    public int HP { get { return _hp; } set { _hp = value; } }
    public int Strength { get { return _str; } private set { _str = value; } }
    public int Defense { get { return _def; } private set { _def = value; } }
    public int PlusHP { get { return _plusStatHP; } set { _plusStatHP = value; } }
    public int PlusStrength { get { return _plusStatStrength; } set { _plusStatStrength = value; } }
    public int PlusDefense { get { return _plusStatDefense; } set { _plusStatDefense = value; } }

    //�񋓌^�̒�`
    public enum NameList
    {
        EvilMage, LizardWarrior, Mushroom, Orc, Skeleton, Slime, Boss
    }

    private void Awake()
    {
        // �v���C���[�̏����X�e�[�^�X��ݒ�
        InitializeStatus();
    }

    private void Start()
    {
        //canvasPlayer����擾
        _moveSliderPlayer = canvasObjPlayer.GetComponentInChildren<MoveSlider_MT>();
        _strengthStatusUI = canvasObjPlayer.GetComponentInChildren<StrengthStatusUI_MT>();
        _defenseStatusUI = canvasObjPlayer.GetComponentInChildren<DefenseStatusUI_MT>();
        _hpStatusUI = canvasObjPlayer.GetComponentInChildren<HPStatusUI_MT>();
        //canvasBoss����擾
        _bossStatusHP = canvasObjBoss.GetComponentInChildren<BossStatusHP_MT>();
        _moveSliderBoss = canvasObjBoss.GetComponentInChildren<MoveSlider_MT>();
      
        // �v���C���[�ȊO��������{����K�p�i�ʂ̂Ƃ���Őݒ肷�邽��
        if (!CompareTag("Player"))
        {
            ApplyMultipliers();
        }
    

        // ���݂�HP���ő�HP�ɐݒ�
        HP = MaxHP;
        _currentHP = HP;


        //HP�o�[�X�V
        if (CompareTag("Player"))
        {
            _moveSliderPlayer.SetMaxHP(MaxHP);
            _moveSliderPlayer.SetCurrentHP(HP);
        }
        if(CompareTag("Boss"))
        {
            _moveSliderBoss.SetMaxHP(MaxHP);
            _moveSliderBoss.SetCurrentHP(HP);
        }
        //�_���[�W�Ɖ񕜂̉�ʌ��ʂ̐F�𓧖���
        _damageImage.color = Color.clear;

    }

    private void Update()
    {
        if (CompareTag("Player"))
        {
            //��ʂɕ\������X�e�[�^�X
            _strengthStatusUI.ChangeText(Strength);
            _defenseStatusUI.ChangeText(Defense);
            _hpStatusUI.ChangeText(HP, MaxHP);
            _moveSliderPlayer.SetMaxHP(MaxHP);
            _moveSliderPlayer.SetCurrentHP(HP);

            //HP�����������ʌ��ʂ�����
            if (IsHPChange())
            {
                ChangeDamageImage();
            }

        }
        if(CompareTag("Boss"))
        {
            //��ʂɕ\������{�XHP
            _bossStatusHP.ChangeText(HP, MaxHP);
            _moveSliderBoss.SetMaxHP(MaxHP);
            _moveSliderBoss.SetCurrentHP(HP);

        }


        //�_���[�W�̉�ʌ��ʂ𓧖��ɖ߂��Ă���
        _damageImage.color = Color.Lerp(_damageImage.color, Color.clear, _fadeSpeed * Time.deltaTime);

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
    }

    // �X�e�[�^�X�̍X�V�����Z�b�g���郁�\�b�h
    public void ResetMultipliers()
    {
        // ���݂̃X�e�[�^�X����{������菜��
        MaxHP = Mathf.FloorToInt(MaxHP / healthMultiplier);
        Strength = Mathf.FloorToInt(Strength / strengthMultiplier);
        Defense = Mathf.FloorToInt(Defense / defenseMultiplier);
    }

    /// <summary>
    /// ��
    /// </summary>
    /// <param name="healAmount"></param>
    public void HealHP(int healAmount)
    {
        HP += healAmount;
        if (HP > MaxHP)
        {
            HP = MaxHP;
        }
    }


    /// <summary>
    /// HP���ς������
    /// </summary>
    /// <returns></returns>
    private bool IsHPChange()
    {
        if (_currentHP > HP)
        {
            _currentHP = HP;
            return true;
        }

        _currentHP = HP;

        return false;
    }

    /// <summary>
    /// �_���[�W���󂯂��Ƃ��ɉ�ʂ�Ԃ�����
    /// </summary>
    private void ChangeDamageImage()
    {
        _damageImage.color = new Color(0.7f, 0, 0, 0.7f);
    }


}
