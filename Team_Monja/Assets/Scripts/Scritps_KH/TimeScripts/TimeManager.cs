using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    // 言葉を表示するために使う経過時間（言葉が変わるたびリセット）
    private float _elapsedTime = 0f;
    // ゲーム全体の経過時間
    private float _elapsedTimeTotalGame = 0f;

    // 次の言葉が出るまでの時間
    private float _timeStageProgress = 20f;
    // 表示された言葉が非表示になるまでの時間
    private float _timeHideWord = 10f;
    // プレイヤーを引きずり始める時間
    private float _timeDragPlayer = 1f;

    private TextMeshProUGUI _textMeshProUGUI = default;
    private DragPlayerToBoss _dragPlayerToBoss = default;
    private Animator _handAnimator = default;

    [SerializeField]
    private WordScriptableObject[] _wordScriptableObject = default;

    [SerializeField]
    private GameObject _damonHand = default;

    int _wordNumber = 0;

    void Start()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        _handAnimator = _damonHand.GetComponent<Animator>();
        Debug.Log(_handAnimator);
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PullPlayer();
        }
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
        if(_elapsedTimeTotalGame > _timeDragPlayer)
        {
            PullPlayer();
        }

        // 言葉を変更して表示する場合
         else if (_elapsedTime > _timeStageProgress && _textMeshProUGUI.text == null)
        {
            DisplayWord();
            _elapsedTime = 0f;
        }

        // 言葉を非表示にする場合
        else if (_elapsedTime > _timeHideWord)
        {
            HideWord();
        }
    }

    private void DisplayWord()
    {
        if (_wordNumber > _wordScriptableObject.Length - 1) return;
        _textMeshProUGUI.text = _wordScriptableObject[_wordNumber].Word;
        _wordNumber++;
    }

    private void HideWord()
    {
        _textMeshProUGUI.text = null;
    }

    private void PullPlayer()
    {
        _damonHand.SetActive(true);
        _dragPlayerToBoss = _damonHand.GetComponent<DragPlayerToBoss>();
        _dragPlayerToBoss.IsDrag = true;
        this.enabled = false;
    }
}
