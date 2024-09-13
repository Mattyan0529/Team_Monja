using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverSelectable_SM : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Selectable _selectable; // �X���C�_�[�A�g�O���A�{�^���̃R���|�[�l���g�ւ̎Q��
    private Color _originalColor; // ����UI�v�f�̐F
    public Color hoverColor = Color.yellow; // �}�E�X�I�[�o�[���̐F

    private bool _isPointerOver = false; // �}�E�X��UI�v�f�̏�ɂ��邩�ǂ������L�^����t���O

    internal static GameObject _currentHoveredObject; // ���݃}�E�X�I�[�o�[����Ă���I�u�W�F�N�g���Ǘ�

    void Start()
    {
        _selectable = GetComponent<Selectable>(); // �X���C�_�[�A�g�O���A�{�^���̃R���|�[�l���g���擾
        _originalColor = _selectable.targetGraphic.color; // ������UI�v�f�̐F��ۑ�
    }

    void Update()
    {
        // �}�E�X���d�Ȃ��Ă���ꍇ�A���̗v�f���D�悳���
        if (_isPointerOver && _currentHoveredObject == gameObject)
        {
            _selectable.targetGraphic.color = hoverColor; // �}�E�X�I�[�o�[���̐F�ɕύX
        }
        // �}�E�X���d�Ȃ��Ă��Ȃ��ꍇ�A�I������Ă���v�f�̂ݐF��ς���
        else if (!_isPointerOver && EventSystem.current.currentSelectedGameObject == gameObject)
        {
            _selectable.targetGraphic.color = hoverColor; // UI�v�f���I������Ă���ꍇ�A�}�E�X�I�[�o�[���̐F�ɕύX
        }
        else
        {
            _selectable.targetGraphic.color = _originalColor; // ����ȊO�͌��̐F�ɖ߂�
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isPointerOver = true; // �}�E�X��UI�v�f�̏�ɂ��邱�Ƃ��L�^
        _currentHoveredObject = gameObject; // �}�E�X�I�[�o�[����Ă���I�u�W�F�N�g���L�^
        _selectable.targetGraphic.color = hoverColor; // �}�E�X�I�[�o�[���̐F�ɕύX
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isPointerOver = false; // �}�E�X��UI�v�f���痣�ꂽ���Ƃ��L�^

        // �}�E�X���d�Ȃ��Ă��Ȃ����ɂ����A�I����Ԃɉ����ĐF��ς���
        if (_currentHoveredObject == gameObject)
        {
            _currentHoveredObject = null;
        }

        if (EventSystem.current.currentSelectedGameObject != gameObject)
        {
            _selectable.targetGraphic.color = _originalColor; // ���̐F�ɖ߂�
        }
    }
}
