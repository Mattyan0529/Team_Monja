using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHP_MT : MonoBehaviour
{
    private Collider _collider;
    private Canvas _canvas;
    private MoveSlider_MT _moveSlider;
    private StatusManager_MT _statusManager;
    private EnemyTriggerManager_MT _enemyTriggerManager;
    private TextMeshProUGUI  _text;

    private List<Transform> _childObjects = new List<Transform>();

    private void Awake()
    {//�G���[���o�邩�炵�����Ȃ���
        GetAllChild();
        _moveSlider = GetComponentInChildren<MoveSlider_MT>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        
    }
    void Start()
    {
        _canvas = GetComponent<Canvas>();
        _statusManager = GetComponentInParent<StatusManager_MT>();
        _collider = GetComponentInParent<Collider>();

        _text.text = this.transform.parent.name;

        SetPlayerArea();
        TagCheck(); 
    }


    void Update()
    {
        LookCamera(); 
        SetSlider();
        NearEnemyCheck();
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
            foreach (Transform child in _childObjects)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    //�q�I�u�W�F�N�g�����ׂČ�������
    private void GetAllChild()
    {
        foreach (Transform child in transform)
        {
            _childObjects.Add(child);
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
        if (this.transform.parent.CompareTag("Enemy") || _enemyTriggerManager != null)
        {
            if (_enemyTriggerManager.objectsInTrigger.Contains(_collider))
            {
                foreach (Transform child in _childObjects)
                {
                    child.gameObject.SetActive(true);
                }
                
            }
            else
            {
                foreach (Transform child in _childObjects)
                {
                   child.gameObject.SetActive(false);
                }
                
            }
        }
    }
}
