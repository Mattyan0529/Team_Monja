using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterWallJudge : MonoBehaviour
{
    private GameObject _player = default;

    private PlayerMove_MT _playerMove = default;
    private Rigidbody _playerRigidbody = default;

    private bool _isCollisionPlayer = false;

    private void Start()
    {
        _playerMove = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerMove_MT>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isCollisionPlayer = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!_isCollisionPlayer) return;

        _playerRigidbody.velocity = Vector3.zero;
        RestorePositionToBackSide();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isCollisionPlayer = false;
        }
    }

    /// <summary>
    /// プレイヤーの位置を後ろに戻す
    /// </summary>
    private void RestorePositionToBackSide()
    {
        _playerRigidbody.MovePosition(_playerMove.PositionBeforeMove);
    }

    /// <summary>
    /// プレイヤーを設定
    /// </summary>
    /// <param name="player"></param>
    public void SetPlayer(GameObject player)
    {
        _player = player;
        _playerRigidbody = player.GetComponent<Rigidbody>();
    }
}
