using UnityEngine;

public class ChangeEnemyMoveType_KH : MonoBehaviour
{
    [SerializeField]
    private float _maxSpeed = 8f;

    [SerializeField]
    private float _randomMoveSpeed = 1f;

    [SerializeField]
    private float _maxRotationSpeed = 8f;

    private RandomWayPointBetweenMove_KH _randomMove = default;
    private FollowPlayer_KH _followPlayer = default;
    private StatusManager_MT _myStatusManager = default;

    private Transform _targetPoint = default;

    private float _nowSpeed = 8f;
    private float _speedAfterCalculation = default;
    private float _nowRotationSpeed = 8f;

    private float _slowToNormalSwitchDistance = 3f;
    private float _normalToFastSwitchDistance = 5f;

    private bool _isMove = true;

    private GameObject _player = default;
    private float _Speed = default;

    #region Property

    public float NowSpeed
    {
        get { return _nowSpeed; }
    }

    public float FollowSpeed
    {
        get { return _maxSpeed * _myStatusManager.SpeedMultiplier; }
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
    /// �X�s�[�h���v�Z���邽�߂̌����x�i�������2���Ďg���j
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

    private void Start()
    {
        _randomMove = GetComponent<RandomWayPointBetweenMove_KH>();
        _followPlayer = GetComponent<FollowPlayer_KH>();
        _myStatusManager = GetComponent<StatusManager_MT>();
    }

    private void Update()
    {
        Debug.Log(NowState);
    }

    /// <summary>
    /// �v���C���[��ݒ�
    /// </summary>
    /// <param name="player"></param>
    public void SetPlayer(GameObject player)
    {
        _player = player;
    }

    /// <summary>
    /// ���̏�ԂɓK��������WayPoint��T������
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
                _nowSpeed = _randomMoveSpeed * _myStatusManager.SpeedMultiplier;
                CalculateSpeed();
                nextWayPoint = _randomMove.SearchTargetWayPoint(myWayPoint);
                _targetPoint = nextWayPoint;
                break;

            case EnemyMoveState.InFollow:
                _isMove = true;
                _nowSpeed = _maxSpeed * _myStatusManager.SpeedMultiplier;
                CalculateSpeed();
                nextWayPoint = _followPlayer.SearchTargetWayPoint(myWayPoint);
                _targetPoint = _player.transform;
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
        // �ړI�n�̕����Ɍ����悤�ɏC��(��]��Y���̂�)
        Vector3 directionVector = _player.transform.position - gameObject.transform.position;
        Quaternion directionQuaternion = Quaternion.LookRotation(directionVector, Vector3.up);
        directionQuaternion = Quaternion.Slerp(transform.rotation, directionQuaternion, Time.deltaTime * NowRotationSpeed);
        gameObject.transform.rotation = Quaternion.Euler(0f, directionQuaternion.eulerAngles.y, 0f);
    }

    /// <summary>
    /// �ړI�n�ւ̋��������Ƃɑ��x�����߂�
    /// </summary>
    private void CalculateSpeed()
    {
        if(_targetPoint == null && _player != null)
        {
            _targetPoint = _player.transform;
        }

        Vector3 distance = _targetPoint.position - gameObject.transform.position;
        distance.y = 0f;
        float magnitude = Vector3.SqrMagnitude(distance);

        // SqrMagnitude��2��̒l�̂܂܏o�Ă���̂ŁA��ׂ�ϐ���2�悷��
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

        // _deceleration�́��Q�Œ�������O��ō���Ă�
        _nowSpeed = _nowSpeed / ((int)_deceleration / 2f);
    }

    private void ResurrectionStart()
    {
        _isMove = false;
    }
}
