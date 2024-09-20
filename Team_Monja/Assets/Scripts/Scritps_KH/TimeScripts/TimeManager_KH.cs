using UnityEngine;
using TMPro;

public class TimeManager_KH : MonoBehaviour
{
    // ���t��\�����邽�߂Ɏg���o�ߎ��ԁi���t���ς�邽�у��Z�b�g�j
    private float _elapsedTime = 0f;
    // �Q�[���S�̂̌o�ߎ���
    private float _elapsedTimeTotalGame = 0f;

    // ���̌��t���o��܂ł̎���
    private float _timeStageProgress = 15f;
    // �\�����ꂽ���t����\���ɂȂ�܂ł̎���
    private float _timeHideWord = 10f;
    // �v���C���[����������n�߂鎞��
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
    /// �o�ߎ��Ԃ��v�シ��
    /// </summary>
    private void UpdateTime()
    {
        _elapsedTime += Time.deltaTime;
        _elapsedTimeTotalGame += Time.deltaTime;

        // �v���C���[����������ꍇ
        if (_elapsedTimeTotalGame > _timeDragPlayer)
        {
            IsTimeOver = true;
            _damonHand.GetComponent<DragPlayerToBoss_KH>().Isdrag = true;
            _HandComingVideo.PlayVideo();
        }

        // ���t��ύX���ĕ\������ꍇ
        else if (_elapsedTime > _timeStageProgress && !_wordDisplay.IsWordDisplay)
        {
            DisplayWord();
            _elapsedTime = 0f;
        }

        // ���t���\���ɂ���ꍇ
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
