using UnityEngine;

public class RandomWayPointBetweenMove_KH : MonoBehaviour, IFollowable_KH
{
    private GameObject _wayPoint = default;

    /// <summary>
    /// MiniWayPointから一番近いメインウェイポイント
    /// </summary>
    private Transform _target = default;

    private EnemyMove_KH _enemyMove = default;

    private void Start()
    {
        _enemyMove = GetComponent<EnemyMove_KH>();
        _wayPoint = _enemyMove.WayPoint;
    }

    /// <summary>
    /// 次のWayPointをランダムに探す
    /// </summary>
    public Transform SearchTargetWayPoint(Transform myWayPoint)
    {
        WayPoint_KH wayPoint;
        GameObject[] MovableWayPoint;
        Transform nextWayPoint;

        // 呼び出し元がMiniWayPointではない場合（追従からランダム移動に戻るとき）
        if (myWayPoint.transform.parent.CompareTag("WayPoint"))
        {
            if (myWayPoint == _target)
            {
                nextWayPoint = _enemyMove.MiniWayPoint.transform.GetChild(0);
            }
            else
            {
                nextWayPoint = SwitchFromFollowUpToRandomMoving();
            }
        }
        else
        {
            wayPoint = myWayPoint.GetComponent<WayPoint_KH>();
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
        Transform miniWayPoint = _enemyMove.MiniWayPoint.transform;
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
