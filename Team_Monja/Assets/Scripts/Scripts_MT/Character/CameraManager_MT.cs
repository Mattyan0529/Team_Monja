using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager_MT : MonoBehaviour
{
    private Camera playerCamera;
    private AudioListener audioListener;
    private EnemyHP_MT enemyHP;

    private float mouseSensitivity = 100.0f; // �}�E�X���x
    private Transform playerBody; // �J�������Ǐ]����v���C���[�I�u�W�F�N�g
    private bool _InWall = false;//�J�������ǂ̒��ɓ����Ă��邩



    void Start()
    {
        //�q�I�u�W�F�N�g����R���|�[�l���g�擾
        playerCamera = GetComponentInChildren<Camera>();
        audioListener = GetComponentInChildren<AudioListener>();
        enemyHP = GetComponentInChildren<EnemyHP_MT>();

        // �J�[�\�������b�N���ĉ�ʒ����ɌŒ�
        Cursor.lockState = CursorLockMode.Locked;

        // �v���C���[��Transform���擾
        playerBody = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        CameraSwitch();
    }

    /// <summary>
    /// �^�O�ɂ���ăJ������؂�ւ���
    /// </summary>
    private void CameraSwitch()
    {
        //�v���C���[�̂Ƃ�
        if (this.gameObject.CompareTag("Player"))
        {
            playerCamera.tag = "MainCamera";
            playerCamera.enabled = true;
            audioListener.enabled = true;
            CameraMove();
            CameraTransparent();
            enemyHP.CameraChange();
        }
        //�G�̂Ƃ�
        else if (this.gameObject.CompareTag("Enemy")|| this.gameObject.CompareTag("Boss"))
        {
            playerCamera.tag = "Untagged";
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
            playerCamera.nearClipPlane = 7.25f; // ���ߋ�����ݒ�
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
