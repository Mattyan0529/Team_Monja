using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackHitDecision_KH : MonoBehaviour
{
    private NormalAttack_KH _normalAttack = default;

    void Start()
    {
        _normalAttack = GetComponentInParent<NormalAttack_KH>();
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Enemy") return;

        _normalAttack.HitDecision(other.gameObject);
    }
}
