using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatOrChangeController_MT : MonoBehaviour
{
    private EatEnemy_MT eatEnemy;
    private ChangeCharacter_MT changeCharacter;
    private EnemyTriggerManager_MT enemyTriggerManager;
    private LockOn lockOn;
    private CharacterAnim_MT characterAnim;
    [SerializeField]
    private float _activationDuration = 0.5f;


    private GameObject _player;
    void Start()
    {
        eatEnemy = GetComponent<EatEnemy_MT>();
        changeCharacter = GetComponent<ChangeCharacter_MT>();
        enemyTriggerManager = GameObject.FindWithTag("NearTrigger").GetComponent<EnemyTriggerManager_MT>();
        lockOn = Camera.main.GetComponentInParent<LockOn>();
        characterAnim = GameObject.FindWithTag("Player").GetComponent<CharacterAnim_MT>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("eat"))
        {
            characterAnim.NowAnim = "Eat";
            StartCoroutine(DeadManDelay(_activationDuration));
            //RemoveClosestObject();
        }

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("change"))
        {
            ChangeTagClosestObject();
            lockOn.ResetRotation();
        }
    }

    private IEnumerator DeadManDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        RemoveClosestObject();
    }

    // 最も近いオブジェクトを食べるメソッド
    private void RemoveClosestObject()
    {
        if (eatEnemy != null)
        {
            if (enemyTriggerManager.objectsInTrigger != null)
            {
                eatEnemy.RemoveClosestObject(enemyTriggerManager.objectsInTrigger, _player.transform);
            }

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
            if (enemyTriggerManager.objectsInTrigger != null)
            {
                changeCharacter.ChangeTagClosestObject(enemyTriggerManager.objectsInTrigger, _player.transform);
            }

        }
        else
        {
            Debug.LogError("ChangeCharacter_MT component is null.");
        }
    }

    public void SetPlayer(GameObject player)
    {
        _player = player;
        characterAnim = player.GetComponent<CharacterAnim_MT>();
    }

}
