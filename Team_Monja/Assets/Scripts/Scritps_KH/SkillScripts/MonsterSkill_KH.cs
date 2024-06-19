using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MonsterSkill_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _residentScript;
    [SerializeField]
    private GameObject _followArea;

    private float _updateTime = 0f;    // ���b�����ɃX�L�����Ăяo����
    private float _elapsedTime = default;

    private float _maxTimeSpacing = 4f;
    private float _minTimeSpacing = 2f;

    private PlayerSkill_KH _playerSkill = default;
    private PlayerManager_KH _playerManager = default;
    private MonsterRandomWalk_KH _monsterRandomWalk = default;

    private HighSpeedAssault_KH _highSpeedAssault = default;
    private WeaponAttack_KH _weaponAttack = default;
    private LongDistanceAttack_KH _longDistanceAttack = default;
    private FlySkill_KH _flySkill = default;
    private Petrification_KH _petrification = default;
    private BossSkill_KH _bossSkill = default;

    private NormalAttack_KH _normalAttack = default;
    private PlayerGuard_KH _playerGuard = default;

    private int _skillNum = 0;

    /// <summary>
    /// �X�L���̑�܂��ȃ^�C�v
    /// ���̃^�C�v���ƂɃX�N���v�g�ɂ܂Ƃ߂Ă��
    /// </summary>
    public enum SkillType
    {
        HighSpeedAssault,
        WeaponAttack,
        LongDistanceAttack,
        Fly,
        Petrification,
        Boss
    }

    void Awake()
    {
        _playerSkill = GetComponent<PlayerSkill_KH>();
        _monsterRandomWalk = GetComponent<MonsterRandomWalk_KH>();
        _playerManager = _residentScript.GetComponent<PlayerManager_KH>();

        if (GetComponent<NormalAttack_KH>())
        {
            _normalAttack = GetComponent<NormalAttack_KH>();
        }
        else if (GetComponent<PlayerGuard_KH>())
        {
            _playerGuard = GetComponent<PlayerGuard_KH>();
        }

        SkillJudge();
    }

    void Start()
    {
        GameobjectTagJudge();
        _updateTime = Random.Range(_minTimeSpacing, _maxTimeSpacing);
    }

    void Update()
    {
        GameobjectTagJudge();
        UpdateTime();

        // ���ڂ肪����������^�O��ύX�iM�������ꂽ�����ڂ肪�������Ȃ������Ƃ��������������Ă��܂��j
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameobjectTagJudge();
        }
    }

    /// <summary>
    /// �����X�^�[����v���C���[�ɕς������X�N���v�g�؂�ւ�
    /// </summary>
    private void GameobjectTagJudge()
    {
        if (gameObject.CompareTag("Player"))
        {
            _playerManager.Player = gameObject;     // �v���C���[�X�V
            _followArea.transform.SetParent(gameObject.transform);
            _playerSkill.enabled = true;

            if (GetComponent<NormalAttack_KH>())
            {
                _normalAttack.enabled = true;
            }
            else if (GetComponent<PlayerGuard_KH>())
            {
                _playerGuard.enabled = true;
            }

            _monsterRandomWalk.enabled = false;
            this.enabled = false;
        }
    }

    /// <summary>
    /// ���̃L�����N�^�[�������Ă���̂͂ǂ̃X�L��������
    /// </summary>
    private void SkillJudge()
    {
        if (GetComponent<BossSkill_KH>())      // �{�X
        {
            _skillNum = (int)SkillType.Boss;
            _bossSkill = GetComponent<BossSkill_KH>();
        }
        else if (GetComponent<HighSpeedAssault_KH>())       // �X�L���F�����ˌ�
        {
            _skillNum = (int)SkillType.HighSpeedAssault;
            _highSpeedAssault = GetComponent<HighSpeedAssault_KH>();
        }
        else if (GetComponent<WeaponAttack_KH>())      // �X�L���F������g�����U��
        {
            _skillNum = (int)SkillType.WeaponAttack;
            _weaponAttack = GetComponent<WeaponAttack_KH>();
        }
        else if (GetComponent<LongDistanceAttack_KH>())       // �X�L���F�������U��
        {
            _skillNum = (int)SkillType.LongDistanceAttack;
            _longDistanceAttack = GetComponent<LongDistanceAttack_KH>();
        }
        else if (GetComponent<FlySkill_KH>())       // �X�L���F���
        {
            _skillNum = (int)SkillType.Fly;
            _flySkill = GetComponent<FlySkill_KH>();
        }
        else if (GetComponent<Petrification_KH>())       // �X�L���F�Ή�
        {
            _skillNum = (int)SkillType.Petrification;
            _petrification = GetComponent<Petrification_KH>();
        }
    }

    /// <summary>
    /// _skillNum���Q�b�g
    /// </summary>
    public int SkillTypeNum
    {
        get { return _skillNum; }
    }

    /// <summary>
    /// ����I�ɃX�L���𔭓�
    /// </summary>
    private void UpdateTime()
    {
        // ���ԉ��Z
        _elapsedTime += Time.deltaTime;

        // �K�莞�ԂɒB���Ă����ꍇ
        if (_elapsedTime > _updateTime)
        {
            RandomCallSkill();      // �X�L������
            _elapsedTime = 0f;
        }
    }

    /// <summary>
    /// �����X�^�[�����b�����ɃX�L�����g��
    /// </summary>
    private void RandomCallSkill()
    {
        switch (SkillTypeNum)
        {
            case (int)SkillType.HighSpeedAssault:      // �����ˌ��̏ꍇ
                _highSpeedAssault.SpeedUp();
                return;

            case (int)SkillType.WeaponAttack:          // ������g�����U���Ȃǂ̏ꍇ
                _weaponAttack.Attack();
                return;

            case (int)SkillType.LongDistanceAttack:        // �������U���̏ꍇ
                _longDistanceAttack.GenerateBullet();
                return;

            case (int)SkillType.Fly:                   // ��ԃX�L���̏ꍇ
                _flySkill.MonsterStartFly();
                return;

            case (int)SkillType.Petrification:         // �Ή��̏ꍇ
                _petrification.CreatePetrificationArea();
                return;
            case (int)SkillType.Boss:       // �{�X�̃X�L���̏ꍇ
                _bossSkill.RandomSkillCall();
                break;
        }

    }
}
