using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ControllerVibration_MT : MonoBehaviour
{
    [SerializeField] private Toggle _vibrationToggle; // UI�̃g�O��
    [SerializeField] private Scriptableobject_SM _settingsData; // ScriptableObject���Q��

    private void Start()
    {
        if (_vibrationToggle != null)
        {
            // �g�O���̏�����Ԃ�ScriptableObject�̒l�Őݒ�
            _vibrationToggle.isOn = _settingsData.isVibrationEnabled;

            // �g�O���̏�Ԃ��ύX���ꂽ�Ƃ��ɌĂяo����郁�\�b�h��o�^
            _vibrationToggle.onValueChanged.AddListener(OnToggleValueChanged);
        }
    }

    // �g�O���̏�Ԃ��ύX���ꂽ�Ƃ��ɌĂ΂�郁�\�b�h
    private void OnToggleValueChanged(bool isOn)
    {
        _settingsData.isVibrationEnabled = isOn; // ScriptableObject�̒l���X�V

        if (!isOn)
        {
            StopVibration(); // �U�����I�t�ɂ����炷���ɐU�����~�߂�
        }
    }

    // �R���g���[���[�̐U�����J�n
    public void VibrateController(float lowFrequency, float highFrequency, float duration)
    {
        if (_settingsData.isVibrationEnabled && Gamepad.current != null)
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
