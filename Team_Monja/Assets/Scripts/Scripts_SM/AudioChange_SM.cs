using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioChange_SM : MonoBehaviour
{
    //Audioミキサーを入れる
    [SerializeField] AudioMixer audioMixer;

    //それぞれのスライダーを入れる
    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider SESlider;
    [SerializeField] Slider VOICESlider;
    // Start is called before the first frame update
    void Start()
    {
        //BGM
        audioMixer.GetFloat("BGM", out float bgmVolume);
        BGMSlider.value = bgmVolume;
        //SE
        audioMixer.GetFloat("SE", out float seVolume);
        SESlider.value = seVolume;
        //VOICE
        audioMixer.GetFloat("VOICE", out float voiceVolume);
        SESlider.value = seVolume;
    }
    public void SetBGM(float volume)
    {
        audioMixer.SetFloat("BGM", volume);
    }

    public void SetSE(float volume)
    {
        audioMixer.SetFloat("SE", volume);
    }

    public void SetVOICE(float volume)
    {
        audioMixer.SetFloat("VOICE", volume);
    }


}
