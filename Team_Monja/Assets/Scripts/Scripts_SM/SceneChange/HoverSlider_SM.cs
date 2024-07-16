using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class HoverSlider_SM : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Slider slider; // スライダーコンポーネントへの参照
    [SerializeField] private Color hoverColor = Color.yellow; // マウスオーバー時の色
    [SerializeField] private AudioMixer audioMixer; // AudioMixerへの参照
    [SerializeField] private string parameterName = "MyVolume"; // ミュートするパラメータ名

    private Color originalColor; // 元のスライダーの色

    void Start()
    {
        Time.timeScale = 1;
        if (slider == null)
        {
            slider = GetComponent<Slider>(); // スライダーコンポーネントを取得
        }
        slider.value = slider.maxValue; // スライダーの初期値を最大値に設定
        originalColor = slider.targetGraphic.color; // 初期のスライダーの色を保存
        slider.onValueChanged.AddListener(OnSliderValueChanged); // スライダーの値が変わるたびに呼ばれるリスナーを追加

        // 初期音量を設定
        SetInitialVolume();
    }

    void Update()
    {
        // スライダーが現在選択されているかをチェック
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            slider.targetGraphic.color = hoverColor; // スライダーが選択されている場合、マウスオーバー時の色に変更
        }
        else
        {
            slider.targetGraphic.color = originalColor; // スライダーが選択されていない場合、元の色に戻す
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        slider.targetGraphic.color = hoverColor; // マウスオーバー時の色に変更
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // スライダーが現在選択されていない場合のみ元の色に戻す
        if (EventSystem.current.currentSelectedGameObject != gameObject)
        {
            slider.targetGraphic.color = originalColor; // 元の色に戻す
        }
    }

    private void SetInitialVolume()
    {
        // スライダーの初期値に基づいて音量を設定
        OnSliderValueChanged(slider.value);
    }

    private void OnSliderValueChanged(float value)
    {
        // スライダーの値を対数関数を使ってdBに変換してAudioMixerに設定
        float volume = Mathf.Lerp(-80f, 0f, Mathf.Log10(value + 1) / Mathf.Log10(slider.maxValue + 1));
        audioMixer.SetFloat(parameterName, volume);
    }
}
