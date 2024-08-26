using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
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
    private float _timeDragPlayer = 100f;

    private Animator _handAnimator = default;
    private WordDisplay _wordDisplay = default;
    private BossGate_MT _bossGate = default;

    [SerializeField]
    private WordScriptableObject[] _wordScriptableObject = default;

    [SerializeField]
    private GameObject _damonHand = default;  
    [SerializeField]
    private GameObject _bossGateObj = default;

    int _wordNumber = 0;

    void Start()
    {
        _wordDisplay = GetComponent<WordDisplay>();
        _handAnimator = _damonHand.GetComponent<Animator>();
        _bossGate = _bossGateObj.GetComponent<BossGate_MT>();
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

        // ���Ԑ؂�̏���
        if(_elapsedTimeTotalGame > _timeDragPlayer)
        {
            PullPlayer();
            _bossGate.OpenGate();
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

    private void PullPlayer()
    {
        _damonHand.SetActive(true);
        this.enabled = false;
    }
}
