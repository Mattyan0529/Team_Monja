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
    /// �����߂̑I�������Đ�
    /// </summary>
    public void PlaySmallSelectionSound()
    {
        _audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.SmallSelectionSound]);
    }

    /// <summary>
    /// �傫�߂̑I�������Đ�
    /// </summary>
    public void PlayBigSelectionSound()
    {
        _audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.BigSelectionSound]);
    }

    /// <summary>
    /// �����̍U�������Đ�
    /// </summary>
    public void PlayExplosionAttackSound()
    {
        _audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.ExplosionAttackSound]);
    }

    /// <summary>
    /// �a���̍U�������Đ�
    /// </summary>
    public void PlaySlashAttackSound()
    {
        _audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.SlashAttackSound]);
    }

    /// <summary>
    /// �������̍U�������Đ�
    /// </summary>
    public void PlayLongDistanceAttackSound()
    {
        _audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.LongDistanceAttackSound]);
    }

    /// <summary>
    /// �H�ׂ鉹���Đ�
    /// </summary>
    public void PlayEatSound()
    {
        _audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.EatSound]);
    }

    /// <summary>
    /// ���ڂ鉹���Đ�
    /// </summary>
    public void PlayPossessionSound()
    {
        _audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.PossessionSound]);
    }

    /// <summary>
    /// �_���[�W�����Đ�
    /// </summary>
    public void PlayDamageSound()
    {
        _audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.DamageSound]);
    }

    /// <summary>
    /// �X���C���̉����Đ�
    /// </summary>
    public void PlaySlimeSound()
    {
        _audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.SlimeSound]);
    }

    /// <summary>
    /// �ق˂قˌN�̉����Đ�
    /// </summary>
    public void PlayBoneSound()
    {
        _audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.BoneSound]);
    }

    /// <summary>
    /// �d�߂̑Ō������Đ�
    /// </summary>
    public void PlaySlowPunchSound()
    {
        _audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.SlowPunchSound]);
    }

    /// <summary>
    /// ���߂̑Ō������Đ�
    /// </summary>
    public void PlayStrongPunchSound()
    {
        _audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.StrongPunchSound]);
    }

    /// <summary>
    /// �����߂̑I�������Đ�
    /// </summary>
    public void PlayMagicSound()
    {
        _audioSource.PlayOneShot(_soundEffects[(int)SoundEffectSubscript.MagicSound]);
    }

    #endregion
}
