using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rolling1 : MonoBehaviour
{
    // ��]���x���w�肷��p�����[�^
    public float rotationSpeed = 45f;

    void Update()
    {
        // X���ɉ�]������
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
