using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamonHandPos : MonoBehaviour
{
    private GameObject _player;

    private CapsuleCollider _playerCollider;

    private float _offset = -2f;
  
    // Update is called once per frame
    void Update()
    {
        PositionCalculation();
    }

    public void SetPlayer(GameObject player)
    {
        _player = player;

        _playerCollider = _player.GetComponent<CapsuleCollider>();
    }

    private void PositionCalculation()
    {
        float yPos = default;
        float colliderHeight = _playerCollider.height;
        Vector3 playerOffset = _playerCollider.center;

        yPos = playerOffset.y + colliderHeight + _player.transform.position.y + _offset;

        this.transform.position = new Vector3(_player.transform.position.x,yPos,_player.transform.position.z -1.75f);
    }

}
