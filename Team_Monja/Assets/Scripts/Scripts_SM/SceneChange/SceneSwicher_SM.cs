using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher_SM : MonoBehaviour
{
    [SerializeField] private string sceneName; // �J�ڂ���V�[���̖��O
    [SerializeField] private bool useLeghtMouse; // �G���^�[�L�[�ŃV�[����J�ڂ��邩�ǂ���
    [SerializeField] private bool useBackspaceKey; // �o�b�N�X�y�[�X�L�[�ŃV�[����J�ڂ��邩�ǂ���
    [SerializeField] private bool isObjectActive; // �w�肵���I�u�W�F�N�g���A�N�e�B�u���ǂ���
    [SerializeField] private GameObject _checkObject;//�A�N�e�B�u���ǂ������`�F�b�N����I�u�W�F�N�g���ǂꂩ

    private bool getAxisSubmitZero = false;//Submit��GetAxis��0�ɂȂ������ǂ���
    private bool getAxisCancelZero = false;//Cancel��GetAxis��0�ɂȂ������ǂ���

    void Start()
    {
        SetupButtonListener();
    }

    void Update()
    {
        CheckObjectActive();
        CheckKeyboardInput();
    }

    // �{�^���̃N���b�N�C�x���g��o�^���郁�\�b�h
    private void SetupButtonListener()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    //�I�u�W�F�N�g���A�N�e�B�u���ǂ����𔻒肷�郁�\�b�h
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

    // �L�[�{�[�h���͂��`�F�b�N���郁�\�b�h
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
            LoadScene();
        }

        if (useBackspaceKey && (Input.GetKeyDown(KeyCode.Backspace) || Input.GetAxis("Cancel") > 0 && getAxisCancelZero))
        {
            LoadScene();
        }

    }

    // �{�^���N���b�N���ɌĂ΂�郁�\�b�h
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

    // �V�[�������[�h���郁�\�b�h
    private void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }

    // �Q�[�����I�����郁�\�b�h
    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


}
