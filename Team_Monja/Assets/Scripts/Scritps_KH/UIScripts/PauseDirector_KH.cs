using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseDirector_KH : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas; // �|�[�Y���ɕ\������L�����o�X�̎Q��
    private bool _isPause = false;

    void Update()
    {
        PauseManagement();
    }

    /// <summary>
    /// �C���v�b�g�Ń|�[�Y��Ԃ�؂肩����
    /// </summary>
    private void PauseManagement()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // �|�[�Y��Ԃ�
            if (!_isPause)
            {
                Pause();

            }
            // �|�[�Y��ԉ���
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
            pauseCanvas.SetActive(true); // �|�[�Y�L�����o�X��\��
        }
    }

    private void CancellationPause()
    {
        Time.timeScale = 1f;
        _isPause = false;
        if (pauseCanvas != null)
        {
            pauseCanvas.SetActive(false); // �|�[�Y�L�����o�X���\��
        }
    }
}
