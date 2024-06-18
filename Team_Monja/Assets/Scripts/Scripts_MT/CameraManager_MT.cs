using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager_MT : MonoBehaviour
{
    Camera playerCamera;
    AudioListener audioListener;

    private float mouseSensitivity = 1000.0f; // �}�E�X���x
    private Transform playerBody; // �J�������Ǐ]����v���C���[�I�u�W�F�N�g
    private bool _InWall = false;//�J�������ǂ̒��ɓ����Ă��邩



    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
        audioListener = GetComponentInChildren<AudioListener>();

        // �J�[�\�������b�N���ĉ�ʒ����ɌŒ�
        Cursor.lockState = CursorLockMode.Locked;

        // �v���C���[��Transform���擾
        playerBody = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCheck();
        if (this.gameObject.CompareTag("Player"))
        {
            CameraMove();
            CameraTransparent();
        }
    }

    /// <summary>
    /// �^�O�ɂ���ăJ������؂�ւ���
    /// </summary>
    private void PlayerCheck()
    {
        //�v���C���[�̂Ƃ�
        if (this.gameObject.CompareTag("Player"))
        {
            playerCamera.enabled = true;
            audioListener.enabled = true;
        }
        //�G�̂Ƃ�
        else if (this.gameObject.CompareTag("Enemy"))
        {
            playerCamera.enabled = false;
            audioListener.enabled = false;
        }

    }�@

    /// <summary>
    ///�}�E�X�ŃJ�����𓮂��� 
    /// </summary>
    private void CameraMove()
    {
        // �}�E�X�̈ړ��ʂ��擾
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        // �v���C���[�̐�����]
        playerBody.Rotate(Vector3.up * mouseX);
    }

    /// <summary>
    /// �J�����̓��ߋ�����ύX
    /// </summary>
    private void CameraTransparent()
    {
        if (_InWall)
        {
            playerCamera.nearClipPlane = 7.5f; // ���ߋ�����ݒ�
        }
        else
        {
            playerCamera.nearClipPlane = 0.03f; // �����l
        }
    }

    // �q�I�u�W�F�N�g��OnTriggerEnter�̏���
    public void OnChildTriggerEnterCamera(Collider other)
    {
        _InWall = true;
    }

    // �q�I�u�W�F�N�g��OnTriggerExit�̏���
    public void OnChildTriggerExitCamera(Collider other)
    {
        _InWall = false;
    }
}
