using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher_SM : MonoBehaviour
{
    [SerializeField] private string sceneName; // 遷移するシーンの名前
    [SerializeField] private bool useLeghtMouse; // エンターキーでシーンを遷移するかどうか
    [SerializeField] private bool useBackspaceKey; // バックスペースキーでシーンを遷移するかどうか
    [SerializeField] private bool useEscapeKey; // エスケープキーでオプション画面を表示するかどうか

    void Start()
    {
        SetupButtonListener();
    }

    void Update()
    {
        CheckKeyboardInput();
    }

    // ボタンのクリックイベントを登録するメソッド
    private void SetupButtonListener()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    // キーボード入力をチェックするメソッド
    private void CheckKeyboardInput()
    {
        if (useLeghtMouse && (Input.GetMouseButtonDown(0) || Input.GetAxis("Submit") > 0))
        {
            LoadScene();
        }

        if (useBackspaceKey && (Input.GetKeyDown(KeyCode.Backspace) || Input.GetAxis("Cancel") > 0))
        {
            LoadScene();
        }

    }

    // ボタンクリック時に呼ばれるメソッド
    private void OnButtonClick()
    {
        if (sceneName != null && sceneName != "")
        {
            LoadScene();
        }
        else
        {
            QuitGame();
        }
    }

    // シーンをロードするメソッド
    private void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    // ゲームを終了するメソッド
    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


}
