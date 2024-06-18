using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerManager_MT : MonoBehaviour
{
    public List<Collider> objectsInTrigger = new List<Collider>(); // トリガー内のオブジェクトを保持するリスト


    //子オブジェクトのOnTriggerStayの処理
    public void OnChildTriggerStayCanEat(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!objectsInTrigger.Contains(other))
            {
                objectsInTrigger.Add(other);
            }
        }
    }


    // 子オブジェクトのOnTriggerExitの処理
    public void OnChildTriggerExitCanEat(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (objectsInTrigger.Contains(other))
            {
                objectsInTrigger.Remove(other);
            }
        }
    }
}
