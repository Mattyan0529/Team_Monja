using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager_MT : MonoBehaviour
{
    Camera playerCamera;
    AudioListener audioListener;

    private float mouseSensitivity = 1000.0f; // マウス感度
    private Transform playerBody; // カメラが追従するプレイヤーオブジェクト
    private bool _InWall = false;//カメラが壁の中に入っているか



    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
        audioListener = GetComponentInChildren<AudioListener>();

        // カーソルをロックして画面中央に固定
        Cursor.lockState = CursorLockMode.Locked;

        // プレイヤーのTransformを取得
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
    /// タグによってカメラを切り替える
    /// </summary>
    private void PlayerCheck()
    {
        //プレイヤーのとき
        if (this.gameObject.CompareTag("Player"))
        {
            playerCamera.enabled = true;
            audioListener.enabled = true;
        }
        //敵のとき
        else if (this.gameObject.CompareTag("Enemy"))
        {
            playerCamera.enabled = false;
            audioListener.enabled = false;
        }

    }　

    /// <summary>
    ///マウスでカメラを動かす 
    /// </summary>
    private void CameraMove()
    {
        // マウスの移動量を取得
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        // プレイヤーの水平回転
        playerBody.Rotate(Vector3.up * mouseX);
    }

    /// <summary>
    /// カメラの透過距離を変更
    /// </summary>
    private void CameraTransparent()
    {
        if (_InWall)
        {
            playerCamera.nearClipPlane = 7.5f; // 透過距離を設定
        }
        else
        {
            playerCamera.nearClipPlane = 0.03f; // 初期値
        }
    }

    // 子オブジェクトのOnTriggerEnterの処理
    public void OnChildTriggerEnterCamera(Collider other)
    {
        _InWall = true;
    }

    // 子オブジェクトのOnTriggerExitの処理
    public void OnChildTriggerExitCamera(Collider other)
    {
        _InWall = false;
    }
}
