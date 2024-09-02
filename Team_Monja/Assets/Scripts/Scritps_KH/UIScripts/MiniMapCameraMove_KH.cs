using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraMove_KH : MonoBehaviour
{

    private GameObject _player;

    void Start()
    {
    }

    void Update()
    {
        CameraMove();
    }

   /// <summary>
   /// �v���C���[��ݒ�
   /// </summary>
   /// <param name="player"></param>
    public void SetPlayer(GameObject player)
    {
        _player = player;
    }

    /// <summary>
    /// �J�������v���C���[��Ǐ]
    /// </summary>
    private void CameraMove()
    {

        gameObject.transform.position = new Vector3
            (_player.transform.position.x, transform.position.y, _player.transform.position.z);
    }
}
