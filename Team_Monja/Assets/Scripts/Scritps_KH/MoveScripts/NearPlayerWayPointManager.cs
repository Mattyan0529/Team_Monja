using System.Collections.Generic;
using UnityEngine;

public class NearPlayerWayPointManager : MonoBehaviour
{
    private List<Transform> _nearPlayerWayPoints = new List<Transform>();

    /// <summary>
    /// プレイヤーに近いWayPointのリストを参照する
    /// </summary>
    public List<Transform> NearPlayerWayPoint
    {
        get { return _nearPlayerWayPoints; }
    }

    /// <summary>
    /// プレイヤーに近いWayPointを追加する
    /// </summary>
    public void AddNearPlayerWayPoint(GameObject addWayPoint)
    {
        _nearPlayerWayPoints.Add(addWayPoint.transform);
    }

    /// <summary>
    /// プレイヤーに近いWayPointから削除する
    /// </summary>
    public void RemoveNearPlayerWayPoint(GameObject removeWayPoint)
    {
        _nearPlayerWayPoints.Remove(removeWayPoint.transform);
    }
}
