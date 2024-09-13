using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverSelectable_SM : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Selectable _selectable; // スライダー、トグル、ボタンのコンポーネントへの参照
    private Color _originalColor; // 元のUI要素の色
    public Color hoverColor = Color.yellow; // マウスオーバー時の色

    private bool _isPointerOver = false; // マウスがUI要素の上にあるかどうかを記録するフラグ

    internal static GameObject _currentHoveredObject; // 現在マウスオーバーされているオブジェクトを管理

    void Start()
    {
        _selectable = GetComponent<Selectable>(); // スライダー、トグル、ボタンのコンポーネントを取得
        _originalColor = _selectable.targetGraphic.color; // 初期のUI要素の色を保存
    }

    void Update()
    {
        // マウスが重なっている場合、その要素が優先される
        if (_isPointerOver && _currentHoveredObject == gameObject)
        {
            _selectable.targetGraphic.color = hoverColor; // マウスオーバー時の色に変更
        }
        // マウスが重なっていない場合、選択されている要素のみ色を変える
        else if (!_isPointerOver && EventSystem.current.currentSelectedGameObject == gameObject)
        {
            _selectable.targetGraphic.color = hoverColor; // UI要素が選択されている場合、マウスオーバー時の色に変更
        }
        else
        {
            _selectable.targetGraphic.color = _originalColor; // それ以外は元の色に戻す
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isPointerOver = true; // マウスがUI要素の上にあることを記録
        _currentHoveredObject = gameObject; // マウスオーバーされているオブジェクトを記録
        _selectable.targetGraphic.color = hoverColor; // マウスオーバー時の色に変更
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isPointerOver = false; // マウスがUI要素から離れたことを記録

        // マウスが重なっていない時にだけ、選択状態に応じて色を変える
        if (_currentHoveredObject == gameObject)
        {
            _currentHoveredObject = null;
        }

        if (EventSystem.current.currentSelectedGameObject != gameObject)
        {
            _selectable.targetGraphic.color = _originalColor; // 元の色に戻す
        }
    }
}
