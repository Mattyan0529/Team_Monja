using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerVibration_MT : MonoBehaviour
{
    

    // コントローラーの振動を開始
    public void VibrateController(float lowFrequency, float highFrequency, float duration)
    {
        if (Gamepad.current != null)
        {
            // コントローラーを振動させる
            Gamepad.current.SetMotorSpeeds(lowFrequency, highFrequency);

            // 一定時間後に振動を止める
            Invoke(nameof(StopVibration), duration);
        }
    }

    // 振動を止めるメソッド
    private void StopVibration()
    {
        if (Gamepad.current != null)
        {
            // 振動を止める
            Gamepad.current.SetMotorSpeeds(0, 0);
        }
    }
}
