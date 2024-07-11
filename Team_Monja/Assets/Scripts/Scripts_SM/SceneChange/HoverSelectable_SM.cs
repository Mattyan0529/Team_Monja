using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverSelectable_SM : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Selectable selectable; // スライダー、トグル、ボタンのコンポーネントへの参照
    private Color originalColor; // 元のUI要素の色
    public Color hoverColor = Color.yellow; // マウスオーバー時の色

    void Start()
    {
        Time.timeScale = 1;
        selectable = GetComponent<Selectable>(); // スライダー、トグル、ボタンのコンポーネントを取得
        originalColor = selectable.targetGraphic.color; // 初期のUI要素の色を保存
    }

    void Update()
    {
        // UI要素が現在選択されているかをチェック
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            selectable.targetGraphic.color = hoverColor; // UI要素が選択されている場合、マウスオーバー時の色に変更
        }
        else
        {
            selectable.targetGraphic.color = originalColor; // UI要素が選択されていない場合、元の色に戻す
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        selectable.targetGraphic.color = hoverColor; // マウスオーバー時の色に変更
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // UI要素が現在選択されていない場合のみ元の色に戻す
        if (EventSystem.current.currentSelectedGameObject != gameObject)
        {
            selectable.targetGraphic.color = originalColor; // 元の色に戻す
        }
    }
}
