using UnityEngine;
using UnityEngine.UI; // Toggle�̂��߂ɒǉ�
using UnityEngine.InputSystem;

public class ControllerVibration_MT : MonoBehaviour
{
    [SerializeField] private Toggle _vibrationToggle; // UI�̃g�O�����A�T�C�����邽�߂̃t�B�[���h
    private bool _isVibrationEnabled = true; // �U���̃I���I�t���Ǘ�����t���O

    private void Start()
    {
        if (_vibrationToggle != null)
        {
            // �g�O���̏�Ԃ��ύX���ꂽ�Ƃ��ɌĂяo����郁�\�b�h��o�^
            _vibrationToggle.onValueChanged.AddListener(OnToggleValueChanged);
            _isVibrationEnabled = _vibrationToggle.isOn; // �g�O���̏�����Ԃ��t���O�ɐݒ�
        }
    }

    // �g�O���̏�Ԃ��ύX���ꂽ�Ƃ��ɌĂ΂�郁�\�b�h
    private void OnToggleValueChanged(bool isOn)
    {
        _isVibrationEnabled = isOn;

        if (!_isVibrationEnabled)
        {
            StopVibration(); // �U�����I�t�ɂ����炷���ɐU�����~�߂�
        }
    }

    // �R���g���[���[�̐U�����J�n
    public void VibrateController(float lowFrequency, float highFrequency, float duration)
    {
        if (_isVibrationEnabled && Gamepad.current != null)
        {
            // �R���g���[���[��U��������
            Gamepad.current.SetMotorSpeeds(lowFrequency, highFrequency);

            // ��莞�Ԍ�ɐU�����~�߂�
            Invoke(nameof(StopVibration), duration);
        }
    }

    // �U�����~�߂郁�\�b�h
    private void StopVibration()
    {
        if (Gamepad.current != null)
        {
            // �U�����~�߂�
            Gamepad.current.SetMotorSpeeds(0, 0);
        }
    }
}
