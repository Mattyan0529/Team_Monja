using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearEnemyDecision_MT : MonoBehaviour
{
    private EnemyTriggerManager_MT parentScript;

    void Start()
    {
        // �e�I�u�W�F�N�g��EnemyTriggerManager_MT�R���|�[�l���g���擾
        parentScript = GetComponentInParent<EnemyTriggerManager_MT>();
        if (parentScript == null)
        {
            Debug.LogError("�e�I�u�W�F�N�g��EnemyTriggerManager_MT�R���|�[�l���g��null�ł�");
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
