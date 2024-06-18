using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeadDecision_MT : MonoBehaviour
{
    StatusManager_MT statusManager;

    // 追記：北
    MonsterRandomWalk_KH _monsterRandomWalk = default;
    MonsterSkill_KH _monsterSkill = default;
    PlayerRangeInJudge_KH _playerRangeInJudge = default;
    private bool _isFirst = true;


    void Start()
    {
        statusManager = GetComponent<StatusManager_MT>();

        // 追記：北
        _monsterRandomWalk = GetComponent<MonsterRandomWalk_KH>();
        _monsterSkill = GetComponent<MonsterSkill_KH>();
        _playerRangeInJudge = GetComponent<PlayerRangeInJudge_KH>();
    }

    void Update()
    {
        if(IsDeadDecision())
        {
            I_Can_Eat();

            if (_isFirst)
            {
                EnemyStop();
            }
        }
    }

    public bool IsDeadDecision()
    {
        if(statusManager.HP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void I_Can_Eat()
    {

    }

    /// <summary>
    /// 食べられたモンスターの動きを止める　追記：北
    /// </summary>
    private void EnemyStop()
    {
        Debug.Log("Stop");

        _playerRangeInJudge.enabled = false;
        _monsterRandomWalk.enabled = false;
        _monsterSkill.enabled = false;

        _isFirst = false;
    }
}
