using TMPro;
using UnityEngine;

public class TimeToVideo : MonoBehaviour
{
    [SerializeField] private float _toVideoTime;
    [SerializeField] private TitleVideo _titleVideo; // TitleVideoスクリプトの参照

    private float _elapsedTime = 0f;

    void Update()
    {
        if (!_titleVideo.IsPlay)
        {
            // 経過時間を増やす
            _elapsedTime += Time.deltaTime;

            // 指定した時間が経過したら
            if (_elapsedTime >= _toVideoTime)
            {
                _titleVideo.PlayVideo();
                _elapsedTime = 0f; // 経過時間をリセット
            }
        }
    }

}
