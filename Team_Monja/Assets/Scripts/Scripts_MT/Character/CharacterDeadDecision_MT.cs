using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeadDecision_MT : MonoBehaviour
{
    StatusManager_MT _statusManager;
    private CharacterAnim_MT _characterAnim;

    // 追記：北
    MonsterRandomWalk_KH _monsterRandomWalk = default;
    MonsterSkill_KH _monsterSkill = default;
    PlayerRangeInJudge_KH _playerRangeInJudge = default;
    private bool _isAlive = true;

    void Start()
    {
        _statusManager = GetComponent<StatusManager_MT>();
        _characterAnim = GetComponent<CharacterAnim_MT>();
        // 追記：北
        _monsterRandomWalk = GetComponent<MonsterRandomWalk_KH>();
        _monsterSkill = GetComponent<MonsterSkill_KH>();
        _playerRangeInJudge = GetComponent<PlayerRangeInJudge_KH>();
    }

    void Update()
    {
        if (IsDeadDecision())
        {
            Debug.Log("しんぢゃった");
            _characterAnim.NowAnim = "Die";

            if (_isAlive)
            {
                EnemyStop();
            }
        }
    }

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
    /// 食べられたモンスターの動きを止める　追記：北
    /// </summary>
    private void EnemyStop()
    {

        _playerRangeInJudge.enabled = false;
        _monsterRandomWalk.enabled = false;
        _monsterSkill.enabled = false;

        _isAlive = false; 
    }
}
