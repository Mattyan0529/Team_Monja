using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatOrChangeController_MT : MonoBehaviour
{
    // �ǋL�F�k
    [SerializeField]
    private GameObject _residentScript = default;
    private AudioSource _audioSource = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;

    EatEnemy_MT eatEnemy;
    ChangeCharacter_MT changeCharacter;
    EnemyTriggerManager_MT enemyTriggerManager;

    void Start()
    {
        eatEnemy = GetComponent<EatEnemy_MT>();
        changeCharacter = GetComponent<ChangeCharacter_MT>();
        enemyTriggerManager = GetComponent<EnemyTriggerManager_MT>();

        // �ǋL�F�k
        _soundEffectManagement = _residentScript.GetComponent<SoundEffectManagement_KH>();

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RemoveClosestObject();
        }

        if (Input.GetKeyDown(KeyCode.M))
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

            // �ǋL�F�k
            if (_audioSource == null)
            {
                _audioSource = GetComponentInChildren<AudioSource>();
            }
            _soundEffectManagement.PlayEatSound(_audioSource);
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

            // �ǋL�F�k
            if (_audioSource == null)
            {
                _audioSource = GetComponentInChildren<AudioSource>();
            }
            _soundEffectManagement.PlayPossessionSound(_audioSource);
        }
        else
        {
            Debug.LogError("ChangeCharacter_MT component is null.");
        }
    }
}
