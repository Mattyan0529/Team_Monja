using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearEnemyDecision_MT : MonoBehaviour
{
    private EnemyTriggerManager_MT parentScript;

    void Start()
    {
        // 親オブジェクトのEnemyTriggerManager_MTコンポーネントを取得
        parentScript = GetComponentInParent<EnemyTriggerManager_MT>();
        if (parentScript == null)
        {
            Debug.LogError("親オブジェクトのEnemyTriggerManager_MTコンポーネントがnullです");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (parentScript != null)
        {
            parentScript.OnChildTriggerStayCanEat(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (parentScript != null)
        {
            parentScript.OnChildTriggerExitCanEat(other);
        }
    }

}
