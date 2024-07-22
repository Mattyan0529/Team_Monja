using System.Net.Sockets;
using UnityEngine;

public class ChangeEnemyMoveType : MonoBehaviour
{
    [SerializeField]
    private GameObject _miniWayPoint = default;

    private RandomWayPointBetweenMove _randomMove = default;
    private FollowPlayer _followPlayer = default;
    private bool _isMove = true;

    public GameObject MiniWayPoint
    {
        get { return _miniWayPoint; }
    }

    public bool IsMove
    {
        get { return _isMove; }
    }

    public enum EnemyMoveState
    {
        InRandomMove,
        InFollow,
        InAttack
    }

    private EnemyMoveState _nowState = EnemyMoveState.InRandomMove;

    public EnemyMoveState NowState
    {
        get { return _nowState; }
        set { _nowState = value; }
    }

    private void Awake()
    {
        _miniWayPoint.transform.position = gameObject.transform.position;
    }

    private void Start()
    {
        _randomMove = GetComponent<RandomWayPointBetweenMove>();
        _followPlayer = GetComponent<FollowPlayer>();
    }

    public Transform EnemyMove(Transform myWayPoint)
    {
        Transform nextWayPoint = default;

        switch (_nowState)
        {
            case EnemyMoveState.InRandomMove:
                _isMove = true;
                nextWayPoint = _randomMove.SearchTargetWayPoint(myWayPoint);
                break;

            case EnemyMoveState.InFollow:
                _isMove = true;
                nextWayPoint = _followPlayer.SearchTargetWayPoint(myWayPoint);
                break; 

            case EnemyMoveState.InAttack:
                _isMove = false;
                break;
        }

        return nextWayPoint;
    }
}
