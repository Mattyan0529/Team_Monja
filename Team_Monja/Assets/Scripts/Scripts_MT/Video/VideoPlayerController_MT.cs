using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController_MT : MonoBehaviour
{
    [SerializeField] private RenderTexture videoRenderTexture;
    [SerializeField]
    private TimeManager_KH _timeManager;
    [SerializeField, Header("再生後、移動してほしいなら")]
    private GameObject _nextPos;
    [SerializeField, Header("次に流す動画がある場合")]
    private VideoPlayerController_MT _nextVideo;

    [SerializeField, Header("HandComingだけ")]
    private VideoPlayerController_MT[] _lookBackVideos;


    [SerializeField, Header("GOGOHandだけ")]
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
        if (_timeManager.IsTimeOver && _lookBackVideos.Length != 0)
        {

                PlayLookBackVideo();
            //オブジェクトを隠す
            this.gameObject.SetActive(false);
            return;
        }

        //連続再生する動画なら
        if (_nextVideo != null)
        {
            _nextVideo.PlayVideo();
            //オブジェクトを隠す
            this.gameObject.SetActive(false);
        }
        else
        {
            //プレイヤーを次の場所に移動
            if (_nextPos != null)
            {
                _player.transform.position = _nextPos.transform.position;
            }
         
            //時間切れだったら
            if ((_timeManager != null) && _timeManager.IsTimeOver && _bossGate != null)
            {
                _timeManager.PullPlayer();
                _bossGate.OpenGate();
            }
        }
        //時間をもとに戻す
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


    private void PlayLookBackVideo()
    {
        switch (_player.name)
        {
            case "slime":
                _lookBackVideos[0].PlayVideo();
                break;
            case "skeleton":
                _lookBackVideos[1].PlayVideo();
                break;
            case "succubus":
                _lookBackVideos[2].PlayVideo();
                break;
            case "berserker":
                _lookBackVideos[3].PlayVideo();
                break;
            case "wight":
                _lookBackVideos[4].PlayVideo();
                break;
            case "mushroom":
                _lookBackVideos[5].PlayVideo();
                break;
            case "lizardman":
                _lookBackVideos[6].PlayVideo();
                break;
        }
    }



    public void SetPlayer(GameObject player)
    {
        _player = player;
    }
}
