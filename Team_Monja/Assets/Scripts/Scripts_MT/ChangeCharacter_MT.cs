using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCharacter_MT : MonoBehaviour
{
    StatusManager_MT statusManagerPlayer;
    StatusManager_MT statusManagerEnemy;
    ClosestEnemyFinder_MT closestEnemyFinder;

    void Start()
    {
        // プレイヤーのStatusManager_MTコンポーネントを取得
        statusManagerPlayer = GetComponent<StatusManager_MT>();
        if (statusManagerPlayer == null)
        {
            Debug.LogError("プレイヤーのStatusManager_MTが見つかりません");
        }

        // ClosestEnemyFinder_MTコンポーネントを取得
        closestEnemyFinder = GetComponent<ClosestEnemyFinder_MT>();
        if (closestEnemyFinder == null)
        {
            Debug.LogError("ClosestEnemyFinder_MTが見つかりません");
        }
    }

    // objectsInTriggerリストから最も近い敵のタグPlayerにする
    public void ChangeTagClosestObject(List<Collider> objectsInTrigger, Transform referencePoint)
    {
        if (closestEnemyFinder == null)
        {
            Debug.LogError("ClosestEnemyFinder_MT is not assigned.");
            return;
        }

        // 最も近い敵のColliderを取得
        Collider closestObject = closestEnemyFinder.GetClosestObject(objectsInTrigger, referencePoint);

        if (closestObject != null && closestObject.gameObject.activeSelf && closestObject.CompareTag("Enemy"))
        {
            statusManagerEnemy = closestObject.GetComponent<StatusManager_MT>();
            if (statusManagerEnemy != null && statusManagerEnemy.HP <= 0)
            {
                // プレイヤーに変更した敵オブジェクトをリストから削除
                objectsInTrigger.Remove(closestObject);

                // 死んだときに切ったスクリプトを復活　追記：北
                closestObject.GetComponent<MonsterSkill_KH>().enabled = true;

                // タグを変更
                closestObject.gameObject.tag = "Player";
                this.gameObject.tag = "Enemy";
            }
            else
            {
                Debug.LogError("一番近い敵が死んでいないか、StatusManager_MTを持っていない。");
            }
        }
    }

}
