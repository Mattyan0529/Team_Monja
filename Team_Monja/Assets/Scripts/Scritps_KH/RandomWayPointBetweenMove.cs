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
        /// 次の目的地を探索中
        /// </summary>
        InSearch,

        /// <summary>
        /// 目的地に移動中
        /// </summary>
        InMove

    }

    void Start()
    {
        // WayPointひとつずつと現在地を比べ、いちばん近いWayPointをtargetとする
        foreach (Transform child in _wayPoints.transform)
        {
            // nullの時はとりあえず今のWayPointを入れる
            if (_targetWayPoint == null)
            {
                _targetWayPoint = child;
                _shortestDistance = Vector3.Distance
                    (_targetWayPoint.transform.position, gameObject.transform.position);
            }

            // このWayPointと現在地の距離
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
    /// 移動し終わったら、また次のWayPointを探す
    /// </summary>
    private void NextWayPointSearch()
    {
        // 到着したので、目的地を現在地に変更
        _currentWayPoint = _targetWayPoint;

        WayPoint wayPoint = _currentWayPoint.GetComponent<WayPoint>();

        GameObject[] MovableWayPoint = wayPoint.NextPoints;

        // 目的地をランダムに選ぶ
        int nextPointsIndex = Random.Range(0, MovableWayPoint.Length - 1);

        _targetWayPoint = MovableWayPoint[nextPointsIndex].transform;
        _nowEnemyState = EnemyState.InMove;
    }

    /// <summary>
    /// 次のWayPointに移動する
    /// </summary>
    private void MoveToTargetWayPoint()
    {
        gameObject.transform.position = Vector3.MoveTowards
            (gameObject.transform.position, _targetWayPoint.transform.position, _speed * Time.deltaTime);

        // 目的地の方向に向くように修正(回転はY軸のみ)
        Vector3 directionVector = _targetWayPoint.position - gameObject.transform.position;
        Quaternion directionQuaternion = Quaternion.LookRotation(directionVector, Vector3.up);
        gameObject.transform.rotation = Quaternion.Euler(0f, directionQuaternion.eulerAngles.y, 0f);

        // ある程度近くなったら次の目的地へ
        if (Vector3.Distance
            (gameObject.transform.position, _targetWayPoint.transform.position) < _followStopDistance)
        {
            _nowEnemyState = EnemyState.InSearch;
        }
    }
}
