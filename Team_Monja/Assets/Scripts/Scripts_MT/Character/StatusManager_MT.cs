using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;
public class StatusManager_MT : MonoBehaviour
{
    [Header("名前を選択してください")]
    public NameList _name;  //列挙型の値を格納する変数
    
    [Space]
  

    // 基礎値
    [SerializeField] private int _baseHP = 10; // インスペクターで設定可能
    [SerializeField] private int _baseStrength = 5; // インスペクターで設定可能
    [SerializeField] private int _baseDefense = 3; // インスペクターで設定可能

    // 倍率
    [SerializeField] private float healthMultiplier = 1.0f;  // インスペクターで設定可能
    [SerializeField] private float strengthMultiplier = 1.0f;  // インスペクターで設定可能
    [SerializeField] private float defenseMultiplier = 1.0f;  // インスペクターで設定可能

    // プラスの値
    [SerializeField] private int _plusStatHP = 0; // インスペクターで設定可能
    [SerializeField] private int _plusStatStrength = 0; // インスペクターで設定可能
    [SerializeField] private int _plusStatDefense = 0; // インスペクターで設定可能

    [SerializeField] private Image _damageImage = default;

    private int _maxHP = 0;
    private int _hp = 0;
    private int _str = 0;
    private int _def = 0;

    //HPを比較するよう
    private int _currentHP = 0;
    //_damageImageを透過するスピード
    private float _fadeSpeed = 0.1f;

    //コンポーネント
    private MoveSlider_MT _moveSliderPlayer;
    private MoveSlider_MT _moveSliderBoss;
    private StrengthStatusUI_MT _strengthStatusUI;
    private DefenseStatusUI_MT _defenseStatusUI;
    private HPStatusUI_MT _hpStatusUI;
    private BossStatusHP_MT _bossStatusHP;



    //canvas
    [SerializeField] private GameObject canvasObjPlayer;
    [SerializeField] private GameObject canvasObjBoss   ;

    // ステータスのインスタンス
    public int MaxHP { get { return _maxHP; } private set { _maxHP = value; } }
    public int HP { get { return _hp; } set { _hp = value; } }
    public int Strength { get { return _str; } private set { _str = value; } }
    public int Defense { get { return _def; } private set { _def = value; } }
    public int PlusHP { get { return _plusStatHP; } set { _plusStatHP = value; } }
    public int PlusStrength { get { return _plusStatStrength; } set { _plusStatStrength = value; } }
    public int PlusDefense { get { return _plusStatDefense; } set { _plusStatDefense = value; } }

    //列挙型の定義
    public enum NameList
    {
        EvilMage, LizardWarrior, Mushroom, Orc, Skeleton, Slime, Boss
    }

    private void Awake()
    {
        // プレイヤーの初期ステータスを設定
        InitializeStatus();
    }

    private void Start()
    {
        //canvasPlayerから取得
        _moveSliderPlayer = canvasObjPlayer.GetComponentInChildren<MoveSlider_MT>();
        _strengthStatusUI = canvasObjPlayer.GetComponentInChildren<StrengthStatusUI_MT>();
        _defenseStatusUI = canvasObjPlayer.GetComponentInChildren<DefenseStatusUI_MT>();
        _hpStatusUI = canvasObjPlayer.GetComponentInChildren<HPStatusUI_MT>();
        //canvasBossから取得
        _bossStatusHP = canvasObjBoss.GetComponentInChildren<BossStatusHP_MT>();
        _moveSliderBoss = canvasObjBoss.GetComponentInChildren<MoveSlider_MT>();
      
        // プレイヤー以外だったら倍率を適用（別のところで設定するため
        if (!CompareTag("Player"))
        {
            ApplyMultipliers();
        }
    

        // 現在のHPを最大HPに設定
        HP = MaxHP;
        _currentHP = HP;


        //HPバー更新
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
        //ダメージと回復の画面効果の色を透明に
        _damageImage.color = Color.clear;

    }

    private void Update()
    {
        if (CompareTag("Player"))
        {
            //画面に表示するステータス
            _strengthStatusUI.ChangeText(Strength);
            _defenseStatusUI.ChangeText(Defense);
            _hpStatusUI.ChangeText(HP, MaxHP);
            _moveSliderPlayer.SetMaxHP(MaxHP);
            _moveSliderPlayer.SetCurrentHP(HP);

            //HPが減ったら画面効果をつける
            if (IsHPChange())
            {
                ChangeDamageImage();
            }

        }
        if(CompareTag("Boss"))
        {
            //画面に表示するボスHP
            _bossStatusHP.ChangeText(HP, MaxHP);
            _moveSliderBoss.SetMaxHP(MaxHP);
            _moveSliderBoss.SetCurrentHP(HP);

        }


        //ダメージの画面効果を透明に戻していく
        _damageImage.color = Color.Lerp(_damageImage.color, Color.clear, _fadeSpeed * Time.deltaTime);

    }


    // ステータスの初期化
    private void InitializeStatus()
    {
        MaxHP = _baseHP + PlusHP;
        Strength = _baseStrength + PlusStrength;
        Defense = _baseDefense + PlusDefense;
    }

    // 倍率をかける
    public void ApplyMultipliers()
    {
        // 現在のステータスに倍率を適用
        MaxHP = Mathf.FloorToInt((_baseHP + PlusHP) * healthMultiplier);
        Strength = Mathf.FloorToInt((_baseStrength + PlusStrength) * strengthMultiplier);
        Defense = Mathf.FloorToInt((_baseDefense + PlusDefense) * defenseMultiplier);

        // 現在のHPを更新（必要に応じて調整）
        HP = Mathf.Clamp(HP, 0, MaxHP);
    }

    // ステータスの更新をリセットするメソッド
    public void ResetMultipliers()
    {
        // 現在のステータスから倍率を取り除く
        MaxHP = Mathf.FloorToInt(MaxHP / healthMultiplier);
        Strength = Mathf.FloorToInt(Strength / strengthMultiplier);
        Defense = Mathf.FloorToInt(Defense / defenseMultiplier);
    }

    /// <summary>
    /// 回復
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
    /// HPが変わったか
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
    /// ダメージを受けたときに画面を赤くする
    /// </summary>
    private void ChangeDamageImage()
    {
        _damageImage.color = new Color(0.7f, 0, 0, 0.7f);
    }


}
