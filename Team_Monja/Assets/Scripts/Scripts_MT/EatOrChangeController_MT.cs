using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatOrChangeController_MT : MonoBehaviour
{
    // 追記：北
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

        // 追記：北
        _soundEffectManagement = _residentScript.GetComponent<SoundEffectManagement_KH>();

        if (eatEnemy == null)
        {
            Debug.LogError("EatEnemy_MTが見つかりません");
        }
        if (changeCharacter == null)
        {
            Debug.LogError("ChangeCharacter_MTが見つかりません");
        }
        if (enemyTriggerManager == null)
        {
            Debug.LogError("EnemyTriggerManager_MTが見つかりません");
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

    // 最も近いオブジェクトを食べるメソッド
    private void RemoveClosestObject()
    {
        if (eatEnemy != null)
        {
            eatEnemy.RemoveClosestObject(enemyTriggerManager.objectsInTrigger, transform);

            // 追記：北
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

    // 最も近いオブジェクトをプレイヤーに変更するメソッド
    private void ChangeTagClosestObject()
    {
        if (changeCharacter != null)
        {
            changeCharacter.ChangeTagClosestObject(enemyTriggerManager.objectsInTrigger, transform);

            // 追記：北
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
