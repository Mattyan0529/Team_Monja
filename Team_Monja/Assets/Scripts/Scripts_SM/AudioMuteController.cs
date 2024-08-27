using UnityEngine;
using UnityEngine.UI;

public class AudioMuteController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _muteThreshold = 0.0f;

    void Start()
    {
        // スライダーの値が変わったときにOnSliderValueChangedを呼び出す
        _volumeSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        // スライダーが下限に到達したらミュート、それ以外はミュート解除
        _audioSource.mute = value <= _muteThreshold;
    }
}
