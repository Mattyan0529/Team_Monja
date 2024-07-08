using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager_MT : MonoBehaviour
{
    private Camera playerCamera;
    private GameObject playerObj;
    private EnemyHP_MT enemyHP;

    private float mouseSensitivity = 100.0f; // �}�E�X���x
    private bool _InWall = false;//�J�������ǂ̒��ɓ����Ă��邩

    private Vector3 _cameraPosition = new Vector3(0, 6, -9);
    private Vector3 _cameraRotation = new Vector3(15, 0, 0);

    private void Awake()
    {
        CameraSwitch();
    }
    void Start()
    {

        playerCamera = GetComponent<Camera>();

        // �J�[�\�������b�N���ĉ�ʒ����ɌŒ�
        Cursor.lockState = CursorLockMode.Locked;


    }

    // Update is called once per frame
    void Update()
    {
        CameraMove();
        CameraTransparent();
    }

    /// <summary>
    ///�v���C���[�ɃJ����������
    /// </summary>
    public void CameraSwitch()
    {
        playerObj = GameObject.FindWithTag("Player");
        transform.SetParent(playerObj.transform);
        this.transform.localPosition = _cameraPosition;
        this.transform.localRotation = Quaternion.Euler(_cameraRotation);

    }

    /// <summary>
    ///�}�E�X�ŃJ�����𓮂��� 
    /// </summary>
    private void CameraMove()
    {
        // �}�E�X�̈ړ��ʂ��擾
        float mouseX = Input.GetAxis("Horizontal2") * mouseSensitivity * Time.deltaTime
         + Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        // �v���C���[�̐�����]
        playerObj.transform.Rotate(Vector3.up * mouseX);
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
