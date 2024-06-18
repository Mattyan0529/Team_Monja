using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosestEnemyFinder_MT : MonoBehaviour
{
    // 最も近いオブジェクトを取得するメソッド
    public Collider GetClosestObject(List<Collider> objectsInTrigger, Transform referencePoint)
    {
        if (objectsInTrigger == null || objectsInTrigger.Count == 0)
        {
            return null;
        }

        Collider closestObject = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider obj in objectsInTrigger)
        {
            float distance = Vector3.Distance(referencePoint.position, obj.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = obj;
            }
        }

        return closestObject;
    }
}
