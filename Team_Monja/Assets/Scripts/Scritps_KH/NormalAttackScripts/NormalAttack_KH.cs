using UnityEngine;

public class NormalAttack_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _residentScript;

    [SerializeField]
    GameObject _coolTimeUIObj = default;

    private float _deleteTime = 0.2f;
    private float _elapsedTime = 0f;

    private float _coolTime = 1f;    // �ʏ�U���𔭓����Ă��玟�ɔ����ł���悤�ɂȂ�܂ł̎���
    private float _coolTimeElapsedTime = 0f;

    [SerializeField]
    private GameObject _attackArea;
    private int _attackCount = default;

    private bool _isAttackInput = false;//���͂̏d���h�~
    private float _attackLockTime = 0;
    private float _attackLockDuration = 0.25f;  //�U���̂��Ƃ��̕b���U���ł��Ȃ�����

    private bool _isAttack = false;
    private bool _canUseNormalAttack = true;

    private int _attackRate = 2;
    private int _defenseRate = 4;

    private WriteHitPoint_KH _writeHitPoint = default;
    private CoolTimeUI_KH _coolTimeUI = default;
    private CreateDamageImage_KH _createDamageImage = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private AudioSource _audioSource = default;
    //���{
    private CharacterAnim_MT _characterAnim = default;

    public bool IsAttack
    {
        get { return _isAttack; }
    }

    void Start()
    {
        _writeHitPoint = _residentScript.GetComponent<WriteHitPoint_KH>();
        _characterAnim = GetComponent<CharacterAnim_MT>();
        _coolTimeUI = _coolTimeUIObj.GetComponent<CoolTimeUI_KH>();
        _createDamageImage = _residentScript.GetComponent<CreateDamageImage_KH>();
        _soundEffectManagement = _residentScript.GetComponent<SoundEffectManagement_KH>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
        UpdateTime();
        UpdateCoolTime();

        if(_attackLockTime > 0)
        {
            _attackLockTime -= Time.deltaTime;
        }
        else
        {
            AttackInputManager();
            if (_isAttackInput)
            {
                _attackLockTime = _attackLockDuration;
                DoAttack();
            }
        }

    }

    private void AttackInputManager()
    {
        if ((Input.GetMouseButtonDown(1) || Input.GetButtonDown("Submit")) && !_isAttackInput)
        {
            _isAttackInput = true;
        }
    }

    private void DoAttack()
    {
        //���͂����Z�b�g
        _isAttackInput = false;

        if (!_canUseNormalAttack) return;

        

        if (_attackCount > 0)
        {
            _attackCount++;
        }
        else
        {
            //�ŏ��̓X�N���v�g����U�����Ăяo��
            _characterAnim.NowAnim = "Attack";
            _attackCount--;
        }

    }

    /// <summary>
    /// �A���U�������邩(�U���A�j���[�V�����̍Ō�
    /// </summary>
    private void ComboOrCoolTime()
    {
        if(_attackCount > 0)
        {
            //�A���U���̓��͂������
           _characterAnim.NowAnim = "Attack";
            _attackCount--;
        }
        else
        {
            //�A���U���̓��͂��Ȃ����
            StartCoolTime();
        }

    }

    /// <summary>
    /// �N�[���^�C�����X�^�[�g����(�U���A�j���[�V�����̍Ō�ɌĂяo��)
    /// </summary>
    private void StartCoolTime()
    {
        _coolTimeUI.StartCoolTime();
        _attackCount = 0;//�U���̓��͉񐔂����Z�b�g
        _canUseNormalAttack = false;
    }

    /// <summary>
    /// �U���͈͂�Cube���A�j���[�V�������琶��
    /// </summary>
    public void NormalAttack()
    {
        _attackArea.SetActive(true);
        _isAttack = true;

        _soundEffectManagement.PlayStrongPunchSound(_audioSource);
    }

    /// <summary>
    /// �ʏ�U���͈̔͂�����
    /// �i�ʏ�̓A�j���[�V��������A�A�j���[�V��������Ă΂�Ȃ�������X�N���v�g����Ăԁj
    /// </summary>
    public void FinishNormalAttack()
    {
        _attackArea.SetActive(false);
        _elapsedTime = 0f;
        _isAttack = false;
    }

    /// <summary>
    /// ��������������擾
    /// </summary>
    public void HitDecision(GameObject hitObj)
    {
        if (!_isAttack) return;

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

        int damage = myAttackPower / _attackRate - targetDefensePower / _defenseRate;

        if (myAttackPower <= targetDefensePower)
        {
            // �h��͂̂ق��������ꍇ�̓_���[�W��1�Ƃ���
            int smallestDamage = 1;
            damage = smallestDamage;
        }

        int hitPointAfterDamage = targetHitPoint - damage;

        _createDamageImage.InstantiateDamageImage(targetStatus.gameObject, damage);
        _writeHitPoint.UpdateHitPoint(targetStatus, hitPointAfterDamage);      // targetStatus��HP���X�V
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
            FinishNormalAttack();
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