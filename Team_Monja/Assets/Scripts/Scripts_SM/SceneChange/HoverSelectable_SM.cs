using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverSelectable_SM : MonoBehaviour
{
    private Selectable _selectable; // スライダー、トグル、ボタンのコンポーネントへの参照
    private Color _originalColor; // 元のUI要素の色
    public Color hoverColor = Color.yellow; // 選択時の色

    private bool _isSelected = false; // オブジェクトが選択されているかどうかを追跡

    void Start()
    {
        _selectable = GetComponent<Selectable>(); // スライダー、トグル、ボタンのコンポーネントを取得
        _originalColor = _selectable.targetGraphic.color; // 初期のUI要素の色を保存
    }

    void Update()
    {
        // 現在選択されているオブジェクトがこのゲームオブジェクトか確認
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            // 選択されている場合、hoverColorに変更
            _selectable.targetGraphic.color = hoverColor;
            _isSelected = true;
        }
        else if (_isSelected)
        {
            // 選択解除された場合、元の色に戻す
            _selectable.targetGraphic.color = _originalColor;
            _isSelected = false;
        }
    }
}
