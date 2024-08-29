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

    void Start()
    {
        // �ǋL�F�k
        _soundEffectManagement = _audioObj.GetComponent<SoundEffectManagement_KH>();
        _audioSource = _audioObj.GetComponents<AudioSource>();

        _eventSystem = EventSystem.current; // ���݂�EventSystem���擾
        _selectedObject = _eventSystem.firstSelectedGameObject; // �ŏ��ɑI�������Q�[���I�u�W�F�N�g���擾

        // �ŏ��̃{�^���𖾎��I�ɑI����Ԃɂ���
        _eventSystem.SetSelectedGameObject(_selectedObject);
    }

    void Update()
    {
        // ���L�[�̓��͂��`�F�b�N
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) ||
            Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
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

            // �}�E�X�I�[�o�[���̃I�u�W�F�N�g���I������Ă���ꍇ�A������N���A
            if (HoverSelectable_SM._currentHoveredObject == _selectedObject)
            {
                HoverSelectable_SM._currentHoveredObject = null;
            }

            // 1�ڂ�AudioSource����SE��炷�iAudioSource�͕ʂɂǂ����ł������j�F�k
            _soundEffectManagement.PlayBigSelectionSound(_audioSource[0]);
        }
    }
}
