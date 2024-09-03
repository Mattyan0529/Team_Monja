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
        //�q�`�x�i�g�h�s����̂�����j�𔭎ˁi����1�F�n�_�@�����Q�F�����@�����R�F�����@�����S�F���C���[�}�X�N�j
        bool rayHit = Physics.Raycast(transform.position,transform.forward,_frontDistance,layer);

        if (rayHit && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            // �J�����̑O���x�N�g������ɂ����ړ��x�N�g�����v�Z
            Vector3 movement = ((positionCamera.transform.position * Input.GetAxis("Vertical") + Camera.main.transform.right * Input.GetAxis("Horizontal")).normalized *  Time.deltaTime) * direction ;

            // ���̂܂܃��[���h���W�n�ňړ�
            rb.MovePosition(rb.position + movement);
        }
    }
}
