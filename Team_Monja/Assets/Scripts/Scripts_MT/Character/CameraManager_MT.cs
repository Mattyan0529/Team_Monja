using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager_MT : MonoBehaviour
{
    private Camera playerCamera;
    private GameObject playerObj;
    private Transform playerTransform;

    private float mouseSensitivity = 100.0f; // �}�E�X���x
    private bool _InWall = false;//�J�������ǂ̒��ɓ����Ă��邩
    private void Awake()
    {
        FindPlayer();
    }
    void Start()
    {

        playerCamera = Camera.main.GetComponent<Camera>();

        // �J�[�\�������b�N���ĉ�ʒ����ɌŒ�
        Cursor.lockState = CursorLockMode.Locked;


    }

    // Update is called once per frame
    void Update()
    {
        CameraMove();
        CameraTransparent();
    }
    private void LateUpdate()
    {
        PlayerFollowing();
    }

    /// <summary>
    ///�v���C���[���擾
    /// </summary>
    public void FindPlayer()
    {
        playerObj = GameObject.FindWithTag("Player");
        if(playerObj != null)
        {
            playerTransform = playerObj.transform;
        }

    }

    /// <summary>
    /// �v���C���[�̍��W�ɃJ�������ړ�
    /// </summary>
    private void PlayerFollowing()
    {
        if(playerObj != null)
        {
            transform.position = playerTransform.position;
        }
    }


    /// <summary>
    ///�}�E�X�ŃJ�����𓮂��� 
    /// </summary>
    private void CameraMove()
    {
        // �}�E�X�̈ړ��ʂ��擾
        float mouseX = Input.GetAxis("Horizontal2") * mouseSensitivity * Time.deltaTime
         + Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //�J��������]
        transform.Rotate(Vector3.up * mouseX,Space.World);
        //�v���C���[���ړ�
        playerObj.transform.Rotate(Vector3.up * mouseX, Space.World);
    }

    /// <summary>
    /// �J�����̓��ߋ�����ύX
    /// </summary>
    private void CameraTransparent()
    {
        if (_InWall)
        {
            playerCamera.nearClipPlane = 7.25f; // ���ߋ�����ݒ�
        }
        else
        {
            playerCamera.nearClipPlane = 0.03f; // �����l
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        _InWall = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _InWall = false;
    }
}
