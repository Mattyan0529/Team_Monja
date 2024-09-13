using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosestEnemyFinder_MT : MonoBehaviour
{
    /// <summary>
    /// 指定されたリスト内で、最も近いオブジェクトを取得するメソッド
    /// </summary>
    /// <param name="objectsInTrigger">トリガー内にいるオブジェクトのリスト</param>
    /// <param name="referencePoint">比較する基準点（プレイヤーなど）</param>
    /// <returns>最も近いオブジェクトのColliderを返す。リストが空またはnullの場合はnullを返す。</returns>
    public Collider GetClosestObject(List<Collider> objectsInTrigger, Transform referencePoint)
    {
        // オブジェクトのリストが空かnullなら処理しない
        if (objectsInTrigger == null || objectsInTrigger.Count == 0)
        {
            return null;
        }

        Collider closestObject = null; // 最も近いオブジェクトを保持する変数
        float closestDistance = Mathf.Infinity; // 初期値は無限大に設定（どのオブジェクトでも距離が小さくなるように）

        // リスト内のすべてのオブジェクトについて距離を計算
        foreach (Collider obj in objectsInTrigger)
        {
            // 基準点（referencePoint）からオブジェクトまでの距離を計算
            float distance = Vector3.Distance(referencePoint.position, obj.transform.position);

            // 現在の最も近い距離よりも小さい場合、最も近いオブジェクトを更新
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = obj; // 最も近いオブジェクトとして設定
            }
        }

        return closestObject; // 最も近いオブジェクトを返す
    }
}
