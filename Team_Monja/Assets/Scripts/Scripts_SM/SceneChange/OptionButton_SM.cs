using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionButton_SM : MonoBehaviour
{
    // �I�v�V������ʂ̃L�����o�X���Q�Ƃ��邽�߂̕ϐ�
    [SerializeField]
    private GameObject ScreenToDisplay;

    // ���̃L�����o�X���Q�Ƃ��邽�߂̕ϐ�
    [SerializeField]
    private GameObject ScreenToDelete;

    // �{�^�����Q�Ƃ��邽�߂̕ϐ�
    [SerializeField]
    private Button toggleButton;

    // �C���X�y�N�^�[�ŃG�X�P�[�v�L�[�ƃ{�^���̐؂�ւ����s�����߂̕ϐ�
    [SerializeField]
    private bool useEscapeKey = true;

    // �V�����L�����o�X���\�����ꂽ�Ƃ��ɍŏ��ɑI�������{�^��
    [SerializeField]
    private Button firstSelectedButton;

    private GameObject lastSelectedObject;

    void Start()
    {
        // �{�^���̃N���b�N�C�x���g�Ƀ��\�b�h��o�^����
        if (toggleButton != null && !useEscapeKey)
        {
            toggleButton.onClick.AddListener(ToggleOptions);
        }
    }

    void Update()
    {
        // �G�X�P�[�v�L�[�������ꂽ��
        if (useEscapeKey && Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOptions();
        }
    }

    // �I�v�V������ʂ̕\��/��\����؂�ւ��郁�\�b�h
    void ToggleOptions()
    {
        if (ScreenToDisplay != null && ScreenToDelete != null)
        {
            bool isOptionsActive = ScreenToDisplay.activeSelf;

            // ���ݑI������Ă���I�u�W�F�N�g��ێ�����
            if (!isOptionsActive)
            {
                if (EventSystem.current != null)
                {
                    lastSelectedObject = EventSystem.current.currentSelectedGameObject;
                }
            }

            // �I�v�V������ʂ�؂�ւ���
            ScreenToDisplay.SetActive(!isOptionsActive);

            // ���̃L�����o�X�̕\��/��\����؂�ւ���
            ScreenToDelete.SetActive(isOptionsActive);

            // �I�v�V������ʂ�\��������A�ŏ��̑I���{�^����ݒ肷��
            if (!isOptionsActive && firstSelectedButton != null)
            {
                if (EventSystem.current != null)
                {
                    EventSystem.current.SetSelectedGameObject(firstSelectedButton.gameObject);
                }
            }

            // �I�v�V������ʂ��\���ɖ߂�����A�O�̑I���𕜌�����
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
