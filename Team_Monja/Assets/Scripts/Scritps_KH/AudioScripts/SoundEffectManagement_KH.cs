using UnityEngine;

public class SoundEffectManagement_KH : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] _soundEffects;

    enum SoundEffectSubscript
    {
        SmallSelectionSound,
        BigSelectionSound,
        SuccubusAttackSound,
        SlashAttackSound,
        LongDistanceAttackSound,
        EatSound,
        PossessionSound,
        DamageSound,
        SlimeSound,
        BoneSound,
        SlowPunchSound,
        StrongPunchSound,
        MagicSound,
        Wind
    }

    #region SoundEffectStart

    /// <summary>
    /// 小さめの選択音を再生
    /// </summary>
    public void PlaySmallSelectionSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.SmallSelectionSound]);
    }

    /// <summary>
    /// 大きめの選択音を再生
    /// </summary>
    public void PlayBigSelectionSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.BigSelectionSound]);
    }

    /// <summary>
    /// サキュバスの攻撃音を再生
    /// </summary>
    public void PlaySuccubusAttackSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.SuccubusAttackSound]);
    }

    /// <summary>
    /// 斬撃の攻撃音を再生
    /// </summary>
    public void PlaySlashAttackSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.SlashAttackSound]);
    }

    /// <summary>
    /// 遠距離の攻撃音を再生
    /// </summary>
    public void PlayLongDistanceAttackSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.LongDistanceAttackSound]);
    }

    /// <summary>
    /// 食べる音を再生
    /// </summary>
    public void PlayEatSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.EatSound]);
    }

    /// <summary>
    /// 乗り移る音を再生
    /// </summary>
    public void PlayPossessionSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.PossessionSound]);
    }

    /// <summary>
    /// ダメージ音を再生
    /// </summary>
    public void PlayDamageSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.DamageSound]);
    }

    /// <summary>
    /// スライムの音を再生
    /// </summary>
    public void PlaySlimeSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.SlimeSound]);
    }

    /// <summary>
    /// 強いやつがしゃべる音を再生
    /// </summary>
    public void PlayPonPonSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.BoneSound]);
    }

    /// <summary>
    /// 重めの打撃音を再生
    /// </summary>
    public void PlaySlowPunchSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.SlowPunchSound]);
    }

    /// <summary>
    /// 強めの打撃音を再生
    /// </summary>
    public void PlayStrongPunchSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.StrongPunchSound]);
    }

    /// <summary>
    /// 小さめの選択音を再生
    /// </summary>
    public void PlayMagicSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.MagicSound]);
    }

    /// <summary>
    /// 風の音を再生
    /// </summary>
    public void PlayWindSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.Wind]);
    }

    #endregion

    #region SoundEffectsStop

    /// <summary>
    /// SEをすべて停止
    /// </summary>
    public void StopSound(AudioSource audioSource)
    {
        audioSource.Stop();
    }

    #endregion
}
