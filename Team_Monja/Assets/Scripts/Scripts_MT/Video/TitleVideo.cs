using UnityEngine;
using UnityEngine.Video;

public class TitleVideo : MonoBehaviour
{
    private VideoPlayer _videoPlayer; // VideoPlayer�̎Q��
    private bool _isPlay = false; // �Đ���Ԃ̃t���O

    [SerializeField]
    private Canvas _canvas; // �L�����o�X�̎Q��

    [SerializeField]
    private BackGroundMusicManagement_KH _bgmManager; // BGM�Ǘ��X�N���v�g�̎Q��

    public bool IsPlay { get => _isPlay; private set => _isPlay = value; } // �t���O�̃v���p�e�B

    private void Start()
    {
        // VideoPlayer�̃R���|�[�l���g���擾
        _videoPlayer = GetComponent<VideoPlayer>();

        // ���悪�I�������Ƃ��̃C�x���g��o�^
        if (_videoPlayer != null)
        {
            _videoPlayer.loopPointReached += OnVideoEnd;
            gameObject.SetActive(false); // ������Ԃœ�����\���ɂ���
        }

        if (_canvas != null)
        {
            _canvas.gameObject.SetActive(true); // ������ԂŃL�����o�X��\���ɂ���
        }
    }

    private void Update()
    {
        // ���悪�Đ����̏ꍇ�A�L�[���͂��`�F�b�N
        if (_isPlay && Input.GetButton("MenuButton"))
        {
            StopVideo(); // �L�[�������ꂽ�瓮����~
        }
    }

    public void PlayVideo()
    {
        // ���悪�Đ����łȂ����Ƃ��m�F���čĐ�
        if (_videoPlayer != null && !_videoPlayer.isPlaying)
        {
            _isPlay = true;
            gameObject.SetActive(true); // ����I�u�W�F�N�g��\��

            // ���悪�Đ�����鎞�ɃL�����o�X���\���ɂ��A���y���~�߂�
            if (_canvas != null)
            {
                _canvas.gameObject.SetActive(false);
            }

            // BGM���~
            if (_bgmManager != null)
            {
                _bgmManager.StopMusic();
            }

            _videoPlayer.Play();
        }
    }

    public void StopVideo()
    {
        if (_videoPlayer != null && _videoPlayer.isPlaying)
        {
            _videoPlayer.Stop(); // ������~
            OnVideoEnd(_videoPlayer); // ����I���������蓮�ŌĂяo��
        }
    }

    // ���悪�I�������Ƃ��ɌĂяo����郁�\�b�h
    private void OnVideoEnd(VideoPlayer vp)
    {
        _isPlay = false; // �Đ��t���O�����Z�b�g
        gameObject.SetActive(false); // ����I�u�W�F�N�g���\���ɂ���

        // ����I����ɃL�����o�X���ĕ\��
        if (_canvas != null)
        {
            _canvas.gameObject.SetActive(true);
        }

        // �^�C�g����BGM���Đ�
        if (_bgmManager != null)
        {
            _bgmManager.PlayTitleMusic();
        }

        Debug.Log("���悪�I�����܂����I");
    }
}
