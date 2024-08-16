using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    private float _elapsedTime = 0f;

    // ���̌��t���o��܂ł̎���
    private float _timeStageProgress = 20f;
    // �\�����ꂽ���t����\���ɂȂ�܂ł̎���
    private float _timeHideWord = 10f;


    private TextMeshProUGUI _textMeshProUGUI = default;
    [SerializeField]
    private WordScriptableObject[] _wordScriptableObject = default;

    int _wordNumber = 0;

    void Start()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        UpdateTime();
    }

    /// <summary>
    /// �o�ߎ��Ԃ��v�シ��
    /// </summary>
    private void UpdateTime()
    {
        _elapsedTime += Time.deltaTime;

         if (_elapsedTime > _timeStageProgress)
        {
            DisplayWord();
            _elapsedTime = 0f;
        }
        else if (_elapsedTime > _timeHideWord)
        {
            HideWord();
        }
    }

    private void DisplayWord()
    {
        _textMeshProUGUI.text = _wordScriptableObject[_wordNumber].Word;
        _wordNumber++;
    }

    private void HideWord()
    {
        _textMeshProUGUI.text = null;
    }
}
