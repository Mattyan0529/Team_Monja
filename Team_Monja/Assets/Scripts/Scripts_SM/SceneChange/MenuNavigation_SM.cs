using UnityEngine;
using UnityEngine.EventSystems;

public class MenuNavigation_SM : MonoBehaviour
{
    // �ǋL�F�k
    [SerializeField]
    private GameObject _audioObj = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private AudioSource[] _audioSource = default;

    private EventSystem _eventSystem;   // EventSystem���i�[���邽�߂̕ϐ�
    private GameObject _selectedObject; // ���ݑI������Ă���I�u�W�F�N�g���i�[���邽�߂̕ϐ�
    private GameObject _previousSelectedObject; // �ǉ��F�O��I������Ă����I�u�W�F�N�g

    void Start()
    {
        // �ǋL�F�k
        _soundEffectManagement = _audioObj.GetComponent<SoundEffectManagement_KH>();
        _audioSource = _audioObj.GetComponents<AudioSource>();

        _eventSystem = EventSystem.current; // ���݂�EventSystem���擾
        _selectedObject = _eventSystem.firstSelectedGameObject; // �ŏ��ɑI�������Q�[���I�u�W�F�N�g���擾
        _previousSelectedObject = _selectedObject; // �ǉ��F���������ɑO��̑I���������ɐݒ�

        // �ŏ��̃{�^���𖾎��I�ɑI����Ԃɂ���
        _eventSystem.SetSelectedGameObject(_selectedObject);
    }

    void Update()
    {
        // GetAxis���g���ē��͂��`�F�b�N�i�Q�[���p�b�h�Ή��j�F�k
        float horizontal = Input.GetAxis("Horizontal"); // ���������̓��͂��擾
        float vertical = Input.GetAxis("Vertical");     // ���������̓��͂��擾

        // ���͂��������ꍇ�ɏ������s��
        if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
        {
            // ���ݑI������Ă���I�u�W�F�N�g��null�̏ꍇ
            if (_eventSystem.currentSelectedGameObject == null)
            {
                // �ŏ��ɑI�������I�u�W�F�N�g���ēx�I��
                _eventSystem.SetSelectedGameObject(_selectedObject);
            }
            else
            {
                // ���ݑI������Ă���I�u�W�F�N�g���X�V
                _selectedObject = _eventSystem.currentSelectedGameObject;
            }

            // �I���I�u�W�F�N�g���O��ƈقȂ�ꍇ�ɂ̂݉����Đ�����
            if (_selectedObject != _previousSelectedObject)
            {
                // 1�ڂ�AudioSource����SE��炷
                _soundEffectManagement.PlayBigSelectionSound(_audioSource[0]);
                Debug.Log("SE");

                // �O��I������Ă����I�u�W�F�N�g���X�V
                _previousSelectedObject = _selectedObject;
            }

            // �}�E�X�I�[�o�[���̃I�u�W�F�N�g���I������Ă���ꍇ�A������N���A
            if (HoverSelectable_SM._currentHoveredObject == _selectedObject)
            {
                HoverSelectable_SM._currentHoveredObject = null;
            }
        }
    }
}
