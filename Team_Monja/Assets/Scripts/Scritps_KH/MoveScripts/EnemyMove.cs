using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private GameObject _wayPoints = default;

    private Transform _currentWayPoint = default;
    private Transform _targetWayPoint = default;

    private GameObject _miniWayPoint = default;

    private float _shortestDistance = default;
    private float _followStopDistance = 2f;
    private float _followShortDistance = 9f;

    private ChangeEnemyMoveType _changeEnemyMoveType = default;
    private PlayerManager_KH _playerManager = default;

    private CharacterAnim_MT _characterAnim = default;

    private EnemyState _nowEnemyState = EnemyState.InSearch;

    private enum EnemyState
    {
        /// <summary>
        /// ���̖ړI�n��T����
        /// </summary>
        InSearch,

        /// <summary>
        /// �ړI�n�Ɉړ���
        /// </summary>
        InMove,

        /// <summary>
        /// �ߋ����̂��߃E�F�C�|�C���g���g�킸�ɒǏ]
        /// </summary>
        InShortDistanceFollowUp
    }

    public GameObject WayPoint
    {
        get { return _wayPoints; }
    }

    void Start()
    {
        _changeEnemyMoveType = GetComponent<ChangeEnemyMoveType>();
        _playerManager = GameObject.FindGameObjectWithTag("ResidentScripts").GetComponent<PlayerManager_KH>();
        _characterAnim = GetComponent<CharacterAnim_MT>();
        _miniWayPoint = _changeEnemyMoveType.MiniWayPoint;

        SearchNearMainWayPoint();

        _nowEnemyState = EnemyState.InMove;
    }

    void Update()
    {
        if (_nowEnemyState == EnemyState.InMove && _changeEnemyMoveType.IsMove == true)
        {
            MoveToTargetWayPoint();
            _characterAnim.NowAnim = "Move";
            JudgeShortDistance();
        }
        else if (_nowEnemyState == EnemyState.InShortDistanceFollowUp)
        {
            FreeFollowUp();
            JudgeShortDistance();
        }
        else
        {
            NextWayPointSearch();
        }


        // MiniWayPoint�̍�����Ή�����L�����N�^�[�̍����ɂ���
        _miniWayPoint.transform.position = new Vector3(_miniWayPoint.transform.position.x, transform.position.y, 
            _miniWayPoint.transform.position.z);
    }

    /// <summary>
    /// �ړ����I�������A�܂�����WayPoint��T��
    /// </summary>
    private void NextWayPointSearch()
    {
        // ���������̂ŁA�ړI�n�����ݒn�ɕύX
        _currentWayPoint = _targetWayPoint;

        // ���C����WayPoint�ł͂Ȃ��Ƃ��납��Follow���Ăяo���ꂽ�烁�C����WayPoint�ɍs��
        if (_changeEnemyMoveType.NowState == ChangeEnemyMoveType.EnemyMoveState.InFollow
            && !_currentWayPoint.transform.parent.CompareTag("WayPoint"))
        {
            SearchNearMainWayPoint();
            _nowEnemyState = EnemyState.InMove;
        }

        if (_changeEnemyMoveType.EnemyMove(_currentWayPoint) == null) return;

        _targetWayPoint = _changeEnemyMoveType.EnemyMove(_currentWayPoint);

        _nowEnemyState = EnemyState.InMove;
    }

    /// <summary>
    /// ����WayPoint�Ɉړ�����
    /// </summary>
    private void MoveToTargetWayPoint()
    {
        if (_changeEnemyMoveType.NowState == ChangeEnemyMoveType.EnemyMoveState.InAttack)
        {
            _changeEnemyMoveType.IsMove = false;
            return;
        }

        gameObject.transform.position = Vector3.MoveTowards
            (gameObject.transform.position, _targetWayPoint.transform.position, _changeEnemyMoveType.NowSpeed * Time.deltaTime);

        // �ړI�n�̕����Ɍ����悤�ɏC��(��]��Y���̂�)
        Vector3 directionVector = _targetWayPoint.position - gameObject.transform.position;
        Quaternion directionQuaternion = Quaternion.LookRotation(directionVector, Vector3.up);
        directionQuaternion = Quaternion.Slerp(transform.rotation, directionQuaternion, Time.deltaTime * _changeEnemyMoveType.NowRotationSpeed);
        gameObject.transform.rotation = Quaternion.Euler(0f, directionQuaternion.eulerAngles.y, 0f);

        Vector3 nowPos = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 targetPos = new Vector3(_targetWayPoint.transform.position.x, 0f, _targetWayPoint.transform.position.z);

        // ������x�߂��Ȃ����玟�̖ړI�n��
        if (Vector3.SqrMagnitude(targetPos - nowPos) < Mathf.Pow(_followStopDistance, 2))
        {
            _nowEnemyState = EnemyState.InSearch;
        }
    }

    private void SearchNearMainWayPoint()
    {
        bool isFirst = true;

        // WayPoint�ЂƂ��ƌ��ݒn���ׁA�����΂�߂�WayPoint��target�Ƃ���
        foreach (Transform child in _wayPoints.transform)
        {
            // �ŏ��͂Ƃ肠��������WayPoint������
            if (isFirst)
            {
                _targetWayPoint = child;
                _shortestDistance = Vector3.Distance
                    (_targetWayPoint.transform.position, gameObject.transform.position);
                isFirst = false;
            }

            // ����WayPoint�ƌ��ݒn�̋���
            float thisWayPointDistance = Vector3.Distance
                (child.transform.position, gameObject.transform.position);

            if (Mathf.Abs(_shortestDistance) > Mathf.Abs(thisWayPointDistance))
            {
                _targetWayPoint = child;
                _shortestDistance = Vector3.Distance
                    (_targetWayPoint.transform.position, gameObject.transform.position);
            }
        }
    }

    /// <summary>
    /// �E�F�C�|�C���g���g�킸�ɒǏ]
    /// </summary>
    private void FreeFollowUp()
    {
        GameObject player = _playerManager.Player;

        Vector3 nowPos = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 playerPos = new Vector3(player.transform.position.x, 0f, player.transform.position.z);

        // �v���C���[�Ƌ߂��ꍇ�͒Ǐ]����߂�
        if (Vector3.SqrMagnitude(playerPos - nowPos) < Mathf.Pow(_followStopDistance, 2)) return;

        // �v���C���[�Ɍ������Đi��
        gameObject.transform.position = Vector3.MoveTowards
            (gameObject.transform.position, player.transform.position, _changeEnemyMoveType.NowSpeed * Time.deltaTime);

        // �ړI�n�̕����Ɍ����悤�ɏC��(��]��Y���̂�)
        Vector3 directionVector = player.transform.position - gameObject.transform.position;
        Quaternion directionQuaternion = Quaternion.LookRotation(directionVector, Vector3.up);
        directionQuaternion = Quaternion.Slerp(transform.rotation, directionQuaternion, Time.deltaTime * _changeEnemyMoveType.NowRotationSpeed);
        gameObject.transform.rotation = Quaternion.Euler(0f, directionQuaternion.eulerAngles.y, 0f);
    }

    private void JudgeShortDistance()
    {
        GameObject player = _playerManager.Player;

        Vector3 nowPos = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 playerPos = new Vector3(player.transform.position.x, 0f, player.transform.position.z);

        if (Vector3.SqrMagnitude(playerPos - nowPos) < Mathf.Pow(_followShortDistance, 2) &&
            _nowEnemyState == EnemyState.InMove)
        {
            _nowEnemyState = EnemyState.InShortDistanceFollowUp;
        }
        else if(Vector3.SqrMagnitude(playerPos - nowPos) > Mathf.Pow(_followShortDistance, 2) &&
            _nowEnemyState == EnemyState.InShortDistanceFollowUp)
        {
            SearchNearMainWayPoint();
            _nowEnemyState = EnemyState.InMove;
        }
    }
}

