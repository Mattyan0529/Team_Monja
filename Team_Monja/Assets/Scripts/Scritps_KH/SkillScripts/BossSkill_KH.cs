using UnityEngine;

public class BossSkill_KH : MonoBehaviour
{
    //松本
    private CharacterAnim_MT _characterAnim = default;

    private LongDistanceAttack_KH _longDistanceAttack = default;
    private WeaponAttack_KH _weaponAttack = default;

    // それぞれの攻撃を実行する割合
    private const int _fireSphereProbability = 1;
    private const int _hitProbability = 4;
    private const int _biteProbability = 5;

    void Start()
    {
        _longDistanceAttack = GetComponent<LongDistanceAttack_KH>();
        _weaponAttack = GetComponent<WeaponAttack_KH>();
        _characterAnim = GetComponent<CharacterAnim_MT>();
    }

    public void RandomSkillCall()
    {
        // 1から10までの乱数
        int skillNum = Random.Range(0, _fireSphereProbability + _hitProbability + _biteProbability);

        switch (skillNum)
        {
            case < _fireSphereProbability:        // 火球
                _longDistanceAttack.GenerateBullet();
                _characterAnim.NowAnim = "Skill";
                break;
            case < _fireSphereProbability + _hitProbability:        // 殴る
                _weaponAttack.Attack();
                _characterAnim.NowAnim = "Attack";
                break;
            case < _fireSphereProbability + _hitProbability + _biteProbability:       // 噛む
                _weaponAttack.Attack();
                _characterAnim.NowAnim = "Attack2";
                break;
        }
    }
}
