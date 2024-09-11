using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    [SerializeField] float lockOnRadius = 10f; // ���b�N�I���͈͂̔��a
    private List<Transform> targets = new List<Transform>(); // �^�[�Q�b�g���̃��X�g
    private int currentTargetIndex = 0; // ���݂̃^�[�Q�b�g�̃C���f�b�N�X
    public bool isLockedOn = false; // ���b�N�I����Ԃ̃t���O
    private CharacterDeadDecision_MT dead;
    [SerializeField] private Transform childObject; // �q�I�u�W�F�N�g�i���C���J�����j
    [SerializeField] private GameObject _lockOnImage; // ���b�N�I�������Ƃ��ɉ�ʂɕ\������C���[�W

    private void Start()
    {
        // �q�I�u�W�F�N�g���C���f�b�N�X�Ŏ擾���� (�����ł̓��C���J������z��)
        childObject = Camera.main.transform; // ���C���J�����Ƃ��ď�����
        _lockOnImage.SetActive(false); // ���������Ƀ��b�N�I���C���[�W���\����
        isLockedOn = false;
    }

    void Update()
    {
        // "TargetButton" �{�^���������ꂽ�u�Ԃ����o
        if (Input.GetButtonDown("TargetButton"))
        {
            if (!isLockedOn)
            {
                // ���b�N�I�����J�n���A�^�[�Q�b�g�����X�g�ɒǉ�
                isLockedOn = true;
                FindTargets();
            }
            else
            {
                FindTargets();
                // ���b�N�I�����Ȃ玟�̃^�[�Q�b�g�ɐ؂�ւ���
                SwitchTarget(1);
            }
        }

        // �^�[�Q�b�g���X�g����łȂ��ꍇ
        if (isLockedOn && targets.Count > 0)
        {
            Transform currentTarget = targets[currentTargetIndex]; // ���݃��b�N�I�����Ă���^�[�Q�b�g
            dead = currentTarget.GetComponent<CharacterDeadDecision_MT>();

            // �L�����N�^�[���^�[�Q�b�g�����Ɍ�������
            Vector3 direction = currentTarget.position - transform.position; // �^�[�Q�b�g�܂ł̕����x�N�g�����v�Z
            Quaternion rotation = Quaternion.LookRotation(direction); // ���̕�����������]���v�Z
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 4f); // �X���[�Y�ɉ�]������

            // �^�[�Q�b�g�����񂾏ꍇ�A���X�g����폜���Ď��̃^�[�Q�b�g�ɐ؂�ւ���
            if (dead.IsDeadDecision())
            {
                targets.RemoveAt(currentTargetIndex); // ���݂̃^�[�Q�b�g�����X�g����폜
                if (targets.Count > 0)
                {
                    currentTargetIndex = currentTargetIndex % targets.Count; // ���X�g����łȂ��ꍇ�͎��̃^�[�Q�b�g��
                }
                else
                {
                    isLockedOn = false; // ���X�g����Ȃ烍�b�N�I��������
                    ResetRotation();
                }
            }

            // �^�[�Q�b�g�����݂���ԁA���b�N�I���C���[�W��Ǐ]������
            Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(currentTarget.position); // �^�[�Q�b�g�̃X�N���[�����W
            if (targetScreenPos.z > 0)
            {
                _lockOnImage.transform.position = targetScreenPos; // �C���[�W���X�N���[�����W�ɔz�u
                _lockOnImage.SetActive(true); // ���b�N�I���C���[�W��\��
            }
            else
            {
                _lockOnImage.SetActive(false); // �J�����O�ɂ���ꍇ�͔�\��
            }
        }
        else
        {
            _lockOnImage.SetActive(false); // ���b�N�I���������A�C���[�W���\��
        }

        // �^�[�Q�b�g�����Ȃ��ꍇ�̓��b�N�I��������
        if (targets.Count <= 0)
        {
            isLockedOn = false;
        }

        // �^�[�Q�b�g�L�����Z���{�^���������ꂽ�烍�b�N�I������
        if (Input.GetButtonDown("TargetCancel"))
        {
            isLockedOn = false;
            ResetRotation();
            targets.Clear(); // �^�[�Q�b�g���X�g���N���A
            _lockOnImage.SetActive(false); // ���b�N�I���C���[�W���\��
        }
    }

    // �^�[�Q�b�g����T��
    void FindTargets()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, lockOnRadius); // �v���C���[���ӂ̃R���C�_�[���擾
        Transform nearestTarget = null;
        float nearestDistance = Mathf.Infinity;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy") || hitCollider.CompareTag("Boss"))
            {
                if (!targets.Contains(hitCollider.transform))
                {
                    float distanceToTarget = Vector3.Distance(transform.position, hitCollider.transform.position); // �v���C���[�Ƃ̋������v�Z
                    if (distanceToTarget < nearestDistance)
                    {
                        nearestDistance = distanceToTarget;
                        nearestTarget = hitCollider.transform; // �ł��߂��^�[�Q�b�g���L�^
                    }
                    targets.Add(hitCollider.transform); // �^�[�Q�b�g���X�g�ɒǉ�
                }
            }
        }

        if (nearestTarget != null)
        {
            targets.Remove(nearestTarget); // �ł��߂��^�[�Q�b�g�����X�g�̍ŏ��Ɉړ�
            targets.Insert(0, nearestTarget);
            currentTargetIndex = 0; // �ŏ��̃^�[�Q�b�g�����݂̃^�[�Q�b�g�ɐݒ�
        }
    }

    // �^�[�Q�b�g��؂�ւ���
    void SwitchTarget(int direction)
    {
        if (targets.Count == 0) return;

        currentTargetIndex += direction; // �^�[�Q�b�g�C���f�b�N�X���X�V
        if (currentTargetIndex < 0)
        {
            currentTargetIndex = targets.Count - 1; // �C���f�b�N�X�����̏ꍇ�A���X�g�̍Ō�ɖ߂�
        }
        else if (currentTargetIndex >= targets.Count)
        {
            currentTargetIndex = 0; // �C���f�b�N�X���͈͊O�̏ꍇ�A���X�g�̍ŏ��ɖ߂�
        }
    }

    // �L�����N�^�[�̉�]�����Z�b�g����
    void ResetRotation()
    {
        Vector3 newRotation = transform.localEulerAngles;
        newRotation.x = 0f; // X���̃��[�e�[�V������0�ɖ߂�
        transform.localEulerAngles = newRotation;
    }
}
