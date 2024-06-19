using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitDecision_KH : MonoBehaviour
{
    private WeaponAttack_KH _weaponAttack = default;

    // Start is called before the first frame update
    void Start()
    {
        _weaponAttack = transform.parent.gameObject.GetComponent<WeaponAttack_KH>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // ÉKÅ[ÉhíÜÇ≈Ç†ÇÍÇŒçUåÇñ≥å¯
        if (other.gameObject.GetComponent<PlayerGuard_KH>() && other.gameObject.GetComponent<PlayerGuard_KH>().IsGuard) return;

        if (gameObject.transform.parent.CompareTag("Enemy") && other.gameObject.CompareTag("Player"))
        {
            _weaponAttack.HitDecision(other.gameObject);
        }
        else if (gameObject.transform.parent.CompareTag("Player") && other.gameObject.CompareTag("Enemy"))
        {
            _weaponAttack.HitDecision(other.gameObject);
        }
    }
}
