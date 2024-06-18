using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManagement_KH : MonoBehaviour
{
    private AudioSource _audioSource = default;

    [SerializeField]
    private AudioClip[] _soundEffects;

    enum SoundEffectSubscript
    {
        SmallSelectionSound,
        BigSelectionSound,
        ExplosionAttackSound,
        SlashAttackSound,
        LongDistanceAttackSound,
        EatSound,
        PossessionSound,
        DamageSound,
        SlimeSound,
        BoneSound,
        SlowPunchSound,
        StrongPunchSound,
        MagicSound
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    #region SoundEffectStart

    /// <summary>
    /// 小さめの選択音を再生
    /// </summary>
    public void PlaySmallSelectionSound()
    {
        _audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.SmallSelectionSound]);
    }

    /// <summary>
    /// 大きめの選択音を再生
    /// </summary>
    public void PlayBigSelectionSound()
    {
        _audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.BigSelectionSound]);
    }

    /// <summary>
    /// 爆発の攻撃音を再生
    /// </summary>
    public void PlayExplosionAttackSound()
    {
        _audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.ExplosionAttackSound]);
    }

    /// <summary>
    /// 斬撃の攻撃音を再生
    /// </summary>
    public void PlaySlashAttackSound()
    {
        _audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.SlashAttackSound]);
    }

    /// <summary>
    /// 遠距離の攻撃音を再生
    /// </summary>
    public void PlayLongDistanceAttackSound()
    {
        _audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.LongDistanceAttackSound]);
    }

    /// <summary>
    /// 食べる音を再生
    /// </summary>
    public void PlayEatSound()
    {
        _audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.EatSound]);
    }

    /// <summary>
    /// 乗り移る音を再生
    /// </summary>
    public void PlayPossessionSound()
    {
        _audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.PossessionSound]);
    }

    /// <summary>
    /// ダメージ音を再生
    /// </summary>
    public void PlayDamageSound()
    {
        _audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.DamageSound]);
    }

    /// <summary>
    /// スライムの音を再生
    /// </summary>
    public void PlaySlimeSound()
    {
        _audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.SlimeSound]);
    }

    /// <summary>
    /// ほねほね君の音を再生
    /// </summary>
    public void PlayBoneSound()
    {
        _audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.BoneSound]);
    }

    /// <summary>
    /// 重めの打撃音を再生
    /// </summary>
    public void PlaySlowPunchSound()
    {
        _audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.SlowPunchSound]);
    }

    /// <summary>
    /// 強めの打撃音を再生
    /// </summary>
    public void PlayStrongPunchSound()
    {
        _audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.StrongPunchSound]);
    }

    /// <summary>
    /// 小さめの選択音を再生
    /// </summary>
    public void PlayMagicSound()
    {
        _audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.MagicSound]);
    }

    #endregion
}
