using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController_MT : MonoBehaviour
{
    [SerializeField] private RenderTexture videoRenderTexture;
    [SerializeField]
    private TimeManager_KH _timeManager;
    [SerializeField, Header("�Đ���A�ړ����Ăق����Ȃ�")]
    private GameObject _nextPos;
    [SerializeField, Header("���ɗ������悪����ꍇ")]
    private VideoPlayerController_MT _nextVideo;

    [SerializeField, Header("HandComing����")]
    private VideoPlayerController_MT[] _lookBackVideos;


    [SerializeField, Header("GOGOHand����")]
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
        if (_timeManager.IsTimeOver && _lookBackVideos.Length != 0)
        {

                PlayLookBackVideo();
            //�I�u�W�F�N�g���B��
            this.gameObject.SetActive(false);
            return;
        }

        //�A���Đ����铮��Ȃ�
        if (_nextVideo != null)
        {
            _nextVideo.PlayVideo();
            //�I�u�W�F�N�g���B��
            this.gameObject.SetActive(false);
        }
        else
        {
            //�v���C���[�����̏ꏊ�Ɉړ�
            if (_nextPos != null)
            {
                _player.transform.position = _nextPos.transform.position;
            }
         
            //���Ԑ؂ꂾ������
            if ((_timeManager != null) && _timeManager.IsTimeOver && _bossGate != null)
            {
                _timeManager.PullPlayer();
                _bossGate.OpenGate();
            }
        }
        //���Ԃ����Ƃɖ߂�
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
