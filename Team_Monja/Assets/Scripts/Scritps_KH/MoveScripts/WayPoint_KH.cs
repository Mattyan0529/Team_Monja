using UnityEngine;

public class WayPoint_KH : MonoBehaviour
{
    private float _gizmosSize = 1f;

    [SerializeField]
    private GameObject _nearPlayerArea = default;

    [SerializeField]
    private GameObject[] _nextPoints = default;

    [SerializeField]
    private GameObject _bossWayPoints = default;

    private NearPlayerWayPointManager_KH _nearPlayerWayPointManager = default;

    public GameObject[] NextPoints
    {
        get { return _nextPoints; }
    }

    private void Start()
    {
        _nearPlayerWayPointManager = _nearPlayerArea.GetComponent<NearPlayerWayPointManager_KH>();
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
    /// �v���C���[�̃E�F�C�|�C���g���W�͈͂ƃE�F�C�|�C���g�̏Փ˂����m
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("NearPlayerArea")) return;
        if (!transform.parent.CompareTag("WayPoint")) return;

        _nearPlayerWayPointManager.AddNearPlayerWayPoint(gameObject);
    }

    /// <summary>
    /// �v���C���[�̒Ǐ]�͈͂ƃE�F�C�|�C���g�̕��������m
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != _nearPlayerArea) return;
        if (!transform.parent.CompareTag("WayPoint")) return;
        _nearPlayerWayPointManager.RemoveNearPlayerWayPoint(gameObject);
    }
}
