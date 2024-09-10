using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerVibration_MT : MonoBehaviour
{
    private void Update()
    {
        // Xbox One �R���g���[���[��B�{�^������������U��
        if (Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame)
        {
            // �U���J�n
            VibrateController(1f, 1f, 1.0f);
        }
    }

    // �R���g���[���[�̐U�����J�n
    public void VibrateController(float lowFrequency, float highFrequency, float duration)
    {
        if (Gamepad.current != null)
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
