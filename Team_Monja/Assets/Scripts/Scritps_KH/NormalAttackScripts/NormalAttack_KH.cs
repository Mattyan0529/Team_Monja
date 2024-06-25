using UnityEngine;

public class NormalAttack_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _residentScript;

    [SerializeField]
    GameObject _coolTimeUIObj = default;

    private float _radius = 4f;      // UŒ‚”ÍˆÍ‚Æ‚µ‚Ä¶‚İo‚·Sphere‚Ì”¼Œa
    private Vector3 _position = Vector3.zero;       // Sphere‚ÌˆÊ’u

    private float _deleteTime = 0.2f;
    private float _elapsedTime = 0f;

    private float _coolTime = 0.5f;    // ’ÊíUŒ‚‚ğ”­“®‚µ‚Ä‚©‚çŸ‚É”­“®‚Å‚«‚é‚æ‚¤‚É‚È‚é‚Ü‚Å‚ÌŠÔ
    private float _coolTimeElapsedTime = 0f;

    [SerializeField]
    private GameObject _attackArea;
    private bool _isAttack = false;
    private bool _canUseNormalAttack = true;

    private WriteHitPoint_KH _writeHitPoint = default;
    private CoolTimeUI _coolTimeUI = default;
    //¼–{
    private CharacterAnim_MT _characterAnim = default;

    void Start()
    {
        _writeHitPoint = _residentScript.GetComponent<WriteHitPoint_KH>();
        _characterAnim = GetComponent<CharacterAnim_MT>();
        _coolTimeUI = _coolTimeUIObj.GetComponent<CoolTimeUI>();
    }

    void Update()
    {
        UpdateTime();
        UpdateCoolTime();
        AttackInputManager();
    }

    private void AttackInputManager()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        Debug.Log(_canUseNormalAttack);
    }

    /// <summary>
    /// UŒ‚”ÍˆÍ‚ÌSphere‚ğ¶¬
    /// </summary>
    private void Attack()
    {
        if (!_canUseNormalAttack) return;

        //¼–{
        _characterAnim.NowAnim = "Attack";


        _isAttack = true;
        _canUseNormalAttack = false;
        _coolTimeUI.StartCoolTime();

        _attackArea.SetActive(true);
    }

    /// <summary>
    /// “–‚½‚Á‚½‘Šè‚ğæ“¾
    /// </summary>
    public void HitDecision(GameObject hitObj)
    {
        // ‘Šè‚Æ©•ª‚ÌStatusManager‚ª—¼•û•K—v
        StatusManager_MT targetStatusManager = hitObj.gameObject.GetComponent<StatusManager_MT>();
        StatusManager_MT myStatusManager = GetComponent<StatusManager_MT>();

        HitPointCalculation(myStatusManager, targetStatusManager);
    }

    /// <summary>
    /// ƒ_ƒ[ƒWŒvZ
    /// </summary>
    public void HitPointCalculation(StatusManager_MT myStatus, StatusManager_MT targetStatus)
    {
        int myAttackPower = myStatus.Strength;        // ©•ª‚ÌUŒ‚—Í‚ğget‚µ‚Ä‚­‚é
        int targetDefensePower = targetStatus.Defense;        // ‘Šè‚Ì–hŒä—Í‚ğget‚µ‚Ä‚­‚é
        int targetHitPoint = targetStatus.HP;        // ‘Šè‚ÌHP‚ğget‚µ‚Ä‚­‚é

        if (myAttackPower < targetDefensePower) return;        // –hŒä—Í‚Ì‚Ù‚¤‚ª‚‚©‚Á‚½‚ç0ƒ_ƒ[ƒW

        int damage = targetHitPoint - (myAttackPower - targetDefensePower);
        _writeHitPoint.UpdateHitPoint(targetStatus, damage);      // targetStatus‚ÌHP‚ğXV
    }

    /// <summary>
    /// ˆê’èŠÔŒãUŒ‚”ÍˆÍ‚ğíœ‚·‚é
    /// </summary>
    private void UpdateTime()
    {
        if (!_isAttack) return;     // UŒ‚’†ˆÈŠO‚Íˆ—‚ğs‚í‚È‚¢

        // ŠÔ‰ÁZ
        _elapsedTime += Time.deltaTime;

        // ‹K’èŠÔ‚É’B‚µ‚Ä‚¢‚½ê‡
        if (_elapsedTime > _deleteTime)
        {
            _attackArea.SetActive(false);
            _elapsedTime = 0f;
            _isAttack = false;
        }
    }

    /// <summary>
    /// ˆê’èŠÔŒãUŒ‚”ÍˆÍ‚ğíœ‚·‚é
    /// </summary>
    private void UpdateCoolTime()
    {
        if (_canUseNormalAttack) return;     // UŒ‚’†ˆÈŠO‚Íˆ—‚ğs‚í‚È‚¢

        // ŠÔ‰ÁZ
        _coolTimeElapsedTime += Time.deltaTime;

        // ‹K’èŠÔ‚É’B‚µ‚Ä‚¢‚½ê‡
        if (_coolTimeElapsedTime > _coolTime)
        {
            _coolTimeElapsedTime = 0f;
            _canUseNormalAttack = true;
        }
    }
}
