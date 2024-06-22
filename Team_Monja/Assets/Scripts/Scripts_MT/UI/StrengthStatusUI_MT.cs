using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StrengthStatusUI_MT : MonoBehaviour
{
    TextMeshProUGUI _textMeshProUGUI;

    private void Start()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    public void ChangeText(int Strength)
    {
        _textMeshProUGUI.text = Strength.ToString();
    }
}
