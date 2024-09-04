using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CanvasChange_SM : MonoBehaviour
{
    // 表示するキャンバスを参照するための変数
    [SerializeField]
    private GameObject _ScreenToDisplay;

    // 非表示にするキャンバスを参照するための変数
    [SerializeField]
    private GameObject _ScreenToDelete;

    // ボタンを参照するための変数
    [SerializeField]
    private Button _toggleButton;

    // インスペクターでエスケープキーとボタンの切り替えを行うための変数
    [SerializeField]
    private bool _useEscapeKey = true;

    //エンターキーとボタンの切り替えを行うための変数
    [SerializeField]
    private bool _useEnterKey = true;

    // 新しいキャンバスが表示されたときに最初に選択されるボタン
    [SerializeField]
    private Button firstSelectedButton;

    private GameObject lastSelectedObject;

    void Start()
    {
        // ボタンのクリックイベントにメソッドを登録する
        if (_toggleButton != null && !_useEscapeKey)
        {
            _toggleButton.onClick.AddListener(ToggleOptions);
        }
    }

    void Update()
    {
        // エスケープキーが押されたら
        if (_useEscapeKey && Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOptions();
        }
        //エンターキーが押されたら
        if (_useEnterKey && Input.GetButtonDown("Submit"))
        {
            ToggleOptions();
        }

        
    }

    // オプション画面の表示/非表示を切り替えるメソッド
    void ToggleOptions()
    {
        if (_ScreenToDisplay != null && _ScreenToDelete != null)
        {
            bool isOptionsActive = _ScreenToDisplay.activeSelf;

            // 現在選択されているオブジェクトを保持する
            if (!isOptionsActive)
            {
                if (EventSystem.current != null)
                {
                    lastSelectedObject = EventSystem.current.currentSelectedGameObject;
                }
            }

            // オプション画面を切り替える
            _ScreenToDisplay.SetActive(!isOptionsActive);

            // 元のキャンバスの表示/非表示を切り替える
            _ScreenToDelete.SetActive(isOptionsActive);

            // オプション画面を表示したら、最初の選択ボタンを設定する
            if (!isOptionsActive && firstSelectedButton != null)
            {
                if (EventSystem.current != null)
                {
                    EventSystem.current.SetSelectedGameObject(firstSelectedButton.gameObject);
                }
            }

            // オプション画面を非表示に戻したら、前の選択を復元する
            if (isOptionsActive && lastSelectedObject != null)
            {
                if (EventSystem.current != null)
                {
                    EventSystem.current.SetSelectedGameObject(lastSelectedObject);
                }
            }
        }
    }
}
