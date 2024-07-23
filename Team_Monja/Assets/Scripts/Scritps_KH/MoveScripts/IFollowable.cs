using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFollowable
{
    Transform SearchTargetWayPoint(Transform myWayPoint);
}
