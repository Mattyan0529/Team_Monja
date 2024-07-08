using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatOrChangeController_MT : MonoBehaviour
{
    private EatEnemy_MT eatEnemy;
    private ChangeCharacter_MT changeCharacter;
    private EnemyTriggerManager_MT enemyTriggerManager;
    void Start()
    {
        eatEnemy = GetComponent<EatEnemy_MT>();
        changeCharacter = GetComponent<ChangeCharacter_MT>();

        GameObject _nearTrigger = GameObject.FindWithTag("NearTrigger");
        enemyTriggerManager = _nearTrigger.GetComponent<EnemyTriggerManager_MT>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("eat"))
        {
            RemoveClosestObject();
        }

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("change"))
        {
            ChangeTagClosestObject();
        }
    }

    // �ł��߂��I�u�W�F�N�g��H�ׂ郁�\�b�h
    private void RemoveClosestObject()
    {
        if (eatEnemy != null)
        {
            if(enemyTriggerManager.objectsInTrigger != null)
            {
                eatEnemy.RemoveClosestObject(enemyTriggerManager.objectsInTrigger, transform);
            }
             
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
            if (enemyTriggerManager.objectsInTrigger != null)
            {
                changeCharacter.ChangeTagClosestObject(enemyTriggerManager.objectsInTrigger, transform);
            }
          
        }
        else
        {
            Debug.LogError("ChangeCharacter_MT component is null.");
        }
    }
}
