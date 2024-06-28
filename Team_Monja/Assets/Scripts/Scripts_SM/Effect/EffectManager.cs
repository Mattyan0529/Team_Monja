using UnityEngine;

/// <summary>
/// 攻撃エフェクトを管理するクラス。
/// </summary>
public class EffectManager : MonoBehaviour
{
    [Header("NormalAttack")]
    // 通常攻撃のオブジェクトプールの参照
    [SerializeField]
    private ObjectPool normalAttackObjectPool;

    // 通常攻撃エフェクトの発生位置のオフセット
    [SerializeField]
    private Vector3 normalAttackEffectOffset = new Vector3(1.0f, 0, 0);

    // 通常攻撃エフェクトの表示時間（秒）
    [SerializeField]
    private float normalAttackEffectDuration = 1.0f;

    // 通常攻撃エフェクトのスケール
    [SerializeField]
    private Vector3 normalAttackEffectScale = new Vector3(1.0f, 1.0f, 1.0f);

    [Space(20)]
    [Header("SpecialAttack")]
    // 特殊攻撃のオブジェクトプールの参照
    [SerializeField]
    private ObjectPool specialAttackObjectPool;

    // 特殊攻撃エフェクトの発生位置のオフセット
    [SerializeField]
    private Vector3 specialAttackEffectOffset = new Vector3(1.5f, 0, 0);

    // 特殊攻撃エフェクトの表示時間（秒）
    [SerializeField]
    private float specialAttackEffectDuration = 1.5f;

    // 特殊攻撃エフェクトのスケール
    [SerializeField]
    private Vector3 specialAttackEffectScale = new Vector3(1.5f, 1.5f, 1.5f);

    /// <summary>
    /// 通常攻撃エフェクトを表示するメソッド。
    /// </summary>
    /// <param name="attackerTransform">攻撃者のTransform。</param>
    public void ShowNormalAttackEffect(Transform attackerTransform)
    {
        // 通常攻撃エフェクトの発生位置を計算する
        Vector3 effectPosition = attackerTransform.position + attackerTransform.forward * normalAttackEffectOffset.x + attackerTransform.up * normalAttackEffectOffset.y + attackerTransform.right * normalAttackEffectOffset.z;

        // 通常攻撃用のオブジェクトプールによりエフェクトを表示する
        normalAttackObjectPool.ShowEffectPublic(effectPosition, attackerTransform.rotation, normalAttackEffectScale, normalAttackEffectDuration);
    }

    /// <summary>
    /// 特殊攻撃エフェクトを表示するメソッド。
    /// </summary>
    /// <param name="attackerTransform">攻撃者のTransform。</param>
    public void ShowSpecialAttackEffect(Transform attackerTransform)
    {
        // 特殊攻撃エフェクトの発生位置を計算する
        Vector3 effectPosition = attackerTransform.position + attackerTransform.forward * specialAttackEffectOffset.x + attackerTransform.up * specialAttackEffectOffset.y + attackerTransform.right * specialAttackEffectOffset.z;

        // 特殊攻撃用のオブジェクトプールによりエフェクトを表示する
        specialAttackObjectPool.ShowEffectPublic(effectPosition, attackerTransform.rotation, specialAttackEffectScale, specialAttackEffectDuration);
    }
}
