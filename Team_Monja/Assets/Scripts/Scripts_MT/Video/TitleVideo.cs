using UnityEngine;
using UnityEngine.Video;

public class TitleVideo : MonoBehaviour
{
    private VideoPlayer _videoPlayer; // VideoPlayerの参照
    private bool _isPlay = false; // 再生状態のフラグ

    public bool IsPlay { get => _isPlay; private set => _isPlay = value; } // フラグのプロパティ

    private void Start()
    {
        // VideoPlayerのコンポーネントを取得
        _videoPlayer = GetComponent<VideoPlayer>();

        // 動画が終了したときのイベントを登録
        if (_videoPlayer != null)
        {
            _videoPlayer.loopPointReached += OnVideoEnd;
            gameObject.SetActive(false); // 初期状態で動画を非表示にする
        }
    }

    private void Update()
    {
        // 動画が再生中の場合、キー入力をチェック
        if (_isPlay && Input.anyKeyDown)
        {
            StopVideo(); // キーが押されたら動画を停止
        }
    }

    public void PlayVideo()
    {
        // 動画が再生中でないことを確認して再生
        if (_videoPlayer != null && !_videoPlayer.isPlaying)
        {
            _isPlay = true;
            gameObject.SetActive(true); // 動画オブジェクトを表示
            _videoPlayer.Play();
        }
    }

    public void StopVideo()
    {
        if (_videoPlayer != null && _videoPlayer.isPlaying)
        {
            _videoPlayer.Stop(); // 動画を停止
            OnVideoEnd(_videoPlayer); // 動画終了処理を手動で呼び出す
        }
    }

    // 動画が終了したときに呼び出されるメソッド
    private void OnVideoEnd(VideoPlayer vp)
    {
        _isPlay = false; // 再生フラグをリセット
        gameObject.SetActive(false); // 動画オブジェクトを非表示にする
        Debug.Log("動画が終了しました！");
        // ここに動画終了後の処理を記述
    }
}
