using UnityEngine;

public class DisplayWordInEvent_KH : MonoBehaviour
{
    private bool _isFirst = true;
    private bool _isDisplay = false;
    private WordDisplay_KH _wordDisplay = default;

    private string[] _words = { "�|�����G��E�ŐH�ׂ�AQ�ŏ��ڂ���I", "�G���߂��ɂ��邩��H�ׂ��Ȃ���I" };

    private enum WordNum
    {
        /// <summary>
        /// ���߂ēG��|�����e�L�X�g��\���������Ƃ�
        /// </summary>
        KillEnemy,

        /// <summary>
        /// �G�̋߂��œ|�����G��H�ׂ悤�Ƃ����Ƃ�
        /// </summary>
        EatEnemy,

        /// <summary>
        /// ���ɕ\������\�肪�Ȃ���
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
