using UnityEngine;

public class NormalAttackHitDecision_KH : MonoBehaviour
{
    private NormalAttack_KH _normalAttack;

    void Start()
    {
        // 親オブジェクトから NormalAttack_KH コンポーネントを取得する
        _normalAttack = GetComponentInParent<NormalAttack_KH>();
        if (_normalAttack == null)
        {
            Debug.LogError("NormalAttack_KH script not found in parent GameObject.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 他のオブジェクトが攻撃範囲に入った時の処理
        // 攻撃範囲に入ったオブジェクトが敵またはボスであれば、攻撃判定を行う
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boss"))
        {
            _normalAttack?.HitDecision(other.gameObject);
        }
    }
}
