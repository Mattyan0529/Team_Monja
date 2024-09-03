using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventionWallScript_WS : MonoBehaviour
{
    private float _frontDistance = 1f;
    private float direction = -1;
    private LayerMask layer = 6;
    private Rigidbody rb;
    private GameObject positionCamera;

    // Start is called before the first frame update
    void Start()
    {
        positionCamera = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        //ＲＡＹ（ＨＩＴ判定のある線）を発射（引数1：始点　引数２：向き　引数３：距離　引数４：レイヤーマスク）
        bool rayHit = Physics.Raycast(transform.position,transform.forward,_frontDistance,layer);

        if (rayHit && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            // カメラの前方ベクトルを基準にした移動ベクトルを計算
            Vector3 movement = ((positionCamera.transform.position * Input.GetAxis("Vertical") + Camera.main.transform.right * Input.GetAxis("Horizontal")).normalized *  Time.deltaTime) * direction ;

            // そのままワールド座標系で移動
            rb.MovePosition(rb.position + movement);
        }
    }
}
