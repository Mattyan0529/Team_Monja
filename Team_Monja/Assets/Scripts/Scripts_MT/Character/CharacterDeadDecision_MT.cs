using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeadDecision_MT : MonoBehaviour
{
    private StatusManager_MT _statusManager;
    private CharacterAnim_MT _characterAnim;
    private GameEndCamera_MT _gameEndCamera;

    // 追記：北
    private MonsterRandomWalk_KH _monsterRandomWalk = default;
    private MonsterSkill_KH _monsterSkill = default;
    private PlayerRangeInJudge_KH _playerRangeInJudge = default;
    private PlayerMove_MT _playerMove = default;
    private PlayerSkill_KH _playerSkill = default;
    private NormalAttack_KH _normalAttack = default;
    private CameraManager_MT _cameraManager = default;

    private bool _isAlive = true;
    private bool _coroutineSwitch = true;


    void Start()
    {
        _statusManager = GetComponent<StatusManager_MT>();
        _characterAnim = GetComponent<CharacterAnim_MT>();
        _gameEndCamera = GetComponent<GameEndCamera_MT>();
        // 追記：北
        _monsterRandomWalk = GetComponent<MonsterRandomWalk_KH>();
        _monsterSkill = GetComponent<MonsterSkill_KH>();
        _playerRangeInJudge = GetComponent<PlayerRangeInJudge_KH>();
        _playerMove = GetComponent<PlayerMove_MT>();
        _playerSkill = GetComponent<PlayerSkill_KH>();
        _normalAttack = GetComponent<NormalAttack_KH>();
    }

    void Update()
    {
        if (IsDeadDecision())
        {
            //プレイヤーなら死んだときにスローモーションにする
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
    /// 死んでいるか
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
    /// 食べられたモンスターとプレイヤーの動きを止める　追記：北
    /// </summary>
    private void EnemyStop()
    {
        _playerRangeInJudge.enabled = false;
        _monsterRandomWalk.enabled = false;
        _monsterSkill.enabled = false;
        _playerMove.enabled = false;
        _playerSkill.enabled = false;
        _normalAttack.enabled = false;

        _characterAnim.NowAnim = "Die";

        _isAlive = false; 
    }

   
}
