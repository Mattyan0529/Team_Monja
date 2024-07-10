using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverSlider_SM : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Slider slider; // �X���C�_�[�R���|�[�l���g�ւ̎Q��
    private Color originalColor; // ���̃X���C�_�[�̐F
    public Color hoverColor = Color.yellow; // �}�E�X�I�[�o�[���̐F

    void Start()
    {
        Time.timeScale = 1;
        slider = GetComponent<Slider>(); // �X���C�_�[�R���|�[�l���g���擾
        originalColor = slider.targetGraphic.color; // �����̃X���C�_�[�̐F��ۑ�
    }

    void Update()
    {
        // �X���C�_�[�����ݑI������Ă��邩���`�F�b�N
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            slider.targetGraphic.color = hoverColor; // �X���C�_�[���I������Ă���ꍇ�A�}�E�X�I�[�o�[���̐F�ɕύX
        }
        else
        {
            slider.targetGraphic.color = originalColor; // �X���C�_�[���I������Ă��Ȃ��ꍇ�A���̐F�ɖ߂�
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        slider.targetGraphic.color = hoverColor; // �}�E�X�I�[�o�[���̐F�ɕύX
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // �X���C�_�[�����ݑI������Ă��Ȃ��ꍇ�̂݌��̐F�ɖ߂�
        if (EventSystem.current.currentSelectedGameObject != gameObject)
        {
            slider.targetGraphic.color = originalColor; // ���̐F�ɖ߂�
        }
    }
}
