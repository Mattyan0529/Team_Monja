using UnityEngine;

public class BulletHitDecision_KH : MonoBehaviour
{
    private IDamagable_KH _skillInterface = default;
    private MeshRenderer _bulletRenderer = default;
    private SphereCollider _bulletCollider = default;
    private Rigidbody _bulletRigidbody = default;

    private GameObject _parent = default;

    void Start()
    {
        _parent = transform.parent.gameObject;

        // MeshRendererの取得
        _bulletRenderer = GetComponent<MeshRenderer>();
        if (_bulletRenderer == null)
        {
            Debug.LogError("MeshRendererが見つかりませんでした。", this);
        }

        // SphereColliderの取得
        _bulletCollider = GetComponent<SphereCollider>();
        if (_bulletCollider == null)
        {
            Debug.LogError("SphereColliderが見つかりませんでした。", this);
        }

        // Rigidbodyの取得
        _bulletRigidbody = GetComponent<Rigidbody>();
        if (_bulletRigidbody == null)
        {
            Debug.LogError("Rigidbodyが見つかりませんでした。", this);
        }

        // LongDistanceAttack_KHの取得
        _skillInterface = _parent.GetComponent<IDamagable_KH>();
        if (_skillInterface == null)
        {
            Debug.LogError("LongDistanceAttack_KHが見つかりませんでした。", this);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _parent) return;

        // ガード中であれば攻撃無効
        if (other.gameObject.GetComponent<PlayerGuard_KH>() && other.gameObject.GetComponent<PlayerGuard_KH>().IsGuard) return;

        if ((_parent.CompareTag("Enemy") || _parent.CompareTag("Boss")) && other.gameObject.CompareTag("Player"))
        {
            _skillInterface.HitDecision(other.gameObject);

            // プレイヤーにあたった弾を削除
            DisableBullet();
        }
        else if (_parent.CompareTag("Player") && (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boss")))
        {
            _skillInterface.HitDecision(other.gameObject);

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
