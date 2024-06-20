using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriger_MT : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // �e�I�u�W�F�N�g�̃X�N���v�g���擾
        CameraManager_MT cameraManager = GetComponentInParent<CameraManager_MT>();
        if (cameraManager != null)
        {
            // �e�I�u�W�F�N�g��OnChildTriggerEnter���\�b�h���Ăяo��
            cameraManager.OnChildTriggerEnterCamera(other);


        }
    }
    
    void OnTriggerExit(Collider other)
    {
        // �e�I�u�W�F�N�g�̃X�N���v�g���擾
        CameraManager_MT cameraManager = GetComponentInParent<CameraManager_MT>();
        if (cameraManager != null)
        {
            // �e�I�u�W�F�N�g��OnChildTriggerExit���\�b�h���Ăяo��
            cameraManager.OnChildTriggerExitCamera(other);


        }
    }
}
