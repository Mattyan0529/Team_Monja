using UnityEngine;

public class DisplayWordInEvent : MonoBehaviour
{
    private bool _isFirst = true;
    private bool _isDisplay = false;
    private WordDisplay _wordDisplay = default;

    void Start()
    {
        _wordDisplay = GetComponent<WordDisplay>();
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
