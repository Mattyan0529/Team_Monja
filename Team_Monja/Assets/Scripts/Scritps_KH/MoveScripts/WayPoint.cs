using UnityEngine;

public class WayPoint : MonoBehaviour
{
    private float _gizmosSize = 1f;

    [SerializeField]
    private GameObject _nearPlayerArea = default;

    [SerializeField]
    private GameObject[] _nextPoints = default;

    private NearPlayerWayPointManager _nearPlayerWayPointManager = default;

    public GameObject[] NextPoints
    {
        get { return _nextPoints; }
    }

    private void Start()
    {
        _nearPlayerWayPointManager = _nearPlayerArea.GetComponent<NearPlayerWayPointManager>();
    }

    /// <summary>
    /// 黄色く見えるようにする
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, _gizmosSize);
    }

    /// <summary>
    /// プレイヤーの追従範囲とウェイポイントの衝突を検知
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("NearPlayerArea")) return;
        if (!transform.parent.CompareTag("WayPoint")) return;
        _nearPlayerWayPointManager.AddNearPlayerWayPoint(gameObject);
    }

    /// <summary>
    /// プレイヤーの追従範囲とウェイポイントの分離を検知
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != _nearPlayerArea) return;
        if (!transform.parent.CompareTag("WayPoint")) return;
        _nearPlayerWayPointManager.RemoveNearPlayerWayPoint(gameObject);
    }
}
