using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeCharacter_MT : MonoBehaviour
{
    [SerializeField] private GameObject _residentScript = default;
    private AudioSource _audioSource = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private MonsterSkill_KH _monsterSkill = default;
    private PlayerSkill_KH _playerSkill = default;

    [SerializeField] private int _IconNum = default;
    [SerializeField] private GameObject _canvasPlayer = default;

    private ChangeIcon_MT changeIcon;
    private StatusManager_MT statusManagerPlayer;
    private ClosestEnemyFinder_MT closestEnemyFinder;
    private GameObject _playerObj;

    void Start()
    {
        _soundEffectManagement = _residentScript.GetComponent<SoundEffectManagement_KH>();
        changeIcon = _canvasPlayer.GetComponentInChildren<ChangeIcon_MT>();
        closestEnemyFinder = GameObject.FindWithTag("PlayerManager").GetComponent<ClosestEnemyFinder_MT>();


        SetPlayer();
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
                //一番近い死んでいる敵のタグをPlayerにする
                closestObject.gameObject.tag = "Player";

                //回転は維持
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
                //タグをEnemyにする
                _playerObj.tag = "Enemy";
                //このキャラクターを殺す
                statusManagerPlayer.HP = 0;


                _monsterSkill.GameobjectTagJudge();
                _playerSkill.GameObjectTagJudge();
                closestObject.GetComponent<MonsterSkill_KH>().GameobjectTagJudge();
            }
            else
            {
                Debug.LogError("Closest enemy is either not dead or does not have StatusManager_MT.");
            }
        }
    }
    public void SetPlayer()
    {
        _playerObj = GameObject.FindWithTag("Player");
        _monsterSkill = _playerObj.GetComponent<MonsterSkill_KH>();
        _playerSkill = _playerObj.GetComponent<PlayerSkill_KH>();
        _audioSource = _playerObj.GetComponent<AudioSource>();
        statusManagerPlayer = _playerObj.GetComponent<StatusManager_MT>();
    }


}
