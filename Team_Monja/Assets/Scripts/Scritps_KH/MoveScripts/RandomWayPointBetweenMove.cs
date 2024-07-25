using UnityEngine;

public class RandomWayPointBetweenMove : MonoBehaviour, IFollowable
{
    private ChangeEnemyMoveType _changeEnemyMoveType = default;
    private FollowPlayer _followPlayer = default;
    private GameObject _wayPoint = default;
    private Transform _target = default;

    private void Start()
    {
        _changeEnemyMoveType = GetComponent<ChangeEnemyMoveType>();
        _followPlayer = GetComponent<FollowPlayer>();
        _wayPoint = GameObject.FindGameObjectWithTag("WayPoint");
    }

    /// <summary>
    /// 次のWayPointをランダムに探す
    /// </summary>
    public Transform SearchTargetWayPoint(Transform myWayPoint)
    {
        WayPoint wayPoint;
        GameObject[] MovableWayPoint;
        Transform nextWayPoint;

        // 呼び出し元がMiniWayPointではない場合（追従からランダム移動に戻るとき）
        if (myWayPoint.transform.parent.CompareTag("WayPoint"))
        {
            if (myWayPoint == _target)
            {
                nextWayPoint = _changeEnemyMoveType.MiniWayPoint.transform.GetChild(0);
            }
            else
            {
                nextWayPoint = SwitchFromFollowUpToRandomMoving();
            }
        }
        else
        {
            wayPoint = myWayPoint.GetComponent<WayPoint>();
            MovableWayPoint = wayPoint.NextPoints;
            // 目的地をランダムに選ぶ
            int nextPointsIndex = Random.Range(0, MovableWayPoint.Length - 1);

            nextWayPoint = MovableWayPoint[nextPointsIndex].transform;
        }

        return nextWayPoint;
    }

    /// <summary>
    /// 目的地となるMiniWayPointからいちばん近いメインWayPointをFollowPlayerのTargetに設定
    /// </summary>
    /// <returns>MiniWayPointからいちばん近いメインWayPoint</returns>
    private Transform SwitchFromFollowUpToRandomMoving()
    {
        Transform miniWayPoint = _changeEnemyMoveType.MiniWayPoint.transform;
        Transform nearestWayPoint = _wayPoint.transform.GetChild(0);

        foreach (Transform child in _wayPoint.transform)
        {
            float shortestDistance = Vector3.Distance
                (nearestWayPoint.position, miniWayPoint.position);

            float thisWayPointDistance = Vector3.Distance
                (child.position, miniWayPoint.position);

            // 現在最も近いWayPointからMiniWayPointまでの距離と、このWayPointからの距離を比べる
            if (shortestDistance > thisWayPointDistance)
            {
                nearestWayPoint = child;
            }
        }

        _target = nearestWayPoint;
        return nearestWayPoint;
    }
}
