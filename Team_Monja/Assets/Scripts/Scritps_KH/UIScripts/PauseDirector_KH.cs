using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseDirector_KH : MonoBehaviour
{
    private bool _isPause = false;

    void Update()
    {
        PauseManagement();
    }

    /// <summary>
    /// インプットでポーズ状態を切りかえる
    /// </summary>
    private void PauseManagement()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ポーズ状態へ
            if (!_isPause)
            {
                Pause();
            }
            // ポーズ状態解除
            else if (_isPause)
            {
                CancellationPause();
            }
        }
    }

    /// <summary>
    /// ポーズ状態にする
    /// </summary>
    private void Pause()
    {
        Time.timeScale = 0f;
        _isPause = true;
    }

    /// <summary>
    /// ポーズ状態解除
    /// </summary>
    private void CancellationPause()
    {
        Time.timeScale = 1f;
        _isPause = false;
    }
}
