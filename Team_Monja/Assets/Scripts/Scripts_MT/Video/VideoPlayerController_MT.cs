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
        ClearRenderTexture(); // 最初はテクスチャをクリアしておく
    }

    public void PlayVideo()
    {  //動画が再生中でないことを確認
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
        //オブジェクトを隠す
        this.gameObject.SetActive(false);
    }

    // RenderTextureをクリアするメソッド
    private void ClearRenderTexture()
    {
        RenderTexture.active = videoRenderTexture;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = null;
    }
}
