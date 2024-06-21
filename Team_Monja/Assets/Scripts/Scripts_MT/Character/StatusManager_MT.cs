using UnityEngine;

public class StatusManager_MT : MonoBehaviour
{
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

    private int _maxHP = 0;
    private int _hp = 0;
    private int _str = 0;
    private int _def = 0;

    //コンポーネント
    MoveSlider_MT moveSlider;

    //canvas
    [SerializeField] private GameObject canvasObj;

    // ステータスのプロパティ
    public int MaxHP { get { return _maxHP; } private set { _maxHP = value; } }
    public int HP { get { return _hp; } set { _hp = value; } }
    public int Strength { get { return _str; } private set { _str = value; } }
    public int Defense { get { return _def; } private set { _def = value; } }
    public int PlusHP { get { return _plusStatHP; } set { _plusStatHP = value; } }
    public int PlusStrength { get { return _plusStatStrength; } set { _plusStatStrength = value; } }
    public int PlusDefense { get { return _plusStatDefense; } set { _plusStatDefense = value; } }

    private void Awake()
    {
        // プレイヤーの初期ステータスを設定
        InitializeStatus();
    }

    private void Start()
    {

            // スライダーコンポーネントの取得
            moveSlider = canvasObj.GetComponentInChildren<MoveSlider_MT>();

        Debug.Log("スライダー" + moveSlider);

        // 倍率を適用
        ApplyMultipliers();

        // 現在のHPを最大HPに設定
        HP = MaxHP;

        //HPバー更新
        if (CompareTag("Player"))
        {
            moveSlider.SetMaxHP(MaxHP);
            moveSlider.SetCurrentHP(HP);
        }

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

        //HPバー更新
        if (moveSlider != null)
        {
            if (CompareTag("Player"))
            {
                moveSlider.SetMaxHP(MaxHP);
                moveSlider.SetCurrentHP(HP);
            }
        }
        Debug.Log($"倍率適用後: MaxHP = {MaxHP}, Strength = {Strength}, Defense = {Defense}, HP = {HP}");
    }

    // ステータスの更新をリセットするメソッド
    public void ResetMultipliers()
    {
        // 現在のステータスから倍率を取り除く
        MaxHP = Mathf.FloorToInt(MaxHP / healthMultiplier);
        Strength = Mathf.FloorToInt(Strength / strengthMultiplier);
        Defense = Mathf.FloorToInt(Defense / defenseMultiplier);

        //HPバー更新
        if (moveSlider != null)
        {
            if (CompareTag("Player"))
            {
                moveSlider.SetMaxHP(MaxHP);
                moveSlider.SetCurrentHP(HP);
            }
        }
        Debug.Log($"倍率リセット後: MaxHP = {MaxHP}, Strength = {Strength}, Defense = {Defense}, HP = {HP}");
    }

    // 回復
    public void HealHP(int healAmount)
    {
        HP += healAmount;
        if (HP > MaxHP)
        {
            HP = MaxHP;
        }
        //HPバー更新
        if (moveSlider != null)
        {
            if (CompareTag("Player"))
            {
                moveSlider.SetCurrentHP(HP);
            }
        }
        Debug.Log($"回復後: HP = {HP}");
    }
}
