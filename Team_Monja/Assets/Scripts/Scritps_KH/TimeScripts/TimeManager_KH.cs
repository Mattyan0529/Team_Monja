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
    // プレイヤーを引きずり始める時間
    private float _timeDragPlayer = 30f;

    private Animator _handAnimator = default;
    private WordDisplay_KH _wordDisplay = default;

    [SerializeField]
    private WordScriptableObject_KH[] _wordScriptableObject = default;

    [SerializeField]
    private GameObject _damonHand = default;
    [SerializeField]
    private VideoPlayerController_MT _HandComingVideo;

    private bool _isInCastle = false;

    private bool _isTimeOver = false;

    private int _wordNumber = 0;


    public bool IsTimeOver { get => _isTimeOver; set => _isTimeOver = value; }
    public bool IsInCastle { get => _isInCastle; set => _isInCastle = value; }

    void Start()
    {
        _wordDisplay = GetComponent<WordDisplay_KH>();
        _handAnimator = _damonHand.GetComponent<Animator>();
        Debug.Log(_handAnimator);
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

        // プレイヤーを引きずる場合
        if (_elapsedTimeTotalGame > _timeDragPlayer)
        {
            IsTimeOver = true;
            _damonHand.GetComponent<DragPlayerToBoss_KH>().Isdrag = true;
            _HandComingVideo.PlayVideo();
        }

        // 言葉を変更して表示する場合
        else if (_elapsedTime > _timeStageProgress && !_wordDisplay.IsWordDisplay)
        {
            DisplayWord();
            _elapsedTime = 0f;
        }

        // 言葉を非表示にする場合
        else if (_elapsedTime > _timeHideWord)
        {
            _wordDisplay.EraseText();
        }
    }

    private void DisplayWord()
    {
        if (_wordNumber > _wordScriptableObject.Length - 1) return;
        _wordDisplay.WriteText(_wordScriptableObject[_wordNumber].Word);
        _wordNumber++;
    }

    public void PullPlayer()
    {
        if (!_isInCastle)
        {
            _damonHand.SetActive(true);

        }
        this.enabled = false;
    }
}
