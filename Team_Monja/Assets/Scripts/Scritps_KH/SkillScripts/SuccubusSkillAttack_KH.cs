using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccubusSkillAttack_KH : MonoBehaviour, IDamagable_KH
{
    [SerializeField]
    private GameObject _residentScript;

    private float _deleteTime = 2f;
    private float _elapsedTime = 0f;
    private float _skillResetTime = 5f;

    // ����̃X�e�[�^�X�����{�ɂ��邩�i�P�����ɐݒ肵�Ăˁj
    private const float _statDecreaseRate = 0.9f;

    private GameObject _attackArea;

    private WriteHitPoint_KH _writeHitPoint = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private AudioSource _audioSource = default;
    private CreateDamageImage_KH _createDamageImage = default;
    private PlayerSkill_KH _playerSkill = default;
    private ChangeEnemyMoveType_KH _changeEnemyMoveType = default;

    // �X�L���Ō��������Ă���X�e�[�^�X
    private List<GameObject> _reducedStatus;
    // �X�e�[�^�X���X�g�̃��X�g�i�������Z�b�g�܂ł̊Ԃɕ�����X�L�����g���\�������邽�߁j
    private List<List<GameObject>> _statEachSkillTimes;

    private bool _isAttack = false;
    private bool _isMoveCoroutine = false;
    private bool _isSkipReset = true;     // ���Z�b�g���΂��Ă���Ƃ���true

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
        _reducedStatus = new List<GameObject>();
        _statEachSkillTimes = new List<List<GameObject>>();
    }

    void Update()
    {
        UpdateTime();

        // �X�L�b�v�������Z�b�g�������s��
        if(_isSkipReset && !_isMoveCoroutine)
        {
            // �X�e�[�^�X���Z�b�g�̗\��
            StartCoroutine(CountSecondsWaitReset());

            _isSkipReset = false;
        }
    }

    public void SpecialAttack()
    {  //���{
        _characterAnim.NowAnim = "Skill";

        _changeEnemyMoveType.IsMove = false;
        _soundEffectManagement.PlaySlimeSound(_audioSource);

        if (!_isMoveCoroutine)
        {
            // �X�e�[�^�X���Z�b�g�̗\��
            StartCoroutine(CountSecondsWaitReset());
        }
        else
        {
            // ���łɃR���[�`���������Ă���̂ň�U�X�L�b�v
            _isSkipReset = true;
        }
    }


    private void CreateAttackArea()
    {
        _isAttack = true;
        _attackArea.SetActive(true);
    }

    private void SuccubusDeleteAttackArea()
    {
        _attackArea.SetActive(false);
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
        _reducedStatus.Add(targetStatusManager.gameObject);

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
        if (_statEachSkillTimes.Count == 0) return;

        List<GameObject> list = new List<GameObject>(_statEachSkillTimes[0]);

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == null) continue;

            StatusManager_MT status = list[i].GetComponent<StatusManager_MT>();

            float strength = status.Strength / _statDecreaseRate;
            status.Strength = Mathf.CeilToInt(strength);

            float defence = status.Defense / _statDecreaseRate;
            status.Defense = Mathf.CeilToInt(defence);
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
            List<GameObject> list = new List<GameObject>(_reducedStatus);
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

    // ���炵���X�e�[�^�X�𐔕b��Ƀ��Z�b�g����
    private IEnumerator CountSecondsWaitReset()
    {
        _isMoveCoroutine = true;

        yield return new WaitForSeconds(_skillResetTime);

        _isMoveCoroutine = false;

        List<List<GameObject>> list = _statEachSkillTimes;

        for (int i = 0; i < list.Count; i++)
        {
            ResetStatus();
        }
    }

    private void OnDisable()
    {
        ResetStatus();
    }
}