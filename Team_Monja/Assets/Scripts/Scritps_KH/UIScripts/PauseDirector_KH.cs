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
<<<<<<< HEAD

            Pause();

=======
>>>>>>> origin/main
            if (!_isPause)
            {
                Pause();
            }
            else if (_isPause)
            {
                CancellationPause();
            }
<<<<<<< HEAD

=======
>>>>>>> origin/main
        }
    }

    private void Pause()
    {
        Time.timeScale = 0f;
<<<<<<< HEAD


        _isPause = true;
        _button.SetActive(true);

=======
        _isPause = true;
        _button.SetActive(true);
>>>>>>> origin/main
    }

    private void CancellationPause()
    {
        Time.timeScale = 1f;
<<<<<<< HEAD


        _isPause = false;
        _button.SetActive(false);

=======
        _isPause = false;
        _button.SetActive(false);
>>>>>>> origin/main
    }
}
