using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HPStatusUI_MT : MonoBehaviour
{
    TextMeshProUGUI _textMeshProUGUI;

    private void Start()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    public void ChangeText(int NowHP , int MaxHP)
    {
        _textMeshProUGUI.text = (NowHP.ToString() + "/" + MaxHP.ToString());
    }
}
