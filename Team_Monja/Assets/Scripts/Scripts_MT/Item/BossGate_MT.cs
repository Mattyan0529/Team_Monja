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


    [SerializeField] private DragPlayerToBoss_KH _damonHand;
    // 追記：北
    private TimeManager_KH _timeManager = default;

    private Canvas _canvasBoss;
    private Collider _collider;

    private bool isClosed = false;

    private Vector3 _moveForBoss = new Vector3(0, 0, 10);


    private void Start()
    {
        _canvasBoss = _canvasObjBoss.GetComponent<Canvas>();
        _collider = _gateObj.GetComponent<Collider>();

        // 追記：北
        _timeManager = _canvasObjPlayer.GetComponentInChildren<TimeManager_KH>();
    }

    private void Update()
    {
        if (_damonHand.Isdrag  ||!isClosed)
        {
            CloseGate();
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

    private void CloseGate()
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
            {//動画を再生
                _bossVideo.PlayVideo();
                //ボス部屋と逆方向に進むのを防止
                this.transform.position += _moveForBoss;
                OpenGate();
            }
        }
    }
    //ボス部屋に入った
    private void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("Player")　|| isClosed) && other.isTrigger == false)
        {
            _timeManager.IsInCastle = true;
            _collider.isTrigger = false;
            _pressF.SetActive(false);
            _canvasBoss.enabled = true;
            //カメラをボスの方向に向ける
            Camera.main.transform.parent.rotation = Quaternion.Euler(0,0,0);
        }

    }



}
