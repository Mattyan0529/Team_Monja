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
    /// ゲームを終わらせる
    /// </summary>
    public void EndGame()
    {

#if UNITY_EDITOR        // エディタ上の場合
        UnityEditor.EditorApplication.isPlaying = false;

#else                   // ビルドしたときの場合
    Application.Quit();

#endif

    }

    /// <summary>
    /// マップ選択画面に行く
    /// </summary>
    public void ToMapSelection()
    {
        StartCoroutine(WaitAndLoadScene("Stage"));
    }

    /// <summary>
    /// ゲーム画面に行く
    /// </summary>
    public void ToGameScene()
    {
        StartCoroutine(WaitAndLoadScene("ゲーム中のシーン"));
    }

    /// <summary>
    /// クリア画面に行く
    /// </summary>
    public void ToClearScene()
    {
        StartCoroutine(WaitAndLoadScene("クリア画面のシーン"));
    }

    /// <summary>
    /// ゲームオーバー画面に行く
    /// </summary>
    public void ToGameOverScene()
    {
        StartCoroutine(WaitAndLoadScene("ゲームオーバー画面のシーン"));
    }

    /// <summary>
    /// 設定を表示する
    /// </summary>
    public void OutPutSetting()
    {
        _setting.SetActive(true);
    }

    /// <summary>
    /// 設定を非表示にする
    /// </summary>
    public void CloseSetting()
    {
        _setting.SetActive(false);
    }

    /// <summary>
    /// SEがなり終わるまでの待ち時間
    /// </summary>
    /// <returns></returns>
    IEnumerator Cor()
    {
        yield return new WaitWhile(() => _audioSource.isPlaying);
    }

    /// <summary>
    /// SEがなり終わった後に指定されたシーンをロードする
    /// </summary>
    /// <param name="sceneName">シーン名</param>
    /// <returns></returns>
    IEnumerator WaitAndLoadScene(string sceneName)
    {
        yield return StartCoroutine(Cor());
        SceneManager.LoadScene(sceneName);
    }
}
