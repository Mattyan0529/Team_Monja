using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerEnterWallJudge : MonoBehaviour
{
    private GameObject _player = default;

    private PlayerMove_MT _playerMove = default;
    private Rigidbody _playerRigidbody = default;

    private void Start()
    {
        _playerMove = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerMove_MT>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            RestorePositionToBackSide();
        }
    }

    /// <summary>
    /// �v���C���[�̈ʒu�����ɖ߂�
    /// </summary>
    private void RestorePositionToBackSide()
    {
        _playerRigidbody.MovePosition(_playerMove.PositionBeforeMove);
    }

    /// <summary>
    /// �v���C���[��ݒ�
    /// </summary>
    /// <param name="player"></param>
    public void SetPlayer(GameObject player)
    {
        _player = player;
        _playerRigidbody = player.GetComponent<Rigidbody>();
    }
}
