using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher_SM : MonoBehaviour
{
    [SerializeField] private string sceneName; // 遷移するシーンの名前
    [SerializeField] private bool useLeghtMouse; // エンターキーでシーンを遷移するかどうか
    [SerializeField] private bool useBackspaceKey; // バックスペースキーでシーンを遷移するかどうか
    [SerializeField] private bool isObjectActive; // 指定したオブジェクトがアクティブかどうか
    [SerializeField] private GameObject _checkObject;//アクティブかどうかをチェックするオブジェクトがどれか

    private bool getAxisSubmitZero = false;//SubmitのGetAxisが0になったかどうか
    private bool getAxisCancelZero = false;//CancelのGetAxisが0になったかどうか
    private ControllerVibration_MT _vibration;

    void Start()
    { if(GameObject.FindWithTag("ResidentScripts") != null)
        {
            _vibration = GameObject.FindWithTag("ResidentScripts").GetComponent<ControllerVibration_MT>();
        }
        SetupButtonListener();
    }

    void Update()
    {
        CheckObjectActive();
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

    //オブジェクトがアクティブかどうかを判定するメソッド
    private void CheckObjectActive()
    {
        if (_checkObject == null || _checkObject.activeSelf == true)
        {
            isObjectActive = true;
        }
        else
        {
            isObjectActive = false;
        }
    }

    // キーボード入力をチェックするメソッド
    private void CheckKeyboardInput()
    {
        if (Input.GetAxis("Submit") == 0 && isObjectActive)
        {
            getAxisSubmitZero = true;
        }

        if (Input.GetAxis("Cancel") == 0 && isObjectActive)
        {
            getAxisCancelZero = true;
        }

        if (useLeghtMouse && (Input.GetMouseButtonDown(0) || Input.GetAxis("Submit") > 0 && getAxisSubmitZero))
        {
            if (_vibration != null)
            {
                _vibration.StopVibration();
            }
            LoadScene();
        }

        if (useBackspaceKey && (Input.GetKeyDown(KeyCode.Backspace) || Input.GetAxis("Cancel") > 0 && getAxisCancelZero))
        {
            if (_vibration != null)
            {
                _vibration.StopVibration();
            }
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
        Time.timeScale = 1;
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
