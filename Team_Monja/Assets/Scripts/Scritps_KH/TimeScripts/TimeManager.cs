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
    private float _timeDragPlayer = 150f;

    private GameObject _player = default;

    private TextMeshProUGUI _textMeshProUGUI = default;
    private DragPlayerToBoss _dragPlayerToBoss = default;

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
        _elapsedTimeTotalGame += Time.deltaTime;

        // �v���C���[����������ꍇ
        if(_elapsedTimeTotalGame > _timeDragPlayer)
        {
            PullPlayer();
        }

        // ���t��ύX���ĕ\������ꍇ
         else if (_elapsedTime > _timeStageProgress)
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
        _textMeshProUGUI.text = _wordScriptableObject[_wordNumber].Word;
        _wordNumber++;
    }

    private void HideWord()
    {
        _textMeshProUGUI.text = null;
    }

    private void PullPlayer()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _dragPlayerToBoss = _player.GetComponent<DragPlayerToBoss>();
        _dragPlayerToBoss.IsDrag = true;
        this.enabled = false;
    }
}
