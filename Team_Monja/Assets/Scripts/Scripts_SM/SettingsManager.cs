using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Scriptableobject_SM _settingsData;
    [SerializeField] private Toggle _vibrationToggle;
    [SerializeField] private Toggle _mapRotationToggle;

    private void Start()
    {
        // UI�̃g�O����`ScriptableObject`�̒l�ŏ�����
        _vibrationToggle.isOn = _settingsData.isVibrationEnabled;
        _mapRotationToggle.isOn = _settingsData.isMapRotationEnabled;

        // �g�O���̕ύX�C�x���g��ݒ�
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
