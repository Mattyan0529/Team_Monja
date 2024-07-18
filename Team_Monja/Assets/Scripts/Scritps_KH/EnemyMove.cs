using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private GameObject _wayPoints = default;

    private Transform _currentWayPoint = default;
    private Transform _targetWayPoint = default;

    private float _shortestDistance = default;
    private float _followStopDistance = 0.5f;
    private float _speed = 4f;

    private ChangeEnemyMoveType _changeEnemyMoveType = default;

    private EnemyState _nowEnemyState = EnemyState.InSearch;

    private enum EnemyState
    {
        /// <summary>
        /// ���̖ړI�n��T����
        /// </summary>
        InSearch,

        /// <summary>
        /// �ړI�n�Ɉړ���
        /// </summary>
        InMove
    }

    void Start()
    {
        _changeEnemyMoveType = GetComponent<ChangeEnemyMoveType>();

        // WayPoint�ЂƂ��ƌ��ݒn���ׁA�����΂�߂�WayPoint��target�Ƃ���
        foreach (Transform child in _wayPoints.transform)
        {
            // null�̎��͂Ƃ肠��������WayPoint������
            if (_targetWayPoint == null)
            {
                _targetWayPoint = child;
                _shortestDistance = Vector3.Distance
                    (_targetWayPoint.transform.position, gameObject.transform.position);
            }

            // ����WayPoint�ƌ��ݒn�̋���
            float thisWayPointDistance = Vector3.Distance
                (child.transform.position, gameObject.transform.position);

            if (Mathf.Abs(_shortestDistance) > Mathf.Abs(thisWayPointDistance))
            {
                _targetWayPoint = child;
                _shortestDistance = Vector3.Distance
                    (_targetWayPoint.transform.position, gameObject.transform.position);
            }
        }

        _nowEnemyState = EnemyState.InMove;
    }

    void Update()
    {
        if (_nowEnemyState == EnemyState.InSearch)
        {
            NextWayPointSearch();
        }
        else if (_nowEnemyState == EnemyState.InMove)
        {
            MoveToTargetWayPoint();
        }
    }

    /// <summary>
    /// �ړ����I�������A�܂�����WayPoint��T��
    /// </summary>
    private void NextWayPointSearch()
    {
        // ���������̂ŁA�ړI�n�����ݒn�ɕύX
        _currentWayPoint = _targetWayPoint;

        if (_changeEnemyMoveType.EnemyMove(_currentWayPoint) == null) return;

        _targetWayPoint = _changeEnemyMoveType.EnemyMove(_currentWayPoint);

        _nowEnemyState = EnemyState.InMove;
    }

    /// <summary>
    /// ����WayPoint�Ɉړ�����
    /// </summary>
    private void MoveToTargetWayPoint()
    {
        gameObject.transform.position = Vector3.MoveTowards
            (gameObject.transform.position, _targetWayPoint.transform.position, _speed * Time.deltaTime);

        // �ړI�n�̕����Ɍ����悤�ɏC��(��]��Y���̂�)
        Vector3 directionVector = _targetWayPoint.position - gameObject.transform.position;
        Quaternion directionQuaternion = Quaternion.LookRotation(directionVector, Vector3.up);
        gameObject.transform.rotation = Quaternion.Euler(0f, directionQuaternion.eulerAngles.y, 0f);

        Vector3 nowPos = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 targetPos = new Vector3(_targetWayPoint.transform.position.x, 0f, _targetWayPoint.transform.position.z);

        // ������x�߂��Ȃ����玟�̖ړI�n��
        if (Vector3.Distance(nowPos, targetPos) < _followStopDistance)
        {
            _nowEnemyState = EnemyState.InSearch;
        }
    }
}

