using UnityEngine;

public class RandomWayPointBetweenMove : MonoBehaviour
{
    [SerializeField]
    private GameObject _wayPoints = default;

    private Transform _currentWayPoint = default;
    private Transform _targetWayPoint = default;

    private float _shortestDistance = default;
    private float _followStopDistance = 0.3f;
    private float _speed = 2f;

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

        WayPoint wayPoint = _currentWayPoint.GetComponent<WayPoint>();

        GameObject[] MovableWayPoint = wayPoint.NextPoints;

        // �ړI�n�������_���ɑI��
        int nextPointsIndex = Random.Range(0, MovableWayPoint.Length - 1);

        _targetWayPoint = MovableWayPoint[nextPointsIndex].transform;
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

        // ������x�߂��Ȃ����玟�̖ړI�n��
        if (Vector3.Distance
            (gameObject.transform.position, _targetWayPoint.transform.position) < _followStopDistance)
        {
            _nowEnemyState = EnemyState.InSearch;
        }
    }
}
