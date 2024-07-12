using UnityEngine;

public class NormalAttack_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _residentScript;

    [SerializeField]
    private GameObject _coolTimeUIObj = default;

    [SerializeField]
    private EffectManager _effectManager; // EffectManager�̎Q�Ƃ�ǉ�

    private int _attackCount = 0; //�U���񐔂��J�E���g

    private float _deleteTime = 0.2f;
    private float _elapsedTime = 0f;

    private float _coolTime = 0.5f; // �ʏ�U���𔭓����Ă��玟�ɔ����ł���悤�ɂȂ�܂ł̎���
    private float _coolTimeElapsedTime = 0f;

    [SerializeField]
    private GameObject _attackArea;
    private bool _isAttack = false;
    private bool _canUseNormalAttack = true;

    private WriteHitPoint_KH _writeHitPoint = default;
    private CoolTimeUI _coolTimeUI = default;
    private CreateDamageImage_KH _createDamageImage = default;
    //���{
    private CharacterAnim_MT _characterAnim = default;
    private Animator _animator;

    public bool IsAttack
    {
        get { return _isAttack; }
    }

    void Start()
    {
        _writeHitPoint = _residentScript.GetComponent<WriteHitPoint_KH>();
        _characterAnim = GetComponent<CharacterAnim_MT>();
        _coolTimeUI = _coolTimeUIObj.GetComponent<CoolTimeUI>();
        _createDamageImage = _residentScript.GetComponent<CreateDamageImage_KH>();
        _animator = GetComponent<Animator>(); // Animator�R���|�[�l���g���擾
        if (_animator == null)
        {
            Debug.LogError("Animator component is not attached to the GameObject.");
        }
    }

    void Update()
    {
        UpdateCoolTime();
        AttackInputManager();
    }

    /// <summary>
    /// �N�[���^�C����ʏ�U�����g����悤�ɂ���
    /// </summary>
    private void UpdateCoolTime()
    {
        if (_canUseNormalAttack) return; // �U�����ȊO�͏������s��Ȃ�

        // ���ԉ��Z
        _coolTimeElapsedTime += Time.deltaTime;

        // �K�莞�ԂɒB���Ă����ꍇ
        if (_coolTimeElapsedTime > _coolTime)
        {
            _coolTimeElapsedTime = 0f;
            _canUseNormalAttack = true;
        }
    }

    private void AttackInputManager()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetButtonDown("attack"))
        {
            if (!_canUseNormalAttack) return;
            if (_attackCount >= 2) return;
            //������
            ResetAttack();

            if (!_isAttack)
            {
                //�U���̓A�j���[�V��������Ăяo��
                _characterAnim.NowAnim = "Attack";
            }
            else //�A���U������ꍇ
            {
                // �A�j���[�V�������~���čŏ��ɖ߂�
                _animator.Play("Attack", -1, 0f);
            }

            //�U���񐔂̃J�E���g�𑝂₷
            _attackCount++;
        }
    }

    /// <summary>
    /// �U���͈͂�Cube���A�j���[�V�������琶��
    /// </summary>
    public void NormalAttack()
    {
        _attackArea.SetActive(true);
        _isAttack = true;

        // �ʏ�U���G�t�F�N�g��\��
        _effectManager.ShowNormalAttackEffect(transform);
    }

    //���{
    /// <summary>
    /// �U����Ԃ����Z�b�g
    /// </summary>
    private void ResetAttack()
    {
        _attackArea.SetActive(false);
    }

    /// <summary>
    /// �A���U���̏I���A�A�j���[�V��������Ăяo��
    /// </summary>
    public void EndAttack()
    {
        _attackArea.SetActive(false);
        _isAttack = false;
        _canUseNormalAttack = false;
        _attackCount = 0;
        //�N�[���^�C���J�n
        _coolTimeUI.StartCoolTime();
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
        int myAttackPower = myStatus.Strength; // �����̍U���͂�get���Ă���
        int targetDefensePower = targetStatus.Defense; // ����̖h��͂�get���Ă���
        int targetHitPoint = targetStatus.HP; // �����HP��get���Ă���

        if (myAttackPower < targetDefensePower) return; // �h��͂̂ق�������������0�_���[�W

        int damage = targetHitPoint - (myAttackPower - targetDefensePower);
        _createDamageImage.InstantiateDamageImage(gameObject, targetStatus.gameObject, myAttackPower - targetDefensePower);
        _writeHitPoint.UpdateHitPoint(targetStatus, damage); // targetStatus��HP���X�V
    }
}
