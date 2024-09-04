using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CanvasChange_SM : MonoBehaviour
{
    // �\������L�����o�X���Q�Ƃ��邽�߂̕ϐ�
    [SerializeField]
    private GameObject _ScreenToDisplay;

    // ��\���ɂ���L�����o�X���Q�Ƃ��邽�߂̕ϐ�
    [SerializeField]
    private GameObject _ScreenToDelete;

    // �{�^�����Q�Ƃ��邽�߂̕ϐ�
    [SerializeField]
    private Button _toggleButton;

    // �C���X�y�N�^�[�ŃG�X�P�[�v�L�[�ƃ{�^���̐؂�ւ����s�����߂̕ϐ�
    [SerializeField]
    private bool _useEscapeKey = true;

    //�G���^�[�L�[�ƃ{�^���̐؂�ւ����s�����߂̕ϐ�
    [SerializeField]
    private bool _useEnterKey = true;

    // �V�����L�����o�X���\�����ꂽ�Ƃ��ɍŏ��ɑI�������{�^��
    [SerializeField]
    private Button firstSelectedButton;

    private GameObject lastSelectedObject;

    void Start()
    {
        // �{�^���̃N���b�N�C�x���g�Ƀ��\�b�h��o�^����
        if (_toggleButton != null && !_useEscapeKey)
        {
            _toggleButton.onClick.AddListener(ToggleOptions);
        }
    }

    void Update()
    {
        // �G�X�P�[�v�L�[�������ꂽ��
        if (_useEscapeKey && Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOptions();
        }
        //�G���^�[�L�[�������ꂽ��
        if (_useEnterKey && Input.GetButtonDown("Submit"))
        {
            ToggleOptions();
        }

        
    }

    // �I�v�V������ʂ̕\��/��\����؂�ւ��郁�\�b�h
    void ToggleOptions()
    {
        if (_ScreenToDisplay != null && _ScreenToDelete != null)
        {
            bool isOptionsActive = _ScreenToDisplay.activeSelf;

            // ���ݑI������Ă���I�u�W�F�N�g��ێ�����
            if (!isOptionsActive)
            {
                if (EventSystem.current != null)
                {
                    lastSelectedObject = EventSystem.current.currentSelectedGameObject;
                }
            }

            // �I�v�V������ʂ�؂�ւ���
            _ScreenToDisplay.SetActive(!isOptionsActive);

            // ���̃L�����o�X�̕\��/��\����؂�ւ���
            _ScreenToDelete.SetActive(isOptionsActive);

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
