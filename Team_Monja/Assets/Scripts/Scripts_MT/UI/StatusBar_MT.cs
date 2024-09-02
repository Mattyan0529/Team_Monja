using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class StatusBar_MT : MonoBehaviour
{
    #region StatusSelect
    // 表示するステータス
    private string[] _elements = { "ATK", "DEF" };

    // Elementsプロパティで外部からアクセスできるようにする
    public string[] Elements
    {
        get { return _elements; }
        set
        {
            _elements = value;
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
    }

    // 選択されたインデックス
    [SerializeField]
    private int _selectedIndex;
    public int SelectedIndex
    {
        get { return _selectedIndex; }
        set { _selectedIndex = value; }
    }

    // 選択された要素を取得
    public string SelectedElement
    {
        get { return _elements[SelectedIndex]; }
    }
    #endregion

    [SerializeField]
    private RectTransform _bar;
    [SerializeField]
    private RectTransform _value;

    private StatusManager_MT _statusManager;

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーのステータスが設定されている場合はバーの長さを更新
        if (_statusManager != null)
        {
            UpdateBarLength();
        }
        else
        {
            Debug.LogWarning("StatusManager_MT is not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 選択されたステータスに基づいてバーの長さを更新
        UpdateBarLength();
    }

    /// <summary>
    /// 選択されたステータスに基づいてバーの長さを更新
    /// </summary>
    private void UpdateBarLength()
    {
        if (_statusManager == null)
            return;

        float newLength = 10f;  // デフォルトのバーの長さ
        switch (SelectedElement)
        {
            case "ATK":
                newLength = _statusManager.Strength * 37.5f; // ATK値に応じたバーの長さ
                break;
            case "DEF":
                newLength = _statusManager.Defense * 37.5f; // DEF値に応じたバーの長さ
                break;
        }

        // バーの長さを調整
        AdjustBarLength(newLength);
    }

    /// <summary>
    /// バーの長さを調整（左端を固定）
    /// </summary>
    /// <param name="newLength">新しい長さ</param>
    public void AdjustBarLength(float newLength)
    {
        _bar.pivot = new Vector2(0, _bar.pivot.y);
        _bar.anchorMin = new Vector2(0, _bar.anchorMin.y);
        _bar.anchorMax = new Vector2(0, _bar.anchorMax.y);

        _bar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newLength);

        AdjustChildPositions();
    }

    /// <summary>
    /// ステータスの値も動かす
    /// </summary>
    private void AdjustChildPositions()
    {
        float barWidth = _bar.rect.width;
        float barHeight = _bar.rect.height;

        float offset = -1250f;  // 右端からのオフセット距離

        _value.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, barHeight);

        Vector2 newPosition = new Vector2(barWidth + offset, 0);

        _value.anchoredPosition = newPosition;
    }

    // プレイヤーのステータスを取得
    public void SetPlayer(GameObject player)
    {
        _statusManager = player.GetComponent<StatusManager_MT>();

        if (_statusManager != null)
        {
            UpdateBarLength();
        }
    }

    void OnValidate()
    {
        // エディター上でインスペクターの変更が即座に反映されるようにする
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            EditorUtility.SetDirty(this);
        }
#endif
    }
}

#region editor
#if UNITY_EDITOR
[CustomEditor(typeof(StatusBar_MT))]
public class ElementSelectorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        StatusBar_MT selector = (StatusBar_MT)target;

        if (selector.Elements != null && selector.Elements.Length > 0)
        {
            selector.SelectedIndex = EditorGUILayout.Popup("Select Element", selector.SelectedIndex, selector.Elements);
        }
        else
        {
            EditorGUILayout.HelpBox("Elements array is empty!", MessageType.Warning);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
#endif
#endregion