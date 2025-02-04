using UnityEngine;
using TMPro;

public class TimeManager_KH : MonoBehaviour
{
    // 言葉を表示するために使う経過時間（言葉が変わるたびリセット）
    private float _elapsedTime = 0f;
    // ゲーム全体の経過時間
    private float _elapsedTimeTotalGame = 0f;

    // 次の言葉が出るまでの時間
    private float _timeStageProgress = 15f;
    // 表示された言葉が非表示になるまでの時間
    private float _timeHideWord = 10f;

    private WordDisplay_KH _wordDisplay = default;
    private GameObject _backGround = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private AudioSource _audioSource = default;

    [SerializeField]
    private WordScriptableObject_KH[] _wordScriptableObject = default;

    private bool _isInCastle = false;

    private bool _isTimeOver = false;

    private int _wordNumber = 0;


    public bool IsTimeOver { get => _isTimeOver; set => _isTimeOver = value; }
    public bool IsInCastle { get => _isInCastle; set => _isInCastle = value; }

    void Start()
    {
        _wordDisplay = GetComponent<WordDisplay_KH>();
        _backGround = transform.parent.Find("WordsBackGround").gameObject;
        _backGround.SetActive(false);
        _soundEffectManagement = GameObject.FindGameObjectWithTag("ResidentScripts").GetComponent<SoundEffectManagement_KH>();
        _audioSource = GetComponent<AudioSource>();
    }

    void LateUpdate()
    {
        UpdateTime();
    }

    /// <summary>
    /// 経過時間を計上する
    /// </summary>
    private void UpdateTime()
    {
        _elapsedTime += Time.deltaTime;
        _elapsedTimeTotalGame += Time.deltaTime;

        // 言葉を変更して表示する場合
        if (_elapsedTime > _timeStageProgress && !_wordDisplay.IsWordDisplay)
        {
            /*_soundEffectManagement.PlayPonPonSound(_audioSource);
            _backGround.SetActive(true);
            DisplayWord();*/
            _elapsedTime = 0f;
        }

        // 言葉を非表示にする場合
        else if (_elapsedTime > _timeHideWord)
        {
            /*_backGround.SetActive(false);
            _wordDisplay.EraseText();*/
        }
    }

    private void DisplayWord()
    {
        if (_wordNumber > _wordScriptableObject.Length - 1) return;
        _wordDisplay.WriteText(_wordScriptableObject[_wordNumber].Word);
        _wordNumber++;
    }
}
