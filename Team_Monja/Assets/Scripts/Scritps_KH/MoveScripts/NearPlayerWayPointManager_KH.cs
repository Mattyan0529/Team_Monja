using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class NearPlayerWayPointManager_KH : MonoBehaviour
{
    private List<Transform> _nearPlayerWayPoints = new List<Transform>();
    private GameObject _player;

    /// <summary>
    /// プレイヤーに近いWayPointのリストを参照する
    /// </summary>
    public List<Transform> NearPlayerWayPoint
    {
        get { return _nearPlayerWayPoints; }
    }

    private void Update()
    {
        gameObject.transform.position = _player.transform.position;
    }

    /// <summary>
    /// プレイヤーを設定
    /// </summary>
    public void SetPlayer(GameObject player)
    {
        _player = player;
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

    /// <summary>
    /// リストの要素をすべて削除する
    /// </summary>
    public void ClearNearPlayerWayPoint()
    {
        _nearPlayerWayPoints.Clear();
    }
}
