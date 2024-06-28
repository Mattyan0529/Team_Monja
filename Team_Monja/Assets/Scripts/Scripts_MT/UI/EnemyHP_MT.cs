using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP_MT : MonoBehaviour
{
    private Collider _collider;
    private Canvas _canvas;
    private MoveSlider_MT _moveSlider;
    private StatusManager_MT _statusManager;
    private EnemyTriggerManager_MT _enemyTriggerManager;

    // Start is called before the first frame update
    void Start()
    {
        _canvas = GetComponent<Canvas>();
        _statusManager = GetComponentInParent<StatusManager_MT>();
        _collider = GetComponentInParent<Collider>();
        _moveSlider = GetComponentInChildren<MoveSlider_MT>();

        SetPlayerArea(); // �v���C���[�Ƃ��̃R���|�[�l���g���擾
        CameraChange(); // �J������ݒ�
        TagCheck(); // �^�O�Ɋ�Â���Canvas�̏�����Ԃ�ݒ�
    }

    // Update is called once per frame
    void Update()
    {
        LookCamera(); // Canvas���J�����Ɍ�����
        SetSlider(); // �X���C�_�[�̒l��ݒ�
        NearEnemyCheck(); // �G���߂��ɂ���ꍇ��Canvas��\������
    }

    // Canvas�����C���J�����ɐݒ�
    public void CameraChange()
    {
        _canvas.worldCamera = Camera.main;
    }

    // �v���C���[��������EnemyTriggerManager���擾
    public void SetPlayerArea()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            _enemyTriggerManager = player.GetComponent<EnemyTriggerManager_MT>();
        }
        else
        {
            Debug.LogError("Player not found.");
        }
    }

    // �G�łȂ����Canvas���A�N�e�B�u�ɂ���
    public void TagCheck()
    {
        if (!this.transform.parent.CompareTag("Enemy"))
        {
            _canvas.enabled = false;
        }
    }

    // Canvas�����C���J�����̕����Ɍ�����
    private void LookCamera()
    {
        _canvas.transform.rotation = Camera.main.transform.rotation;
    }

    // �X���C�_�[�̒l��ݒ肷��
    private void SetSlider()
    {
        _moveSlider.SetMaxHP(_statusManager.MaxHP);
        _moveSlider.SetCurrentHP(_statusManager.HP);
    }

    // �G���߂��ɂ�����Canvas��\������
    private void NearEnemyCheck()
    {
        if (this.transform.parent.CompareTag("Enemy"))
        {
            if (_enemyTriggerManager.objectsInTrigger.Contains(_collider))
            {
                _canvas.enabled = true;
            }
            else
            {
                _canvas.enabled = false;
            }
        }
    }
}
