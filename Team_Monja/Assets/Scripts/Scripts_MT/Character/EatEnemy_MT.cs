using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatEnemy_MT : MonoBehaviour
{
    // �ǋL�F�k
    [SerializeField]
    private GameObject _residentScript = default;
    private AudioSource _audioSource = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;

    StatusManager_MT statusManagerEnemy;
    StatusManager_MT statusManagerPlayer;
    ClosestEnemyFinder_MT closestEnemyFinder;


    private void Start()
    {
        statusManagerPlayer = GetComponent<StatusManager_MT>();
        closestEnemyFinder = GetComponent<ClosestEnemyFinder_MT>();

        // �ǋL�F�k
        _soundEffectManagement = _residentScript.GetComponent<SoundEffectManagement_KH>();

        if (closestEnemyFinder == null)
        {
            Debug.LogError("ClosestEnemyFinder_MT is not assigned.");
        }
    }

    public void RemoveClosestObject(List<Collider> objectsInTrigger, Transform referencePoint)
    {
        if (objectsInTrigger == null)
        {
            Debug.LogError("objectsInTrigger����ł�");
            return;
        }

        if (objectsInTrigger.Count == 0)
        {
            return;
        }

        // ClosestEnemyFinder_MT���g�p���čł��߂��G���擾
        Collider closestObject = closestEnemyFinder.GetClosestObject(objectsInTrigger, referencePoint);

        if (closestObject != null && closestObject.gameObject.activeSelf &&( closestObject.CompareTag("Enemy") || closestObject.CompareTag("Boss")))
        {
            statusManagerEnemy = closestObject.GetComponent<StatusManager_MT>();
            if (statusManagerEnemy != null && statusManagerEnemy.HP <= 0 )
            {
                //����MaxHP���L�^
                int currentMaxHP = statusManagerPlayer.MaxHP;

                // ���݂̃X�e�[�^�X�̔{�������Z�b�g
                statusManagerPlayer.ResetMultipliers();

                // �v���X�X�e�[�^�X�����Z
                statusManagerPlayer.PlusHP += statusManagerEnemy.PlusHP;
                statusManagerPlayer.PlusStrength += statusManagerEnemy.PlusStrength;
                statusManagerPlayer.PlusDefense += statusManagerEnemy.PlusDefense;

                // �ł��߂��G�����X�g����폜���A��A�N�e�B�u��
                objectsInTrigger.Remove(closestObject);
                closestObject.gameObject.SetActive(false);

                // �X�e�[�^�X�̔{�����ēK�p
                statusManagerPlayer.ApplyMultipliers();
                //�H�ׂ�MaxHP����������������
                statusManagerPlayer.HealHP(statusManagerPlayer.MaxHP - currentMaxHP);

                // �ǋL�F�k
                if (_audioSource == null)
                {
                    _audioSource = GetComponentInChildren<AudioSource>();
                }
                _soundEffectManagement.PlayEatSound(_audioSource);
            }
            else
            {
                Debug.LogError("��ԋ߂��G������ł��Ȃ����AStatusManager_MT�������Ă��Ȃ��B");
            }
        }
    }
}