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
    // �ǋL�F�k
    private TimeManager_KH _timeManager = default;

    private Canvas _canvasBoss;
    private Collider _collider;

    private bool isClosed = false;

    public bool IsClosed { get => isClosed; set => isClosed = value; }

    private void Start()
    {
        _canvasBoss = _canvasObjBoss.GetComponent<Canvas>();
        _collider = _gateObj.GetComponent<Collider>();

        // �ǋL�F�k
        _timeManager = _canvasObjPlayer.GetComponentInChildren<TimeManager_KH>();
    }

    

    /// <summary>
    /// �{�X�����ɓ����悤�ɂȂ�
    /// </summary>
    public void OpenGate()
    {
        _collider.isTrigger = true;
        isClosed = true;
        _pressF.SetActive(false);

        // �ǋL�F�k
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
                //������Đ�
                _bossVideo.PlayVideo();
             
            }
        }
    }
    //�{�X�����ɓ�����
    private void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("Player")�@|| isClosed) && other.isTrigger == false)
        {
            _timeManager.IsInCastle = true;
            _collider.isTrigger = false;
            _pressF.SetActive(false);
            _canvasBoss.enabled = true;
            //�J�������{�X�̕����Ɍ�����
            Camera.main.transform.parent.rotation = Quaternion.Euler(0,0,0);
        }

    }



}
