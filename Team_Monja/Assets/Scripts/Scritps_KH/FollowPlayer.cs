using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour,IFollowable
{
    private GameObject _nearPlayerArea = default;
    private Transform _player = default;
    private Transform _targetWayPoint = default;
    private Transform[] _wayPoints = default;
    private int[,] _nextWayPointTable = default;

    private NearPlayerWayPointManager _nearPlayerWayPointManager = default;
    private SearchWayPointTwoDimensionalArray _searchWayPointTwoDimensionalArray = default;

    void Start()
    {
        SearchPlayer();

        _nearPlayerWayPointManager = _nearPlayerArea.gameObject.GetComponent<NearPlayerWayPointManager>();
        _searchWayPointTwoDimensionalArray =
            _nearPlayerArea.gameObject.GetComponent<SearchWayPointTwoDimensionalArray>();
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
        GameObject player = GameObject.FindWithTag("Player");
        List<Transform> _nearPlayerWayPoints = _nearPlayerWayPointManager.NearPlayerWayPoint;

        if (_nearPlayerWayPoints.Count == 0)
        {
            return null;
        }

        Transform nearestWayPoint = _nearPlayerWayPoints[0];
        foreach (Transform wayPoint in _nearPlayerWayPoints)
        {
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
    /// プレイヤーが変わったときに変数の中身を変更する
    /// </summary>
    private void SearchPlayer()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _nearPlayerArea = _player.transform.Find("NearPlayerArea").gameObject;
    }

    /// <summary>
    /// ノードテーブル内を探索して、次のWayPointを返す
    /// </summary>
    public Transform SearchTargetWayPoint(Transform myWayPoint)
    {
        _wayPoints = _searchWayPointTwoDimensionalArray.WayPoints;
        _nextWayPointTable = _searchWayPointTwoDimensionalArray.NextWayPointTable;

        int playerIndex = 0;
        int myIndex = 0;

        // wayPoints内のプレイヤーに近いWayPointの添え字を探す
        for (int i = 0; i < _wayPoints.Length; i++)
        {
            if (_targetWayPoint.gameObject.name == _wayPoints[i].gameObject.name)
            {
                // ノードテーブルは2列目からなので+1
                playerIndex = i + 1;
            }

            if (myWayPoint.gameObject.name == _wayPoints[i].gameObject.name)
            {
                // ノードテーブルは2行目からなので+1
                myIndex = i + 1;
            }
        }

        int nextWayPointIndex = _nextWayPointTable[myIndex, playerIndex];

        if (nextWayPointIndex == 0) return null; 

        // 配列は0オリジンだがノードテーブルは1オリジンなので-1する
        Transform nextWayPoint = _wayPoints[nextWayPointIndex - 1];

        return nextWayPoint;
    }
}
