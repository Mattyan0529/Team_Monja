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
        /// 次の目的地を探索中
        /// </summary>
        InSearch,

        /// <summary>
        /// 目的地に移動中
        /// </summary>
        InMove,

        /// <summary>
        /// 近距離のためウェイポイントを使わずに追従
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


        // MiniWayPointの高さを対応するキャラクターの高さにする
        _miniWayPoint.transform.position = new Vector3(_miniWayPoint.transform.position.x, transform.position.y, 
            _miniWayPoint.transform.position.z);
    }

    /// <summary>
    /// 移動し終わったら、また次のWayPointを探す
    /// </summary>
    private void NextWayPointSearch()
    {
        // 到着したので、目的地を現在地に変更
        _currentWayPoint = _targetWayPoint;

        // メインのWayPointではないところからFollowが呼び出されたらメインのWayPointに行く
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
    /// 次のWayPointに移動する
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

        // 目的地の方向に向くように修正(回転はY軸のみ)
        Vector3 directionVector = _targetWayPoint.position - gameObject.transform.position;
        Quaternion directionQuaternion = Quaternion.LookRotation(directionVector, Vector3.up);
        directionQuaternion = Quaternion.Slerp(transform.rotation, directionQuaternion, Time.deltaTime * _changeEnemyMoveType.NowRotationSpeed);
        gameObject.transform.rotation = Quaternion.Euler(0f, directionQuaternion.eulerAngles.y, 0f);

        Vector3 nowPos = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 targetPos = new Vector3(_targetWayPoint.transform.position.x, 0f, _targetWayPoint.transform.position.z);

        // ある程度近くなったら次の目的地へ
        if (Vector3.SqrMagnitude(targetPos - nowPos) < Mathf.Pow(_followStopDistance, 2))
        {
            _nowEnemyState = EnemyState.InSearch;
        }
    }

    private void SearchNearMainWayPoint()
    {
        bool isFirst = true;

        // WayPointひとつずつと現在地を比べ、いちばん近いWayPointをtargetとする
        foreach (Transform child in _wayPoints.transform)
        {
            // 最初はとりあえず今のWayPointを入れる
            if (isFirst)
            {
                _targetWayPoint = child;
                _shortestDistance = Vector3.Distance
                    (_targetWayPoint.transform.position, gameObject.transform.position);
                isFirst = false;
            }

            // このWayPointと現在地の距離
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
    /// ウェイポイントを使わずに追従
    /// </summary>
    private void FreeFollowUp()
    {
        GameObject player = _playerManager.Player;

        Vector3 nowPos = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 playerPos = new Vector3(player.transform.position.x, 0f, player.transform.position.z);

        // プレイヤーと近い場合は追従をやめる
        if (Vector3.SqrMagnitude(playerPos - nowPos) < Mathf.Pow(_followStopDistance, 2)) return;

        // プレイヤーに向かって進む
        gameObject.transform.position = Vector3.MoveTowards
            (gameObject.transform.position, player.transform.position, _changeEnemyMoveType.NowSpeed * Time.deltaTime);

        // 目的地の方向に向くように修正(回転はY軸のみ)
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

