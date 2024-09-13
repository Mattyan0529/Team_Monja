using UnityEngine;

public class DeathSpwanSoul_TH : MonoBehaviour
{
    [SerializeField]
    private GameObject _particleSystemObject;  // �p�[�e�B�N���V�X�e����GameObject

    private float _rotateSpeed = 1.5f;
    private float _pingPongValue = 0.3f;
    private float _nowPositionY = default;

    private CharacterDeadDecision_MT characterDeadDecision;

    void Start()
    {
        // �e�I�u�W�F�N�g����擾
        characterDeadDecision = GetComponentInParent<CharacterDeadDecision_MT>();

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
            {
                MoveArrow();
                ToggleParticleSystem(true);  // ���S��ԂŃp�[�e�B�N���V�X�e����\��
            }
        }
        else
        {
            ToggleParticleSystem(false); // ������ԂŃp�[�e�B�N���V�X�e�����\��
        }
    }

    private void MoveArrow()
    {
        // �����ŉ�]
        transform.Rotate(0, 0, _rotateSpeed);
        // �㉺�ɂӂ�ӂ핂������
        transform.position = new Vector3(transform.position.x,
            _nowPositionY + Mathf.PingPong(Time.time, _pingPongValue),
            transform.position.z);
    }

    private void ToggleParticleSystem(bool isActive)
    {
        if (_particleSystemObject != null)
        {
            _particleSystemObject.SetActive(isActive);
        }
    }
}