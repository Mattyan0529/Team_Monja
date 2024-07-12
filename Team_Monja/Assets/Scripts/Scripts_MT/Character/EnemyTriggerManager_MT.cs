using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerManager_MT : MonoBehaviour
{
    public List<Collider> objectsInTrigger = new List<Collider>(); // トリガー内のオブジェクトを保持するリスト

    private GameObject _playerObj;

    private void Start()
    {
        SetToPlayer();
    }

    /// <summary>
    /// プレイヤーを探して自身を子オブジェクトにする
    /// </summary>
    public void SetToPlayer()
    {
        _playerObj = GameObject.FindWithTag("Player");
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
