using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerManager_MT : MonoBehaviour
{
    public List<Collider> objectsInTrigger = new List<Collider>(); // �g���K�[���̃I�u�W�F�N�g��ێ����郊�X�g

    private GameObject _playerObj;

    private GameEndCamera_MT _gameEndCamera;


    private void Start()
    {
        //�J�����̐e�I�u�W�F�N�g����擾
        _gameEndCamera = GameObject.FindWithTag("CameraPos").GetComponent<GameEndCamera_MT>();
    }

    private void Update()
    {//�Q�[���I�[�o�[�ɂȂ����烊�X�g�̗v�f���폜
        if (_gameEndCamera.IsGameOver)
        {
            objectsInTrigger.Clear();
            this.gameObject.SetActive(false);
        }
    }

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
