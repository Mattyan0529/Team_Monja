using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class HoverSlider_SM : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Slider slider; // �X���C�_�[�R���|�[�l���g�ւ̎Q��
    [SerializeField] private Color hoverColor = Color.yellow; // �}�E�X�I�[�o�[���̐F
    [SerializeField] private AudioMixer audioMixer; // AudioMixer�ւ̎Q��
    [SerializeField] private string parameterName = "MyVolume"; // �~���[�g����p�����[�^��

    private Color originalColor; // ���̃X���C�_�[�̐F

    void Start()
    {
        Time.timeScale = 1;
        if (slider == null)
        {
            slider = GetComponent<Slider>(); // �X���C�_�[�R���|�[�l���g���擾
        }
        slider.value = slider.maxValue; // �X���C�_�[�̏����l���ő�l�ɐݒ�
        originalColor = slider.targetGraphic.color; // �����̃X���C�_�[�̐F��ۑ�
        slider.onValueChanged.AddListener(OnSliderValueChanged); // �X���C�_�[�̒l���ς�邽�тɌĂ΂�郊�X�i�[��ǉ�

        // �������ʂ�ݒ�
        SetInitialVolume();
    }

    void Update()
    {
        // �X���C�_�[�����ݑI������Ă��邩���`�F�b�N
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            slider.targetGraphic.color = hoverColor; // �X���C�_�[���I������Ă���ꍇ�A�}�E�X�I�[�o�[���̐F�ɕύX
        }
        else
        {
            slider.targetGraphic.color = originalColor; // �X���C�_�[���I������Ă��Ȃ��ꍇ�A���̐F�ɖ߂�
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        slider.targetGraphic.color = hoverColor; // �}�E�X�I�[�o�[���̐F�ɕύX
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // �X���C�_�[�����ݑI������Ă��Ȃ��ꍇ�̂݌��̐F�ɖ߂�
        if (EventSystem.current.currentSelectedGameObject != gameObject)
        {
            slider.targetGraphic.color = originalColor; // ���̐F�ɖ߂�
        }
    }

    private void SetInitialVolume()
    {
        // �X���C�_�[�̏����l�Ɋ�Â��ĉ��ʂ�ݒ�
        OnSliderValueChanged(slider.value);
    }

    private void OnSliderValueChanged(float value)
    {
        // �X���C�_�[�̒l��ΐ��֐����g����dB�ɕϊ�����AudioMixer�ɐݒ�
        float volume = Mathf.Lerp(-80f, 0f, Mathf.Log10(value + 1) / Mathf.Log10(slider.maxValue + 1));
        audioMixer.SetFloat(parameterName, volume);
    }
}
