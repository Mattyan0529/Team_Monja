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
    private PlayerSkill_KH _playerSkill = default;
    private NormalAttack_KH _normalAttack = default;
    private PlayerGuard_KH _playerGuard = default;
    private KillCount_MT _killCount;

    private bool _isAlive = true;
    private bool _coroutineSwitch = true;
    private bool _oneTimeSwitch = true;



    void Start()
    {
        _statusManager = GetComponent<StatusManager_MT>();
        _characterAnim = GetComponent<CharacterAnim_MT>();
        _gameEndCamera = GameObject.FindWithTag("CameraPos").GetComponent<GameEndCamera_MT>();
        _killCount = GameObject.FindWithTag("PlayerManager").GetComponent<KillCount_MT>();

        // 追記：北
        _monsterRandomWalk = GetComponent<MonsterRandomWalk_KH>();
        _monsterSkill = GetComponent<MonsterSkill_KH>();
        _playerRangeInJudge = GetComponent<PlayerRangeInJudge_KH>();
        _playerSkill = GetComponent<PlayerSkill_KH>();
        _normalAttack = GetComponent<NormalAttack_KH>();
        _playerGuard = GetComponent<PlayerGuard_KH>();

    }

    void Update()
    {
        if (IsDeadDecision())
        {
            //プレイヤーなら死んだときにスローモーションにする
            if(CompareTag("Player") && _coroutineSwitch)
            {
                if(this.gameObject == GameObject.FindWithTag("Player"))
                {
                    StartCoroutine(_gameEndCamera.GameOverCoroutine());
                    _coroutineSwitch = false;
                }

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
            CallOnDeathOneTime();
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

   //死んだときに一度だけ呼ぶ
   private void CallOnDeathOneTime()
    {
        if (_oneTimeSwitch)
        {
            _killCount.KillCountUP(_statusManager._name.ToString());
            _oneTimeSwitch = false;
        }
    }


}
