using UnityEngine;

public class WeaponHitDecision_KH : MonoBehaviour
{
    private IDamagable _skillInterface = default;
    private bool _isBoss = false;

    void Awake()
    {
        _skillInterface = transform.parent.gameObject.GetComponent<IDamagable>();

        if (gameObject.transform.parent.CompareTag("Boss"))
        {
            _isBoss = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ガード中であれば攻撃無効
        if (other.gameObject.GetComponent<PlayerGuard_KH>() && other.gameObject.GetComponent<PlayerGuard_KH>().IsGuard) return;

        if(_isBoss && other.gameObject.CompareTag("Player"))
        {
            _skillInterface.HitDecision(other.gameObject);
            return;
        }
        else if(_isBoss)
        {
            return;
        }
        
        if (gameObject.transform.parent.CompareTag("Enemy") && other.gameObject.CompareTag("Player"))
        {
            _skillInterface.HitDecision(other.gameObject);
        }
        else if (gameObject.transform.parent.CompareTag("Player") && (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boss")))
        {
            _skillInterface.HitDecision(other.gameObject);
        }
    }
}
