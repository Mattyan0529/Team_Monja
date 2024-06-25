using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeadDecision_MT : MonoBehaviour
{
    StatusManager_MT _statusManager;
    private CharacterAnim_MT _characterAnim;

    // �ǋL�F�k
    MonsterRandomWalk_KH _monsterRandomWalk = default;
    MonsterSkill_KH _monsterSkill = default;
    PlayerRangeInJudge_KH _playerRangeInJudge = default;
    PlayerMove_MT _playerMove = default;
    private bool _isAlive = true;

    void Start()
    {
        _statusManager = GetComponent<StatusManager_MT>();
        _characterAnim = GetComponent<CharacterAnim_MT>();
        // �ǋL�F�k
        _monsterRandomWalk = GetComponent<MonsterRandomWalk_KH>();
        _monsterSkill = GetComponent<MonsterSkill_KH>();
        _playerRangeInJudge = GetComponent<PlayerRangeInJudge_KH>();
        _playerMove = GetComponent<PlayerMove_MT>();
    }

    void Update()
    {
        if (IsDeadDecision())
        {
            Debug.Log("����������");
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
    /// �H�ׂ�ꂽ�����X�^�[�ƃv���C���[�̓������~�߂�@�ǋL�F�k
    /// </summary>
    private void EnemyStop()
    {
        _playerRangeInJudge.enabled = false;
        _monsterRandomWalk.enabled = false;
        _monsterSkill.enabled = false;
        _playerMove.enabled = false;

        _isAlive = false; 
    }
}
