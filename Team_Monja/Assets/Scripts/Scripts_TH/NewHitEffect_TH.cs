//using UnityEngine;
//using System.Collections;

//public class HitEffect2_TH : MonoBehaviour
//{
//    [SerializeField] private GameObject[] _HitEffects; // �A�N�e�B�u�ɂ������G�t�F�N�g�I�u�W�F�N�g�̔z��
//    [SerializeField] private float _activationDuration = 3f; // �A�N�e�B�u�ɂ��鎞��
//    [SerializeField] private CapsuleCollider _capsuleCollider; // Inspector�Őݒ肷��CapsuleCollider

//    private Vector3 _debugClosestPoint = Vector3.zero; // �f�o�b�O�p�ϐ�
//    private GameObject _currentObjectToActivate; // ����̃g���K�[�ɑΉ�����I�u�W�F�N�g
//    private StatusManager_MT _StatusManager;

//    private void Start()
//    {
//        _StatusManager = GetComponent<StatusManager_MT>();
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        //�q�I�u�W�F�N�g�ɂ���Ή������邩��

//        if ((other.CompareTag("NormalHit") || other.CompareTag("SlashHit") || other.CompareTag("HeartHit") || other.CompareTag("ThunderHit")) && _StatusManager.HP > 0 )
//        {
//            if (gameObject.CompareTag("Enemy") && other.gameObject.transform.parent.CompareTag("Enemy"))
//            {
//                return;
//            }
//            else
//            {
//                Debug.Log(gameObject.name + other.name);

//                // �G�t�F�N�g��I��
//                if (other.CompareTag("NormalHit"))
//                {
//                    _currentObjectToActivate = _HitEffects[0]; // NormalHit�p�̃G�t�F�N�g
//                }
//                else if (other.CompareTag("SlashHit"))
//                {
//                    _currentObjectToActivate = _HitEffects[1]; // SlashHit�p�̃G�t�F�N�g
//                }
//                else if (other.CompareTag("HeartHit"))
//                {
//                    _currentObjectToActivate = _HitEffects[2]; // HeartHit�p�̃G�t�F�N�g
//                }
//                else if (other.CompareTag("ThunderHit"))
//                {
//                    _currentObjectToActivate = _HitEffects[3]; // ThunderHit�p�̃G�t�F�N�g
//                }

//                // �e�I�u�W�F�N�g���擾���ACapsuleCollider��̍ł��߂��ʒu�ɃG�t�F�N�g���A�N�e�B�u��
//                Transform parentTransform = other.transform.parent;
//                if (parentTransform != null)
//                {
//                    _debugClosestPoint = GetClosestPointOnSideOfCapsule(parentTransform.position);
//                    if (_debugClosestPoint != Vector3.zero)
//                    {
//                        ActivateObjectAtPosition(_debugClosestPoint);
//                        StartCoroutine(DeactivateAfterDelay(_activationDuration));
//                    }
//                }
//                else
//                {
//                    Debug.Log("�e�I�u�W�F�N�g������܂���");
//                }
//            }
//        }
//    }

//    private Vector3 GetClosestPointOnSideOfCapsule(Vector3 targetPosition)
//    {
//        if (_capsuleCollider != null)
//        {
//            // �J�v�Z���R���C�_�[�̍����Ɣ��a���擾
//            float height = _capsuleCollider.height;
//            float radius = _capsuleCollider.radius;
//            // �J�v�Z���R���C�_�[�̒��S���W�����[���h���W�ɕϊ�
//            Vector3 center = _capsuleCollider.center;
//            Vector3 capsuleCenter = _capsuleCollider.transform.TransformPoint(center);
//            // �J�v�Z���R���C�_�[�̏�����x�N�g�����擾
//            Vector3 upDirection = _capsuleCollider.transform.up;
//            // �^�[�Q�b�g�ʒu�ւ̃x�N�g�����v�Z
//            Vector3 toTarget = targetPosition - capsuleCenter;
//            // ������ւ̎ˉe�x�N�g�����v�Z
//            Vector3 projection = Vector3.Project(toTarget, upDirection);
//            // �J�v�Z���̔����̍������v�Z
//            float halfHeight = height / 2;
//            // �J�v�Z���̎���ōł��߂��_���v�Z
//            Vector3 closestPointOnAxis = capsuleCenter + Vector3.ClampMagnitude(projection, halfHeight);
//            // ����̍ł��߂��_����^�[�Q�b�g�ւ̕����𐳋K��
//            Vector3 directionToTarget = (targetPosition - closestPointOnAxis).normalized;
//            // �J�v�Z���̑��ʏ�Ń^�[�Q�b�g�ɍł��߂��_��Ԃ�
//            return closestPointOnAxis + directionToTarget * radius;
//        }

//        // �J�v�Z���R���C�_�[��������Ȃ��ꍇ�̃G���[���O
//        Debug.LogError("�J�v�Z���R���C�_�[��������܂���");
//        return Vector3.zero;
//    }

//    private void ActivateObjectAtPosition(Vector3 position)
//    {
//        if (_currentObjectToActivate != null)
//        {
//            _currentObjectToActivate.transform.position = position;
//            _currentObjectToActivate.SetActive(true);
//        }
//    }

