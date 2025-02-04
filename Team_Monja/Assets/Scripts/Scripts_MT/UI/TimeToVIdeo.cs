using TMPro;
using UnityEngine;

public class TimeToVideo : MonoBehaviour
{
    [SerializeField] private float _toVideoTime;
    [SerializeField] private TitleVideo _titleVideo; // TitleVideo�X�N���v�g�̎Q��

    private float _elapsedTime = 0f;

    void Update()
    {
        if (!_titleVideo.IsPlay)
        {
            // �o�ߎ��Ԃ𑝂₷
            _elapsedTime += Time.deltaTime;

            // �w�肵�����Ԃ��o�߂�����
            if (_elapsedTime >= _toVideoTime)
            {
                _titleVideo.PlayVideo();
                _elapsedTime = 0f; // �o�ߎ��Ԃ����Z�b�g
            }
        }
    }

}
