using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseDirector_KH : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
<<<<<<< HEAD
            Pause();
=======
            if (!_isPause)
            {
                Pause();
            }
            else if (_isPause)
            {
                CancellationPause();
            }
>>>>>>> origin/main
        }
    }

    private void Pause()
    {
        Time.timeScale = 0f;
<<<<<<< HEAD
=======
        _isPause = true;
        _button.SetActive(true);
>>>>>>> origin/main
    }

    private void CancellationPause()
    {
        Time.timeScale = 1f;
<<<<<<< HEAD
=======
        _isPause = false;
        _button.SetActive(false);
>>>>>>> origin/main
    }
}
