
using UnityEngine;
using System.Collections;

public class TriggerHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] _HitEffects; // �A�N�e�B�u�ɂ������I�u�W�F�N�g
    private GameObject _objectToActivate; // �A�N�e�B�u�ɂ������I�u�W�F�N�g
    [SerializeField] private float _activationDuration = 3f; // �I�u�W�F�N�g���A�N�e�B�u�ł��鎞�ԁi�b�j
    
    [SerializeField] private CapsuleCollider _capsuleCollider; // �C���X�y�N�^�[��Őݒ肷��CapsuleCollider
    private Vector3 _debugClosestPoint = Vector3.zero; // �f�o�b�O�p�̕ϐ�

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NormalHit") || other.CompareTag("SlashHit") || other.CompareTag("HeartHit") || other.CompareTag("ThunderHit"))    
        {
            print("�U����������: " + other);

            if(other.CompareTag("NormalHit"))
            {
                _objectToActivate = _HitEffects[0];
            }
            if (other.CompareTag("SlashHit"))
            {
                _objectToActivate = _HitEffects[1];
            }
            if (other.CompareTag("HeartHit"))
            {
                _objectToActivate = _HitEffects[2];
            }
            if (other.CompareTag("ThunderHit"))
            {
                _objectToActivate = _HitEffects[4];
            }

            // �R���W���������I�u�W�F�N�g�̐e���擾
            Transform parentTransform = other.transform.parent;

            if (parentTransform != null)
            {
                // �e�I�u�W�F�N�g�Ƃ̋�������ɃJ�v�Z���̑��ʂ̍ł��߂��_���擾
                _debugClosestPoint = GetClosestPointOnSideOfCapsule(parentTransform.position);

                if (_debugClosestPoint != Vector3.zero)
                {
                    // �f�o�b�O�p�Ɍv�Z���ʂ��o��
                    Debug.Log("�ł��߂��J�v�Z���̑��ʂ̓_: " + _debugClosestPoint);

                    // ���̍��W�ɃI�u�W�F�N�g���ړ����ăA�N�e�B�u��
                    ActivateObjectAtPosition(_debugClosestPoint);
                    StartCoroutine(DeactivateAfterDelay(_activationDuration));
                }
            }
            else
            {
                Debug.Log("�e�I�u�W�F�N�g������܂���");
            }
        }
    }

    private Vector3 GetClosestPointOnSideOfCapsule(Vector3 targetPosition)
    {
        if (_capsuleCollider != null)
        {
            // �J�v�Z���R���C�_�[�̍����A���a�A���[�J����Ԃł̒��S���擾
            float height = _capsuleCollider.height;
            float radius = _capsuleCollider.radius;
            Vector3 center = _capsuleCollider.center;

            // ���[�J�����W�n���烏�[���h���W�n�ɕϊ�
            Vector3 capsuleCenter = _capsuleCollider.transform.TransformPoint(center);

            // �J�v�Z���̎��̕����i���[�J����Y���ɉ����Ă���Ɖ���j
            Vector3 upDirection = _capsuleCollider.transform.up;

            // �J�v�Z���̒��S���ɉ������ł��߂��_�����߂�i���������ɐ����j
            Vector3 toTarget = targetPosition - capsuleCenter;
            Vector3 projection = Vector3.Project(toTarget, upDirection);

            // ���������̐�����ݒ�i�J�v�Z���̏㉺�����v�Z�j
            float halfHeight = height;
            Vector3 closestPointOnAxis = capsuleCenter + Vector3.ClampMagnitude(projection, halfHeight);

            // �f�o�b�O�p�Ɏ������̓_�����O�ɕ\��
            Debug.Log("�J�v�Z���̒��S���ɉ������ł��߂��_: " + closestPointOnAxis);

            // �J�v�Z���̑��ʏ�ōł��߂��_���v�Z���邽�߁A�J�v�Z���̒��S������̋����𔼌a�ɐ���
            Vector3 directionToTarget = (targetPosition - closestPointOnAxis).normalized;
            Vector3 closestPointOnSide = closestPointOnAxis + directionToTarget * radius;

            // �f�o�b�O�p�ɑ��ʂ̍ł��߂��_�����O�ɕ\��
            Debug.Log("�J�v�Z���̑��ʏ�ōł��߂��_: " + closestPointOnSide);

            return closestPointOnSide;
        }
        Debug.LogError("�J�v�Z���R���C�_�[��������Ȃ�����");
        return Vector3.zero; // �J�v�Z���R���C�_�[��������Ȃ������ꍇ
    }


    private void ActivateObjectAtPosition(Vector3 position)
    {
        if (_objectToActivate != null)
        {
            Debug.Log("�I�u�W�F�N�g�̈ʒu�� " + position + " �Ɉړ�");
            _objectToActivate.transform.position = position;
            _objectToActivate.SetActive(true);
        }
    }

    private IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (_objectToActivate != null)
        {
            _objectToActivate.SetActive(false);
            Debug.Log("�I�u�W�F�N�g���A�N�e�B�u��");
        }
    }

    // �V�[���r���[�Ƀf�o�b�O����`��
    void OnDrawGizmos()
    {
        if (_debugClosestPoint != Vector3.zero)
        {
            // �J�v�Z���̍ł��߂����ʂ̓_��Ԃ����ŕ\��
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_debugClosestPoint, 0.1f);
        }
    }
}
