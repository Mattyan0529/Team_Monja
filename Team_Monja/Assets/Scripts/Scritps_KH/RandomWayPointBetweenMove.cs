using UnityEngine;

public class RandomWayPointBetweenMove : MonoBehaviour, IFollowable
{
    private ChangeEnemyMoveType _changeEnemyMoveType = default;

    private void Start()
    {
        _changeEnemyMoveType = GetComponent<ChangeEnemyMoveType>();
    }

    /// <summary>
    /// 次のWayPointをランダムに探す
    /// </summary>
    public Transform SearchTargetWayPoint(Transform myWayPoint)
    {
                WayPoint wayPoint;
        GameObject[] MovableWayPoint;

        // 呼び出し元がMiniWayPointではない場合
        if (myWayPoint.transform.parent.CompareTag("WayPoint"))
        {
            Transform miniWayPoint = _changeEnemyMoveType.MiniWayPoint.transform;
            int index = 0;
            MovableWayPoint = new GameObject[miniWayPoint.childCount];

            foreach (Transform child in miniWayPoint)
            {
                MovableWayPoint[index] = child.gameObject;
                index++;
            }
        }
        else
        {
            wayPoint = myWayPoint.GetComponent<WayPoint>();
            MovableWayPoint = new GameObject[wayPoint.NextPoints.Length];
            MovableWayPoint = wayPoint.NextPoints;
        }

        // 目的地をランダムに選ぶ
        int nextPointsIndex = Random.Range(0, MovableWayPoint.Length - 1);

        Transform nextWayPoint = MovableWayPoint[nextPointsIndex].transform;

        return nextWayPoint;
    }
}
