using UnityEngine;
using UnityEngine.EventSystems;

public class MenuNavigation_SM : MonoBehaviour
{
    private EventSystem eventSystem;   // EventSystem���i�[���邽�߂̕ϐ�
    private GameObject selectedObject; // ���ݑI������Ă���I�u�W�F�N�g���i�[���邽�߂̕ϐ�

    void Start()
    {
        eventSystem = EventSystem.current; // ���݂�EventSystem���擾
        selectedObject = eventSystem.firstSelectedGameObject; // �ŏ��ɑI�������Q�[���I�u�W�F�N�g���擾

        // �ŏ��̃{�^���𖾎��I�ɑI����Ԃɂ���
        eventSystem.SetSelectedGameObject(selectedObject);
    }

    void Update()
    {
        // ���L�[�̓��͂��`�F�b�N
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            // ���ݑI������Ă���I�u�W�F�N�g��null�̏ꍇ
            if (eventSystem.currentSelectedGameObject == null)
            {
                // �ŏ��ɑI�������I�u�W�F�N�g���ēx�I��
                eventSystem.SetSelectedGameObject(selectedObject);
            }
            else
            {
                // ���ݑI������Ă���I�u�W�F�N�g���X�V
                selectedObject = eventSystem.currentSelectedGameObject;
            }
        }
    }
}