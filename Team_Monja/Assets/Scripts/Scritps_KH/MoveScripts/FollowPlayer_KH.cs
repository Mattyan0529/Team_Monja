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
        SearchPlayer();

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
    /// �v���C���[�����ԋ߂�WayPoint���擾
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
        _nearPlayerArea = GameObject.FindGameObjectWithTag("NearPlayerArea");
    }

    /// <summary>
    /// �m�[�h�e�[�u������T�����āA����WayPoint��Ԃ�
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

        // wayPoints���̃v���C���[�ɋ߂�WayPoint�̓Y������T��
        for (int i = 0; i < _wayPoints.Length; i++)
        {
            if (_targetWayPoint.gameObject.name == _wayPoints[i].gameObject.name)
            {
                // �m�[�h�e�[�u����2��ڂ���Ȃ̂�+1
                targetIndex = i + 1;
            }

            if (myWayPoint.gameObject.name == _wayPoints[i].gameObject.name)
            {
                // �m�[�h�e�[�u����2�s�ڂ���Ȃ̂�+1
                myIndex = i + 1;
            }
        }

        int nextWayPointIndex = _nextWayPointTable[myIndex, targetIndex];
        if (nextWayPointIndex == 0) return null;

        // �z���0�I���W�������m�[�h�e�[�u����1�I���W���Ȃ̂�-1����
        Transform nextWayPoint = _wayPoints[nextWayPointIndex - 1];
        return nextWayPoint;
    }
}