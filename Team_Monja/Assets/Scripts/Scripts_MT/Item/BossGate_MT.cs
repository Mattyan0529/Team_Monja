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

    [SerializeField] private DragPlayerToBoss_KH _DamonHand;
    // �ǋL�F�k
    private TimeManager_KH _timeManager = default;

    private Canvas _canvasBoss;
    private Collider _collider;

    private bool isClosed = false;


    private void Start()
    {
        _canvasBoss = _canvasObjBoss.GetComponent<Canvas>();
        _collider = _gateObj.GetComponent<Collider>();

        // �ǋL�F�k
        _timeManager = _canvasObjPlayer.GetComponentInChildren<TimeManager_KH>();
    }

    private void Update()
    {
        if (_DamonHand.Isdrag  ||!isClosed)
        {
            CloseGate();
        }
    }

    /// <summary>
    /// �{�X�����ɓ����悤�ɂȂ�
    /// </summary>
    public void OpenGate()
    {
        _collider.isTrigger = true;

        _pressF.SetActive(false);

        // �ǋL�F�k
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

        if ((other.gameObject.CompareTag("Player") || !_timeManager.IsInCastle ) && !_DamonHand.Isdrag)
        {
            _pressF.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F) || Input.GetAxis("Submit") > 0)
            {
                OpenGate();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _timeManager.IsInCastle = true;
        _collider.isTrigger = false;
        _pressF.SetActive(false);
        _canvasBoss.enabled = true;

    }



}
