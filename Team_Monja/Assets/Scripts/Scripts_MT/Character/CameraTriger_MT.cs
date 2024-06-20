using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriger_MT : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // 親オブジェクトのスクリプトを取得
        CameraManager_MT cameraManager = GetComponentInParent<CameraManager_MT>();
        if (cameraManager != null)
        {
            // 親オブジェクトのOnChildTriggerEnterメソッドを呼び出し
            cameraManager.OnChildTriggerEnterCamera(other);


        }
    }
    
    void OnTriggerExit(Collider other)
    {
        // 親オブジェクトのスクリプトを取得
        CameraManager_MT cameraManager = GetComponentInParent<CameraManager_MT>();
        if (cameraManager != null)
        {
            // 親オブジェクトのOnChildTriggerExitメソッドを呼び出し
            cameraManager.OnChildTriggerExitCamera(other);


        }
    }
}
