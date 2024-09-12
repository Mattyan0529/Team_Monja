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
    [SerializeField] private LayerMask obstacleLayerMask; // ��Q���̃��C���[�}�X�N

    private Camera _camera; // ���C���J����

    private void Start()
    {
        _camera = Camera.main; // ���C���J�����̎擾
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
                isLockedOn = true;
                FindTargets();
            }
            else
            {
                FindTargets();
                SwitchTarget(1);
            }
        }

        if (isLockedOn && targets.Count > 0)
        {
            Transform currentTarget = targets[currentTargetIndex]; // ���݃��b�N�I�����Ă���^�[�Q�b�g
            dead = currentTarget.GetComponent<CharacterDeadDecision_MT>();

            Vector3 direction = currentTarget.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 4f); // �v���C���[���^�[�Q�b�g������

            // �^�[�Q�b�g�ƃv���C���[�̊Ԃɏ�Q�������邩�m�F
            if (IsObstacleBetween(transform.position, currentTarget))
            {
                CancelLockOn(); // ��Q��������ꍇ�Ƀ��b�N�I��������
                return;
            }

            if (dead != null && dead.IsDeadDecision())
            {
                targets.RemoveAt(currentTargetIndex);
                if (targets.Count > 0)
                {
                    currentTargetIndex = currentTargetIndex % targets.Count;
                }
                else
                {
                    CancelLockOn();
                }
            }

            Vector3 targetScreenPos = _camera.WorldToScreenPoint(currentTarget.position);
            if (targetScreenPos.z > 0)
            {
                _lockOnImage.transform.position = targetScreenPos;
                _lockOnImage.SetActive(true);
            }
            else
            {
                CancelLockOn(); // �^�[�Q�b�g�������Ȃ��Ȃ����ꍇ�Ƀ��b�N�I��������
            }
        }
        else
        {
            _lockOnImage.SetActive(false); // �^�[�Q�b�g�����Ȃ��ꍇ�̓C���[�W���\����
        }

        if (Input.GetButtonDown("TargetCancel"))
        {
            CancelLockOn();
        }
    }

    void FindTargets()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, lockOnRadius);
        Transform nearestTarget = null;
        float nearestDistance = Mathf.Infinity;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy") || hitCollider.CompareTag("Boss"))
            {
                if (!targets.Contains(hitCollider.transform))
                {
                    float distanceToTarget = Vector3.Distance(transform.position, hitCollider.transform.position);
                    if (distanceToTarget < nearestDistance)
                    {
                        nearestDistance = distanceToTarget;
                        nearestTarget = hitCollider.transform;
                    }
                    targets.Add(hitCollider.transform);
                }
            }
        }

        if (nearestTarget != null)
        {
            targets.Remove(nearestTarget);
            targets.Insert(0, nearestTarget);
            currentTargetIndex = 0;
        }
    }

    void SwitchTarget(int direction)
    {
        if (targets.Count == 0) return;

        currentTargetIndex += direction;
        if (currentTargetIndex < 0)
        {
            currentTargetIndex = targets.Count - 1;
        }
        else if (currentTargetIndex >= targets.Count)
        {
            currentTargetIndex = 0;
        }
    }

    public void CancelLockOn()
    {
        isLockedOn = false;
        targets.Clear();
        _lockOnImage.SetActive(false);
        ResetRotation();
    }

    void ResetRotation()
    {
        Vector3 newRotation = transform.localEulerAngles;
        newRotation.x = 0f;
        transform.localEulerAngles = newRotation;
    }

    bool IsObstacleBetween(Vector3 fromPosition, Transform target)
    {
        // �v���C���[�ƃ^�[�Q�b�g�̊Ԃɏ�Q�����Ȃ����m�F����Raycast
        Vector3 directionToTarget = (target.position - fromPosition).normalized;
        float distanceToTarget = Vector3.Distance(fromPosition, target.position);

        // Raycast���g���ď�Q���̔���
        if (Physics.Raycast(fromPosition, directionToTarget, distanceToTarget, obstacleLayerMask))
        {
            return true; // ��Q��������
        }

        return false; // ��Q�����Ȃ�
    }
}
