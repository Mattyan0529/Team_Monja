using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverButton_SM : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button; // ボタンコンポーネントへの参照
    private Color originalColor; // 元のボタンの色
    public Color hoverColor = Color.yellow; // マウスオーバー時の色

    void Start()
    {
        Time.timeScale = 1;
        button = GetComponent<Button>(); // ボタンコンポーネントを取得
        originalColor = button.image.color; // 初期のボタンの色を保存
    }

    void Update()
    {
        // ボタンが現在選択されているかをチェック
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            button.image.color = hoverColor; // ボタンが選択されている場合、マウスオーバー時の色に変更
        }
        else
        {
            button.image.color = originalColor; // ボタンが選択されていない場合、元の色に戻す
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        button.image.color = hoverColor; // マウスオーバー時の色に変更
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // ボタンが現在選択されていない場合のみ元の色に戻す
        if (EventSystem.current.currentSelectedGameObject != gameObject)
        {
            button.image.color = originalColor; // 元の色に戻す
        }
    }
}