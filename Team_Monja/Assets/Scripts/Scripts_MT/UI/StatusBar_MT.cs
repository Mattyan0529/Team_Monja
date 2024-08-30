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
            // エディターでの変更を反映させるために、対象のオブジェクトをマークする
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
    }

    // 選択されたインデックス
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
    private string _useStatus;

    // 元の左端の座標
    private Vector3 _initialLeftEdge;

    // Start is called before the first frame update
    void Start()
    {
        // 初期の左端の座標を計算して記憶
        _initialLeftEdge = _bar.transform.position - _bar.transform.right * (_bar.transform.localScale.x / 2);

        // 現在選択されているステータスを取得
        _useStatus = SelectedElement;

        // 初期状態でステータスに応じたバーの長さを設定
        UpdateBarLength();
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーのステータスが正しく取得されているかチェック
        if (_statusManager == null)
        {
            Debug.LogWarning("StatusManager_MT is not assigned.");
            return;
        }

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

        // ステータスに基づいて適切な値にバーの長さを設定
        float newLength = 10f;  // デフォルトのバーの長さ
        switch (SelectedElement)
        {
            case "ATK":
                newLength = _statusManager.Strength * 75; // ATK値に応じたバーの長さ
                break;
            case "DEF":
                newLength = _statusManager.Defense * 75; // DEF値に応じたバーの長さ
                break;
        }
        Debug.Log(newLength);
        // バーの長さを調整
        AdjustBarLength(newLength);
    }

    /// <summary>
    /// バーの長さを調整（左端を固定）
    /// </summary>
    /// <param name="newLength">新しい長さ</param>
    public void AdjustBarLength(float newLength)
    {
        // バーのアンカーとピボットを左端に設定（これにより左端が固定される）
        _bar.pivot = new Vector2(0, _bar.pivot.y);
        _bar.anchorMin = new Vector2(0, _bar.anchorMin.y);
        _bar.anchorMax = new Vector2(0, _bar.anchorMax.y);

        // 現在のバーのサイズを取得
        Vector2 barSize = _bar.rect.size;

        // 新しいサイズを設定
        _bar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newLength);

        // バーの右端に子オブジェクトを配置するための位置調整
        AdjustChildPositions();
    }

    /// <summary>
    /// ステータスの値も動かす
    /// </summary>
    private void AdjustChildPositions()
    {
        // バーの幅と高さを取得
        float barWidth = _bar.rect.width;
        float barHeight = _bar.rect.height;

        // オフセットを適用する（任意の値に変更可能）
        float offset = -1250f;  // 右端からのオフセット距離

        // _value のサイズをバーと一致させる
        _value.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, barHeight);

        // _value の新しい位置を計算（_value のアンカーが左側に設定されている前提）
        Vector2 newPosition = new Vector2(barWidth + offset, 0);

        // 新しい位置を設定
        _value.anchoredPosition = newPosition;
    }





    // プレイヤーのステータスを取得
    public void SetPlayer(GameObject player)
    {
        _statusManager = player.GetComponent<StatusManager_MT>();

        // ステータスが設定されたら、バーの長さを更新
        if (_statusManager != null)
        {
            UpdateBarLength();
        }
    }
}

#region editor
/// <summary>
/// ドロップダウンメニューで選べるようにする
/// </summary>
#if UNITY_EDITOR
[CustomEditor(typeof(StatusBar_MT))]
public class ElementSelectorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // ベースのインスペクターを表示
        base.OnInspectorGUI();

        // StatusBar_MTスクリプトへの参照を取得
        StatusBar_MT selector = (StatusBar_MT)target;

        // 配列が空でない場合、ドロップダウンメニューを表示
        if (selector.Elements != null && selector.Elements.Length > 0)
        {
            // インデックスをドロップダウンで選択
            selector.SelectedIndex = EditorGUILayout.Popup("Select Element", selector.SelectedIndex, selector.Elements);
        }
        else
        {
            EditorGUILayout.HelpBox("Elements array is empty!", MessageType.Warning);
        }

        // インスペクターの変更を反映
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
#endif
#endregion
