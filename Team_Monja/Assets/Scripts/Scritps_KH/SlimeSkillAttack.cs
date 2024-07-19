using JetBrains.Annotations;
using UnityEngine;

public class SlimeSkillAttack : MonoBehaviour, IDamagable
{
    [SerializeField]
    private GameObject _residentScript;

    [SerializeField]
    private EffectManager _effectManager; // EffectManager�̎Q�Ƃ�ǉ�


    private float _deleteTime = 0.5f;
    private float _elapsedTime = 0f;

    private GameObject _attackArea;

    private WriteHitPoint_KH _writeHitPoint = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private AudioSource _audioSource = default;
    private PlayerMove_MT _playerMove = default;
    private CreateDamageImage_KH _createDamageImage = default;
    private PlayerSkill_KH _playerSkill = default;
    private ChangeEnemyMoveType _changeEnemyMoveType = default;

    private bool _isAttack = false;

    //���{
    private CharacterAnim_MT _characterAnim = default;

    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove_MT>();
    }

    void Start()
    {
        _writeHitPoint = _residentScript.GetComponent<WriteHitPoint_KH>();
        _soundEffectManagement = _residentScript.GetComponent<SoundEffectManagement_KH>();
        _createDamageImage = _residentScript.GetComponent<CreateDamageImage_KH>();
        _characterAnim = GetComponent<CharacterAnim_MT>();
        _audioSource = GetComponent<AudioSource>();
        _playerSkill = GetComponent<PlayerSkill_KH>();
        _changeEnemyMoveType = GetComponent<ChangeEnemyMoveType>();

        // �q�I�u�W�F�N�g�̒�����AttackArea���擾
        _attackArea = transform.Find("AttackArea").gameObject;
        _attackArea.SetActive(false);
    }

    void Update()
    {
        UpdateTime();
    }

    public void SpecialAttack()
    {
        // �G�̃����_���ړ����i�v���C���[���U���͈͊O�j�͏������Ȃ�
        if (gameObject.CompareTag("Enemy") || gameObject.CompareTag("Boss") &&
            _changeEnemyMoveType.NowState == ChangeEnemyMoveType.EnemyMoveState.InRandomMove) return;

        //���{
        _characterAnim.NowAnim = "Skill";

        // �������~�߂�
        if (gameObject.CompareTag("Enemy") || gameObject.CompareTag("Boss"))
        {
            _changeEnemyMoveType.NowState = ChangeEnemyMoveType.EnemyMoveState.InAttack;
        }

        _soundEffectManagement.PlayStrongPunchSound(_audioSource);
    }

    private void CreateAttackArea()
    {
        _isAttack = true;
        _attackArea.SetActive(true);
        //�X�L���G�t�F�N�g
        if (_effectManager != null)
        {
            _effectManager.ShowSpecialAttackEffect(transform);
        }
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

        int damage = targetHitPoint - (myAttackPower - targetDefensePower);

        if (myAttackPower < targetDefensePower)
        {
            // �h��͂̂ق��������ꍇ�̓_���[�W��1�Ƃ���
            int smallestDamage = 1;
            damage = smallestDamage;
        }

        _createDamageImage.InstantiateDamageImage(gameObject, targetStatus.gameObject, myAttackPower - targetDefensePower);
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
            // �������ĊJ����
            if (gameObject.CompareTag("Enemy") || gameObject.CompareTag("Boss"))
            {
                _changeEnemyMoveType.NowState = ChangeEnemyMoveType.EnemyMoveState.InFollow;
            }

            _attackArea.SetActive(false);
            _elapsedTime = 0f;
            _isAttack = false;
            _playerSkill.IsUseSkill = false;
        }
    }
}
