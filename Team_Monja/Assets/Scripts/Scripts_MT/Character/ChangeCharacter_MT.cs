using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCharacter_MT : MonoBehaviour
{
    // �ǋL�F�k
    [SerializeField]
    private GameObject _residentScript = default;
    private AudioSource _audioSource = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;

    [SerializeField,Header("���̃L�����N�^�[�̔ԍ�������Ă˂Ă�")]
    private int _IconNum = default;
    [SerializeField,Header("canvas�����Ȃ���")]
    private GameObject _canvas = default;

    ChangeIcon_MT changeIcon;
    StatusManager_MT statusManagerPlayer;
    StatusManager_MT statusManagerEnemy;
    ClosestEnemyFinder_MT closestEnemyFinder;

    void Start()
    {
        // �ǋL�F�k
        _soundEffectManagement = _residentScript.GetComponent<SoundEffectManagement_KH>();

        // �v���C���[��ChangeIcon_MT�R���|�[�l���g���擾
        changeIcon = _canvas.GetComponentInChildren<ChangeIcon_MT>();
        if (changeIcon == null)
        {
            Debug.LogError("�v���C���[��ChangeIcon_MT��������܂���");
        } 
        
        // �v���C���[��StatusManager_MT�R���|�[�l���g���擾
        statusManagerPlayer = GetComponent<StatusManager_MT>();
        if (statusManagerPlayer == null)
        {
            Debug.LogError("�v���C���[��StatusManager_MT��������܂���");
        }

        // ClosestEnemyFinder_MT�R���|�[�l���g���擾
        closestEnemyFinder = GetComponent<ClosestEnemyFinder_MT>();
        if (closestEnemyFinder == null)
        {
            Debug.LogError("ClosestEnemyFinder_MT��������܂���");
        }
    }

    // objectsInTrigger���X�g����ł��߂��G�̃^�OPlayer�ɂ���
    public void ChangeTagClosestObject(List<Collider> objectsInTrigger, Transform referencePoint)
    {
        changeIcon.IconChange(_IconNum);

        if (closestEnemyFinder == null)
        {
            Debug.LogError("ClosestEnemyFinder_MT is not assigned.");
            return;
        }

        // �ł��߂��G��Collider��CharacterAnim_MT���擾
        Collider closestObject = closestEnemyFinder.GetClosestObject(objectsInTrigger, referencePoint);
        CharacterAnim_MT closestObjectAnim = closestObject.GetComponent<CharacterAnim_MT>();

        if (closestObject != null && closestObject.gameObject.activeSelf && (closestObject.CompareTag("Enemy") || closestObject.CompareTag("Boss")))
        {
            statusManagerEnemy = closestObject.GetComponent<StatusManager_MT>();
            if (statusManagerEnemy != null && statusManagerEnemy.HP <= 0)
            {
                // �v���C���[�ɕύX�����G�I�u�W�F�N�g�����X�g����폜
                objectsInTrigger.Remove(closestObject);

                // ���񂾂Ƃ��ɐ؂����X�N���v�g�𕜊��@�ǋL�F�k
                closestObject.GetComponent<MonsterSkill_KH>().enabled = true;
                if (_audioSource == null)
                {
                    _audioSource = GetComponentInChildren<AudioSource>();
                }
                _soundEffectManagement.PlayPossessionSound(_audioSource);

                // �߂��̓G�̃^�O��ύX
                closestObject.gameObject.tag = "Player";
                //�A�j���[�V������������
                closestObjectAnim.NowAnim = "NewCharacter";
                //���g��HP��0�ɂ���
                statusManagerPlayer.HP = 0;
                //���g�̃^�O��ύX
                this.gameObject.tag = "Enemy";
            }
            else
            {
                Debug.LogError("��ԋ߂��G������ł��Ȃ����AStatusManager_MT�������Ă��Ȃ��B");
            }
        }
    }

}
