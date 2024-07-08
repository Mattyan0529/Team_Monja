using System.Collections.Generic;
using UnityEngine;

public class ChangeCharacter_MT : MonoBehaviour
{
    [SerializeField] private GameObject _residentScript = default;
    private AudioSource _audioSource = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private MonsterSkill_KH _monsterSkill = default;

    [SerializeField] private int _IconNum = default;
    [SerializeField] private GameObject _canvas = default;

    private ChangeIcon_MT changeIcon;
    private StatusManager_MT statusManagerPlayer;
    private ClosestEnemyFinder_MT closestEnemyFinder;
    private EnemyHP_MT enemyHP;
    private EnemyTriggerManager_MT enemyTriggerManager;

    void Start()
    {
        _soundEffectManagement = _residentScript.GetComponent<SoundEffectManagement_KH>();
        _monsterSkill = GetComponent<MonsterSkill_KH>();
        _audioSource = GetComponentInChildren<AudioSource>();

        changeIcon = _canvas.GetComponentInChildren<ChangeIcon_MT>();
        statusManagerPlayer = GetComponent<StatusManager_MT>();
        closestEnemyFinder = GetComponent<ClosestEnemyFinder_MT>();
        enemyHP = GetComponentInChildren<EnemyHP_MT>();

        GameObject nearTrigger = GameObject.FindWithTag("NearTrigger");
        if (nearTrigger != null)
        {
            enemyTriggerManager = nearTrigger.GetComponent<EnemyTriggerManager_MT>();
        }
        else
        {
            Debug.LogError("NearTrigger not found.");
        }
    }

    public void ChangeTagClosestObject(List<Collider> objectsInTrigger, Transform referencePoint)
    {
        changeIcon.IconChange(_IconNum);

        if (closestEnemyFinder == null)
        {
            Debug.LogError("ClosestEnemyFinder_MT is not assigned.");
            return;
        }

        Collider closestObject = closestEnemyFinder.GetClosestObject(objectsInTrigger, referencePoint);
        if (closestObject != null && closestObject.gameObject.activeSelf && (closestObject.CompareTag("Enemy") || closestObject.CompareTag("Boss")))
        {
            StatusManager_MT statusManagerEnemy = closestObject.GetComponent<StatusManager_MT>();
            if (statusManagerEnemy != null && statusManagerEnemy.HP <= 0)
            {
                objectsInTrigger.Remove(closestObject);

                closestObject.GetComponent<MonsterSkill_KH>().enabled = true;
                _soundEffectManagement.PlayPossessionSound(_audioSource);

                closestObject.gameObject.tag = "Player";
                closestObject.gameObject.transform.rotation = transform.rotation;

                CharacterAnim_MT closestObjectAnim = closestObject.GetComponent<CharacterAnim_MT>();
                if (closestObjectAnim != null)
                {
                    closestObjectAnim.NowAnim = "NewCharacter";
                }
                else
                {
                    Debug.LogError("CharacterAnim_MT not found on closest object.");
                }

                statusManagerPlayer.HP = 0;
                tag = "Enemy";

                _monsterSkill.GameobjectTagJudge();
                closestObject.GetComponent<MonsterSkill_KH>().GameobjectTagJudge();
            }
            else
            {
                Debug.LogError("Closest enemy is either not dead or does not have StatusManager_MT.");
            }
        }
    }
}
