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

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        PlayGameMusic();
    }

    /// <summary>
    /// �^�C�g����BGM���Đ�
    /// </summary>
    public void PlayTitleMusic()
    {
        _audioSource.clip = _BackGroundMusics[(int)BackGroundMusicSubscript.Title];
        _audioSource.Play();
    }

    /// <summary>
    /// �}�b�v�I����BGM���Đ�
    /// </summary>
    public void PlayMapSelectionMusic()
    {
        _audioSource.clip = _BackGroundMusics[(int)BackGroundMusicSubscript.MapSelection];
        _audioSource.Play();
    }

    /// <summary>
    /// �Q�[������BGM���Đ�
    /// </summary>
    public void PlayGameMusic()
    {
        _audioSource.clip = _BackGroundMusics[(int)BackGroundMusicSubscript.Game];
        _audioSource.Play();
    }

    /// <summary>
    /// �{�X�X�e�[�W��BGM���Đ�
    /// </summary>
    public void PlayBossMusic()
    {
        _audioSource.clip = _BackGroundMusics[(int)BackGroundMusicSubscript.Boss];
        _audioSource.Play();
    }

    /// <summary>
    /// �Q�[���N���A��BGM���Đ�
    /// </summary>
    public void PlayClearMusic()
    {
        _audioSource.clip = _BackGroundMusics[(int)BackGroundMusicSubscript.GameClear];
        _audioSource.Play();
    }

    /// <summary>
    /// �Q�[���I�[�o�[��BGM���Đ�
    /// </summary>
    public void PlayGameOverMusic()
    {
        _audioSource.clip = _BackGroundMusics[(int)BackGroundMusicSubscript.GameOver];
        _audioSource.Play();
    }

    /// <summary>
    /// �S�Ă�BGM���~
    /// </summary>
    public void StopMusic()
    {
        _audioSource.Stop();
    }
}
