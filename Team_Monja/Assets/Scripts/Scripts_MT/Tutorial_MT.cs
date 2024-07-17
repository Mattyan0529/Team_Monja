using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_MT : MonoBehaviour
{
    [SerializeField,
        Header("�q�I�u�W�F�N�g�̒��ɂ���܂��B"),
        TooltipAttribute("��A�N�e�B�u�ɂ��Ă�")]
    private GameObject _canvas;

    private void Update()
    {
        LookCamera();
    }
    // Canvas�����C���J�����̕����Ɍ�����
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
