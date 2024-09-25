using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossGate_MT : MonoBehaviour
{
    [SerializeField] private GameObject _canvasObjBoss = default;
    [SerializeField] private GameObject _canvasObjPlayer = default;
    [SerializeField] private GameObject _gateObj = default;
    [SerializeField] private GameObject _pressF = default;
    [SerializeField] private VideoPlayerController_MT _bossVideo;
    // スカイボックス用のマテリアル
    [SerializeField] private Material _afterSkyboxMaterial;

    [SerializeField] private DragPlayerToBoss_KH _damonHand;
    // 追記：北
    private TimeManager_KH _timeManager = default;
    private BackGroundMusicManagement_KH _backGroundMusicManagement = default;
    private GameObject[] _enemies = default;

    private Canvas _canvasBoss;
    private Collider _collider;

    private bool isClosed = false;
    private bool isDeleteEnemy = false;

    public bool IsClosed { get => isClosed; set => isClosed = value; }

    private void Start()
    {
        _canvasBoss = _canvasObjBoss.GetComponent<Canvas>();
        _collider = _gateObj.GetComponent<Collider>();

        // 追記：北
        _timeManager = _canvasObjPlayer.GetComponentInChildren<TimeManager_KH>();
        _backGroundMusicManagement = GameObject.FindGameObjectWithTag("ResidentScripts").GetComponent<BackGroundMusicManagement_KH>();
    }

    private void Update()
    {
        if (isDeleteEnemy)
        {
            _enemies = GameObject.FindGameObjectsWithTag("Enemy");

            for (int i = 0; i < _enemies.Length; i++)
            {
                _enemies[i].gameObject.SetActive(false);
            }

            isDeleteEnemy = false;
        }
    }


    /// <summary>
    /// ボス部屋に入れるようになる
    /// </summary>
    public void OpenGate()
    {
        _collider.isTrigger = true;
        isClosed = true;
        _pressF.SetActive(false);

        // 追記：北
        _timeManager.IsInCastle = true;
    }

    public void CloseGate()
    {
        isClosed = true;
        _collider.isTrigger = false;
        _pressF.SetActive(false);

    }

    private void OnTriggerStay(Collider other)
    {

        if ((other.gameObject.CompareTag("Player") && !_timeManager.IsInCastle ) && !_damonHand.Isdrag)
        {
            _pressF.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F) || Input.GetAxis("Submit") > 0)
            {

                OpenGate();
                _backGroundMusicManagement.PlayBossMusic();

                isDeleteEnemy = true;
                //動画を再生
                _bossVideo.PlayVideo();
            }
        }
    }
    //ボス部屋に入った
    private void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("Player")　&& isClosed) && other.isTrigger == false)
        {
            _timeManager.IsInCastle = true;
            _collider.isTrigger = false;
            _pressF.SetActive(false);
            _canvasBoss.enabled = true;
            ChangeSkybox(_afterSkyboxMaterial);
            //カメラをボスの方向に向ける
            Camera.main.transform.parent.rotation = Quaternion.Euler(0,0,0);
        }

    }
    // スカイボックスを変更するメソッド
    private void ChangeSkybox(Material newMaterial)
    {
        if (newMaterial != null)
        {
            RenderSettings.skybox = newMaterial;
            // もし環境の反射も更新したい場合は以下の行を追加
            DynamicGI.UpdateEnvironment();
        }
        else
        {
            Debug.LogWarning("新しいスカイボックスのマテリアルが指定されていません！");
        }
    }

}
