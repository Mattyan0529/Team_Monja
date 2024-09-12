using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerManager_MT : MonoBehaviour
{
    public List<Collider> objectsInTrigger = new List<Collider>(); // トリガー内のオブジェクトを保持するリスト

    private GameObject _playerObj;

    private GameEndCamera_MT _gameEndCamera;


    private void Start()
    {
        //カメラの親オブジェクトから取得
        _gameEndCamera = GameObject.FindWithTag("CameraPos").GetComponent<GameEndCamera_MT>();
    }

    private void Update()
    {//ゲームオーバーになったらリストの要素を削除
        if (_gameEndCamera.IsGameOver)
        {
            objectsInTrigger.Clear();
            this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// プレイヤーを探して自身を子オブジェクトにする
    /// </summary>
    public void SetToPlayer(GameObject player)
    {
        _playerObj = player;
        transform.SetParent(_playerObj.transform);
        this.transform.localPosition = Vector3.zero;
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boss"))
        {
            if (!objectsInTrigger.Contains(other))
            {
                objectsInTrigger.Add(other);
            }
        }
    }


    // 子オブジェクトのOnTriggerExitの処理
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boss"))
        {
            if (objectsInTrigger.Contains(other))
            {
                objectsInTrigger.Remove(other);
            }
        }
    }
}
