using UnityEngine;

public class BossSkillAttack : MonoBehaviour,IDamagable
{
    //松本
    private CharacterAnim_MT _characterAnim = default;

    // それぞれの攻撃を実行する割合
    private const int _fireSphereProbability = 1;
    private const int _hitProbability = 4;
    private const int _biteProbability = 5;

    void Start()
    {
        _characterAnim = GetComponent<CharacterAnim_MT>();
    }

    public void SpecialAttack()
    {
        // 1から10までの乱数
        int skillNum = Random.Range(0, _fireSphereProbability + _hitProbability + _biteProbability);

        switch (skillNum)
        {
            case < _fireSphereProbability:        // 火球
                FireSphere();
                _characterAnim.NowAnim = "Skill";
                break;
            case < _fireSphereProbability + _hitProbability:        // 殴る
                HitAttack();
                _characterAnim.NowAnim = "Attack";
                break;
            case < _fireSphereProbability + _hitProbability + _biteProbability:       // 噛む
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
        // 相手と自分のStatusManagerが両方必要
        StatusManager_MT targetStatusManager = hitObj.gameObject.GetComponent<StatusManager_MT>();
        StatusManager_MT myStatusManager = GetComponent<StatusManager_MT>();

        HitPointCalculation(myStatusManager, targetStatusManager);
    }

    /// <summary>
    /// ダメージ計算
    /// </summary>
    public void HitPointCalculation(StatusManager_MT myStatus, StatusManager_MT targetStatus)
    {
        int myAttackPower = myStatus.Strength;        // 自分の攻撃力をgetしてくる
        int targetDefensePower = targetStatus.Defense;        // 相手の防御力をgetしてくる
        int targetHitPoint = targetStatus.HP;        // 相手のHPをgetしてくる

        if (myAttackPower < targetDefensePower) return;        // 防御力のほうが高かったら0ダメージ

        int damage = targetHitPoint - (myAttackPower - targetDefensePower);
        //_createDamageImage.InstantiateDamageImage(gameObject, targetStatus.gameObject, myAttackPower - targetDefensePower);
        //_writeHitPoint.UpdateHitPoint(targetStatus, damage);      // targetStatusのHPを更新
    }
}

