using UnityEngine;

public class NormalAttack_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _residentScript;

    [SerializeField]
    GameObject _coolTimeUIObj = default;

    private float _deleteTime = 0.2f;
    private float _elapsedTime = 0f;

    private float _coolTime = 0.5f;    // �ʏ�U���𔭓����Ă��玟�ɔ����ł���悤�ɂȂ�܂ł̎���
    private float _coolTimeElapsedTime = 0f;

    [SerializeField]
    private GameObject _attackArea;
    private bool _isAttack = false;
    private bool _canUseNormalAttack = true;

    private WriteHitPoint_KH _writeHitPoint = default;
    private CoolTimeUI _coolTimeUI = default;
    private AudioSource _audioSource = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    //���{
    private CharacterAnim_MT _characterAnim = default;

    void Start()
    {
        _writeHitPoint = _residentScript.GetComponent<WriteHitPoint_KH>();
        _characterAnim = GetComponent<CharacterAnim_MT>();
        _coolTimeUI = _coolTimeUIObj.GetComponent<CoolTimeUI>();
        _soundEffectManagement = _residentScript.GetComponent<SoundEffectManagement_KH>();
        _audioSource = GetComponentInChildren<AudioSource>();
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
    }

    /// <summary>
    /// �U���͈͂�Sphere�𐶐�
    /// </summary>
    private void Attack()
    {
        if (!_canUseNormalAttack) return;

        // SE��炷
        _soundEffectManagement.PlayStrongPunchSound(_audioSource);

        //���{
        _characterAnim.NowAnim = "Attack";


        _isAttack = true;
        _canUseNormalAttack = false;
        _coolTimeUI.StartCoolTime();

        _attackArea.SetActive(true);
    }

    /// <summary>
    /// ��������������擾
    /// </summary>
    public void HitDecision(GameObject hitObj)
    {
        // ����Ǝ�����StatusManager�������K�v
        StatusManager_MT targetStatusManager = hitObj.gameObject.GetComponent<StatusManager_MT>();
        StatusManager_MT myStatusManager = GetComponent<StatusManager_MT>();

        HitPointCalculation(myStatusManager, targetStatusManager);
    }

    /// <summary>
    /// �_���[�W�v�Z
    /// </summary>
    public void HitPointCalculation(StatusManager_MT myStatus, StatusManager_MT targetStatus)
    {
        int myAttackPower = myStatus.Strength;        // �����̍U���͂�get���Ă���
        int targetDefensePower = targetStatus.Defense;        // ����̖h��͂�get���Ă���
        int targetHitPoint = targetStatus.HP;        // �����HP��get���Ă���

        if (myAttackPower < targetDefensePower) return;        // �h��͂̂ق�������������0�_���[�W

        int damage = targetHitPoint - (myAttackPower - targetDefensePower);
        _writeHitPoint.UpdateHitPoint(targetStatus, damage);      // targetStatus��HP���X�V
    }

    /// <summary>
    /// ��莞�Ԍ�U���͈͂��폜����
    /// </summary>
    private void UpdateTime()
    {
        if (!_isAttack) return;     // �U�����ȊO�͏������s��Ȃ�

        // ���ԉ��Z
        _elapsedTime += Time.deltaTime;

        // �K�莞�ԂɒB���Ă����ꍇ
        if (_elapsedTime > _deleteTime)
        {
            _attackArea.SetActive(false);
            _elapsedTime = 0f;
            _isAttack = false;
        }
    }

    /// <summary>
    /// �N�[���^�C����ʏ�U�����g����悤�ɂ���
    /// </summary>
    private void UpdateCoolTime()
    {
        if (_canUseNormalAttack) return;     // �U�����ȊO�͏������s��Ȃ�

        // ���ԉ��Z
        _coolTimeElapsedTime += Time.deltaTime;

        // �K�莞�ԂɒB���Ă����ꍇ
        if (_coolTimeElapsedTime > _coolTime)
        {
            _coolTimeElapsedTime = 0f;
            _canUseNormalAttack = true;
        }
    }
}
