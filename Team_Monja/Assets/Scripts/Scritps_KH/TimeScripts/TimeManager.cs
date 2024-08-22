using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    // ���t��\�����邽�߂Ɏg���o�ߎ��ԁi���t���ς�邽�у��Z�b�g�j
    private float _elapsedTime = 0f;
    // �Q�[���S�̂̌o�ߎ���
    private float _elapsedTimeTotalGame = 0f;

    // ���̌��t���o��܂ł̎���
    private float _timeStageProgress = 20f;
    // �\�����ꂽ���t����\���ɂȂ�܂ł̎���
    private float _timeHideWord = 10f;
    // �v���C���[����������n�߂鎞��
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
    /// �o�ߎ��Ԃ��v�シ��
    /// </summary>
    private void UpdateTime()
    {
        _elapsedTime += Time.deltaTime;
        _elapsedTimeTotalGame += Time.deltaTime;

        // �v���C���[����������ꍇ
        if(_elapsedTimeTotalGame > _timeDragPlayer)
        {
            PullPlayer();
        }

        // ���t��ύX���ĕ\������ꍇ
         else if (_elapsedTime > _timeStageProgress && _textMeshProUGUI.text == null)
        {
            DisplayWord();
            _elapsedTime = 0f;
        }

        // ���t���\���ɂ���ꍇ
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
