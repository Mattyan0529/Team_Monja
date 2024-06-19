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
        _button.SetActive(true);
    }

    private void CancellationPause()
    {
        Time.timeScale = 1f;
        _isPause = false;
        _button.SetActive(false);
    }
}
