using UnityEngine;

public class ActivateObjectsOnCollision : MonoBehaviour
{
    // アクティブにしたいオブジェクトをInspectorで設定
    public GameObject[] objectsToActivate;

    void OnCollisionEnter(Collision collision)
    {
        // 4つのオブジェクトをアクティブにする
        for (int i = 0; i < objectsToActivate.Length; i++)
        {
            objectsToActivate[i].SetActive(true);
        }
    }
}

