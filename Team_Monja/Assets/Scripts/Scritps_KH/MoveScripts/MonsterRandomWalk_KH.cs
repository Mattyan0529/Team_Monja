using UnityEngine;

public class MonsterRandomWalk_KH : MonoBehaviour
{
    private Vector3 _monsterRotation = new Vector3(0f, 0f, 0f);
    private Rigidbody _rigidbody = default;
    //���{
    CharacterAnim_MT _characterAnim = default;


    private float _updateTime = 2f;
    private float _elapsedTime = 0f;

    private float _speed = 4000f;
    private float _maxSpeed = 2f;

    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        //���{
        _characterAnim = GetComponent<CharacterAnim_MT>();
        MonsterWalk();
    }

    void Update()
    {
        UpdateTime();
        MonsterWalk();
    }

    /// <summary>
    /// �����X�^�[��O���ɓ�����
    /// </summary>
    private void MonsterWalk()
    {
        // �����ꂩ�̕����ōő呬�x�ɒB���Ă���ꍇ�͉������Ȃ�
        if (_rigidbody.velocity.x > _maxSpeed | _rigidbody.velocity.z > _maxSpeed) return;
        if (_rigidbody.velocity.x < -_maxSpeed | _rigidbody.velocity.z < -_maxSpeed) return;

        // ����
        _rigidbody.AddForce(transform.forward * _speed * Time.deltaTime, ForceMode.Force);

        //���{
        _characterAnim.NowAnim = "Move";
    }

    /// <summary>
    /// �����X�^�[�̕�����ς���
    /// </summary>
    private void UpdateRotation()
    {
        // �����������_���Ō��߂�
        _monsterRotation = new Vector3(0f, Random.Range(0f, 360f), 0f);

        // �������X�V
        _rigidbody.velocity = Vector3.zero;
        gameObject.transform.rotation = Quaternion.Euler(_monsterRotation);
    }

    /// <summary>
    /// ����I�ɕ�����ς���
    /// </summary>
    private void UpdateTime()
    {
        // ���ԉ��Z
        _elapsedTime += Time.deltaTime;

        // �K�莞�ԂɒB���Ă����ꍇ
        if (_elapsedTime > _updateTime)
        {
            UpdateRotation();
            _elapsedTime = 0f;
        }
    }
}
