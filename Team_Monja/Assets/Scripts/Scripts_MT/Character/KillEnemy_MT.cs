using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillEnemy_MT : MonoBehaviour
{
    // 最も近いオブジェクトを取得するスクリプト
    private ClosestEnemyFinder_MT _closestEnemyFinder;
    // トリガー内の敵を管理するスクリプト
    private EnemyTriggerManager_MT _enemyTriggerManager;
    //死んだときのカメラを制御するスクリプト
    private GameEndCamera_MT _gameEndCamera;

    private bool _coroutineSwitch = true;

    private void Start()
    {
        _closestEnemyFinder = GetComponent<ClosestEnemyFinder_MT>();
        _enemyTriggerManager = GetComponent<EnemyTriggerManager_MT>();
        _gameEndCamera = GetComponent<GameEndCamera_MT>();
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーの周りの敵を取得
        Collider closestEnemy = _closestEnemyFinder.GetClosestObject(_enemyTriggerManager.objectsInTrigger, transform);

        // 近くの敵が存在する場合
        if (closestEnemy != null)
        {
            // 敵のStatusManager_MTコンポーネントを取得
            StatusManager_MT enemyStatus = closestEnemy.GetComponent<StatusManager_MT>();

            // 近くの敵がボスであり、HPが0以下の場合
            if (enemyStatus != null && closestEnemy.CompareTag("Boss") && enemyStatus.HP <= 0)
            {
                if(_coroutineSwitch)
                {
                    // ボスが死んだときの処理
                    StartCoroutine(_gameEndCamera.GameClearCoroutine());
                    _coroutineSwitch = false;
                }
          
            }
        }
    }

}