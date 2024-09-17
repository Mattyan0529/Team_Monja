using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseDirector_KH : MonoBehaviour
{
    [SerializeField] private GameObject _pauseCanvas; // ポーズ時に表示するキャンバスの参照
    [SerializeField] private GameObject _playerCanvas; // プレイヤーキャンバスの参照
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
        // EscapeキーまたはコントローラーのPauseボタンでポーズ状態を切り替える
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("MenuButton"))
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
        if (_pauseCanvas != null)
        {
            _pauseCanvas.SetActive(true); // ポーズキャンバスを表示
        }

        if (_playerCanvas != null)
        {
            _playerCanvas.SetActive(false); // プレイヤーキャンバスを非表示
        }
    }

    private void CancellationPause()
    {
        Time.timeScale = 1f;
        _isPause = false;
        if (_pauseCanvas != null)
        {
            _pauseCanvas.SetActive(false); // ポーズキャンバスを非表示
        }

        if (_playerCanvas != null)
        {
            _playerCanvas.SetActive(true); // プレイヤーキャンバスを表示
        }
    }
}
