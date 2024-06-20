using UnityEngine;

public class SoundEffectManagement_KH : MonoBehaviour
{
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
        MagicSound,
        Wind
    }

    #region SoundEffectStart

    /// <summary>
    /// �����߂̑I�������Đ�
    /// </summary>
    public void PlaySmallSelectionSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.SmallSelectionSound]);
    }

    /// <summary>
    /// �傫�߂̑I�������Đ�
    /// </summary>
    public void PlayBigSelectionSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.BigSelectionSound]);
    }

    /// <summary>
    /// �����̍U�������Đ�
    /// </summary>
    public void PlayExplosionAttackSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.ExplosionAttackSound]);
    }

    /// <summary>
    /// �a���̍U�������Đ�
    /// </summary>
    public void PlaySlashAttackSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.SlashAttackSound]);
    }

    /// <summary>
    /// �������̍U�������Đ�
    /// </summary>
    public void PlayLongDistanceAttackSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.LongDistanceAttackSound]);
    }

    /// <summary>
    /// �H�ׂ鉹���Đ�
    /// </summary>
    public void PlayEatSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.EatSound]);
    }

    /// <summary>
    /// ���ڂ鉹���Đ�
    /// </summary>
    public void PlayPossessionSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.PossessionSound]);
    }

    /// <summary>
    /// �_���[�W�����Đ�
    /// </summary>
    public void PlayDamageSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.DamageSound]);
    }

    /// <summary>
    /// �X���C���̉����Đ�
    /// </summary>
    public void PlaySlimeSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.SlimeSound]);
    }

    /// <summary>
    /// �ق˂قˌN�̉����Đ�
    /// </summary>
    public void PlayBoneSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.BoneSound]);
    }

    /// <summary>
    /// �d�߂̑Ō������Đ�
    /// </summary>
    public void PlaySlowPunchSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.SlowPunchSound]);
    }

    /// <summary>
    /// ���߂̑Ō������Đ�
    /// </summary>
    public void PlayStrongPunchSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.StrongPunchSound]);
    }

    /// <summary>
    /// �����߂̑I�������Đ�
    /// </summary>
    public void PlayMagicSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.MagicSound]);
    }

    /// <summary>
    /// ���̉����Đ�
    /// </summary>
    public void PlayWindSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.Wind]);
    }

    #endregion

    #region SoundEffectsStop

    /// <summary>
    /// SE�����ׂĒ�~
    /// </summary>
    public void StopSound(AudioSource audioSource)
    {
        audioSource.Stop();
    }

    #endregion
}
