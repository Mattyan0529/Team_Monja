using UnityEngine;

public class ActivateObjectsOnCollision : MonoBehaviour
{
    // �A�N�e�B�u�ɂ������I�u�W�F�N�g��Inspector�Őݒ�
    public GameObject[] objectsToActivate;

    void OnCollisionEnter(Collision collision)
    {
        // 4�̃I�u�W�F�N�g���A�N�e�B�u�ɂ���
        for (int i = 0; i < objectsToActivate.Length; i++)
        {
            objectsToActivate[i].SetActive(true);
        }
    }
}

