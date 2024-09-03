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
        //ＲＡＹ（ＨＩＴ判定のある線）を発射（引数1：始点　引数２：向き　引数３：距離　引数４：レイヤーマスク）
        bool rayHit = Physics.Raycast(transform.position,transform.forward,_frontDistance,_layer);
        //ＲＡＹの可視化（引数1：始点　引数２：向き長さ　引数３：色）
        Debug.DrawRay(transform.position,Vector3.forward, Color.red);

        //RAYが判定したかつ移動操作がかかっている場合
        if (rayHit && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            
        }
    }
}
