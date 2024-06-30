using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager_MT : MonoBehaviour
{
    private Camera playerCamera;
    private AudioListener audioListener;
    private EnemyHP_MT enemyHP;

    private float mouseSensitivity = 100.0f; // マウス感度
    private Transform playerBody; // カメラが追従するプレイヤーオブジェクト
    private bool _InWall = false;//カメラが壁の中に入っているか



    void Start()
    {
        //子オブジェクトからコンポーネント取得
        playerCamera = GetComponentInChildren<Camera>();
        audioListener = GetComponentInChildren<AudioListener>();
        enemyHP = GetComponentInChildren<EnemyHP_MT>();

        // カーソルをロックして画面中央に固定
        Cursor.lockState = CursorLockMode.Locked;

        // プレイヤーのTransformを取得
        playerBody = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        CameraSwitch();
    }

    /// <summary>
    /// タグによってカメラを切り替える
    /// </summary>
    private void CameraSwitch()
    {
        //プレイヤーのとき
        if (this.gameObject.CompareTag("Player"))
        {
            playerCamera.tag = "MainCamera";
            playerCamera.enabled = true;
            audioListener.enabled = true;
            CameraMove();
            CameraTransparent();
            enemyHP.CameraChange();
        }
        //敵のとき
        else if (this.gameObject.CompareTag("Enemy")|| this.gameObject.CompareTag("Boss"))
        {
            playerCamera.tag = "Untagged";
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
            playerCamera.nearClipPlane = 7.25f; // 透過距離を設定
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
