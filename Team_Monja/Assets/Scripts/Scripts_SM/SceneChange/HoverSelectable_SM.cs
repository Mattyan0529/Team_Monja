using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverSelectable_SM : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Selectable _selectable; // �X���C�_�[�A�g�O���A�{�^���̃R���|�[�l���g�ւ̎Q��
    private Color _originalColor; // ����UI�v�f�̐F
    public Color hoverColor = Color.yellow; // �}�E�X�I�[�o�[���̐F

    private bool _isPointerOver = false; // �}�E�X��UI�v�f�̏�ɂ��邩�ǂ������L�^����t���O

    void Start()
    {
        Time.timeScale = 1;
        _selectable = GetComponent<Selectable>(); // �X���C�_�[�A�g�O���A�{�^���̃R���|�[�l���g���擾
        _originalColor = _selectable.targetGraphic.color; // ������UI�v�f�̐F��ۑ�
    }

    void Update()
    {
        // UI�v�f�����ݑI������Ă��邩�A�܂��̓}�E�X����ɂ��邩���`�F�b�N
        if (EventSystem.current.currentSelectedGameObject == gameObject || _isPointerOver)
        {
            _selectable.targetGraphic.color = hoverColor; // UI�v�f���I������Ă��邩�}�E�X�I�[�o�[����Ă���ꍇ�A�}�E�X�I�[�o�[���̐F�ɕύX
        }
        else
        {
            _selectable.targetGraphic.color = _originalColor; // UI�v�f���I������Ă��Ȃ��ꍇ�A���̐F�ɖ߂�
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isPointerOver = true; // �}�E�X��UI�v�f�̏�ɂ��邱�Ƃ��L�^
        _selectable.targetGraphic.color = hoverColor; // �}�E�X�I�[�o�[���̐F�ɕύX
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isPointerOver = false; // �}�E�X��UI�v�f���痣�ꂽ���Ƃ��L�^

        // UI�v�f�����ݑI������Ă��Ȃ��ꍇ�̂݌��̐F�ɖ߂�
        if (EventSystem.current.currentSelectedGameObject != gameObject)
        {
            _selectable.targetGraphic.color = _originalColor; // ���̐F�ɖ߂�
        }
    }
}
