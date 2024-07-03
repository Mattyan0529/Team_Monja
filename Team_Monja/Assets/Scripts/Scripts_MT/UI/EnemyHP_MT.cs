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

        SetPlayerArea(); // プレイヤーとそのコンポーネントを取得
        TagCheck(); // タグに基づいてCanvasの初期状態を設定
    }

    // Update is called once per frame
    void Update()
    {
        LookCamera(); // Canvasをカメラに向ける
        SetSlider(); // スライダーの値を設定
        NearEnemyCheck(); // 敵が近くにいる場合にCanvasを表示する
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
           _childObj.gameObject.SetActive(false);
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