//    private IEnumerator DeactivateAfterDelay(float delay)
//    {
//        yield return new WaitForSeconds(delay);
//        if (_currentObjectToActivate != null)
//        {
//            _currentObjectToActivate.SetActive(false);
//        }
//    }
//}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HitEffect2_TH : MonoBehaviour
{
    [SerializeField] private GameObject[] _HitEffects; // �A�N�e�B�u�ɂ������G�t�F�N�g�I�u�W�F�N�g�̔z��
    [SerializeField] private float _activationDuration = 3f; // �A�N�e�B�u�ɂ��鎞��
    [SerializeField] private CapsuleCollider _capsuleCollider; // Inspector�Őݒ肷��CapsuleCollider

    private Vector3 _debugClosestPoint = Vector3.zero; // �f�o�b�O�p�ϐ�
    private GameObject _currentObjectToActivate; // ����̃g���K�[�ɑΉ�����I�u�W�F�N�g
    private StatusManager_MT _StatusManager;
    private Dictionary<string, GameObject> _hitEffectMap; // �^�O�ƃG�t�F�N�g�̃}�b�s���O�p
    private bool _hasTriggeredAtZeroHP = false; // HP���[���̂Ƃ��ɃG�t�F�N�g���\�����ꂽ�����m�F����t���O

    private void Start()
    {
        _StatusManager = GetComponent<StatusManager_MT>();

        // �e�^�O�ɑΉ�����G�t�F�N�g���}�b�s���O
        _hitEffectMap = new Dictionary<string, GameObject>()
        {
            { "NormalHit", _HitEffects[0] },
            { "SlashHit", _HitEffects[1] },
            { "HeartHit", _HitEffects[2] },
            { "ThunderHit", _HitEffects[3] }
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        // �������g�̎q�I�u�W�F�N�g�܂��͎��g�ł���ꍇ�͖���
        if (other.transform.IsChildOf(transform))
        {
            return;
        }

        // HP��0�ȉ��̏ꍇ�ł��A��񂾂��G�t�F�N�g�𔭐�������
        if (_StatusManager.HP <= 0 && _hasTriggeredAtZeroHP)
        {
            return; // ���ł�0HP���̃G�t�F�N�g�����������ꍇ�͉������Ȃ�
        }

        // �^�O�ɑΉ�����G�t�F�N�g�����݂��邩�m�F
        if (_hitEffectMap.ContainsKey(other.tag) && !IsSameEnemy(other))
        {
            _currentObjectToActivate = _hitEffectMap[other.tag];

            // �e�I�u�W�F�N�g���擾���ACapsuleCollider��̍ł��߂��ʒu�ɃG�t�F�N�g���A�N�e�B�u��
            Transform parentTransform = other.transform.parent;
            if (parentTransform != null)
            {
                _debugClosestPoint = GetClosestPointOnSideOfCapsule(parentTransform.position);
                if (_debugClosestPoint != Vector3.zero)
                {
                    ActivateObjectAtPosition(_debugClosestPoint);
                    StartCoroutine(DeactivateAfterDelay(_activationDuration));

                    // HP��0�̏ꍇ�A��x�����t���O�𗧂Ă�
                    if (_StatusManager.HP <= 0)
                    {
                        _hasTriggeredAtZeroHP = true;
                    }
                }
            }
            else
            {
                Debug.LogWarning("�e�I�u�W�F�N�g��������܂���: " + other.name);
            }
        }
    }

    // �����G�l�~�[���m�̃q�b�g�𖳎�
    private bool IsSameEnemy(Collider other)
    {
        return gameObject.CompareTag("Enemy") && other.gameObject.transform.parent.CompareTag("Enemy");
    }

    private Vector3 GetClosestPointOnSideOfCapsule(Vector3 targetPosition)
    {
        if (_capsuleCollider == null)
        {
            Debug.LogError("�J�v�Z���R���C�_�[��������܂���");
            return Vector3.zero;
        }

        // �J�v�Z���R���C�_�[�̃v���p�e�B���擾
        float height = _capsuleCollider.height;
        float radius = _capsuleCollider.radius;
        Vector3 center = _capsuleCollider.center;
        Vector3 capsuleCenter = _capsuleCollider.transform.TransformPoint(center);
        Vector3 upDirection = _capsuleCollider.transform.up;

        // �^�[�Q�b�g�ւ̃x�N�g�����v�Z
        Vector3 toTarget = targetPosition - capsuleCenter;
        Vector3 projection = Vector3.Project(toTarget, upDirection);
        float halfHeight = height / 2;

        // ����̍ł��߂��_���v�Z
        Vector3 closestPointOnAxis = capsuleCenter + Vector3.ClampMagnitude(projection, halfHeight);
        Vector3 directionToTarget = (targetPosition - closestPointOnAxis).normalized;

        // �J�v�Z���̑��ʏ�ōł��߂��_��Ԃ�
        return closestPointOnAxis + directionToTarget * radius;
    }

    private void ActivateObjectAtPosition(Vector3 position)
    {
        if (_currentObjectToActivate != null)
        {
            _currentObjectToActivate.transform.position = position;
            _currentObjectToActivate.SetActive(true);
        }
        else
        {
            Debug.LogWarning("�A�N�e�B�x�[�g����I�u�W�F�N�g���ݒ肳��Ă��܂���");
        }
    }

    private IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (_currentObjectToActivate != null)
        {
            _currentObjectToActivate.SetActive(false);
        }
    }
}
