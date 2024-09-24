using UnityEngine;
using UnityEngine.Video;

public class TitleVideo : MonoBehaviour
{
    private VideoPlayer _videoPlayer; // VideoPlayer�̎Q��
    private bool _isPlay = false; // �Đ���Ԃ̃t���O

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
    }

    private void Update()
    {
        // ���悪�Đ����̏ꍇ�A�L�[���͂��`�F�b�N
        if (_isPlay && Input.anyKeyDown)
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
        Debug.Log("���悪�I�����܂����I");
        // �����ɓ���I����̏������L�q
    }
}
