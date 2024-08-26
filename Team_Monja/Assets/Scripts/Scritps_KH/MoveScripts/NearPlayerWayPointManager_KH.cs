using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class NearPlayerWayPointManager_KH : MonoBehaviour
{
    private List<Transform> _nearPlayerWayPoints = new List<Transform>();
    private GameObject _residentScript = default;
    private PlayerManager_KH _playerManager = default;

    /// <summary>
    /// プレイヤーに近いWayPointのリストを参照する
    /// </summary>
    public List<Transform> NearPlayerWayPoint
    {
        get { return _nearPlayerWayPoints; }
    }

    private void Start()
    {
        _residentScript = GameObject.FindGameObjectWithTag("ResidentScripts");
        _playerManager = _residentScript.GetComponent<PlayerManager_KH>();
    }

    private void Update()
    {
        gameObject.transform.position = _playerManager.Player.transform.position;
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
