using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher_SM : MonoBehaviour
{
    [SerializeField] private string sceneName; // �J�ڂ���V�[���̖��O
    [SerializeField] private GameObject optionPanel; // �I�v�V������ʂ�UI�p�l��
    [SerializeField] private bool useEnterKey; // �G���^�[�L�[�ŃV�[����J�ڂ��邩�ǂ���
    [SerializeField] private bool useBackspaceKey; // �o�b�N�X�y�[�X�L�[�ŃV�[����J�ڂ��邩�ǂ���
    [SerializeField] private bool useEscapeKey; // �G�X�P�[�v�L�[�ŃI�v�V������ʂ�\�����邩�ǂ���

    void Start()
    {
        SetupButtonListener();
    }

    void Update()
    {
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

    // �L�[�{�[�h���͂��`�F�b�N���郁�\�b�h
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
