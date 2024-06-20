using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeadDecision_MT : MonoBehaviour
{
    StatusManager_MT statusManager;

    // �ǋL�F�k
    MonsterRandomWalk_KH _monsterRandomWalk = default;
    MonsterSkill_KH _monsterSkill = default;
    PlayerRangeInJudge_KH _playerRangeInJudge = default;
    private bool _isAlive = true;

    void Start()
    {
        statusManager = GetComponent<StatusManager_MT>();

        // �ǋL�F�k
        _monsterRandomWalk = GetComponent<MonsterRandomWalk_KH>();
        _monsterSkill = GetComponent<MonsterSkill_KH>();
        _playerRangeInJudge = GetComponent<PlayerRangeInJudge_KH>();
    }

    void Update()
    {
        if (IsDeadDecision())
        {
            Debug.Log("����������");
            if (_isAlive)
            {
                EnemyStop();
            }
        }
    }

    public bool IsDeadDecision()
    {
        if (statusManager.HP <= 0)
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
    /// �H�ׂ�ꂽ�����X�^�[�̓������~�߂�@�ǋL�F�k
    /// </summary>
    private void EnemyStop()
    {
        Debug.Log("Stop");

        _playerRangeInJudge.enabled = false;
        _monsterRandomWalk.enabled = false;
        _monsterSkill.enabled = false;

        _isAlive = false; 
    }
}
