using UnityEngine;

public class WayPoint : MonoBehaviour
{
    private float _gizmosSize = 0.3f;

    [SerializeField]
    private GameObject _followArea = default;

    [SerializeField]
    private GameObject[] _nextPoints = default;

    private NearPlayerWayPointManager _nearPlayerWayPointManager = default;

    public GameObject[] NextPoints
    {
        get { return _nextPoints; }
    }

    private void Start()
    {
        _nearPlayerWayPointManager = _followArea.GetComponent<NearPlayerWayPointManager>();
    }

    /// <summary>
    /// ���F��������悤�ɂ���
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, _gizmosSize);
    }

    /// <summary>
    /// �v���C���[�̒Ǐ]�͈͂ƃE�F�C�|�C���g�̏Փ˂����m
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != _followArea) return;
        _nearPlayerWayPointManager.AddNearPlayerWayPoint(gameObject);
    }

    /// <summary>
    /// �v���C���[�̒Ǐ]�͈͂ƃE�F�C�|�C���g�̕��������m
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != _followArea) return;
        _nearPlayerWayPointManager.RemoveNearPlayerWayPoint(gameObject);
    }
}
