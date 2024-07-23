using System.Collections.Generic;
using UnityEngine;

public class NearPlayerWayPointManager : MonoBehaviour
{
    private List<Transform> _nearPlayerWayPoints = new List<Transform>();

    /// <summary>
    /// �v���C���[�ɋ߂�WayPoint�̃��X�g���Q�Ƃ���
    /// </summary>
    public List<Transform> NearPlayerWayPoint
    {
        get { return _nearPlayerWayPoints; }
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
}
