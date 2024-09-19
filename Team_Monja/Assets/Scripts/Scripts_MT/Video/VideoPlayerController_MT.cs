using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController_MT : MonoBehaviour
{
    [SerializeField]private RenderTexture videoRenderTexture;
    [SerializeField] private GameObject _nextPos;

    [SerializeField, Header("HandComingだけ")]
    private TimeManager_KH _timeManager;
    [SerializeField, Header("HandComingだけ")]
    private BossGate_MT _bossGate;
    private VideoPlayer _videoPlayer;
    private GameObject _player;

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
        
        Time.timeScale = 1;
        //プレイヤーを次のの場所に移動
        _player.transform.position = _nextPos.transform.position;
        if(_timeManager != null)
        {
            _timeManager.PullPlayer();
            _bossGate.OpenGate();
        }
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

    public void SetPlayer(GameObject player)
    {
        _player = player;
    }
}
