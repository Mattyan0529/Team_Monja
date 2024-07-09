using UnityEngine;

public class WeaponHitDecision_KH : MonoBehaviour
{
    private IDamagable _skillInterface = default;

    void Start()
    {
        _skillInterface = transform.parent.gameObject.GetComponent<IDamagable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // ÉKÅ[ÉhíÜÇ≈Ç†ÇÍÇŒçUåÇñ≥å¯
        if (other.gameObject.GetComponent<PlayerGuard_KH>() && other.gameObject.GetComponent<PlayerGuard_KH>().IsGuard) return;

        if ((gameObject.transform.parent.CompareTag("Enemy") || gameObject.transform.parent.CompareTag("Boss")) && other.gameObject.CompareTag("Player"))
        {
            _skillInterface.HitDecision(other.gameObject);
        }
        else if (gameObject.transform.parent.CompareTag("Player") && (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boss")))
        {
            _skillInterface.HitDecision(other.gameObject);
        }
    }
}
