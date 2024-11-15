using UnityEngine;

public class DisplayWordInEvent_KH : MonoBehaviour
{
    private bool _isFirst = true;
    private bool _isDisplay = false;
    private WordDisplay_KH _wordDisplay = default;

    private string[] _words = { "倒した敵はEで食べる、Qで乗り移れるよ！", "敵が近くにいるから食べられないよ！" };

    private enum WordNum
    {
        /// <summary>
        /// 初めて敵を倒したテキストを表示したいとき
        /// </summary>
        KillEnemy,

        /// <summary>
        /// 敵の近くで倒した敵を食べようとしたとき
        /// </summary>
        EatEnemy,

        /// <summary>
        /// 特に表示する予定がない時
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
