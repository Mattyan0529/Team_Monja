using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController_MT : MonoBehaviour
{
    [SerializeField]private RenderTexture videoRenderTexture;
    [SerializeField] private GameObject _nextPos;

    [SerializeField, Header("HandComing����")]
    private TimeManager_KH _timeManager;
    [SerializeField, Header("HandComing����")]
    private BossGate_MT _bossGate;
    private VideoPlayer _videoPlayer;
    private GameObject _player;

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
        
        Time.timeScale = 1;
        //�v���C���[�����̂̏ꏊ�Ɉړ�
        _player.transform.position = _nextPos.transform.position;
        if(_timeManager != null)
        {
            _timeManager.PullPlayer();
            _bossGate.OpenGate();
        }
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

    public void SetPlayer(GameObject player)
    {
        _player = player;
    }
}
