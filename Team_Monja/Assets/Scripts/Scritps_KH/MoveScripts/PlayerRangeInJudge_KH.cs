using UnityEngine;

public class PlayerRangeInJudge_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _followArea;

    private MonsterRandomWalk_KH _monsterRandomWalk = default;

    private float _speed = 2f;

    private float _followStopDistance = 1.1f;

    private Vector3 _beforePos;
    private Vector3 _afterPos;

    private float _updateTime = 0.5f;
    private float _elapsedTime = 0f;

    void Start()
    {
        _monsterRandomWalk = GetComponent<MonsterRandomWalk_KH>();
        _beforePos = gameObject.transform.position;
    }

    /// <summary>
    /// �v���C���[�Ǐ]
    /// </summary>
    private void FollowPlayer()
    {
        // �v���C���[�ɂ������Ă�Sphere�̏ꏊ�i�v���C���[�̏ꏊ�Ɠ��`�j
        Vector3 _playerPos = _followArea.transform.position;

        // �K��l�ȏ�Ƀv���C���[�ƓG���߂�������Ǐ]����߂�
        if (Vector3.Distance(gameObject.transform.position, _playerPos) < _followStopDistance) return;

        // �Ǐ]
        transform.position = Vector3.MoveTowards(gameObject.transform.position, _playerPos, _speed * Time.deltaTime);
    }

    /// <summary>
    /// �����Ǐ]���̃����X�^�[�̕����X�V
    /// </summary>
    private void MonsterRotation()
    {
        _afterPos = gameObject.transform.position;
        Vector3 _difference = _afterPos - _beforePos;

        _difference.y = 0f;

        if (_difference == Vector3.zero) return;        // �����Ă��Ȃ��Ƃ��͏��������Ȃ��i_difference��0��������G���[�ɂȂ�j

        // �ړ����������Ɍ����悤�ɏC��(��]��Y���̂�)
        Quaternion newDirection = Quaternion.LookRotation(_difference, Vector3.up);
        gameObject.transform.rotation = Quaternion.Euler(0f, newDirection.eulerAngles.y, 0f);
        _beforePos = _afterPos;
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
            MonsterRotation();
            _elapsedTime = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[���Ǐ]�͈͂ɓ������烉���_���ړ�����߂�
        if (other.gameObject == _followArea && this.enabled)
        {
            _monsterRandomWalk.enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // �v���C���[���Ǐ]�͈͂ɂ���ԒǏ]����
        if (other.gameObject == _followArea && this.enabled)
        {
            FollowPlayer();
            UpdateTime();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �v���C���[���Ǐ]�͈͂���O�ꂽ�烉���_���ړ����n�߂�
        if (other.gameObject == _followArea && this.enabled)
        {
            _elapsedTime = 0f;
            _monsterRandomWalk.enabled = true;
        }
    }
}
