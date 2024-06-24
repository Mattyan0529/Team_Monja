using UnityEngine;

public class BossSkill_KH : MonoBehaviour
{
    //¼–{
    private CharacterAnim_MT _characterAnim = default;

    private LongDistanceAttack_KH _longDistanceAttack = default;
    private WeaponAttack_KH _weaponAttack = default;

    // ‚»‚ê‚¼‚ê‚ÌUŒ‚‚ğÀs‚·‚éŠ„‡
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
        // 1‚©‚ç10‚Ü‚Å‚Ì—”
        int skillNum = Random.Range(0, _fireSphereProbability + _hitProbability + _biteProbability);

        switch (skillNum)
        {
            case < _fireSphereProbability:        // ‰Î‹…
                _longDistanceAttack.GenerateBullet();
                _characterAnim.NowAnim = "Skill";
                break;
            case < _fireSphereProbability + _hitProbability:        // ‰£‚é
                _weaponAttack.Attack();
                _characterAnim.NowAnim = "Attack";
                break;
            case < _fireSphereProbability + _hitProbability + _biteProbability:       // Šš‚Ş
                _weaponAttack.Attack();
                _characterAnim.NowAnim = "Attack2";
                break;
        }
    }
}
