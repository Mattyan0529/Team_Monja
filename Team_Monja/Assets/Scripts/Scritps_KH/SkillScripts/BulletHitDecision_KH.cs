using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BulletHitDecision_KH : MonoBehaviour
{
    private LongDistanceAttack_KH _longDistanceAttack = default;
    private Renderer _bulletRenderer = default;
    private SphereCollider _bulletCollider = default;
    private Rigidbody _bulletRigidbody = default;

    private GameObject _parent = default;

    void Start()
    {
        _parent = transform.parent.gameObject;
        _longDistanceAttack = _parent.GetComponent<LongDistanceAttack_KH>();
        _bulletRenderer = GetComponent<Renderer>();
        _bulletCollider = GetComponent<SphereCollider>();
        _bulletRigidbody = GetComponent<Rigidbody>();
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
