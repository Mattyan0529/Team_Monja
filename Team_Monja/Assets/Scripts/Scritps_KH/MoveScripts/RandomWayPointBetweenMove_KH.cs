using UnityEngine;

public class RandomWayPointBetweenMove_KH : MonoBehaviour, IFollowable_KH
{
    private GameObject _wayPoint = default;

    /// <summary>
    /// MiniWayPoint�����ԋ߂����C���E�F�C�|�C���g
    /// </summary>
    private Transform _target = default;

    private EnemyMove_KH _enemyMove = default;

    private void Start()
    {
        _enemyMove = GetComponent<EnemyMove_KH>();
        _wayPoint = _enemyMove.WayPoint;
    }

    /// <summary>
    /// ����WayPoint�������_���ɒT��
    /// </summary>
    public Transform SearchTargetWayPoint(Transform myWayPoint)
    {
        WayPoint_KH wayPoint;
        GameObject[] MovableWayPoint;
        Transform nextWayPoint;

        // �Ăяo������MiniWayPoint�ł͂Ȃ��ꍇ�i�Ǐ]���烉���_���ړ��ɖ߂�Ƃ��j
        if (myWayPoint.transform.parent.CompareTag("WayPoint"))
        {
            if (myWayPoint == _target)
            {
                nextWayPoint = _enemyMove.MiniWayPoint.transform.GetChild(0);
            }
            else
            {
                nextWayPoint = SwitchFromFollowUpToRandomMoving();
            }
        }
        else
        {
            wayPoint = myWayPoint.GetComponent<WayPoint_KH>();
            MovableWayPoint = wayPoint.NextPoints;
            // �ړI�n�������_���ɑI��
            int nextPointsIndex = Random.Range(0, MovableWayPoint.Length - 1);

            nextWayPoint = MovableWayPoint[nextPointsIndex].transform;
        }

        return nextWayPoint;
    }

    /// <summary>
    /// �ړI�n�ƂȂ�MiniWayPoint���炢���΂�߂����C��WayPoint��FollowPlayer��Target�ɐݒ�
    /// </summary>
    /// <returns>MiniWayPoint���炢���΂�߂����C��WayPoint</returns>
    private Transform SwitchFromFollowUpToRandomMoving()
    {
        Transform miniWayPoint = _enemyMove.MiniWayPoint.transform;
        Transform nearestWayPoint = _wayPoint.transform.GetChild(0);

        foreach (Transform child in _wayPoint.transform)
        {
            float shortestDistance = Vector3.Distance
                (nearestWayPoint.position, miniWayPoint.position);

            float thisWayPointDistance = Vector3.Distance
                (child.position, miniWayPoint.position);

            // ���ݍł��߂�WayPoint����MiniWayPoint�܂ł̋����ƁA����WayPoint����̋������ׂ�
            if (shortestDistance > thisWayPointDistance)
            {
                nearestWayPoint = child;
            }
        }

        _target = nearestWayPoint;
        return nearestWayPoint;
    }
}
