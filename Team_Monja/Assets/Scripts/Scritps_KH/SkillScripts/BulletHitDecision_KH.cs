using UnityEngine;

public class BulletHitDecision_KH : MonoBehaviour
{
    private LongDistanceAttack_KH _longDistanceAttack = default;
    private MeshRenderer _bulletRenderer = default;
    private SphereCollider _bulletCollider = default;
    private Rigidbody _bulletRigidbody = default;

    private GameObject _parent = default;

    void Start()
    {
        _parent = transform.parent.gameObject;

        // MeshRenderer‚Ìæ“¾
        _bulletRenderer = GetComponent<MeshRenderer>();
        if (_bulletRenderer == null)
        {
            Debug.LogError("MeshRenderer‚ªŒ©‚Â‚©‚è‚Ü‚¹‚ñ‚Å‚µ‚½B", this);
        }

        // SphereCollider‚Ìæ“¾
        _bulletCollider = GetComponent<SphereCollider>();
        if (_bulletCollider == null)
        {
            Debug.LogError("SphereCollider‚ªŒ©‚Â‚©‚è‚Ü‚¹‚ñ‚Å‚µ‚½B", this);
        }

        // Rigidbody‚Ìæ“¾
        _bulletRigidbody = GetComponent<Rigidbody>();
        if (_bulletRigidbody == null)
        {
            Debug.LogError("Rigidbody‚ªŒ©‚Â‚©‚è‚Ü‚¹‚ñ‚Å‚µ‚½B", this);
        }

        // LongDistanceAttack_KH‚Ìæ“¾
        _longDistanceAttack = _parent.GetComponent<LongDistanceAttack_KH>();
        if (_longDistanceAttack == null)
        {
            Debug.LogError("LongDistanceAttack_KH‚ªŒ©‚Â‚©‚è‚Ü‚¹‚ñ‚Å‚µ‚½B", this);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _parent) return;

        // ƒK[ƒh’†‚Å‚ ‚ê‚ÎUŒ‚–³Œø
        if (other.gameObject.GetComponent<PlayerGuard_KH>() && other.gameObject.GetComponent<PlayerGuard_KH>().IsGuard) return;

        if (_parent.CompareTag("Enemy") && other.gameObject.CompareTag("Player"))
        {
            _longDistanceAttack.HitDecision(other.gameObject);

            // ƒvƒŒƒCƒ„[‚É‚ ‚½‚Á‚½’e‚ğíœ
            DisableBullet();
        }
        else if (_parent.CompareTag("Player") && other.gameObject.CompareTag("Enemy"))
        {
            _longDistanceAttack.HitDecision(other.gameObject);

            // “G‚É‚ ‚½‚Á‚½’e‚ğíœ
            DisableBullet();
        }
    }

    /// <summary>
    /// ’e‚ÌCollider‚ÆRenderer‚ğ—LŒø‰»‚·‚é
    /// </summary>
    public void ActivateBullet()
    {
        _bulletRenderer.enabled = true;
        _bulletCollider.enabled = true;
    }

    /// <summary>
    /// ’e‚ğ–³Œø‰»‚·‚é
    /// </summary>
    public void DisableBullet()
    {
        _bulletRenderer.enabled = false;
        _bulletCollider.enabled = false;
        _bulletRigidbody.velocity = Vector3.zero;
    }
}
