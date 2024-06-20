using UnityEngine;

public class BossSkill_KH : MonoBehaviour
{
    private LongDistanceAttack_KH _longDistanceAttack = default;
    private WeaponAttack_KH _weaponAttack = default;

    // ���ꂼ��̍U�������s���銄��
    private const int _fireSphereProbability = 1;
    private const int _hitProbability = 4;
    private const int _biteProbability = 5;

    void Start()
    {
        _longDistanceAttack = GetComponent<LongDistanceAttack_KH>();
        _weaponAttack = GetComponent<WeaponAttack_KH>();
    }

    public void RandomSkillCall()
    {
        // 1����10�܂ł̗���
        int skillNum = Random.Range(0, _fireSphereProbability + _hitProbability + _biteProbability);

        switch (skillNum)
        {
            case < _fireSphereProbability:        // �΋�
                _longDistanceAttack.GenerateBullet();
                break;
            case < _fireSphereProbability + _hitProbability:        // ����
                _weaponAttack.Attack();
                break;
            case < _fireSphereProbability + _hitProbability + _biteProbability:       // ����
                _weaponAttack.Attack();
                break;
        }
    }
}
