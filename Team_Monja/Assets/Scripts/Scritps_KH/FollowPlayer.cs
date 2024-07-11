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
    /// �v���C���[�����ԋ߂�WayPoint���擾
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

            // ���ݍł��߂�WayPoint����v���C���[�܂ł̋����ƁA����WayPoint����̋������ׂ�
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
    /// �G�ƃv���C���[�Ǐ]�͈͂̏Փ˂����m
    /// </summary>
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform != _followArea) return;
        
        Transform targetWayPoint = SearchWayPointNearPlayer();

        if (targetWayPoint == null) return;

        Debug.Log(targetWayPoint);
    }
}
