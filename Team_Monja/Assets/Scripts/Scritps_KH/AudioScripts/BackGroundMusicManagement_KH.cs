using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusicManagement_KH : MonoBehaviour
{
    private AudioSource _audioSource = default;

    [SerializeField]
    private AudioClip[] _BackGroundMusics;

    enum BackGroundMusicSubscript
    {
        Title,
        MapSelection,
        Game,
        Boss,
        GameClear,
        GameOver
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        _audioSource = GetComponent<AudioSource>();
        PlayTitleMusic();
    }

    /// <summary>
    /// タイトルのBGMを再生
    /// </summary>
    public void PlayTitleMusic()
    {
        _audioSource.clip = _BackGroundMusics[(int)BackGroundMusicSubscript.Title];
        _audioSource.Play();
    }

    /// <summary>
    /// マップ選択のBGMを再生
    /// </summary>
    public void PlayMapSelectionMusic()
    {
        _audioSource.clip = _BackGroundMusics[(int)BackGroundMusicSubscript.MapSelection];
        _audioSource.Play();
    }

    /// <summary>
    /// ゲーム中のBGMを再生
    /// </summary>
    public void PlayGameMusic()
    {
        _audioSource.clip = _BackGroundMusics[(int)BackGroundMusicSubscript.Game];
        _audioSource.Play();
    }

    /// <summary>
    /// ボスステージのBGMを再生
    /// </summary>
    public void PlayBossMusic()
    {
        _audioSource.clip = _BackGroundMusics[(int)BackGroundMusicSubscript.Boss];
        _audioSource.Play();
    }

    /// <summary>
    /// ゲームクリアのBGMを再生
    /// </summary>
    public void PlayClearMusic()
    {
        _audioSource.clip = _BackGroundMusics[(int)BackGroundMusicSubscript.GameClear];
        _audioSource.Play();
    }

    /// <summary>
    /// ゲームオーバーのBGMを再生
    /// </summary>
    public void PlayGameOverMusic()
    {
        _audioSource.clip = _BackGroundMusics[(int)BackGroundMusicSubscript.GameOver];
        _audioSource.Play();
    }
}
