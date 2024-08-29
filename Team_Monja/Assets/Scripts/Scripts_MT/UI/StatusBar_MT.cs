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
            // �G�f�B�^�[�ł̕ύX�𔽉f�����邽�߂ɁA�Ώۂ̃I�u�W�F�N�g���}�[�N����
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
    }

    // �I�����ꂽ�C���f�b�N�X
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
    private GameObject _bar;

    private StatusManager_MT _statusManager;
    private string _useStatus;

    // ���̍��[�̍��W
    private Vector3 _initialLeftEdge;

    // Start is called before the first frame update
    void Start()
    {
        // �����̍��[�̍��W���v�Z���ċL��
        _initialLeftEdge = _bar.transform.position - _bar.transform.right * (_bar.transform.localScale.x / 2);

        // ���ݑI������Ă���X�e�[�^�X���擾
        _useStatus = SelectedElement;

        // ������ԂŃX�e�[�^�X�ɉ������o�[�̒�����ݒ�
        UpdateBarLength();
    }

    // Update is called once per frame
    void Update()
    {
        // �v���C���[�̃X�e�[�^�X���������擾����Ă��邩�`�F�b�N
        if (_statusManager == null)
        {
            Debug.LogWarning("StatusManager_MT is not assigned.");
            return;
        }

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

        // �X�e�[�^�X�Ɋ�Â��ēK�؂Ȓl�Ƀo�[�̒�����ݒ�
        float newLength = 10f;  // �f�t�H���g�̃o�[�̒���
        switch (SelectedElement)
        {
            case "ATK":
                newLength = _statusManager.Strength * 0.1f; // ATK�l�ɉ������o�[�̒���
                break;
            case "DEF":
                newLength = _statusManager.Defense * 0.1f; // DEF�l�ɉ������o�[�̒���
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
        // ���݂̃X�P�[�����擾
        Vector3 barScale = _bar.transform.localScale;

        // �X�P�[����X��ύX
        float previousScaleX = barScale.x;
        barScale.x = newLength;
        _bar.transform.localScale = barScale;

        // �X�P�[����ύX�����������ʒu��␳�i���[���Œ肷��j
        float adjustment = (newLength - previousScaleX) / 2;
        _bar.transform.position -= _bar.transform.right * adjustment;

        // ���[�������̈ʒu�ɖ߂�
        _bar.transform.position = _initialLeftEdge + _bar.transform.right * (newLength / 2);
    }

    // �v���C���[�̃X�e�[�^�X���擾
    public void SetPlayer(GameObject player)
    {
        _statusManager = player.GetComponent<StatusManager_MT>();

        // �X�e�[�^�X���ݒ肳�ꂽ��A�o�[�̒������X�V
        if (_statusManager != null)
        {
            UpdateBarLength();
        }
    }
}

#region editor
/// <summary>
/// �h���b�v�_�E�����j���[�őI�ׂ�悤�ɂ���
/// </summary>
#if UNITY_EDITOR
[CustomEditor(typeof(StatusBar_MT))]
public class ElementSelectorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // �x�[�X�̃C���X�y�N�^�[��\��
        base.OnInspectorGUI();

        // StatusBar_MT�X�N���v�g�ւ̎Q�Ƃ��擾
        StatusBar_MT selector = (StatusBar_MT)target;

        // �z�񂪋�łȂ��ꍇ�A�h���b�v�_�E�����j���[��\��
        if (selector.Elements != null && selector.Elements.Length > 0)
        {
            // �C���f�b�N�X���h���b�v�_�E���őI��
            selector.SelectedIndex = EditorGUILayout.Popup("Select Element", selector.SelectedIndex, selector.Elements);
        }
        else
        {
            EditorGUILayout.HelpBox("Elements array is empty!", MessageType.Warning);
        }

        // �C���X�y�N�^�[�̕ύX�𔽉f
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
#endif
#endregion
