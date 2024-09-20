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
    //ゲームオーバーのキャンバス
    [SerializeField] private GameObject _gameOverCanvas = default;
    //bossのキャンバス
    [SerializeField] private GameObject _bossCanvas = default;



    private float _slowTimeScale = 0.3f; //スローモーションの時の時間速度
    private Vector3 _deadCameraPosition = new Vector3(0, 0, 0);　//死んだときのカメラの位置
    private Vector3 _deadCameraRotation = new Vector3(0, 0, 0);  // 死んだときのカメラの向き
    private bool _isCoroutineActive = false;//コルーチンが動作中かどうか
    private bool _isGameClear = false;//ゲームクリア
    private bool _isGameOver = false;


    public bool IsGameOver
    {
        get { return _isGameOver; }
    }


    private void Start()
    {
        _cameraManager = GameObject.FindWithTag("CameraPos").GetComponent<CameraManager_MT>();
        //ボスが完成したら下の行のコメント消す
        _statusManagerBoss = GameObject.FindWithTag("Boss").GetComponent<StatusManager_MT>();
    }

    private void Update()
    {
        //誤動作防止
        if (_statusManagerPlayer.HP > 0 && _isCoroutineActive && !_isGameClear)
        {
            ResetGameOverCorouine();
        }
    }

    private void ResetGameOverCorouine()
    {
        StopCoroutine(GameOverCoroutine());
        _cameraManager.enabled = true;
        Time.timeScale = 1;
    }

    public void SetPlayer(GameObject player)
    {
        _statusManagerPlayer = player.GetComponent<StatusManager_MT>();
    }

    public IEnumerator GameOverCoroutine()
    {
        _isCoroutineActive = true;

        _isGameOver = true;

        float slowTime = 1.75f;

        _deadCameraPosition = new Vector3(0, 6, 0);
        _deadCameraRotation = new Vector3(90, 180, 0);

        _cameraManager.enabled = false;

        _bossCanvas.SetActive(false);

        if (_canvasPlayer != null)
        {
            foreach (Transform child in _canvasPlayer.transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        Time.timeScale = _slowTimeScale;

        yield return new WaitForSeconds(slowTime);

        Camera.main.transform.localPosition = _deadCameraPosition;
        Camera.main.transform.localRotation = Quaternion.Euler(_deadCameraRotation);

        Time.timeScale = 0.0001f;

        _gameOverCanvas.SetActive(true);


        _isCoroutineActive = false;
    }

    public IEnumerator GameClearCoroutine()
    {
        _isCoroutineActive = true;
        //ゲームクリアのboolをtrue
        _isGameClear = true;

        //何秒待つのか
        float slowTime = 1f;

        //カメラ操作をできなくする
        _cameraManager.enabled = false;

        _bossCanvas.SetActive(false);

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

        //時間停止
        Time.timeScale = 0;

        //ゲームクリアシーンに移動
        SceneManager.LoadScene("GameClearScene");

        _isCoroutineActive = false;
    }


}
