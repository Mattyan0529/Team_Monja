using UnityEngine;

public class NormalAttackHitDecision_KH : MonoBehaviour
{
    private NormalAttack_KH _normalAttack = default;

    void Start()
    {
        _normalAttack = GetComponentInParent<NormalAttack_KH>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Enemy" && other.gameObject.tag != "Boss") return;

        _normalAttack.HitDecision(other.gameObject);
    }
}
