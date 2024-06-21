using UnityEngine;

public class WriteHitPoint_KH : MonoBehaviour
{
    private CharacterAnim_MT _characterAnim;

    private GameObject _nowPlayer;

    /// <summary>
    /// そのキャラクターのHPを更新する（減らす）
    /// </summary>
    /// <param name="afterAttackedHitPoint">攻撃された後のHP</param>
    public void UpdateHitPoint(StatusManager_MT attackdStatus, int afterAttackedHitPoint)
    {
        _nowPlayer = GameObject.FindWithTag("Player");
        _characterAnim = _nowPlayer.GetComponent<CharacterAnim_MT>();
        attackdStatus.HP = afterAttackedHitPoint;
        _characterAnim.NowAnim = "GotDamage";
    }
}
