using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerManager_MT : MonoBehaviour
{
    public List<Collider> objectsInTrigger = new List<Collider>(); // �g���K�[���̃I�u�W�F�N�g��ێ����郊�X�g

    private GameObject _playerObj;


    /// <summary>
    /// �v���C���[��T���Ď��g���q�I�u�W�F�N�g�ɂ���
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


    // �q�I�u�W�F�N�g��OnTriggerExit�̏���
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
