using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraMove_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _residentScript = default;

    private PlayerManager_KH _playerManager = default;

    void Start()
    {
        _playerManager = _residentScript.GetComponent<PlayerManager_KH>();
    }

    void Update()
    {
        CameraMove();
    }

    /// <summary>
    /// カメラがプレイヤーを追従
    /// </summary>
    private void CameraMove()
    {
        GameObject player = _playerManager.Player;

        gameObject.transform.position = new Vector3
            (player.transform.position.x, transform.position.y, player.transform.position.z);
    }
}
