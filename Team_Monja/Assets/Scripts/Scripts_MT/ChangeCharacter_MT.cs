using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCharacter_MT : MonoBehaviour
{
    StatusManager_MT statusManagerPlayer;
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
            // プレイヤーに変更した敵オブジェクトをリストから削除
            objectsInTrigger.Remove(closestObject);

            // タグを変更
            closestObject.gameObject.tag = "Player";
            this.gameObject.tag = "Enemy";
        }
        else
        {
            Debug.Log("No closest object found.");
        }
    }

}
