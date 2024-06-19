using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill_KH : MonoBehaviour
{
    private LongDistanceAttack_KH _longDistanceAttack = default;
    private WeaponAttack_KH _weaponAttack = default;

    private int _fireSphereAttackPower = 10;
    private int _hitAttackPower = 10;
    private int _biteAttackPower = 10;

    void Start()
    {
        _longDistanceAttack = GetComponent<LongDistanceAttack_KH>();
        _weaponAttack = GetComponent<WeaponAttack_KH>();
    }

    public void RandomSkillCall()
    {
        // 1����10�܂ł̗���
        int skillNum = Random.Range(1,11);

        switch (skillNum)
        {
            case <2:        // �΋�
                _longDistanceAttack.GenerateBullet();
                break;
            case <6:        // ����
                _weaponAttack.Attack();
                break;
            case <11:       // ����
                _weaponAttack.Attack();
                break;
        }
    }
}
