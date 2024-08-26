using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class NearPlayerWayPointManager_KH : MonoBehaviour
{
    private List<Transform> _nearPlayerWayPoints = new List<Transform>();
    private GameObject _residentScript = default;
    private PlayerManager_KH _playerManager = default;

    /// <summary>
    /// �v���C���[�ɋ߂�WayPoint�̃��X�g���Q�Ƃ���
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
    /// �v���C���[�ɋ߂�WayPoint��ǉ�����
    /// </summary>
    public void AddNearPlayerWayPoint(GameObject addWayPoint)
    {
        _nearPlayerWayPoints.Add(addWayPoint.transform);
    }

    /// <summary>
    /// �v���C���[�ɋ߂�WayPoint����폜����
    /// </summary>
    public void RemoveNearPlayerWayPoint(GameObject removeWayPoint)
    {
        _nearPlayerWayPoints.Remove(removeWayPoint.transform);
    }

    /// <summary>
    /// ���X�g�̗v�f�����ׂč폜����
    /// </summary>
    public void ClearNearPlayerWayPoint()
    {
        _nearPlayerWayPoints.Clear();
    }
}
