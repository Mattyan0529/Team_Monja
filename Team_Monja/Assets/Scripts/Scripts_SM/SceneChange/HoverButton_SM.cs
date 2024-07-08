using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverButton_SM : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button; // �{�^���R���|�[�l���g�ւ̎Q��
    private Color originalColor; // ���̃{�^���̐F
    public Color hoverColor = Color.yellow; // �}�E�X�I�[�o�[���̐F

    void Start()
    {
        Time.timeScale = 1;
        button = GetComponent<Button>(); // �{�^���R���|�[�l���g���擾
        originalColor = button.image.color; // �����̃{�^���̐F��ۑ�
    }

    void Update()
    {
        // �{�^�������ݑI������Ă��邩���`�F�b�N
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            button.image.color = hoverColor; // �{�^�����I������Ă���ꍇ�A�}�E�X�I�[�o�[���̐F�ɕύX
        }
        else
        {
            button.image.color = originalColor; // �{�^�����I������Ă��Ȃ��ꍇ�A���̐F�ɖ߂�
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        button.image.color = hoverColor; // �}�E�X�I�[�o�[���̐F�ɕύX
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // �{�^�������ݑI������Ă��Ȃ��ꍇ�̂݌��̐F�ɖ߂�
        if (EventSystem.current.currentSelectedGameObject != gameObject)
        {
            button.image.color = originalColor; // ���̐F�ɖ߂�
        }
    }
}