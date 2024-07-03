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

    private Transform _childObj;

    // Start is called before the first frame update
    void Start()
    {
        _canvas = GetComponent<Canvas>();
        _statusManager = GetComponentInParent<StatusManager_MT>();
        _collider = GetComponentInParent<Collider>();
        _moveSlider = GetComponentInChildren<MoveSlider_MT>();
        _childObj = transform.GetChild(0);

        SetPlayerArea(); // �v���C���[�Ƃ��̃R���|�[�l���g���擾
        TagCheck(); // �^�O�Ɋ�Â���Canvas�̏�����Ԃ�ݒ�
    }

    // Update is called once per frame
    void Update()
    {
        LookCamera(); // Canvas���J�����Ɍ�����
        SetSlider(); // �X���C�_�[�̒l��ݒ�
        NearEnemyCheck(); // �G���߂��ɂ���ꍇ��Canvas��\������
    }


    // EnemyTriggerManager���擾
    public void SetPlayerArea()
    {
        GameObject nearTrigger = GameObject.FindWithTag("NearTrigger");
        if (nearTrigger != null)
        {
            _enemyTriggerManager = nearTrigger.GetComponent<EnemyTriggerManager_MT>();
        }
        else
        {
            Debug.LogError("NearTrigger not found.");
        }
    }

    // �G�łȂ����HP�o�[���A�N�e�B�u�ɂ���
    public void TagCheck()
    {
        if (!this.transform.parent.CompareTag("Enemy"))
        {
           _childObj.gameObject.SetActive(false);
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

    // �G���߂��ɂ�����HP�o�[��\������
    private void NearEnemyCheck()
    {
        if (this.transform.parent.CompareTag("Enemy"))
        {
            if (_enemyTriggerManager.objectsInTrigger.Contains(_collider))
            {
                _childObj.gameObject.SetActive(true);
            }
            else
            {
                _childObj.gameObject.SetActive(false);
            }
        }
    }
}
