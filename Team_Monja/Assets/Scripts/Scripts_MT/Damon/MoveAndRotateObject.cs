using UnityEngine;

public class MoveAndRotateObject : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform; // �ړ���Ɖ�]���Transform
    [SerializeField] private float _moveDuration = 3f; // �ړ��Ɖ�]�ɂ�����b��
    [SerializeField] private ParticleSystem[] _particleSystems; // �����̃p�[�e�B�N���V�X�e��
    [SerializeField] private float _startParticleSize = 1f; // �����̃p�[�e�B�N���T�C�Y
    [SerializeField] private float _endParticleSize = 3f; // �ŏI�I�ȃp�[�e�B�N���T�C�Y

    private Vector3 _startPosition; // �J�n�ʒu
    private Quaternion _startRotation; // �J�n���̉�]
    private Vector3 _startScale; // �J�n���̃X�P�[��
    private float _elapsedTime; // �o�ߎ���

    void Start()
    {
        // �J�n�ʒu�A��]�A�X�P�[����ۑ�
        _startPosition = transform.position;
        _startRotation = transform.rotation;
        _startScale = transform.localScale;
        _elapsedTime = 0f;
    }

    void Update()
    {
        // �o�ߎ��Ԃ𑝉�
        _elapsedTime += Time.deltaTime;

        // �i�����v�Z
        float progress = Mathf.Clamp01(_elapsedTime / _moveDuration);

        // �ʒu�̕��
        transform.position = Vector3.Lerp(_startPosition, _targetTransform.position, progress);

        // ��]�̕��
        transform.rotation = Quaternion.Lerp(_startRotation, _targetTransform.rotation, progress);

        // �X�P�[���̕��
        transform.localScale = Vector3.Lerp(_startScale, _targetTransform.localScale, progress);

        // �p�[�e�B�N���̃T�C�Y���i���ɉ����ĕω�������
        for (int i = 0; i < _particleSystems.Length; i++)
        {
            var renderer = _particleSystems[i].GetComponent<ParticleSystemRenderer>();
            if (renderer != null)
            {
                float maxSize = Mathf.Lerp(_startParticleSize, _endParticleSize, progress);

                // ��ڂ̃p�[�e�B�N���V�X�e���ɂ̓T�C�Y��������
                if (i == 1)
                {
                    maxSize *= 0.75f;
                }

                renderer.maxParticleSize = maxSize;
            }
        }
    }
}
