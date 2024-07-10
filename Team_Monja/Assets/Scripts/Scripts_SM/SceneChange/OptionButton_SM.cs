using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionButton_SM : MonoBehaviour
{
    // オプション画面のキャンバスを参照するための変数
    [SerializeField]
    private GameObject ScreenToDisplay;

    // 元のキャンバスを参照するための変数
    [SerializeField]
    private GameObject ScreenToDelete;

    // ボタンを参照するための変数
    [SerializeField]
    private Button toggleButton;

    // インスペクターでエスケープキーとボタンの切り替えを行うための変数
    [SerializeField]
    private bool useEscapeKey = true;

    // 新しいキャンバスが表示されたときに最初に選択されるボタン
    [SerializeField]
    private Button firstSelectedButton;

    private GameObject lastSelectedObject;

    void Start()
    {
        // ボタンのクリックイベントにメソッドを登録する
        if (toggleButton != null && !useEscapeKey)
        {
            toggleButton.onClick.AddListener(ToggleOptions);
        }
    }

    void Update()
    {
        // エスケープキーが押されたら
        if (useEscapeKey && Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOptions();
        }
    }

    // オプション画面の表示/非表示を切り替えるメソッド
    void ToggleOptions()
    {
        if (ScreenToDisplay != null && ScreenToDelete != null)
        {
            bool isOptionsActive = ScreenToDisplay.activeSelf;

            // 現在選択されているオブジェクトを保持する
            if (!isOptionsActive)
            {
                if (EventSystem.current != null)
                {
                    lastSelectedObject = EventSystem.current.currentSelectedGameObject;
                }
            }

            // オプション画面を切り替える
            ScreenToDisplay.SetActive(!isOptionsActive);

            // 元のキャンバスの表示/非表示を切り替える
            ScreenToDelete.SetActive(isOptionsActive);

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
