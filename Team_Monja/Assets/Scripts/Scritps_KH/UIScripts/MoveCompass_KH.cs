using UnityEngine;
using UnityEngine.UI;

public class MoveCompass_KH : MonoBehaviour
{
    // �ړI�n�ƂȂ�I�u�W�F�N�g
    [Header("�{�X������Ă�")]
    [SerializeField]
    private GameObject _destinationObj = default;

    [SerializeField]
    private GameObject _CompassImage = default;

    [SerializeField]
    private Toggle _rotationMapToggle;

    [SerializeField]
    private Scriptableobject_SM _settingsData; // ScriptableObject�̎Q��

    private GameObject _player;
    private Quaternion _initialPlayerRotation; // �v���C���[�̏�����]

    private void Start()
    {
        if (_rotationMapToggle != null)
        {
            // �g�O���̏�����Ԃ�ScriptableObject�̒l�Őݒ�
            _rotationMapToggle.isOn = _settingsData.isMapRotationEnabled;

            // �g�O���̏�Ԃ��ύX���ꂽ�Ƃ��ɌĂяo����郁�\�b�h��o�^
            _rotationMapToggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        // ScriptableObject�̒l�����[�J���ϐ��ɔ��f
        OnToggleValueChanged(_settingsData.isMapRotationEnabled);

        if (_player != null)
        {
            _initialPlayerRotation = _player.transform.rotation; // �v���C���[�̏�����]��ۑ�
        }
    }

    void Update()
    {
        UpdateOrientation();
    }

    /// <summary>
    /// �g�O���̒l���؂�ւ�����Ƃ��ɌĂ΂�郁�\�b�h
    /// </summary>
    /// <param name="isOn"></param>
    private void OnToggleValueChanged(bool isOn)
    {
        _settingsData.isMapRotationEnabled = isOn; // ScriptableObject�̒l���X�V

        if (!isOn)
        {
            gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            _CompassImage.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }

    public void SetPlayer(GameObject player)
    {
        _player = player;
        _initialPlayerRotation = _player.transform.rotation; // �v���C���[���ύX���ꂽ���ɏ�����]���X�V
    }

    /// <summary>
    /// �~�j�}�b�v��A�C�R������]������
    /// </summary>
    private void UpdateOrientation()
    {
        if (_player == null || _destinationObj == null) return;

        // �v���C���[�̈ʒu
        Vector3 myPos = _player.transform.position;
        // �ړI�n
        Vector3 target = _destinationObj.transform.position;

        // ���ʎ��΂�������p�x
        Vector3 direction = target - myPos;
        direction.y = 0f;

        if (direction == Vector3.zero) return; // �����Ă��Ȃ��Ƃ��͏��������Ȃ�

        // Y���̉�]��Z���̉�]�Ƃ��Ĕ��f����
        Quaternion newDirection = Quaternion.LookRotation(direction, Vector3.up);

        float angle;

        // �J�����̉�]����ɂ��邩�ǂ���
        if (_settingsData.isMapRotationEnabled)
        {
            // �v���C���[�̏�����]����ɃJ������Y���̉�]�𒲐�
            angle = Camera.main.transform.rotation.eulerAngles.y - _initialPlayerRotation.eulerAngles.y - newDirection.eulerAngles.y;
            _CompassImage.transform.parent = gameObject.transform;
        }
        else
        {
            // �J�����̉�]���l�����Ȃ��ꍇ
            angle = newDirection.eulerAngles.y;
            _CompassImage.transform.parent = gameObject.transform.parent;
        }

        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
