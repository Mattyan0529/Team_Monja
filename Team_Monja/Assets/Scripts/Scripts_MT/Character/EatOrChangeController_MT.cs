using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatOrChangeController_MT : MonoBehaviour
{
    EatEnemy_MT eatEnemy;
    ChangeCharacter_MT changeCharacter;
    EnemyTriggerManager_MT enemyTriggerManager;

    void Start()
    {
        eatEnemy = GetComponent<EatEnemy_MT>();
        changeCharacter = GetComponent<ChangeCharacter_MT>();
        enemyTriggerManager = GetComponent<EnemyTriggerManager_MT>();

        if (eatEnemy == null)
        {
            Debug.LogError("EatEnemy_MT��������܂���");
        }
        if (changeCharacter == null)
        {
            Debug.LogError("ChangeCharacter_MT��������܂���");
        }
        if (enemyTriggerManager == null)
        {
            Debug.LogError("EnemyTriggerManager_MT��������܂���");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RemoveClosestObject();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeTagClosestObject();
        }
    }

    // �ł��߂��I�u�W�F�N�g��H�ׂ郁�\�b�h
    private void RemoveClosestObject()
    {
        if (eatEnemy != null)
        {
            eatEnemy.RemoveClosestObject(enemyTriggerManager.objectsInTrigger, transform);
        }
        else
        {
            Debug.LogError("EatEnemy_MT component is null.");
        }
    }

    // �ł��߂��I�u�W�F�N�g���v���C���[�ɕύX���郁�\�b�h
    private void ChangeTagClosestObject()
    {
        if (changeCharacter != null)
        {
            changeCharacter.ChangeTagClosestObject(enemyTriggerManager.objectsInTrigger, transform);
        }
        else
        {
            Debug.LogError("ChangeCharacter_MT component is null.");
        }
    }
}
