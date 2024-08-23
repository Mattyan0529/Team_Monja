using UnityEngine;
using TMPro;

public class WordDisplay : MonoBehaviour
{
    private TextMeshProUGUI _textMeshProUGUI = default;
    private bool _isWordDisplay = false;

    public bool IsWordDisplay
    {
        get { return _isWordDisplay; }
    }

    void Start()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    public void WriteText(string word)
    {
        _textMeshProUGUI.text = word;
        _isWordDisplay = true;
    }

    public void EraseText()
    {
        _textMeshProUGUI.text = null;
        _isWordDisplay = false;
    }
}
