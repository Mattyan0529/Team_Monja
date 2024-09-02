using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class NearPlayerWayPointManager_KH : MonoBehaviour
{
    private List<Transform> _nearPlayerWayPoints = new List<Transform>();
    private GameObject _player;

    /// <summary>
    /// �v���C���[�ɋ߂�WayPoint�̃��X�g���Q�Ƃ���
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
    /// �v���C���[��ݒ�
    /// </summary>
    public void SetPlayer(GameObject player)
    {
        _player = player;
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
