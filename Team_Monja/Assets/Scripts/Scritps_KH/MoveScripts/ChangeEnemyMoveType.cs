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
        set { _isMove = value; }
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
                nextWayPoint = _followPlayer.SearchTargetWayPoint(myWayPoint);
                LookAtPlayer();
                break;
        }

        return nextWayPoint;
    }

    private void LookAtPlayer()
    {
        // ñ⁄ìIínÇÃï˚å¸Ç…å¸Ç≠ÇÊÇ§Ç…èCê≥(âÒì]ÇÕYé≤ÇÃÇ›)
        Vector3 directionVector = _followPlayer.Player.position - gameObject.transform.position;
        Quaternion directionQuaternion = Quaternion.LookRotation(directionVector, Vector3.up);
        gameObject.transform.rotation = Quaternion.Euler(0f, directionQuaternion.eulerAngles.y, 0f);
    }
}
