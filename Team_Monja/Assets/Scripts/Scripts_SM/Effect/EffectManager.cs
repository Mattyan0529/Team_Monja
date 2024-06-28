using UnityEngine;

/// <summary>
/// �U���G�t�F�N�g���Ǘ�����N���X�B
/// </summary>
public class EffectManager : MonoBehaviour
{
    [Header("NormalAttack")]
    // �ʏ�U���̃I�u�W�F�N�g�v�[���̎Q��
    [SerializeField]
    private ObjectPool normalAttackObjectPool;

    // �ʏ�U���G�t�F�N�g�̔����ʒu�̃I�t�Z�b�g
    [SerializeField]
    private Vector3 normalAttackEffectOffset = new Vector3(1.0f, 0, 0);

    // �ʏ�U���G�t�F�N�g�̕\�����ԁi�b�j
    [SerializeField]
    private float normalAttackEffectDuration = 1.0f;

    // �ʏ�U���G�t�F�N�g�̃X�P�[��
    [SerializeField]
    private Vector3 normalAttackEffectScale = new Vector3(1.0f, 1.0f, 1.0f);

    [Space(20)]
    [Header("SpecialAttack")]
    // ����U���̃I�u�W�F�N�g�v�[���̎Q��
    [SerializeField]
    private ObjectPool specialAttackObjectPool;

    // ����U���G�t�F�N�g�̔����ʒu�̃I�t�Z�b�g
    [SerializeField]
    private Vector3 specialAttackEffectOffset = new Vector3(1.5f, 0, 0);

    // ����U���G�t�F�N�g�̕\�����ԁi�b�j
    [SerializeField]
    private float specialAttackEffectDuration = 1.5f;

    // ����U���G�t�F�N�g�̃X�P�[��
    [SerializeField]
    private Vector3 specialAttackEffectScale = new Vector3(1.5f, 1.5f, 1.5f);

    /// <summary>
    /// �ʏ�U���G�t�F�N�g��\�����郁�\�b�h�B
    /// </summary>
    /// <param name="attackerTransform">�U���҂�Transform�B</param>
    public void ShowNormalAttackEffect(Transform attackerTransform)
    {
        // �ʏ�U���G�t�F�N�g�̔����ʒu���v�Z����
        Vector3 effectPosition = attackerTransform.position + attackerTransform.forward * normalAttackEffectOffset.x + attackerTransform.up * normalAttackEffectOffset.y + attackerTransform.right * normalAttackEffectOffset.z;

        // �ʏ�U���p�̃I�u�W�F�N�g�v�[���ɂ��G�t�F�N�g��\������
        normalAttackObjectPool.ShowEffectPublic(effectPosition, attackerTransform.rotation, normalAttackEffectScale, normalAttackEffectDuration);
    }

    /// <summary>
    /// ����U���G�t�F�N�g��\�����郁�\�b�h�B
    /// </summary>
    /// <param name="attackerTransform">�U���҂�Transform�B</param>
    public void ShowSpecialAttackEffect(Transform attackerTransform)
    {
        // ����U���G�t�F�N�g�̔����ʒu���v�Z����
        Vector3 effectPosition = attackerTransform.position + attackerTransform.forward * specialAttackEffectOffset.x + attackerTransform.up * specialAttackEffectOffset.y + attackerTransform.right * specialAttackEffectOffset.z;

        // ����U���p�̃I�u�W�F�N�g�v�[���ɂ��G�t�F�N�g��\������
        specialAttackObjectPool.ShowEffectPublic(effectPosition, attackerTransform.rotation, specialAttackEffectScale, specialAttackEffectDuration);
    }
}
