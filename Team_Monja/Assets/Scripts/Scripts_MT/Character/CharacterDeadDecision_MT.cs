using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeadDecision_MT : MonoBehaviour
{
    private StatusManager_MT _statusManager;
    private CharacterAnim_MT _characterAnim;
    private GameEndCamera_MT _gameEndCamera;

    // �ǋL�F�k
    private MonsterSkill_KH _monsterSkill = default;
    private PlayerSkill_KH _playerSkill = default;
    private NormalAttack_KH _normalAttack = default;
    private PlayerGuard_KH _playerGuard = default;
    private EnemyMove _enemyMove = default;

    private bool _isAlive = true;
    private bool _coroutineSwitch = true;


    void Start()
    {
        _statusManager = GetComponent<StatusManager_MT>();
        _characterAnim = GetComponent<CharacterAnim_MT>();
        _gameEndCamera = GameObject.FindWithTag("CameraPos").GetComponent<GameEndCamera_MT>();
        // �ǋL�F�k
        _monsterSkill = GetComponent<MonsterSkill_KH>();
        _playerSkill = GetComponent<PlayerSkill_KH>();
        _normalAttack = GetComponent<NormalAttack_KH>();
        _playerGuard = GetComponent<PlayerGuard_KH>();
        _enemyMove = GetComponent<EnemyMove>();

    }

    void Update()
    {
        if (IsDeadDecision())
        {
            //�v���C���[�Ȃ玀�񂾂Ƃ��ɃX���[���[�V�����ɂ���
            if(CompareTag("Player") && _coroutineSwitch)
            {
                
                    StartCoroutine(_gameEndCamera.GameOverCoroutine());
                    _coroutineSwitch = false;
            }
            if (_isAlive)
            {
                EnemyStop();
            }
        }
    }

    /// <summary>
    /// ����ł��邩
    /// </summary>
    /// <returns></returns>
    public bool IsDeadDecision()
    {
        if (_statusManager.HP <= 0)
        {
           
            return true;

        }
        else
        {
            _isAlive = true;
            return false;
        }
    }

    /// <summary>
    /// �H�ׂ�ꂽ�����X�^�[�ƃv���C���[�̓������~�߂�@�ǋL�F�k
    /// </summary>
    private void EnemyStop()
    {
        _enemyMove.enabled = false;
        _monsterSkill.enabled = false;
        _playerSkill.enabled = false;
        if(_normalAttack != null)
        {
            _normalAttack.enabled = false;
        }
        if(_playerGuard != null)
        {
            _playerGuard.enabled = false;
        }

        _characterAnim.NowAnim = "Die";

        _isAlive = false; 
    }

   
}
