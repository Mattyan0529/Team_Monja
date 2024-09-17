using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseDirector_KH : MonoBehaviour
{
    [SerializeField] private GameObject _pauseCanvas; // �|�[�Y���ɕ\������L�����o�X�̎Q��
    [SerializeField] private GameObject _playerCanvas; // �v���C���[�L�����o�X�̎Q��
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
        // Escape�L�[�܂��̓R���g���[���[��Pause�{�^���Ń|�[�Y��Ԃ�؂�ւ���
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("MenuButton"))
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
        if (_pauseCanvas != null)
        {
            _pauseCanvas.SetActive(true); // �|�[�Y�L�����o�X��\��
        }

        if (_playerCanvas != null)
        {
            _playerCanvas.SetActive(false); // �v���C���[�L�����o�X���\��
        }
    }

    private void CancellationPause()
    {
        Time.timeScale = 1f;
        _isPause = false;
        if (_pauseCanvas != null)
        {
            _pauseCanvas.SetActive(false); // �|�[�Y�L�����o�X���\��
        }

        if (_playerCanvas != null)
        {
            _playerCanvas.SetActive(true); // �v���C���[�L�����o�X��\��
        }
    }
}
