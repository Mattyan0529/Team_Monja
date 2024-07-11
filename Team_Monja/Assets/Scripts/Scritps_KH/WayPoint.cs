using UnityEngine;

public class WayPoint : MonoBehaviour
{
    private float _gizmosSize = 0.3f;

    [SerializeField]
    private GameObject[] _nextPoints = default;

    public GameObject[] NextPoints
    {
        get { return _nextPoints; }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, _gizmosSize);
    }
}
