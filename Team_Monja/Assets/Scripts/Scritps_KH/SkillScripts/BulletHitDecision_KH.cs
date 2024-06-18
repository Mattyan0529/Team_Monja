using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BulletHitDecision_KH : MonoBehaviour
{
    private LongDistanceAttack_KH _longDistanceAttack = default;
    private GameObject _parent = default;

    // Start is called before the first frame update
    void Start()
    {
        _longDistanceAttack = _parent.GetComponent<LongDistanceAttack_KH>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetParent(GameObject parent)
    {
        _parent = parent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _parent) return;
        if (!other.gameObject.GetComponent<PlayerGuard_KH>()) return;
        if (!other.gameObject.GetComponent<PlayerGuard_KH>().IsGuard) return;       // ƒK[ƒh’†‚Å‚ ‚ê‚ÎUŒ‚–³Œø

        if (_parent.CompareTag("Enemy") && other.gameObject.CompareTag("Player"))
        {
            _longDistanceAttack.HitDecision(other.gameObject);

            // ƒvƒŒƒCƒ„[‚É‚ ‚½‚Á‚½’e‚ğíœ
            Destroy(gameObject);
        }
        else if (_parent.CompareTag("Player") && other.gameObject.CompareTag("Enemy"))
        {
            _longDistanceAttack.HitDecision(other.gameObject);

            // “G‚É‚ ‚½‚Á‚½’e‚ğíœ
            Destroy(gameObject);
        }

        // áŠQ•¨‚È‚Ç‚Éƒ^ƒO‚ğ‚Â‚¯‚Ä’e‚ğíœ‚·‚é•K—v‚ ‚è
    }
}
