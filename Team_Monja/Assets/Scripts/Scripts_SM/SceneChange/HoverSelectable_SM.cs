using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverSelectable_SM : MonoBehaviour
{
    private Selectable _selectable; // �X���C�_�[�A�g�O���A�{�^���̃R���|�[�l���g�ւ̎Q��
    private Color _originalColor; // ����UI�v�f�̐F
    public Color hoverColor = Color.yellow; // �I�����̐F

    private bool _isSelected = false; // �I�u�W�F�N�g���I������Ă��邩�ǂ�����ǐ�

    void Start()
    {
        _selectable = GetComponent<Selectable>(); // �X���C�_�[�A�g�O���A�{�^���̃R���|�[�l���g���擾
        _originalColor = _selectable.targetGraphic.color; // ������UI�v�f�̐F��ۑ�
    }

    void Update()
    {
        // ���ݑI������Ă���I�u�W�F�N�g�����̃Q�[���I�u�W�F�N�g���m�F
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            // �I������Ă���ꍇ�AhoverColor�ɕύX
            _selectable.targetGraphic.color = hoverColor;
            _isSelected = true;
        }
        else if (_isSelected)
        {
            // �I���������ꂽ�ꍇ�A���̐F�ɖ߂�
            _selectable.targetGraphic.color = _originalColor;
            _isSelected = false;
        }
    }
}
