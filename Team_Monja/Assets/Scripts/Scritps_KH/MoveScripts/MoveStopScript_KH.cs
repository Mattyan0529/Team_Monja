using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStopScript_KH : MonoBehaviour
{
    private PlayerMove_MT _playerMove = default;
    private EnemyMove_KH _enemyMove = default;

    private void Awake()
    {
        _playerMove = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerMove_MT>();
        _enemyMove = GetComponent<EnemyMove_KH>();
    }

    public void PlayerMoveStop()
    {
        if (!gameObject.CompareTag("Player")) return;
        _playerMove.enabled = false;
    }

    public void PlayerMoveStart()
    {
        if (!gameObject.CompareTag("Player")) return;
        _playerMove.enabled = true;
    }

    public void EnemyMoveStop()
    {
        if (!gameObject.CompareTag("Enemy") && !gameObject.CompareTag("Boss")) return;
        _enemyMove.enabled = false;
    }

    public void EnemyMoveStart()
    {
        if (!gameObject.CompareTag("Enemy") && !gameObject.CompareTag("Boss")) return;
        _enemyMove.enabled = true;
    }
}
