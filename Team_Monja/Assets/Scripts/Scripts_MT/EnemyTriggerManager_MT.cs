using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerManager_MT : MonoBehaviour
{
    public List<Collider> objectsInTrigger = new List<Collider>(); // �g���K�[���̃I�u�W�F�N�g��ێ����郊�X�g


    //�q�I�u�W�F�N�g��OnTriggerStay�̏���
    public void OnChildTriggerStayCanEat(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!objectsInTrigger.Contains(other))
            {
                objectsInTrigger.Add(other);
            }
        }
    }


    // �q�I�u�W�F�N�g��OnTriggerExit�̏���
    public void OnChildTriggerExitCanEat(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (objectsInTrigger.Contains(other))
            {
                objectsInTrigger.Remove(other);
            }
        }
    }
}
