using UnityEngine;

public class WriteHitPoint_KH : MonoBehaviour
{
    /// <summary>
    /// そのキャラクターのHPを更新する（減らす）
    /// </summary>
    /// <param name="afterAttackedHitPoint">攻撃された後のHP</param>
    public void UpdateHitPoint(StatusManager_MT attackdStatus, int afterAttackedHitPoint)
    {
        attackdStatus.HP = afterAttackedHitPoint;
    }
}
