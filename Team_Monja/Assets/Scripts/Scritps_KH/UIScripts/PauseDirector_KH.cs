using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseDirector_KH : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas; // ポーズ時に表示するキャンバスの参照
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


    private void Pause()
    {
        Time.timeScale = 0f;
        _isPause = true;
        if (pauseCanvas != null)
        {
            pauseCanvas.SetActive(true); // ポーズキャンバスを表示
        }
    }

    private void CancellationPause()
    {
        Time.timeScale = 1f;
        _isPause = false;
        if (pauseCanvas != null)
        {
            pauseCanvas.SetActive(false); // ポーズキャンバスを非表示
        }
    }
}
