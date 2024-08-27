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
        // �X���C�_�[�̒l���ς�����Ƃ���OnSliderValueChanged���Ăяo��
        _volumeSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        // �X���C�_�[�������ɓ��B������~���[�g�A����ȊO�̓~���[�g����
        _audioSource.mute = value <= _muteThreshold;
    }
}
