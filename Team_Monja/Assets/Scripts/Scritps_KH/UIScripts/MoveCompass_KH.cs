using System.Runtime.CompilerServices;
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

    private bool _isRotation = false;

    private GameObject _player;

    private void Start()
    {
        if (_rotationMapToggle != null)
        {
            // �g�O���̏�Ԃ��ύX���ꂽ�Ƃ��ɌĂяo����郁�\�b�h��o�^
            _rotationMapToggle.onValueChanged.AddListener(OnToggleValueChanged);
            _isRotation = _rotationMapToggle.isOn; // �g�O���̏�����Ԃ��t���O�ɐݒ�
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
        _isRotation = isOn;

        if (!_isRotation)
        {
            gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            _CompassImage.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }


    public void SetPlayer(GameObject player)
    {
        _player = player;
    }

    /// <summary>
    /// �~�j�}�b�v��A�C�R������]������
    /// </summary>
    private void UpdateOrientation()
    {
        // �����̈ʒu
        Vector3 myPos = _player.transform.position;
        // �ړI�n
        Vector3 target = _destinationObj.transform.position;

        // ���ʎ��΂�������p�x
        Vector3 direction = target-myPos;

        direction.y = 0f;

        if (direction == Vector3.zero) return;        // �����Ă��Ȃ��Ƃ��͏��������Ȃ��i_difference��0��������G���[�ɂȂ�j

        // Y���̉�]��Z���̉�]�Ƃ��Ĕ��f����
        Quaternion newDirection = Quaternion.LookRotation(direction, Vector3.up);

        float angle;

        // �v���C���[�̎��_�Ɉˑ����ĉ�]���邩
        if (_isRotation)
        {
            angle = _player.transform.rotation.eulerAngles.y - newDirection.eulerAngles.y;
            _CompassImage.transform.parent = gameObject.transform;
        }
        else
        {
            angle = newDirection.eulerAngles.y;
            _CompassImage.transform.parent = gameObject.transform.parent;
        }

        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
