using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DemonTextDisplay : MonoBehaviour
{
    // �Z���t�̃��X�g
    public string[] texts;
    // ���ݕ\�����Ă���Z���t�̃C���f�b�N�X
    int _textNumber;
    // ���ݕ\�����Ă���e�L�X�g���e
    string _displayText;
    // ���ݕ\�����Ă��镶���̃C���f�b�N�X
    int _textCharNumber;
    // �\�����x���Ǘ�����J�E���^�[
    private int _displayTextSpeed = 1000;
    // �e�L�X�g�̍Đ��𐧌䂷��t���O
    bool _speakBool = true;
    // �N���b�N���͂����o���邽�߂̃t���O
    bool _click;
    // �e�L�X�g�\�����I���������ǂ������Ǘ�����t���O
    bool _textStop;
    // ���ׂẴe�L�X�g���\�����ꂽ�����Ǘ�����t���O
    bool _allTextsDisplayed = false;
    // TextMeshProUGUI�R���|�[�l���g�̎Q��
    TextMeshProUGUI _textMeshPro;
    // �Z���t�̍Ō�̕�����\�����I�������𔻒肷��t���O
    private bool _finishedSentence = false;
    // �V�[���J�ڗp�I�u�W�F�N�g�̎Q��
    public GameObject sceneTransitionObject;

    void Start()
    {
        // TextMeshProUGUI�R���|�[�l���g���擾
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        // �V�[���J�ڗp�I�u�W�F�N�g���A�N�e�B�u�ɐݒ�
        if (sceneTransitionObject != null)
        {
            sceneTransitionObject.SetActive(false);
        }
    }

    void Update()
    {
        // ���ׂẴe�L�X�g���\������Ă��Ȃ��ꍇ�ɏ������s��
        if (!_allTextsDisplayed)
        {
            // �e�L�X�g�\�����I�����Ă��Ȃ��ꍇ�ɕ\�����X�V
            if (_textStop == false)
            {
                HandleTextDisplay();
                HandleInput();
            }
        }
        else
        {
            // ���ׂẴe�L�X�g���\�����ꂽ��A�V�[���J�ڃI�u�W�F�N�g��L���ɂ���
            if (sceneTransitionObject != null && !sceneTransitionObject.activeSelf)
            {
                sceneTransitionObject.SetActive(true);
                Debug.Log("Scene transition object activated.");
            }
        }
    }

    // �e�L�X�g�\���̏������s��
    void HandleTextDisplay()
    {
        // �\�����x�̃J�E���^�[���C���N�������g
        _displayTextSpeed++;
        if (_displayTextSpeed % 25 == 0) // ��葬�x�ŕ�����\��
        {
            if (_textCharNumber < texts[_textNumber].Length) // �܂��\�����镶�����c���Ă���ꍇ
            {
                // ���̕�����ǉ����ĕ\��
                _displayText += texts[_textNumber][_textCharNumber];
                _textCharNumber++;
                _finishedSentence = false; // �Z���t�����S�ɕ\������Ă��Ȃ�
            }
            else // ���݂̃Z���t���S�ĕ\�����ꂽ�ꍇ
            {
                HandleTextEnd(); // �e�L�X�g�̏I���������Ăяo��
            }

            // �X�V���ꂽ�e�L�X�g��\��
            _textMeshPro.text = _displayText;
            _click = false; // �N���b�N�t���O�����Z�b�g
        }
    }

    // ���[�U�[���͂���������
    void HandleInput()
    {
        // �}�E�X�N���b�N�����o�����ꍇ
        if (Input.GetMouseButtonDown(0))
        {
            _click = true; // �N���b�N�t���O���Z�b�g
            _finishedSentence = false; // �Z���t�����S�ɕ\������Ă��Ȃ����Ƃ𖾎�
            Debug.Log(_finishedSentence); // �f�o�b�O�p�Ƀt���O�̏�Ԃ��o��
        }
    }

    // �Z���t���S�ĕ\�����ꂽ�Ƃ��̏���
    void HandleTextEnd()
    {
        if (_textNumber < texts.Length - 1) // ���̃Z���t�����݂���ꍇ
        {
            if (_click) // ���[�U�[���N���b�N�����ꍇ
            {
                ResetForNextText(); // ���̃Z���t��\�����鏀�����s��
            }
        }
        else // �Ō�̃Z���t���\�����ꂽ�ꍇ
        {
            if (_click) // ���[�U�[���N���b�N�����ꍇ
            {
                _displayText = ""; // �\���e�L�X�g�����Z�b�g
                _textCharNumber = 0; // �����C���f�b�N�X�����Z�b�g
                _textStop = true; // �e�L�X�g�\���I���t���O���Z�b�g
                _allTextsDisplayed = true; // ���ׂẴe�L�X�g���\�����ꂽ���Ƃ�����
                Debug.Log("All texts have been displayed."); // �f�o�b�O�p�Ƀ��b�Z�[�W���o��
            }
        }
        _finishedSentence = true; // �Z���t�̍Ō�̕������o���I�������Ƃ�����
    }

    // ���̃Z���t��\�����鏀�����s��
    void ResetForNextText()
    {
        _displayText = ""; // �\���e�L�X�g�����Z�b�g
        _textCharNumber = 0; // �����C���f�b�N�X�����Z�b�g
        _textNumber++; // ���̃Z���t�ɐi��
        _finishedSentence = false; // �Z���t�̍Ō�̕������o���I����Ă��Ȃ����Ƃ�����
    }

    // ���ׂẴe�L�X�g���\�����ꂽ�����m�F���邽�߂̃v���p�e�B
    public bool AllTextsDisplayed
    {
        get { return _allTextsDisplayed; }
    }

    // �Z���t�̍Ō�̕������\�����ꂽ�����m�F���邽�߂̃v���p�e�B
    public bool FinishedSentence
    {
        get { return _finishedSentence; }
    }
}
