using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class StatusBar_MT : MonoBehaviour
{
    #region StatusSelect
    // �\������X�e�[�^�X
    private string[] _elements = { "ATK", "DEF" };

    // Elements�v���p�e�B�ŊO������A�N�Z�X�ł���悤�ɂ���
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

    // �I�����ꂽ�C���f�b�N�X
    [SerializeField]
    private int _selectedIndex;
    public int SelectedIndex
    {
        get { return _selectedIndex; }
        set { _selectedIndex = value; }
    }

    // �I�����ꂽ�v�f���擾
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
        // �v���C���[�̃X�e�[�^�X���ݒ肳��Ă���ꍇ�̓o�[�̒������X�V
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
        // �I�����ꂽ�X�e�[�^�X�Ɋ�Â��ăo�[�̒������X�V
        UpdateBarLength();
    }

    /// <summary>
    /// �I�����ꂽ�X�e�[�^�X�Ɋ�Â��ăo�[�̒������X�V
    /// </summary>
    private void UpdateBarLength()
    {
        if (_statusManager == null)
            return;

        float newLength = 10f;  // �f�t�H���g�̃o�[�̒���
        switch (SelectedElement)
        {
            case "ATK":
                newLength = _statusManager.Strength * 37.5f; // ATK�l�ɉ������o�[�̒���
                break;
            case "DEF":
                newLength = _statusManager.Defense * 37.5f; // DEF�l�ɉ������o�[�̒���
                break;
        }

        // �o�[�̒����𒲐�
        AdjustBarLength(newLength);
    }

    /// <summary>
    /// �o�[�̒����𒲐��i���[���Œ�j
    /// </summary>
    /// <param name="newLength">�V��������</param>
    public void AdjustBarLength(float newLength)
    {
        _bar.pivot = new Vector2(0, _bar.pivot.y);
        _bar.anchorMin = new Vector2(0, _bar.anchorMin.y);
        _bar.anchorMax = new Vector2(0, _bar.anchorMax.y);

        _bar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newLength);

        AdjustChildPositions();
    }

    /// <summary>
    /// �X�e�[�^�X�̒l��������
    /// </summary>
    private void AdjustChildPositions()
    {
        float barWidth = _bar.rect.width;
        float barHeight = _bar.rect.height;

        float offset = -1250f;  // �E�[����̃I�t�Z�b�g����

        _value.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, barHeight);

        Vector2 newPosition = new Vector2(barWidth + offset, 0);

        _value.anchoredPosition = newPosition;
    }

    // �v���C���[�̃X�e�[�^�X���擾
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
        // �G�f�B�^�[��ŃC���X�y�N�^�[�̕ύX�������ɔ��f�����悤�ɂ���
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