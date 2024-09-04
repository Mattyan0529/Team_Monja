using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    [SerializeField] float lockOnRadius = 10f; // ���b�N�I���͈͂̔��a
    private List<Transform> targets = new List<Transform>(); // �^�[�Q�b�g���̃��X�g
    private int currentTargetIndex = 0; // ���݂̃^�[�Q�b�g�̃C���f�b�N�X
    private bool isLockedOn = false; // ���b�N�I����Ԃ̃t���O
    [SerializeField] CharacterDeadDecision_MT dead;

    void Update()
    {
        // "TargetButton" �{�^���������ꂽ�u�Ԃ����o
        if (Input.GetButtonDown("TargetButton"))
        {
            if (!isLockedOn)
            {
                // ���b�N�I�����J�n����ꍇ�A�^�[�Q�b�g�����X�g�ɒǉ����čŏ��̃^�[�Q�b�g�����b�N�I��
                isLockedOn = true;
                FindTargets();
            }
            else
            {
                // ���b�N�I�����̏ꍇ�A���̃^�[�Q�b�g�ɐ؂�ւ���
                SwitchTarget(1);
            }
        }

        // �^�[�Q�b�g���X�g����łȂ��ꍇ
        if (isLockedOn && targets.Count > 0)
        {
            // ���݃��b�N�I�����Ă���^�[�Q�b�g��Transform���擾
            Transform currentTarget = targets[currentTargetIndex];

            // ���݃^�[�Q�b�g���Ă���I�u�W�F�N�g�̃X�N���v�g���擾
           dead = currentTarget.GetComponent<CharacterDeadDecision_MT>();

            // �L�����N�^�[�����݂̃^�[�Q�b�g�̕����������悤�ɂ���
            Vector3 direction = currentTarget.position - transform.position; // �^�[�Q�b�g�܂ł̕����x�N�g�����v�Z
            Quaternion rotation = Quaternion.LookRotation(direction); // ���̕�����������]���v�Z

            // ���݂̉�]����^�[�Q�b�g�����̉�]�ցA�X���[�Y�ɉ�]������
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 4f);

            if (dead.IsDeadDecision())
            {
                targets.RemoveAt(currentTargetIndex); // ���݂̃^�[�Q�b�g�����X�g����폜

                // ���X�g���܂���łȂ��ꍇ�A���̃^�[�Q�b�g�����b�N�I��
                if (targets.Count > 0)
                {
                    currentTargetIndex = currentTargetIndex % targets.Count; // �V�����C���f�b�N�X�����X�g�͈͓̔��Ɏ��܂�悤�ɒ���
                }
                else
                {
                    isLockedOn = false; // ���X�g����Ȃ烍�b�N�I��������
                }
            }

        }


        if (Input.GetButtonDown("TargetCancel"))
        {
            isLockedOn = false;
        }
    }

    void FindTargets()
    {
        // �v���C���[�̈ʒu�𒆐S�ɁA�w�肵�����a���ɂ��邷�ׂẴR���C�_�[���擾
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, lockOnRadius);

        // ���݂̃^�[�Q�b�g���X�g���N���A
        targets.Clear();

        // �擾�����R���C�_�[�̒�����A�uEnemy�v�^�O�������̂��^�[�Q�b�g���X�g�ɒǉ�
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                // �^�[�Q�b�g���X�g�ɓG��Transform��ǉ�
                targets.Add(hitCollider.transform);
            }
        }

        // �^�[�Q�b�g���X�g����łȂ���΁A�ŏ��̃^�[�Q�b�g�Ƀ��b�N�I��
        if (targets.Count > 0)
        {
            // ���݂̃^�[�Q�b�g�C���f�b�N�X��0�i�ŏ��̃^�[�Q�b�g�j�ɐݒ�
            currentTargetIndex = 0;
        }
    }

    void SwitchTarget(int direction)
    {
        // �^�[�Q�b�g��1���Ȃ��ꍇ�͏������I��
        if (targets.Count == 0) return;

        // ���݂̃^�[�Q�b�g�C���f�b�N�X���A�����ɉ����čX�V
        currentTargetIndex += direction;

        // �C���f�b�N�X�����X�g�͈̔͂𒴂��Ȃ��悤�ɒ���
        // �C���f�b�N�X�����̒l�ɂȂ����ꍇ�i�͈͊O�j�A���X�g�̍Ō�̃^�[�Q�b�g�Ƀ��[�v����
        if (currentTargetIndex < 0)
        {
            currentTargetIndex = targets.Count - 1;
        }
        // �C���f�b�N�X�����X�g�̃T�C�Y�ȏ�ɂȂ����ꍇ�i�͈͊O�j�A���X�g�̍ŏ��̃^�[�Q�b�g�Ƀ��[�v����
        else if (currentTargetIndex >= targets.Count)
        {
            currentTargetIndex = 0;
        }
    }


}


