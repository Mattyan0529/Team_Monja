using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP_MT : MonoBehaviour
{
    private Camera _mainCamera;
    private Canvas _canvas;
    private MoveSlider_MT _moveSlider;
    private StatusManager_MT _statusManager;

    // Start is called before the first frame update
    void Start()
    {
        _canvas = GetComponent<Canvas>();
        //�e�I�u�W�F�N�g�̃R���|�[�l���g���擾
        _statusManager = GetComponentInParent<StatusManager_MT>();
        //�q�I�u�W�F�N�g����R���|�[�l���g���擾
        _moveSlider = GetComponentInChildren<MoveSlider_MT>();

        CameraChange();
        TagCheck();
    }

    // Update is called once per frame
    void Update()
    {
        LookCamera();
        SetSlider();
    }

    //eventCamra��ݒ�
    public void CameraChange()
    {
        _canvas.worldCamera = Camera.main;
    }

    //EnemyCanvas��Main Camera�Ɍ�������
    private void LookCamera()
    {
        _canvas.transform.rotation = Camera.main.transform.rotation;
    }
    //�X���C�_�[�̓��e���Z�b�g
    private void SetSlider()
    {
        _moveSlider.SetMaxHP(_statusManager.MaxHP);
        _moveSlider.SetCurrentHP(_statusManager.HP);
    }
    //Enemy�^�O�����Ă�����Active�ɂ���
    private void TagCheck()
    {
        if (this.transform.parent.CompareTag("Enemy"))
        {
            _canvas.enabled = true;
        }
    }

}
