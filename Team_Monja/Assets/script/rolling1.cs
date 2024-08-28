using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rolling1 : MonoBehaviour
{
    // 回転速度を指定するパラメータ
    public float rotationSpeed = 45f;

    void Update()
    {
        // X軸に回転させる
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
