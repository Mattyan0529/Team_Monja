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
    private GameObject _bar;

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
                newLength = _statusManager.Strength * 0.1f; // ATK値に応じたバーの長さ
                break;
            case "DEF":
                newLength = _statusManager.Defense * 0.1f; // DEF値に応じたバーの長さ
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
        // 現在のスケールを取得
        Vector3 barScale = _bar.transform.localScale;

        // スケールのXを変更
        float previousScaleX = barScale.x;
        barScale.x = newLength;
        _bar.transform.localScale = barScale;

        // スケールを変更した分だけ位置を補正（左端を固定する）
        float adjustment = (newLength - previousScaleX) / 2;
        _bar.transform.position -= _bar.transform.right * adjustment;

        // 左端を初期の位置に戻す
        _bar.transform.position = _initialLeftEdge + _bar.transform.right * (newLength / 2);
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
