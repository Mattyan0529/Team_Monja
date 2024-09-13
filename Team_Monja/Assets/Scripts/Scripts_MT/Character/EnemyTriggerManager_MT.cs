using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerManager_MT : MonoBehaviour
{
    // トリガー内の敵オブジェクトを保持するリスト
    public List<Collider> objectsInTrigger = new List<Collider>();

    private GameObject _playerObj; // プレイヤーオブジェクトを保持する変数
    private GameEndCamera_MT _gameEndCamera; // ゲーム終了時のカメラ制御用スクリプト

    private void Start()
    {
        // カメラの親オブジェクトからGameEndCamera_MTスクリプトを取得
        _gameEndCamera = GameObject.FindWithTag("CameraPos").GetComponent<GameEndCamera_MT>();
    }

    private void Update()
    {
        // ゲームオーバー時にトリガー内のオブジェクトリストをクリアし、オブジェクトを無効化
        if (_gameEndCamera.IsGameOver)
        {
            objectsInTrigger.Clear(); // リストの要素をすべて削除
            gameObject.SetActive(false); // 自分自身を非アクティブにする
        }
    }

    /// <summary>
    /// プレイヤーを探して、自身をプレイヤーの子オブジェクトにするメソッド
    /// </summary>
    public void SetToPlayer(GameObject player)
    {
        _playerObj = player; // プレイヤーオブジェクトを設定
        transform.SetParent(_playerObj.transform); // 自身をプレイヤーの子オブジェクトに設定
        transform.localPosition = Vector3.zero; // プレイヤーの位置に合わせて自身のローカルポジションをリセット
    }

    private void OnTriggerStay(Collider other)
    {
        // トリガー内にEnemyまたはBossがいる場合、そのColliderをリストに追加
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boss"))
        {
            // リストに含まれていない場合のみ追加
            if (!objectsInTrigger.Contains(other))
            {
                objectsInTrigger.Add(other);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // トリガーからEnemyまたはBossが出た場合、そのColliderをリストから削除
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boss"))
        {
            objectsInTrigger.Remove(other); // リストに含まれている場合のみ削除
        }
    }
}
