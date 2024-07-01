using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections; // これが必要な名前空間のインポートよ

public class SceneSwitcher_SM : MonoBehaviour
{
    [SerializeField] private string sceneName; // 遷移するシーンの名前
    [SerializeField] private GameObject optionPanel; // オプション画面のUIパネル
    [SerializeField] private bool useEnterKey; // エンターキーでシーンを遷移するかどうか
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
        if (useEnterKey && Input.GetKeyDown(KeyCode.Return))
        {
            LoadScene();
        }

        if (useBackspaceKey && Input.GetKeyDown(KeyCode.Backspace))
        {
            LoadScene();
        }

        if (useEscapeKey && Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOptionPanel();
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
        // 現在のシーンをアンロードしてから新しいシーンをロード
        StartCoroutine(UnloadCurrentAndLoadNewScene());
    }

    // 現在のシーンをアンロードしてから新しいシーンをロードするコルーチン
    private IEnumerator UnloadCurrentAndLoadNewScene()
    {
        // 現在のシーンをアンロード
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        // アンロードが完了するまで待つ
        while (!unloadOperation.isDone)
        {
            yield return null;
        }

        // 新しいシーンをロード
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

    // オプション画面の表示/非表示を切り替えるメソッド
    private void ToggleOptionPanel()
    {
        if (optionPanel != null)
        {
            optionPanel.SetActive(!optionPanel.activeSelf);
        }
    }
}
