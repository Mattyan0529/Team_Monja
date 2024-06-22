    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DefenseStatusUI_MT : MonoBehaviour
{
    TextMeshProUGUI _textMeshProUGUI;

    private void Start()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    public void ChangeText(int Defense)
    {
        _textMeshProUGUI.text = Defense.ToString(); 
    }
}
