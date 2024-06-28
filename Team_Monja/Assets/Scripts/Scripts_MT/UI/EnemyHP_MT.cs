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

        SetPlayerArea(); // プレイヤーとそのコンポーネントを取得
        CameraChange(); // カメラを設定
        TagCheck(); // タグに基づいてCanvasの初期状態を設定
    }

    // Update is called once per frame
    void Update()
    {
        LookCamera(); // Canvasをカメラに向ける
        SetSlider(); // スライダーの値を設定
        NearEnemyCheck(); // 敵が近くにいる場合にCanvasを表示する
    }

    // Canvasをメインカメラに設定
    public void CameraChange()
    {
        _canvas.worldCamera = Camera.main;
    }

    // プレイヤーを見つけてEnemyTriggerManagerを取得
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

    // 敵でなければCanvasを非アクティブにする
    public void TagCheck()
    {
        if (!this.transform.parent.CompareTag("Enemy"))
        {
            _canvas.enabled = false;
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

    // 敵が近くにいたらCanvasを表示する
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
