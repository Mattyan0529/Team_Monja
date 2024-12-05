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
    /// �o�ߎ��Ԃ��v�シ��
    /// </summary>
    private void UpdateTime()
    {
        _elapsedTime += Time.deltaTime;
        _elapsedTimeTotalGame += Time.deltaTime;

        // ���t��ύX���ĕ\������ꍇ
        if (_elapsedTime > _timeStageProgress && !_wordDisplay.IsWordDisplay)
        {
            /*_soundEffectManagement.PlayPonPonSound(_audioSource);
            _backGround.SetActive(true);
            DisplayWord();*/
            _elapsedTime = 0f;
        }

        // ���t���\���ɂ���ꍇ
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
