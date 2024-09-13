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

    [SerializeField]
    private GameObject _name;  
    [SerializeField]
    private GameObject _hpBar;
    private void Awake()
    {
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


    // EnemyTriggerManagerを取得
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

    // 敵でなければHPバーを非アクティブにする
    public void TagCheck()
    {

        if (!this.transform.parent.CompareTag("Enemy"))
        {
            _hpBar.SetActive(false);
            _name.SetActive(true);
        }
    }


    // Canvasをメインカメラの方向に向ける
    private void LookCamera()
    {
        _canvas.transform.rotation = Camera.main.transform.rotation;
    }

    // スライダーの値を設定する
    private void SetSlider()
    {
        _moveSlider.SetMaxHP(_statusManager.MaxHP);
        _moveSlider.SetCurrentHP(_statusManager.HP);
    }

    // 敵が近くにいたらHPバーを表示する
    private void NearEnemyCheck()
    {
        if (this.transform.parent.CompareTag("Enemy") || _enemyTriggerManager != null)
        {
            if (_enemyTriggerManager.objectsInTrigger.Contains(_collider))
            {
                _name.SetActive(true);
                _hpBar.SetActive(true);
            }
            else
            {
                _name.SetActive(false);
                _hpBar.SetActive(false);
            }
        }
    }
}
