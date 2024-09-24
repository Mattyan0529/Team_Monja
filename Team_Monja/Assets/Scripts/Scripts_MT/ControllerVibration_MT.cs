using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ControllerVibration_MT : MonoBehaviour
{
    [SerializeField] private Toggle _vibrationToggle; // UIのトグル
    [SerializeField] private Scriptableobject_SM _settingsData; // ScriptableObjectを参照

    private void Start()
    {
        if (_vibrationToggle != null)
        {
            // トグルの初期状態をScriptableObjectの値で設定
            _vibrationToggle.isOn = _settingsData.isVibrationEnabled;

            // トグルの状態が変更されたときに呼び出されるメソッドを登録
            _vibrationToggle.onValueChanged.AddListener(OnToggleValueChanged);
        }
    }

    // トグルの状態が変更されたときに呼ばれるメソッド
    private void OnToggleValueChanged(bool isOn)
    {
        _settingsData.isVibrationEnabled = isOn; // ScriptableObjectの値を更新

        if (!isOn)
        {
            StopVibration(); // 振動をオフにしたらすぐに振動を止める
        }
    }

    // コントローラーの振動を開始
    public void VibrateController(float lowFrequency, float highFrequency, float duration)
    {
        if (_settingsData.isVibrationEnabled && Gamepad.current != null)
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
