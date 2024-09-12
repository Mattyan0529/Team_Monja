using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerVibration_MT : MonoBehaviour
{
    

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
