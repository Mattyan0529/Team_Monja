using UnityEngine;

public class WeaponHitDecision_KH : MonoBehaviour
{
    private IDamagable_KH _skillInterface = default;
    private InvincibleManagementScript_KH _invincibleManagementScript = default;
    private bool _isBoss = false;

    void Awake()
    {
        _skillInterface = transform.parent.gameObject.GetComponent<IDamagable_KH>();

        if (gameObject.transform.parent.CompareTag("Boss"))
        {
            _isBoss = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Enemy") && !other.gameObject.CompareTag("Boss")) return;

        // ƒK[ƒh’†‚Å‚ ‚ê‚ÎUŒ‚–³Œø
        if (other.gameObject.GetComponent<PlayerGuard_KH>() && other.gameObject.GetComponent<PlayerGuard_KH>().IsGuard) return;

        // ‹N‚«ã‚ª‚è‚â•ßH’†‚Ì‚½‚ß–³“G‚Å‚ ‚ê‚ÎUŒ‚–³Œø
        if (other.gameObject.GetComponent<InvincibleManagementScript_KH>().IsInvincible) return;

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
