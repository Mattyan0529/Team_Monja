using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeadDecision_MT : MonoBehaviour
{
    private StatusManager_MT _statusManager;
    private CharacterAnim_MT _characterAnim;

    // 追記：北
    private MonsterRandomWalk_KH _monsterRandomWalk = default;
    private MonsterSkill_KH _monsterSkill = default;
    private PlayerRangeInJudge_KH _playerRangeInJudge = default;
    private PlayerMove_MT _playerMove = default;
    private PlayerSkill_KH _playerSkill = default;
    private NormalAttack_KH _normalAttack = default;
    private CameraManager_MT _cameraManager = default;

    private bool _isAlive = true;
    private bool _coroutineSwitch = true;
    private float _slowTimeScale = 0.5f; //だんだん時間が止まる処理で使う
    
    private Vector3 _deadCameraPosition = new Vector3(0, 6, -2);　//死んだときのカメラの位置
    private Vector3 _deadCameraRotation = new Vector3(90, 180, 0);  // 死んだときのカメラの向き

    //ゲームオーバーの画面画像
    [SerializeField] private GameObject _gameOverImage = default;
    //canvas
    [SerializeField] private GameObject _canvasPlayer = default;

    void Start()
    {
        _statusManager = GetComponent<StatusManager_MT>();
        _characterAnim = GetComponent<CharacterAnim_MT>();
        _cameraManager = GetComponent<CameraManager_MT>();
        // 追記：北
        _monsterRandomWalk = GetComponent<MonsterRandomWalk_KH>();
        _monsterSkill = GetComponent<MonsterSkill_KH>();
        _playerRangeInJudge = GetComponent<PlayerRangeInJudge_KH>();
        _playerMove = GetComponent<PlayerMove_MT>();
        _playerSkill = GetComponent<PlayerSkill_KH>();
        _normalAttack = GetComponent<NormalAttack_KH>();
    }

    void Update()
    {
        if (IsDeadDecision())
        {
            //プレイヤーなら死んだときにスローモーションにする
            if(CompareTag("Player") && _coroutineSwitch)
            {
                //カメラ操作をできなくする
                _cameraManager.enabled = false;

                StartCoroutine(GameOverCoroutine());
                _coroutineSwitch = false;
            }

            _characterAnim.NowAnim = "Die";

            if (_isAlive)
            {
                EnemyStop();
            }
        }
    }

    /// <summary>
    /// 死んでいるか
    /// </summary>
    /// <returns></returns>
    public bool IsDeadDecision()
    {
        if (_statusManager.HP <= 0)
        {
            return true;
        }
        else
        {
            _isAlive = true;
            return false;
        }
    }

    /// <summary>
    /// 食べられたモンスターとプレイヤーの動きを止める　追記：北
    /// </summary>
    private void EnemyStop()
    {
        _playerRangeInJudge.enabled = false;
        _monsterRandomWalk.enabled = false;
        _monsterSkill.enabled = false;
        _playerMove.enabled = false;
        _playerSkill.enabled = false;
        _normalAttack.enabled = false;

        _isAlive = false; 
    }

    private IEnumerator GameOverCoroutine()
    {
        //何秒待つのか
        float slowTime = 1.5f;
        //カメラを取得
        Camera mainCamera = Camera.main;

      
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
    }

}
