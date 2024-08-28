using UnityEngine;

public class DisplayWordInEvent_KH : MonoBehaviour
{
    private bool _isFirst = true;
    private bool _isDisplay = false;
    private WordDisplay_KH _wordDisplay = default;

    void Start()
    {
        _wordDisplay = GetComponent<WordDisplay_KH>();
    }

    private void Update()
    {
        if (_isDisplay && !_wordDisplay.IsWordDisplay)
        {
            _wordDisplay.WriteText("“|‚µ‚½“G‚ÍE‚ÅH‚×‚éAQ‚Åæ‚èˆÚ‚ê‚é‚æI");
            _isDisplay = false;
        }
    }

    public void KillEnemyForFirstTime()
    {
        if (!_isFirst) return;

        _isFirst = false;
        _isDisplay = true;
    }
}
