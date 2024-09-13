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
    private EnemyTriggerManager_MT _enemyTriggerManager;
    private CharacterDeadDecision_MT characterDeadDecision;
    private ClosestEnemyFinder_MT _closestEnemyFinder;
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        // �e�I�u�W�F�N�g����擾
        characterDeadDecision = GetComponentInParent<CharacterDeadDecision_MT>();
        _closestEnemyFinder = GameObject.FindWithTag("PlayerManager").GetComponent<ClosestEnemyFinder_MT>();
        _enemyTriggerManager = GameObject.FindWithTag("NearTrigger").GetComponent<EnemyTriggerManager_MT>();

        _nowPositionY = transform.position.y;

        // ������ԂŃp�[�e�B�N���V�X�e�����\���ɂ��Ă���
        if (_particleSystemObject != null)
        {
            _particleSystemObject.SetActive(false);
        }
    }

    void Update()
    {
        if (characterDeadDecision.IsDeadDecision())
        {
            if (!CompareTag("Player"))
            {// �^�O�����ŏ��̎q�I�u�W�F�N�g����������
                GameObject foundObject = FindFirstObjectWithTag(transform, _targetTag);
                Debug.Log(foundObject);
                //�v���C���[�����ԋ߂��ꍇ
                if(_enemyTriggerManager.objectsInTrigger != null &&
                    (this.gameObject == _closestEnemyFinder.GetClosestObject(_enemyTriggerManager.objectsInTrigger, _player.transform).gameObject))
                {
                    foundObject.SetActive(true);
                }
                else
                {
                    foundObject.SetActive(false);
                }
      
                ToggleParticleSystem(true);  // ���S��ԂŃp�[�e�B�N���V�X�e����\��
            }
        }
        else
        {

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

    public void SetPlayer(GameObject player)
    {
        _player = player;
    }
}