using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndCamera_MT : MonoBehaviour
{


    private StatusManager_MT _statusManagerPlayer = default;
    private StatusManager_MT _statusManagerBoss = default;
    private CameraManager_MT _cameraManager = default;

    //canvas
    [SerializeField] private GameObject _canvasPlayer = default;
    //ゲームオーバーの画面画像
    [SerializeField] private GameObject _gameOverImage = default;


    private float _slowTimeScale = 0.5f; //だんだん時間が止まる処理で使う
    private Vector3 _deadCameraPosition = new Vector3(0, 0, 0);　//死んだときのカメラの位置
    private Vector3 _deadCameraRotation = new Vector3(0, 0, 0);  // 死んだときのカメラの向き
    private bool _isCoroutineActive = false;//コルーチンが動作中かどうか

    private void Start()
    {
        //ボスのオブジェクトを取得
        GameObject bossObj = GameObject.FindWithTag("Boss");

        _cameraManager = GetComponent<CameraManager_MT>();
        _statusManagerPlayer = GetComponent<StatusManager_MT>();
        _statusManagerBoss = bossObj.GetComponent<StatusManager_MT>();
    }

    private void Update()
    {
        //誤動作防止
        if (_statusManagerPlayer.HP > 0 && _isCoroutineActive)
        {
            ResetGameOverCorouine();
        }  
    }

    private void ResetGameOverCorouine()
    {
        StopCoroutine(GameOverCoroutine());
        _cameraManager.enabled = true;
        Time.timeScale = 1;
        Debug.Log("resetgameover");
    }

    public IEnumerator GameOverCoroutine()
    {
        _isCoroutineActive = true;

        //何秒待つのか
        float slowTime = 1.5f;

        //カメラの位置を設定
        _deadCameraPosition = new Vector3(0, 6, 0);
        //カメラの向きを設定
        _deadCameraRotation = new Vector3(90, 180, 0);

        //カメラを取得
        Camera mainCamera = Camera.main;

        //カメラ操作をできなくする
        _cameraManager.enabled = false;

        // 親オブジェクトが設定されているか確認
        if (_canvasPlayer != null)
        {
            // 親オブジェクトの全ての子オブジェクトを取得
            foreach (Transform child in _canvasPlayer.transform)
            {
                // 子オブジェクトを非アクティブにする
                child.gameObject.SetActive(false);
            }
        }

        //スローモーション開始
        Time.timeScale = _slowTimeScale;

        yield return new WaitForSeconds(slowTime);

        //死んだときのカメラを調整
        mainCamera.transform.localPosition = _deadCameraPosition;
        mainCamera.transform.localRotation = Quaternion.Euler(_deadCameraRotation);

        //時間停止
        Time.timeScale = 0;

        //ゲームオーバーの画像を出す
        _gameOverImage.SetActive(true);

        _isCoroutineActive = false;
    }
    public IEnumerator GameClearCoroutine()
    {
        _isCoroutineActive = true;

        //何秒待つのか
        float slowTime = 1.5f;

        ////カメラの位置を設定
        //_deadCameraPosition = new Vector3(0, 6, 0);
        ////カメラの向きを設定
        //_deadCameraRotation = new Vector3(90, 180, 0);

        //カメラを取得
        Camera mainCamera = Camera.main;

        //カメラ操作をできなくする
        _cameraManager.enabled = false;

        // 親オブジェクトが設定されているか確認
        if (_canvasPlayer != null)
        {
            // 親オブジェクトの全ての子オブジェクトを取得
            foreach (Transform child in _canvasPlayer.transform)
            {
                // 子オブジェクトを非アクティブにする
                child.gameObject.SetActive(false);
            }
        }

        //スローモーション開始
        Time.timeScale = _slowTimeScale;

        yield return new WaitForSeconds(slowTime);

        ////死んだときのカメラを調整
        //mainCamera.transform.localPosition = _deadCameraPosition;
        //mainCamera.transform.localRotation = Quaternion.Euler(_deadCameraRotation);

        //時間停止
        Time.timeScale = 0;

        //ゲームクリアシーンに移動
        SceneManager.LoadScene("GameClearScene");

        _isCoroutineActive = false;
    }


}
