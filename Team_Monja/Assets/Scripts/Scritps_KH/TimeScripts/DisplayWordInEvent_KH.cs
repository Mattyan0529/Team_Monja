using UnityEngine;

public class DisplayWordInEvent_KH : MonoBehaviour
{
    private bool _isFirst = true;
    private bool _isDisplay = false;
    private WordDisplay_KH _wordDisplay = default;

    private string[] _words = { "“|‚µ‚½“G‚ÍE‚ÅH‚×‚éAQ‚Åæ‚èˆÚ‚ê‚é‚æI", "“G‚ª‹ß‚­‚É‚¢‚é‚©‚çH‚×‚ç‚ê‚È‚¢‚æI" };

    private enum WordNum
    {
        /// <summary>
        /// ‰‚ß‚Ä“G‚ğ“|‚µ‚½ƒeƒLƒXƒg‚ğ•\¦‚µ‚½‚¢‚Æ‚«
        /// </summary>
        KillEnemy,

        /// <summary>
        /// “G‚Ì‹ß‚­‚Å“|‚µ‚½“G‚ğH‚×‚æ‚¤‚Æ‚µ‚½‚Æ‚«
        /// </summary>
        EatEnemy,

        /// <summary>
        /// “Á‚É•\¦‚·‚é—\’è‚ª‚È‚¢
        /// </summary>
        None
    }

    private WordNum _displayWordNum = WordNum.None;

    void Start()
    {
        _wordDisplay = GetComponent<WordDisplay_KH>();
    }

    private void Update()
    {
        if (_displayWordNum == WordNum.KillEnemy && !_wordDisplay.IsWordDisplay)
        {
            _wordDisplay.WriteText(_words[(int)WordNum.KillEnemy]);
            _displayWordNum = WordNum.None;
        }

        if(_displayWordNum == WordNum.EatEnemy && !_wordDisplay.IsWordDisplay)
        {
            _wordDisplay.WriteText(_words[(int)WordNum.EatEnemy]);
            _displayWordNum = WordNum.None;
        }
    }

    public void KillEnemyForFirstTime()
    {
        if (!_isFirst) return;

        _isFirst = false;
        _displayWordNum = WordNum.KillEnemy;
    }

    public void EatWhenNearEnemy()
    {
        _displayWordNum = WordNum.EatEnemy;
    }
}
