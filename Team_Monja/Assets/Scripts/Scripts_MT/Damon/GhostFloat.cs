using UnityEngine;

public class GhostFloat : MonoBehaviour
{
    // �h���U���Ƒ��x��ݒ肷��
    [SerializeField] private float _amplitude = 0.5f; // �U��
    [SerializeField] private float _frequency = 1f; // ���g��

    private Vector3 _startPos;

    void Start()
    {
        // �����ʒu��ۑ�����
        _startPos = transform.position;
    }

    void Update()
    {
        // Y�������̗h����v�Z����
        float yOffset = Mathf.Sin(Time.time * _frequency) * _amplitude;

        // �V�����ʒu��ݒ肷��
        transform.position = new Vector3(_startPos.x, _startPos.y + yOffset, _startPos.z);
    }
}
