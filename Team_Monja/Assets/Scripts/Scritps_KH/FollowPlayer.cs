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

    /// <summary>
    /// �v���C���[���ς�����Ƃ��ɕϐ��̒��g��ύX����
    /// </summary>
    private void SearchPlayer()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _nearPlayerArea = _player.transform.Find("NearPlayerArea").gameObject;
    }

    /// <summary>
    /// �m�[�h�e�[�u������T�����āA����WayPoint��Ԃ�
    /// </summary>
    public Transform SearchTargetWayPoint(Transform myWayPoint)
    {
        _wayPoints = _searchWayPointTwoDimensionalArray.WayPoints;
        _nextWayPointTable = _searchWayPointTwoDimensionalArray.NextWayPointTable;

        int playerIndex = 0;
        int myIndex = 0;

        // wayPoints���̃v���C���[�ɋ߂�WayPoint�̓Y������T��
        for (int i = 0; i < _wayPoints.Length; i++)
        {
            if (_targetWayPoint.gameObject.name == _wayPoints[i].gameObject.name)
            {
                // �m�[�h�e�[�u����2��ڂ���Ȃ̂�+1
                playerIndex = i + 1;
            }

            if (myWayPoint.gameObject.name == _wayPoints[i].gameObject.name)
            {
                // �m�[�h�e�[�u����2�s�ڂ���Ȃ̂�+1
                myIndex = i + 1;
            }
        }

        int nextWayPointIndex = _nextWayPointTable[myIndex, playerIndex];

        if (nextWayPointIndex == 0) return null; 

        // �z���0�I���W�������m�[�h�e�[�u����1�I���W���Ȃ̂�-1����
        Transform nextWayPoint = _wayPoints[nextWayPointIndex - 1];

        return nextWayPoint;
    }
}
