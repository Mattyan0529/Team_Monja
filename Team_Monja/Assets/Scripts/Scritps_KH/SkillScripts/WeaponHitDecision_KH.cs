using UnityEngine;

public class WeaponHitDecision_KH : MonoBehaviour
{
    private WeaponAttack_KH _weaponAttack = default;

    void Start()
    {
        _weaponAttack = transform.parent.gameObject.GetComponent<WeaponAttack_KH>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // ÉKÅ[ÉhíÜÇ≈Ç†ÇÍÇŒçUåÇñ≥å¯
        if (other.gameObject.GetComponent<PlayerGuard_KH>() && other.gameObject.GetComponent<PlayerGuard_KH>().IsGuard) return;

        if ((gameObject.transform.parent.CompareTag("Enemy") || gameObject.transform.parent.CompareTag("Boss")) && other.gameObject.CompareTag("Player"))
        {
            _weaponAttack.HitDecision(other.gameObject);
        }
        else if (gameObject.transform.parent.CompareTag("Player") && (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boss")))
        {
            _weaponAttack.HitDecision(other.gameObject);
        }
    }
}
