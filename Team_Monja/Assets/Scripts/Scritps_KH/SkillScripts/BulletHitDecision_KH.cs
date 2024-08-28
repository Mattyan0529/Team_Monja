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

        // MeshRenderer�̎擾
        _bulletRenderer = GetComponent<MeshRenderer>();
        if (_bulletRenderer == null)
        {
            Debug.LogError("MeshRenderer��������܂���ł����B", this);
        }

        // SphereCollider�̎擾
        _bulletCollider = GetComponent<SphereCollider>();
        if (_bulletCollider == null)
        {
            Debug.LogError("SphereCollider��������܂���ł����B", this);
        }

        // Rigidbody�̎擾
        _bulletRigidbody = GetComponent<Rigidbody>();
        if (_bulletRigidbody == null)
        {
            Debug.LogError("Rigidbody��������܂���ł����B", this);
        }

        // LongDistanceAttack_KH�̎擾
        _skillInterface = _parent.GetComponent<IDamagable_KH>();
        if (_skillInterface == null)
        {
            Debug.LogError("LongDistanceAttack_KH��������܂���ł����B", this);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _parent) return;

        // �K�[�h���ł���΍U������
        if (other.gameObject.GetComponent<PlayerGuard_KH>() && other.gameObject.GetComponent<PlayerGuard_KH>().IsGuard) return;

        if ((_parent.CompareTag("Enemy") || _parent.CompareTag("Boss")) && other.gameObject.CompareTag("Player"))
        {
            _skillInterface.HitDecision(other.gameObject);

            // �v���C���[�ɂ��������e���폜
            DisableBullet();
        }
        else if (_parent.CompareTag("Player") && (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boss")))
        {
            _skillInterface.HitDecision(other.gameObject);

            // �G�ɂ��������e���폜
            DisableBullet();
        }
    }

    /// <summary>
    /// �e��Collider��Renderer��L��������
    /// </summary>
    public void ActivateBullet()
    {
        _bulletRenderer.enabled = true;
        _bulletCollider.enabled = true;
    }

    /// <summary>
    /// �e�𖳌�������
    /// </summary>
    public void DisableBullet()
    {
        _bulletRenderer.enabled = false;
        _bulletCollider.enabled = false;
        _bulletRigidbody.velocity = Vector3.zero;
    }
}
