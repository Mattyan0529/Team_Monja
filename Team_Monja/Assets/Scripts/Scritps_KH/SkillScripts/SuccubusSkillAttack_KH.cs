using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccubusSkillAttack_KH : MonoBehaviour, IDamagable_KH
{
    [SerializeField]
    private GameObject _residentScript;

    [SerializeField]
    private EffectManager _effectManager; // EffectManager�̎Q�Ƃ�ǉ�


    private float _deleteTime = 0.5f;
    private float _elapsedTime = 0f;
    private float _skillResetTime = 5f;

    // ����̃X�e�[�^�X�����{�ɂ��邩�i�P�����ɐݒ肵�Ăˁj
    private const float _statDecreaseRate = 0.7f;

    private GameObject _attackArea;

    private WriteHitPoint_KH _writeHitPoint = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private AudioSource _audioSource = default;
    private CreateDamageImage_KH _createDamageImage = default;
    private PlayerSkill_KH _playerSkill = default;
    private ChangeEnemyMoveType_KH _changeEnemyMoveType = default;

    // �X�L���Ō��������Ă���X�e�[�^�X
    private List<StatusManager_MT> _reducedStatus;
    // �X�e�[�^�X���X�g�̃��X�g�i�������Z�b�g�܂ł̊Ԃɕ�����X�L�����g���\�������邽�߁j
    private List<List<StatusManager_MT>> _statEachSkillTimes;

    private bool _isAttack = false;

    //���{
    private CharacterAnim_MT _characterAnim = default;

    void Start()
    {
        _writeHitPoint = _residentScript.GetComponent<WriteHitPoint_KH>();
        _soundEffectManagement = _residentScript.GetComponent<SoundEffectManagement_KH>();
        _createDamageImage = _residentScript.GetComponent<CreateDamageImage_KH>();
        _characterAnim = GetComponent<CharacterAnim_MT>();
        _audioSource = GetComponent<AudioSource>();
        _playerSkill = GetComponent<PlayerSkill_KH>();
        _changeEnemyMoveType = GetComponent<ChangeEnemyMoveType_KH>();

        // �q�I�u�W�F�N�g�̒�����AttackArea���擾
        _attackArea = transform.Find("AttackArea").gameObject;
        _attackArea.SetActive(false);

        // ���X�g��������
        _reducedStatus = new List<StatusManager_MT>();
        _statEachSkillTimes = new List<List<StatusManager_MT>>();
    }

    void Update()
    {
        UpdateTime();
    }

    public void SpecialAttack()
    {  //���{
        _characterAnim.NowAnim = "Skill";

        _changeEnemyMoveType.IsMove = false;
        _soundEffectManagement.PlaySlimeSound(_audioSource);
        Invoke("ResetStatus", _skillResetTime);
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


    public void HitDecision(GameObject hitObj)
    {
        // ����Ǝ�����StatusManager�������K�v
        StatusManager_MT targetStatusManager = hitObj.gameObject.GetComponent<StatusManager_MT>();
        StatusManager_MT myStatusManager = GetComponent<StatusManager_MT>();

        float strength = targetStatusManager.Strength * _statDecreaseRate;
        targetStatusManager.Strength = (int)strength;

        float defence = targetStatusManager.Defense * _statDecreaseRate;
        targetStatusManager.Defense = (int)defence;

        // ���������Ă���X�e�[�^�X���X�g�ɒǉ�
        _reducedStatus.Add(targetStatusManager);

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

        int damage = myAttackPower - targetDefensePower;

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

    private void ResetStatus()
    {
        if (_statEachSkillTimes == null)
        {
            return;
        }

        if (_statEachSkillTimes.Count == 0) return;
        List<StatusManager_MT> list = new List<StatusManager_MT>(_statEachSkillTimes[0]);

        for (int i = 0; i < list.Count; i++)
        {
            float strength = list[i].Strength / _statDecreaseRate;
            list[i].Strength = (int)strength;

            float defence = list[i].Defense / _statDecreaseRate;
            list[i].Defense = (int)defence;
        }
        _statEachSkillTimes.Remove(_statEachSkillTimes[0]);
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
            // ���X�g�ւ̒ǉ��������Œ��ߐ؂�Ȃ̂ŁA���X�g�����X�g�֒ǉ�����
            List<StatusManager_MT> list = new List<StatusManager_MT>(_reducedStatus);
            _statEachSkillTimes.Add(list);
            _reducedStatus.Clear();

            _characterAnim.NowAnim = "Idle";
            _attackArea.SetActive(false);
            _elapsedTime = 0f;

            _changeEnemyMoveType.IsMove = true;
            _isAttack = false;
            _playerSkill.IsUseSkill = false;
        }
    }

    private void OnDisable()
    {
        List<List<StatusManager_MT>> list = _statEachSkillTimes;

        for (int i = 0; i < list.Count; i++)
        {
            ResetStatus();
        }
    }
}