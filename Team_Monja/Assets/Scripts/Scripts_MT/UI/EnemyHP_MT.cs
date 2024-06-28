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
        //親オブジェクトのコンポーネントを取得
        _statusManager = GetComponentInParent<StatusManager_MT>();
        //子オブジェクトからコンポーネントを取得
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

    //eventCamraを設定
    public void CameraChange()
    {
        _canvas.worldCamera = Camera.main;
    }

    //EnemyCanvasをMain Cameraに向かせる
    private void LookCamera()
    {
        _canvas.transform.rotation = Camera.main.transform.rotation;
    }
    //スライダーの内容をセット
    private void SetSlider()
    {
        _moveSlider.SetMaxHP(_statusManager.MaxHP);
        _moveSlider.SetCurrentHP(_statusManager.HP);
    }
    //EnemyタグがついていたらActiveにする
    private void TagCheck()
    {
        if (this.transform.parent.CompareTag("Enemy"))
        {
            _canvas.enabled = true;
        }
    }

}
