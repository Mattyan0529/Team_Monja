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

        // ガード中であれば攻撃無効
        if (other.gameObject.GetComponent<PlayerGuard_KH>() && other.gameObject.GetComponent<PlayerGuard_KH>().IsGuard) return;

        if (_parent.CompareTag("Enemy") && other.gameObject.CompareTag("Player"))
        {
            _longDistanceAttack.HitDecision(other.gameObject);

            // プレイヤーにあたった弾を削除
            DisableBullet();
        }
        else if (_parent.CompareTag("Player") && other.gameObject.CompareTag("Enemy"))
        {
            _longDistanceAttack.HitDecision(other.gameObject);

            // 敵にあたった弾を削除
            DisableBullet();
        }
    }

    /// <summary>
    /// 弾のColliderとRendererを有効化する
    /// </summary>
    public void ActivateBullet()
    {
        _bulletRenderer.enabled = true;
        _bulletCollider.enabled = true;
    }

    /// <summary>
    /// 弾を無効化する
    /// </summary>
    public void DisableBullet()
    {
        _bulletRenderer.enabled = false;
        _bulletCollider.enabled = false;
        _bulletRigidbody.velocity = Vector3.zero;
    }
}
