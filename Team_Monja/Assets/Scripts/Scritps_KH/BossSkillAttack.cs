using UnityEngine;

public class BossSkillAttack : MonoBehaviour,IDamagable
{
    //���{
    private CharacterAnim_MT _characterAnim = default;

    // ���ꂼ��̍U�������s���銄��
    private const int _fireSphereProbability = 1;
    private const int _hitProbability = 4;
    private const int _biteProbability = 5;

    void Start()
    {
        _characterAnim = GetComponent<CharacterAnim_MT>();
    }

    public void SpecialAttack()
    {
        // 1����10�܂ł̗���
        int skillNum = Random.Range(0, _fireSphereProbability + _hitProbability + _biteProbability);

        switch (skillNum)
        {
            case < _fireSphereProbability:        // �΋�
                FireSphere();
                _characterAnim.NowAnim = "Skill";
                break;
            case < _fireSphereProbability + _hitProbability:        // ����
                HitAttack();
                _characterAnim.NowAnim = "Attack";
                break;
            case < _fireSphereProbability + _hitProbability + _biteProbability:       // ����
                BiteAttack();
                _characterAnim.NowAnim = "Attack2";
                break;
        }
    }

    private void FireSphere()
    {

    }

    private void HitAttack()
    {

    }

    private void BiteAttack()
    {

    }

    public void HitDecision(GameObject hitObj)
    {
        // ����Ǝ�����StatusManager�������K�v
        StatusManager_MT targetStatusManager = hitObj.gameObject.GetComponent<StatusManager_MT>();
        StatusManager_MT myStatusManager = GetComponent<StatusManager_MT>();

        HitPointCalculation(myStatusManager, targetStatusManager);
    }

    /// <summary>
    /// �_���[�W�v�Z
    /// </summary>
    public void HitPointCalculation(StatusManager_MT myStatus, StatusManager_MT targetStatus)
    {
        int myAttackPower = myStatus.Strength;        // �����̍U���͂�get���Ă���
        int targetDefensePower = targetStatus.Defense;        // ����̖h��͂�get���Ă���
        int targetHitPoint = targetStatus.HP;        // �����HP��get���Ă���

        if (myAttackPower < targetDefensePower) return;        // �h��͂̂ق�������������0�_���[�W

        int damage = targetHitPoint - (myAttackPower - targetDefensePower);
        //_createDamageImage.InstantiateDamageImage(gameObject, targetStatus.gameObject, myAttackPower - targetDefensePower);
        //_writeHitPoint.UpdateHitPoint(targetStatus, damage);      // targetStatus��HP���X�V
    }
}

