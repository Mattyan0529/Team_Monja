using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCharacter_MT : MonoBehaviour
{
    // 追記：北
    [SerializeField]
    private GameObject _residentScript = default;
    private AudioSource _audioSource = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private MonsterSkill_KH _monsterSkill = default;

    [SerializeField,Header("このキャラクターの番号をいれてねてね")]
    private int _IconNum = default;
    [SerializeField,Header("canvas入れろなさい")]
    private GameObject _canvas = default;

    ChangeIcon_MT changeIcon;
    StatusManager_MT statusManagerPlayer;
    StatusManager_MT statusManagerEnemy;
    ClosestEnemyFinder_MT closestEnemyFinder;
    EnemyHP_MT enemyHP;
    EnemyTriggerManager_MT enemyTriggerManager;

    void Start()
    {
        // 追記：北
        _soundEffectManagement = _residentScript.GetComponent<SoundEffectManagement_KH>();
        _monsterSkill = GetComponent<MonsterSkill_KH>();
        _audioSource = GetComponentInChildren<AudioSource>();

        // プレイヤーのコンポーネントを取得
        changeIcon = _canvas.GetComponentInChildren<ChangeIcon_MT>();
        statusManagerPlayer = GetComponent<StatusManager_MT>();
        closestEnemyFinder = GetComponent<ClosestEnemyFinder_MT>();
        enemyHP = GetComponentInChildren<EnemyHP_MT>();

        GameObject nearTrigger = GameObject.FindWithTag("NearTrigger");
        enemyTriggerManager = nearTrigger.GetComponent<EnemyTriggerManager_MT>();

    }

    // objectsInTriggerリストから最も近い敵のタグPlayerにする
    public void ChangeTagClosestObject(List<Collider> objectsInTrigger, Transform referencePoint)
    {
        changeIcon.IconChange(_IconNum);

        if (closestEnemyFinder == null)
        {
            Debug.LogError("ClosestEnemyFinder_MT is not assigned.");
            return;
        }

        // 最も近い敵のColliderとCharacterAnim_MTを取得
        Collider closestObject = closestEnemyFinder.GetClosestObject(objectsInTrigger, referencePoint);
        CharacterAnim_MT closestObjectAnim = closestObject.GetComponent<CharacterAnim_MT>();

        if (closestObject != null && closestObject.gameObject.activeSelf && (closestObject.CompareTag("Enemy") || closestObject.CompareTag("Boss")))
        {
            statusManagerEnemy = closestObject.GetComponent<StatusManager_MT>();
            if (statusManagerEnemy != null && statusManagerEnemy.HP <= 0)
            {
                // プレイヤーに変更した敵オブジェクトをリストから削除
                objectsInTrigger.Remove(closestObject);

                // 死んだときに切ったスクリプトを復活　追記：北
                closestObject.GetComponent<MonsterSkill_KH>().enabled = true;
                _soundEffectManagement.PlayPossessionSound(_audioSource);
                MonsterSkill_KH monsterSkill = closestObject.GetComponent<MonsterSkill_KH>();
                monsterSkill.enabled = true;

                // 近くの敵のタグを変更
                closestObject.gameObject.tag = "Player";
                // 近くの敵のRotationをリセット
                closestObject.gameObject.transform.rotation = this.gameObject.transform.rotation;
           
                //アニメーションを初期化
                closestObjectAnim.NowAnim = "NewCharacter";
                //自身のHPを0にする
                statusManagerPlayer.HP = 0;
                //自身のタグを変更
                this.gameObject.tag = "Enemy";

                _monsterSkill.GameobjectTagJudge();
                monsterSkill.GameobjectTagJudge();
            }
            else
            {
                Debug.LogError("一番近い敵が死んでいないか、StatusManager_MTを持っていない。");
            }
        }
    }

}
