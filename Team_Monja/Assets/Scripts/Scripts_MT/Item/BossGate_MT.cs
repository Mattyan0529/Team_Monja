using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossGate_MT : MonoBehaviour
{
    [SerializeField] private GameObject _canvasObjBoss = default;
    [SerializeField] private GameObject _gateObj = default;
    [SerializeField] private GameObject _pressF = default;

    private Canvas _canvasBoss;
    private Collider _collider;

    private void Start()
    {
        _canvasBoss = _canvasObjBoss.GetComponent<Canvas>();
        _collider = _gateObj.GetComponent<Collider>();
    }


    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Player") )
        {
            _pressF.SetActive(true);
            if(Input.GetKeyDown(KeyCode.F))
            {
                _collider.isTrigger = true;
                _canvasBoss.enabled = true;
                _pressF.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _collider.isTrigger = false;
        _pressF.SetActive(false);
    }

}
