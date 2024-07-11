using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform _followArea = default;
    private Transform _player = default;
    private Transform _targetWayPoint = default;

    private NearPlayerWayPointManager _nearPlayerWayPointManager = default;

    void Start()
    {
        SearchPlayer();
        _nearPlayerWayPointManager = _followArea.gameObject.GetComponent<NearPlayerWayPointManager>();
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

    private void SearchPlayer()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _followArea = _player.transform.Find("FollowArea");
    }

    /// <summary>
    /// 敵とプレイヤー追従範囲の衝突を検知
    /// </summary>
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform != _followArea) return;
        
        Transform targetWayPoint = SearchWayPointNearPlayer();

        if (targetWayPoint == null) return;

        Debug.Log(targetWayPoint);
    }
}
