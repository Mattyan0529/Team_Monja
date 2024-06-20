using UnityEngine;

public class WriteHitPoint_KH : MonoBehaviour
{
    private CharacterAnim_MT _characterAnim;

    private GameObject _nowPlayer;
    private void Start()
    {
        _characterAnim = GetComponent<CharacterAnim_MT>();
        //_nowPlayer = GameObject.FindWithTag("Player");
    }

    /// <summary>
    /// そのキャラクターのHPを更新する（減らす）
    /// </summary>
    /// <param name="afterAttackedHitPoint">攻撃された後のHP</param>
    public void UpdateHitPoint(StatusManager_MT attackdStatus, int afterAttackedHitPoint)
    {
        attackdStatus.HP = afterAttackedHitPoint;
        _characterAnim.NowAnim = "Move";
    }
}
