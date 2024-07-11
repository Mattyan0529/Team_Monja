using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverSelectable_SM : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Selectable selectable; // �X���C�_�[�A�g�O���A�{�^���̃R���|�[�l���g�ւ̎Q��
    private Color originalColor; // ����UI�v�f�̐F
    public Color hoverColor = Color.yellow; // �}�E�X�I�[�o�[���̐F

    void Start()
    {
        Time.timeScale = 1;
        selectable = GetComponent<Selectable>(); // �X���C�_�[�A�g�O���A�{�^���̃R���|�[�l���g���擾
        originalColor = selectable.targetGraphic.color; // ������UI�v�f�̐F��ۑ�
    }

    void Update()
    {
        // UI�v�f�����ݑI������Ă��邩���`�F�b�N
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            selectable.targetGraphic.color = hoverColor; // UI�v�f���I������Ă���ꍇ�A�}�E�X�I�[�o�[���̐F�ɕύX
        }
        else
        {
            selectable.targetGraphic.color = originalColor; // UI�v�f���I������Ă��Ȃ��ꍇ�A���̐F�ɖ߂�
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        selectable.targetGraphic.color = hoverColor; // �}�E�X�I�[�o�[���̐F�ɕύX
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // UI�v�f�����ݑI������Ă��Ȃ��ꍇ�̂݌��̐F�ɖ߂�
        if (EventSystem.current.currentSelectedGameObject != gameObject)
        {
            selectable.targetGraphic.color = originalColor; // ���̐F�ɖ߂�
        }
    }
}
