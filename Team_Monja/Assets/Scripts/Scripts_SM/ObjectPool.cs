using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトプールクラス
/// </summary>
public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    GameObject prefab; // プールするオブジェクトのPrefab

    [SerializeField]
    int poolSize = 10; // 初期プールサイズ

    List<GameObject> pool; // プールのリスト

    void Awake()
    {
        pool = new List<GameObject>();
        // 初期プールを作成する
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    // オブジェクトをプールから取得する
    GameObject GetObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // プールが足りない場合は新しいオブジェクトを生成して追加する
        GameObject newObj = Instantiate(prefab);
        newObj.SetActive(true);
        pool.Add(newObj);
        return newObj;
    }

    // オブジェクトをプールに戻す
    void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    // エフェクトを表示し、一定時間後にプールに戻す
    void ShowEffect(Vector3 position, Quaternion rotation, Vector3 scale, float duration)
    {
        GameObject effect = GetObject();
        effect.transform.position = position;
        effect.transform.rotation = rotation;
        effect.transform.localScale = scale;
        StartCoroutine(ReturnEffectToPool(effect, duration));
    }

    // 一定時間後にエフェクトをプールに戻すコルーチン
    IEnumerator ReturnEffectToPool(GameObject effect, float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnObject(effect);
    }

    // 外部から呼び出されるメソッド：エフェクトを表示する
    public void ShowEffectPublic(Vector3 position, Quaternion rotation, Vector3 scale, float duration)
    {
        ShowEffect(position, rotation, scale, duration);
    }
}
