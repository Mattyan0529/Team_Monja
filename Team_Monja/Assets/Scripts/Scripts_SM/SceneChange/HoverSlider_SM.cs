using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverSlider_SM : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Slider slider; // スライダーコンポーネントへの参照
    private Color originalColor; // 元のスライダーの色
    public Color hoverColor = Color.yellow; // マウスオーバー時の色

    void Start()
    {
        Time.timeScale = 1;
        slider = GetComponent<Slider>(); // スライダーコンポーネントを取得
        originalColor = slider.targetGraphic.color; // 初期のスライダーの色を保存
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
}
