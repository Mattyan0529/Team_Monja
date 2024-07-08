using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager_MT : MonoBehaviour
{
    private Camera playerCamera;
    private GameObject playerObj;
    private EnemyHP_MT enemyHP;

    private float mouseSensitivity = 100.0f; // マウス感度
    private bool _InWall = false;//カメラが壁の中に入っているか

    private Vector3 _cameraPosition = new Vector3(0, 6, -9);
    private Vector3 _cameraRotation = new Vector3(15, 0, 0);

    private void Awake()
    {
        CameraSwitch();
    }
    void Start()
    {

        playerCamera = GetComponent<Camera>();

        // カーソルをロックして画面中央に固定
        Cursor.lockState = CursorLockMode.Locked;


    }

    // Update is called once per frame
    void Update()
    {
        CameraMove();
        CameraTransparent();
    }

    /// <summary>
    ///プレイヤーにカメラをつける
    /// </summary>
    public void CameraSwitch()
    {
        playerObj = GameObject.FindWithTag("Player");
        transform.SetParent(playerObj.transform);
        this.transform.localPosition = _cameraPosition;
        this.transform.localRotation = Quaternion.Euler(_cameraRotation);

    }

    /// <summary>
    ///マウスでカメラを動かす 
    /// </summary>
    private void CameraMove()
    {
        // マウスの移動量を取得
        float mouseX = Input.GetAxis("Horizontal2") * mouseSensitivity * Time.deltaTime
         + Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        // プレイヤーの水平回転
        playerObj.transform.Rotate(Vector3.up * mouseX);
    }

    /// <summary>
    /// カメラの透過距離を変更
    /// </summary>
    private void CameraTransparent()
    {
        if (_InWall)
        {
            playerCamera.nearClipPlane = 7.25f; // 透過距離を設定
        }
        else
        {
            playerCamera.nearClipPlane = 0.03f; // 初期値
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
