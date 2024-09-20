using UnityEngine;

public class DeathSpwanSoul_TH : MonoBehaviour
{
    [SerializeField]
    private GameObject _particleSystemObject;  // �p�[�e�B�N���V�X�e����GameObject

    private float _rotateSpeed = 1.5f;
    private float _pingPongValue = 0.3f;
    private float _nowPositionY = default;
    private string _targetTag = "TutorialUI";
    private GameObject _player;
    private GameObject _tutorialObj;
    private EnemyTriggerManager_MT _enemyTriggerManager;
    private CharacterDeadDecision_MT characterDeadDecision;
    private ClosestEnemyFinder_MT _closestEnemyFinder;

    void Start()
    {
      

        // �e�I�u�W�F�N�g����擾
        characterDeadDecision = GetComponent<CharacterDeadDecision_MT>();
        if (characterDeadDecision == null)
        {
            Debug.LogError("CharacterDeadDecision_MT component not found on this GameObject.");
        }

        // �v���C���[�}�l�[�W���[����ł��߂��G��T���X�N���v�g���擾
        GameObject playerManager = GameObject.FindWithTag("PlayerManager");
        if (playerManager != null)
        {
            _closestEnemyFinder = playerManager.GetComponent<ClosestEnemyFinder_MT>();
            if (_closestEnemyFinder == null)
            {
                Debug.LogError("ClosestEnemyFinder_MT component not found on PlayerManager.");
            }
        }
        else
        {
            Debug.LogError("PlayerManager with tag 'PlayerManager' not found.");
        }

        // �߂��̃g���K�[����G�̃g���K�[�}�l�[�W���[���擾
        GameObject nearTrigger = GameObject.FindWithTag("NearTrigger");
        if (nearTrigger != null)
        {
            _enemyTriggerManager = nearTrigger.GetComponent<EnemyTriggerManager_MT>();
            if (_enemyTriggerManager == null)
            {
                Debug.LogError("EnemyTriggerManager_MT component not found on NearTrigger.");
            }
        }
        else
        {
            Debug.LogError("NearTrigger with tag 'NearTrigger' not found.");
        }

        _nowPositionY = transform.position.y;
        // �^�O�����ŏ��̎q�I�u�W�F�N�g����������
        _tutorialObj = FindFirstObjectWithTag(transform, _targetTag);

        // _tutorialObj��������Ȃ��ꍇ�̌x��
        if (_tutorialObj == null)
        {
            Debug.LogWarning("Tag 'TutorialUI' not found in child objects.");
        }

        // ������ԂŃp�[�e�B�N���V�X�e�����\���ɂ��Ă���
        if (_particleSystemObject != null)
        {
            _particleSystemObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("_particleSystemObject is not assigned.");
        }
    }

    public void SetPlayer(GameObject player)
    {
        _player = player;
    }

    void Update()
    {
        if (characterDeadDecision != null && characterDeadDecision.IsDeadDecision())
        {
            if (!CompareTag("Player"))
            {

                if (_player != null && _closestEnemyFinder != null && _enemyTriggerManager != null)
                {
                    var closestObject = _closestEnemyFinder.GetClosestObject(_enemyTriggerManager.objectsInTrigger, _player.transform);

                    if (closestObject != null)
                    {
                        if (this.gameObject == closestObject.gameObject)
                        {
                            if (_tutorialObj != null)
                            {
                                _tutorialObj.SetActive(true);
                            }
                            else
                            {
                                Debug.LogWarning("_tutorialObj is null.");
                            }
                        }
                    }
                    else
                    {
                        Debug.LogWarning("ClosestObject is null.");
                    }
                }
                else
                {
                    Debug.LogWarning("One of the required components is null.");
                }
                ToggleParticleSystem(true);  // ���S��ԂŃp�[�e�B�N���V�X�e����\��
            }
        }
        else
        {
            if (_tutorialObj != null)
            {
                _tutorialObj.SetActive(false);
            }
            ToggleParticleSystem(false); // ������ԂŃp�[�e�B�N���V�X�e�����\��
        }
    }


    private void ToggleParticleSystem(bool isActive)
    {
        if (_particleSystemObject != null)
        {
            _particleSystemObject.SetActive(isActive);
        }
    }

    // �ċA�I�Ƀ^�O�����ŏ��̃I�u�W�F�N�g���������郁�\�b�h
    private GameObject FindFirstObjectWithTag(Transform parent, string tag)
    {
        // �q�I�u�W�F�N�g�����ׂĎ擾
        foreach (Transform child in parent)
        {
            // �^�O����v����ꍇ�A�I�u�W�F�N�g��Ԃ�
            if (child.CompareTag(tag))
            {
                return child.gameObject;
            }

            // �q�I�u�W�F�N�g�̎q���ċA�I�Ɍ���
            GameObject foundObject = FindFirstObjectWithTag(child, tag);
            if (foundObject != null)
            {
                return foundObject;
            }
        }

        // �^�O�����I�u�W�F�N�g��������Ȃ��ꍇ
        return null;
    }
}
