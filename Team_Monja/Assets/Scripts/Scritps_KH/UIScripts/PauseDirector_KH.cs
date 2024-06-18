using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseDirector_KH : MonoBehaviour
{
    [SerializeField]
    GameObject _button = default;

    private bool _isPause = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isPause)
            {
                Pause();
            }
            if (_isPause)
            {
                CancellationPause();
            }
        }
    }

    private void Pause()
    {
        _button.SetActive(true);
        Time.timeScale = 0f;
        _isPause = true;
    }

    private void CancellationPause()
    {
        _button.SetActive(false);
        Time.timeScale = 1f;
        _isPause = false;
    }
}
