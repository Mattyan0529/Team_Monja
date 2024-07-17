using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_MT : MonoBehaviour
{
    [SerializeField,
        Header("子オブジェクトの中にあります。"),
        TooltipAttribute("非アクティブにしてね")]
    private GameObject _canvas;

    private void Update()
    {
        LookCamera();
    }
    // Canvasをメインカメラの方向に向ける
    private void LookCamera()
    {
        _canvas.transform.rotation = Camera.main.transform.rotation;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canvas.SetActive(true);
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canvas.SetActive(false);
        }
    }
}
