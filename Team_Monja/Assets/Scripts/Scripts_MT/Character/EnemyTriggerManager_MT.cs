using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerManager_MT : MonoBehaviour
{
    // �g���K�[���̓G�I�u�W�F�N�g��ێ����郊�X�g
    public List<Collider> objectsInTrigger = new List<Collider>();

    private GameObject _playerObj; // �v���C���[�I�u�W�F�N�g��ێ�����ϐ�
    private GameEndCamera_MT _gameEndCamera; // �Q�[���I�����̃J��������p�X�N���v�g

    private void Start()
    {
        // �J�����̐e�I�u�W�F�N�g����GameEndCamera_MT�X�N���v�g���擾
        _gameEndCamera = GameObject.FindWithTag("CameraPos").GetComponent<GameEndCamera_MT>();
    }

    private void Update()
    {
        // �Q�[���I�[�o�[���Ƀg���K�[���̃I�u�W�F�N�g���X�g���N���A���A�I�u�W�F�N�g�𖳌���
        if (_gameEndCamera.IsGameOver)
        {
            objectsInTrigger.Clear(); // ���X�g�̗v�f�����ׂč폜
            gameObject.SetActive(false); // �������g���A�N�e�B�u�ɂ���
        }
    }

    /// <summary>
    /// �v���C���[��T���āA���g���v���C���[�̎q�I�u�W�F�N�g�ɂ��郁�\�b�h
    /// </summary>
    public void SetToPlayer(GameObject player)
    {
        _playerObj = player; // �v���C���[�I�u�W�F�N�g��ݒ�
        transform.SetParent(_playerObj.transform); // ���g���v���C���[�̎q�I�u�W�F�N�g�ɐݒ�
        transform.localPosition = Vector3.zero; // �v���C���[�̈ʒu�ɍ��킹�Ď��g�̃��[�J���|�W�V���������Z�b�g
    }

    private void OnTriggerStay(Collider other)
    {
        // �g���K�[����Enemy�܂���Boss������ꍇ�A����Collider�����X�g�ɒǉ�
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boss"))
        {
            // ���X�g�Ɋ܂܂�Ă��Ȃ��ꍇ�̂ݒǉ�
            if (!objectsInTrigger.Contains(other))
            {
                objectsInTrigger.Add(other);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �g���K�[����Enemy�܂���Boss���o���ꍇ�A����Collider�����X�g����폜
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boss"))
        {
            objectsInTrigger.Remove(other); // ���X�g�Ɋ܂܂�Ă���ꍇ�̂ݍ폜
        }
    }
}
