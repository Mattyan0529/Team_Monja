using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomSkillAttack_KH : MonoBehaviour, IDamagable_KH
{
    [SerializeField]
    private GameObject _residentScript;


    private float _deleteTime = 0.5f;
    private float _elapsedTime = 0f;

    private int _attackRate = 2;
    private int _defenseRate = 4;

    private GameObject _attackArea;

    private WriteHitPoint_KH _writeHitPoint = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private AudioSource _audioSource = default;
    private ChangeEnemyMoveType_KH _changeEnemyMoveType = default;
    private CreateDamageImage_KH _createDamageImage = default;
    private PlayerSkill_KH _playerSkill = default;

    private bool _isAttack = false;

    //���{
    private CharacterAnim_MT _characterAnim = default;

    private void Awake()
    {
        _changeEnemyMoveType = GetComponent<ChangeEnemyMoveType_KH>();
    }

    void Start()
    {
        _writeHitPoint = _residentScript.GetComponent<WriteHitPoint_KH>();
        _soundEffectManagement = _residentScript.GetComponent<SoundEffectManagement_KH>();
        _createDamageImage = _residentScript.GetComponent<CreateDamageImage_KH>();
        _characterAnim = GetComponent<CharacterAnim_MT>();
        _audioSource = GetComponent<AudioSource>();
        _playerSkill = GetComponent<PlayerSkill_KH>();

        // �q�I�u�W�F�N�g�̒�����AttackArea���擾
        _attackArea = transform.Find("AttackArea").gameObject;
        _attackArea.SetActive(false);
    }

    void Update()
    {
        UpdateTime();
    }

    public void SpecialAttack()
    {  //���{
        _characterAnim.NowAnim = "Skill";

        _changeEnemyMoveType.IsMove = false;
    }

    /// <summary>
    /// �A�j���[�V��������Ăяo���U���͈͐���
    /// </summary>
    private void CreateAttackArea()
    {
        _isAttack = true;
        _attackArea.SetActive(true);

        _soundEffectManagement.PlayWindSound(_audioSource);
    }

    /// <summary>
    /// �A�j���[�V��������Ăяo�����񂾂Ƃ��ɍU���͈͂��������\�b�h
    /// </summary>
    private void MushroomDeleteAttackArea()
    {
        _attackArea.SetActive(false);
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
            _characterAnim.NowAnim = "Idle";
            _attackArea.SetActive(false);
            _elapsedTime = 0f;
            _changeEnemyMoveType.IsMove = true;
            _isAttack = false;
            _playerSkill.IsUseSkill = false;
        }
    }
}

