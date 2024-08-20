using System.Net.Sockets;
using UnityEngine;

public class ChangeEnemyMoveType : MonoBehaviour
{
    [SerializeField]
    private GameObject _miniWayPoint = default;

    [SerializeField]
    private float _maxSpeed = 8f;

    [SerializeField]
    private float _maxRotationSpeed = 8f;

    private RandomWayPointBetweenMove _randomMove = default;
    private FollowPlayer _followPlayer = default;

    private Transform _targetPoint = default;

    private float _nowSpeed = 8f;
    private float _nowRotationSpeed = 8f;

    private float _slowToNormalSwitchDistance = 3f;
    private float _normalToFastSwitchDistance = 5f;

    private bool _isMove = true;

    #region Property

    public GameObject MiniWayPoint
    {
        get { return _miniWayPoint; }
    }

    public float MaxSpeed
    {
        get { return _maxSpeed; }
    }

    public float MaxRotationSpeed
    {
        get { return _maxRotationSpeed; }
    }

    public float NowSpeed
    {
        get { return _nowSpeed; }
    }

    public float NowRotationSpeed
    {
        get { return _nowRotationSpeed; }
    }


    public bool IsMove
    {
        get { return _isMove; }
        set { _isMove = value; }
    }

    #endregion

    #region EnemyMoveState

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

    #endregion

    #region Deceleration

    /// <summary>
    /// スピードを計算するための減速度（これを÷2して使う）
    /// </summary>
    public enum Deceleration
    {
        fast = 2,
        normal,
        slow
    }

    private Deceleration _deceleration = Deceleration.fast;

    public float NowDeceleration
    {
        get { return (int)_deceleration / 2f; }
    }

    #endregion

    private void Awake()
    {
        _miniWayPoint.transform.position = gameObject.transform.position;
    }

    private void Start()
    {
        _randomMove = GetComponent<RandomWayPointBetweenMove>();
        _followPlayer = GetComponent<FollowPlayer>();
    }

    private void Update()
    {
        CalculateSpeed();
    }

    /// <summary>
    /// 今の状態に適した次のWayPointを探索する
    /// </summary>
    /// <param name="myWayPoint"></param>
    /// <returns></returns>
    public Transform EnemyMove(Transform myWayPoint)
    {
        Transform nextWayPoint = default;

        switch (_nowState)
        {
            case EnemyMoveState.InRandomMove:
                _isMove = true;
                nextWayPoint = _randomMove.SearchTargetWayPoint(myWayPoint);
                _targetPoint = nextWayPoint;
                break;

            case EnemyMoveState.InFollow:
                _isMove = true;
                nextWayPoint = _followPlayer.SearchTargetWayPoint(myWayPoint);
                _targetPoint = _followPlayer.Player;
                break; 

            case EnemyMoveState.InAttack:
                _isMove = false;
                nextWayPoint = _followPlayer.SearchTargetWayPoint(myWayPoint);
                LookAtPlayer();
                break;
        }
        Debug.Log(gameObject.name + nextWayPoint);
        return nextWayPoint;
    }

    private void LookAtPlayer()
    {
        // 目的地の方向に向くように修正(回転はY軸のみ)
        Vector3 directionVector = _followPlayer.Player.position - gameObject.transform.position;
        Quaternion directionQuaternion = Quaternion.LookRotation(directionVector, Vector3.up);
        directionQuaternion = Quaternion.Slerp(transform.rotation, directionQuaternion, Time.deltaTime * NowRotationSpeed);
        gameObject.transform.rotation = Quaternion.Euler(0f, directionQuaternion.eulerAngles.y, 0f);
    }

    /// <summary>
    /// 目的地への距離をもとに速度を求める
    /// </summary>
    private void CalculateSpeed()
    {
        if(_targetPoint == null)
        {
            _targetPoint = _followPlayer.Player;
        }

        Vector3 distance = _targetPoint.position - gameObject.transform.position;
        distance.y = 0f;
        float magnitude = Vector3.SqrMagnitude(distance);

        // SqrMagnitudeは2乗の値のまま出てくるので、比べる変数も2乗する
        if(magnitude > Mathf.Pow(_normalToFastSwitchDistance, 2))
        {
            _deceleration = Deceleration.fast;
        }
        else if (magnitude > Mathf.Pow(_slowToNormalSwitchDistance, 2))
        {
            _deceleration = Deceleration.normal;
        }
        else
        {
            _deceleration = Deceleration.slow;
        }

        _nowSpeed = _maxSpeed / NowDeceleration;
    }
}
