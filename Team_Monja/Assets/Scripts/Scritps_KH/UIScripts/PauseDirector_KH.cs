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

    /// <summary>
    /// �|�[�Y��Ԃɂ���
    /// </summary>
    private void Pause()
    {
        Time.timeScale = 0f;
        _isPause = true;
    }

    /// <summary>
    /// �|�[�Y��ԉ���
    /// </summary>
    private void CancellationPause()
    {
        Time.timeScale = 1f;
        _isPause = false;
    }
}
