using UnityEngine;
using UnityEngine.UI; // Toggleのために追加
using UnityEngine.InputSystem;

public class ControllerVibration_MT : MonoBehaviour
{
    [SerializeField] private Toggle _vibrationToggle; // UIのトグルをアサインするためのフィールド
    private bool _isVibrationEnabled = true; // 振動のオンオフを管理するフラグ

    private void Start()
    {
        if (_vibrationToggle != null)
        {
            // トグルの状態が変更されたときに呼び出されるメソッドを登録
            _vibrationToggle.onValueChanged.AddListener(OnToggleValueChanged);
            _isVibrationEnabled = _vibrationToggle.isOn; // トグルの初期状態をフラグに設定
        }
    }

    // トグルの状態が変更されたときに呼ばれるメソッド
    private void OnToggleValueChanged(bool isOn)
    {
        _isVibrationEnabled = isOn;

        if (!_isVibrationEnabled)
        {
            StopVibration(); // 振動をオフにしたらすぐに振動を止める
        }
    }

    // コントローラーの振動を開始
    public void VibrateController(float lowFrequency, float highFrequency, float duration)
    {
        if (_isVibrationEnabled && Gamepad.current != null)
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
