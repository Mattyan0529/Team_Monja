using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventionWallScript_WS : MonoBehaviour
{
    private float _frontDistance = 1f;
    private float _direction = -1f;
    private LayerMask _layer = 6;
    private Rigidbody _rb;
    private GameObject _positionCamera;
    private PlayerMove_MT _pleayerMove;

    // Start is called before the first frame update
    void Start()
    {
        _positionCamera = GameObject.FindWithTag("MainCamera");
        _pleayerMove = GameObject.FindWithTag("PlayerManager").GetComponent<PlayerMove_MT>();
    }

    // Update is called once per frame
    void Update()
    {
        //�q�`�x�i�g�h�s����̂�����j�𔭎ˁi����1�F�n�_�@�����Q�F�����@�����R�F�����@�����S�F���C���[�}�X�N�j
        bool rayHit = Physics.Raycast(transform.position,transform.forward,_frontDistance,_layer);
        //�q�`�x�̉����i����1�F�n�_�@�����Q�F���������@�����R�F�F�j
        Debug.DrawRay(transform.position,Vector3.forward, Color.red);

        //RAY�����肵�����ړ����삪�������Ă���ꍇ
        if (rayHit && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            
        }
    }
}
