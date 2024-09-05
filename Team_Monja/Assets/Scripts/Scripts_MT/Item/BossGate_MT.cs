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

    // 追記：北
    private TimeManager_KH _timeManager = default;

    private Canvas _canvasBoss;
    private Collider _collider;

    private void Start()
    {
        _canvasBoss = _canvasObjBoss.GetComponent<Canvas>();
        _collider = _gateObj.GetComponent<Collider>();

        // 追記：北
        _timeManager = _canvasObjPlayer.GetComponentInChildren<TimeManager_KH>();
    }

    /// <summary>
    /// ボス部屋に入れるようになる
    /// </summary>
    public void OpenGate()
    {
        _collider.isTrigger = true;
        _canvasBoss.enabled = true;
        _pressF.SetActive(false);

        // 追記：北
        _timeManager.IsInCastle = true;
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
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
        _collider.isTrigger = false;
        _pressF.SetActive(false);
    }

}
