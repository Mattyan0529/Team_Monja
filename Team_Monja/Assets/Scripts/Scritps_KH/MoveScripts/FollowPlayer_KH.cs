using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer_KH : MonoBehaviour, IFollowable_KH
{
    private GameObject _nearPlayerArea = default;
    private Transform _player = default;
    private Transform _targetWayPoint = default;
    private Transform[] _wayPoints = default;
    private int[,] _nextWayPointTable = default;

    private NearPlayerWayPointManager_KH _nearPlayerWayPointManager = default;
    private SearchWayPointTwoDimensionalArray_KH _searchWayPointTwoDimensionalArray = default;
    private EnemyMove_KH _enemyMove;

    public Transform TargetWayPoint
    {
        get { return _targetWayPoint; }
        set { _targetWayPoint = value; }
    }

    public Transform Player
    {
        get { return _player; }
    }

    void Start()
    {
        _nearPlayerArea = GameObject.FindGameObjectWithTag("NearPlayerArea");
        _nearPlayerWayPointManager = _nearPlayerArea.gameObject.GetComponent<NearPlayerWayPointManager_KH>();
        _searchWayPointTwoDimensionalArray =
            _nearPlayerArea.gameObject.GetComponent<SearchWayPointTwoDimensionalArray_KH>();
        _enemyMove = GetComponent<EnemyMove_KH>();
    }

    private void Update()
    {
        if (SearchWayPointNearPlayer() == null) return;
        _searchWayPointTwoDimensionalArray.TargetWayPoint = SearchWayPointNearPlayer();
    }

    /// <summary>
    /// プレイヤーから一番近いWayPointを取得
    /// </summary>
    private Transform SearchWayPointNearPlayer()
    {
        List<Transform> _nearPlayerWayPoints = _nearPlayerWayPointManager.NearPlayerWayPoint;

        if (_nearPlayerWayPoints.Count == 0)
        {
            return null;
        }

        Transform nearestWayPoint = _nearPlayerWayPoints[0];
        foreach (Transform wayPoint in _nearPlayerWayPoints)
        {
            if(wayPoint.transform.parent.name != _enemyMove.WayPoint.name)
            {
                continue;
            }

            float shortestDistance = Vector3.Distance
                (nearestWayPoint.position, _player.transform.position);

            float thisWayPointDistance = Vector3.Distance
                (wayPoint.position, _player.transform.position);

            // 現在最も近いWayPointからプレイヤーまでの距離と、このWayPointからの距離を比べる
            if (shortestDistance > thisWayPointDistance)
            {
                nearestWayPoint = wayPoint;
            }
        }
        _targetWayPoint = nearestWayPoint;
        return _targetWayPoint;
    }

    /// <summary>
    /// プレイヤーを設定
    /// </summary>
    /// <param name="player"></param>
    public void SetPlayer(GameObject player)
    {
        _player = player.transform;
    }

    /// <summary>
    /// ノードテーブル内を探索して、次のWayPointを返す
    /// </summary>
    public Transform SearchTargetWayPoint(Transform myWayPoint)
    {
        GameObject wayPoint = _enemyMove.WayPoint;
        _wayPoints = new Transform[wayPoint.transform.childCount];
        
        for(int i = 0; i < wayPoint.transform.childCount; i++)
        {
            _wayPoints[i] = wayPoint.transform.GetChild(i).transform;
        }

        if (gameObject.CompareTag("Enemy"))
        {
            _nextWayPointTable = _searchWayPointTwoDimensionalArray.NextWayPointTable;
        }
        else if(gameObject.CompareTag("Boss"))
        {
            _nextWayPointTable = _searchWayPointTwoDimensionalArray.BossWayPointTable;
        }

        int targetIndex = 0;
        int myIndex = 0;

        // wayPoints内のプレイヤーに近いWayPointの添え字を探す
        for (int i = 0; i < _wayPoints.Length; i++)
        {
            if (_targetWayPoint.gameObject.name == _wayPoints[i].gameObject.name)
            {
                // ノードテーブルは2列目からなので+1
                targetIndex = i + 1;
            }

            if (myWayPoint.gameObject.name == _wayPoints[i].gameObject.name)
            {
                // ノードテーブルは2行目からなので+1
                myIndex = i + 1;
            }
        }

        int nextWayPointIndex = _nextWayPointTable[myIndex, targetIndex];
        if (nextWayPointIndex == 0) return null;

        // 配列は0オリジンだがノードテーブルは1オリジンなので-1する
        Transform nextWayPoint = _wayPoints[nextWayPointIndex - 1];
        return nextWayPoint;
    }
}
