using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController_MT : MonoBehaviour
{
    public RenderTexture videoRenderTexture;
    private VideoPlayer _videoPlayer;

    void Start()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _videoPlayer.loopPointReached += OnVideoEnd;
        _videoPlayer.targetTexture = videoRenderTexture;
        ClearRenderTexture(); // �ŏ��̓e�N�X�`�����N���A���Ă���
    }

    public void PlayVideo()
    {  //���悪�Đ����łȂ����Ƃ��m�F
        if (_videoPlayer != null && !_videoPlayer.isPlaying)
        {
            _videoPlayer.Play();
            Time.timeScale = 0;
        }
    }

    private void OnVideoEnd(VideoPlayer source)
    {
        Debug.Log("Video has ended!");
        Time.timeScale = 1;
        //�I�u�W�F�N�g���B��
        this.gameObject.SetActive(false);
    }

    // RenderTexture���N���A���郁�\�b�h
    private void ClearRenderTexture()
    {
        RenderTexture.active = videoRenderTexture;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = null;
    }
}
