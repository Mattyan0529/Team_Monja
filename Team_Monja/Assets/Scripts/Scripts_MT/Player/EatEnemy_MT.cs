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
    private CharacterAnim_MT characterAnim;
    StatusManager_MT statusManagerEnemy;
    StatusManager_MT statusManagerPlayer;
    ClosestEnemyFinder_MT closestEnemyFinder;
    GameObject _player;

    private void Start()
    {
        closestEnemyFinder = GameObject.FindWithTag("PlayerManager").GetComponent<ClosestEnemyFinder_MT>();
        _soundEffectManagement = _residentScript.GetComponent<SoundEffectManagement_KH>();
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
                characterAnim.NowAnim = "Eat";
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

                _soundEffectManagement.PlayEatSound(_audioSource);
            }
            else
            {
                Debug.LogError("��ԋ߂��G������ł��Ȃ����AStatusManager_MT�������Ă��Ȃ��B");
            }
        }
    }
    public void SetPlayer(GameObject player)
    {
        statusManagerPlayer = player.GetComponent<StatusManager_MT>();
        _audioSource = player.GetComponent<AudioSource>();
        characterAnim = player.GetComponent<CharacterAnim_MT>();
    }
}