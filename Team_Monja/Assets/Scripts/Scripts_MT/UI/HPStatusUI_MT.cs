using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HPStatusUI_MT : MonoBehaviour
{
    TextMeshProUGUI _textMeshProUGUI;

    private void Start()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        Debug.Log(_textMeshProUGUI);
    }

    public void ChangeText(int NowHP , int MaxHP)
    {
        Debug.Log(_textMeshProUGUI);
        _textMeshProUGUI.text = (NowHP.ToString() + "/" + MaxHP.ToString());
    }
}
