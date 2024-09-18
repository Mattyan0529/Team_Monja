using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Scriptableobject_SM _settingsData;
    [SerializeField] private Toggle _vibrationToggle;
    [SerializeField] private Toggle _mapRotationToggle;

    private void Start()
    {
        // UIのトグルを`ScriptableObject`の値で初期化
        _vibrationToggle.isOn = _settingsData.isVibrationEnabled;
        _mapRotationToggle.isOn = _settingsData.isMapRotationEnabled;

        // トグルの変更イベントを設定
        _vibrationToggle.onValueChanged.AddListener(OnVibrationToggleChanged);
        _mapRotationToggle.onValueChanged.AddListener(OnMapRotationToggleChanged);
    }

    private void OnVibrationToggleChanged(bool isOn)
    {
        _settingsData.isVibrationEnabled = isOn;
    }

    private void OnMapRotationToggleChanged(bool isOn)
    {
        _settingsData.isMapRotationEnabled = isOn;
    }
}
