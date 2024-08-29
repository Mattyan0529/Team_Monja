using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverSelectable_SM : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Selectable _selectable; // スライダー、トグル、ボタンのコンポーネントへの参照
    private Color _originalColor; // 元のUI要素の色
    public Color hoverColor = Color.yellow; // マウスオーバー時の色

    private bool _isPointerOver = false; // マウスがUI要素の上にあるかどうかを記録するフラグ

    void Start()
    {
        Time.timeScale = 1;
        _selectable = GetComponent<Selectable>(); // スライダー、トグル、ボタンのコンポーネントを取得
        _originalColor = _selectable.targetGraphic.color; // 初期のUI要素の色を保存
    }

    void Update()
    {
        // UI要素が現在選択されているか、またはマウスが上にあるかをチェック
        if (EventSystem.current.currentSelectedGameObject == gameObject || _isPointerOver)
        {
            _selectable.targetGraphic.color = hoverColor; // UI要素が選択されているかマウスオーバーされている場合、マウスオーバー時の色に変更
        }
        else
        {
            _selectable.targetGraphic.color = _originalColor; // UI要素が選択されていない場合、元の色に戻す
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isPointerOver = true; // マウスがUI要素の上にあることを記録
        _selectable.targetGraphic.color = hoverColor; // マウスオーバー時の色に変更
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isPointerOver = false; // マウスがUI要素から離れたことを記録

        // UI要素が現在選択されていない場合のみ元の色に戻す
        if (EventSystem.current.currentSelectedGameObject != gameObject)
        {
            _selectable.targetGraphic.color = _originalColor; // 元の色に戻す
        }
    }
}
