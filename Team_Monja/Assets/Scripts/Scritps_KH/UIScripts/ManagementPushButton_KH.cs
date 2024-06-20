using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagementPushButton_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _setting = default;

    [SerializeField]
    private GameObject _soundEffectManager = default;

    private AudioSource _audioSource = default;

    void Start()
    {
        _audioSource = _soundEffectManager.GetComponent<AudioSource>();
    }

    /// <summary>
    /// �Q�[�����I��点��
    /// </summary>
    public void EndGame()
    {

#if UNITY_EDITOR        // �G�f�B�^��̏ꍇ
        UnityEditor.EditorApplication.isPlaying = false;

#else                   // �r���h�����Ƃ��̏ꍇ
    Application.Quit();

#endif

    }

    /// <summary>
    /// �}�b�v�I����ʂɍs��
    /// </summary>
    public void ToMapSelection()
    {
        StartCoroutine(WaitAndLoadScene("Stage"));
    }

    /// <summary>
    /// �Q�[����ʂɍs��
    /// </summary>
    public void ToGameScene()
    {
        StartCoroutine(WaitAndLoadScene("�Q�[�����̃V�[��"));
    }

    /// <summary>
    /// �N���A��ʂɍs��
    /// </summary>
    public void ToClearScene()
    {
        StartCoroutine(WaitAndLoadScene("�N���A��ʂ̃V�[��"));
    }

    /// <summary>
    /// �Q�[���I�[�o�[��ʂɍs��
    /// </summary>
    public void ToGameOverScene()
    {
        StartCoroutine(WaitAndLoadScene("�Q�[���I�[�o�[��ʂ̃V�[��"));
    }

    /// <summary>
    /// �ݒ��\������
    /// </summary>
    public void OutPutSetting()
    {
        _setting.SetActive(true);
    }

    /// <summary>
    /// �ݒ���\���ɂ���
    /// </summary>
    public void CloseSetting()
    {
        _setting.SetActive(false);
    }

    /// <summary>
    /// SE���Ȃ�I���܂ł̑҂�����
    /// </summary>
    /// <returns></returns>
    IEnumerator Cor()
    {
        yield return new WaitWhile(() => _audioSource.isPlaying);
    }

    /// <summary>
    /// SE���Ȃ�I�������Ɏw�肳�ꂽ�V�[�������[�h����
    /// </summary>
    /// <param name="sceneName">�V�[����</param>
    /// <returns></returns>
    IEnumerator WaitAndLoadScene(string sceneName)
    {
        yield return StartCoroutine(Cor());
        SceneManager.LoadScene(sceneName);
    }
}
