using UnityEngine;
using System.Collections;

public class DragPlayerToBoss : MonoBehaviour
{
    [SerializeField]
    private GameObject _targetWayPoint = default;
    private GameObject _player = default;
    private GameObject _nearPlayerArea = default;
    private Transform[] _wayPoints = default;
    private int[,] _nextWayPointTable = default;
    private Transform _nextWayPoint = default;
    private Transform _currentWayPoint = default;

    private SearchWayPointTwoDimensionalArray _searchWayPointTwoDimensionalArray = default;
    private EnemyMove _enemyMove;
    private PlayerManager_KH _playerManager = default;

    private float _speed = 100f;
    private float _followStopDistance = 0.5f;
    private float _shortestDistance = default;

    // 今移動している状態ならfalse 移動が終わり探索待ちかどうか
    private bool _isSearch = true;
    // メインのウェイポイントに届けるためのフラグ（最初の一回の探索かどうか）
    private bool _isFirst = true;
    // プレイヤーを引きずる状態かどうか
    private bool _isdrag = false;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        _playerManager = GameObject.FindGameObjectWithTag("ResidentScripts").GetComponent<PlayerManager_KH>();

        _nearPlayerArea = GameObject.FindGameObjectWithTag("NearPlayerArea");
        _searchWayPointTwoDimensionalArray =
            _nearPlayerArea.gameObject.GetComponent<SearchWayPointTwoDimensionalArray>();
        _enemyMove = _player.GetComponent<EnemyMove>();
    }

    private void Update()
    {
        if (!_isdrag) return;
        _player = _playerManager.Player;

        if (_isFirst)
        {
            PlayerToMainWayPoint();
        }
        else
        {
            DragPlayer();
        }
    }

    private void IsDragTrue()
    {
        _isdrag = true;
    }

    /// <summary>
    /// プレイヤーをボスへ引っ張るときの最初の１回のみ呼び出し
    /// </summary>
    public void PlayerToMainWayPoint()
    {
        SearchNearMainWayPoint();
        _isFirst = false;
    }

    /// <summary>
    /// PlayerToMainWayPointを先に呼び出してから
    /// </summary>
    public void DragPlayer()
    {
        if (_isSearch)
        {
            SearchTargetWayPoint(_currentWayPoint);
        }
        else
        {
            MoveCharactor();
        }
    }

    /// <summary>
    /// ノードテーブル内を探索して、次のWayPointを返す
    /// </summary>
    private void SearchTargetWayPoint(Transform myWayPoint)
    {
        // すでに目的地に到着していた場合
        if(_targetWayPoint.name == myWayPoint.name)
        {
            StartCoroutine(DeactivateAfterOneSecond());
            _isdrag = false;
            return;
        }

        _nextWayPointTable = _searchWayPointTwoDimensionalArray.NextWayPointTable;

        int targetIndex = 0;
        int myIndex = 0;

        // wayPoints内のプレイヤーに近いWayPointの添え字を探す
        for (int i = 0; i < _wayPoints.Length; i++)
        {
            if (_targetWayPoint.name == _wayPoints[i].gameObject.name)
            {
                // ノードテーブルは2列目からなので+1
                targetIndex = i + 1;
            }

            if (myWayPoint.gameObject.name == _wayPoints[i].gameObject.name)
            {
                // ノードテーブルは2行目からなので+1
                myIndex = i + 1;
            }
        }

        int nextWayPointIndex = _nextWayPointTable[myIndex, targetIndex];

        if (nextWayPointIndex == 0) return;

        // 配列は0オリジンだがノードテーブルは1オリジンなので-1する
        Transform nextWayPoint = _wayPoints[nextWayPointIndex - 1];

        _nextWayPoint = nextWayPoint;
        _isSearch = false;
    }

    /// <summary>
    /// 現在地からいちばん近いWayPointを探す（最初）
    /// </summary>
    private void SearchNearMainWayPoint()
    {
        bool isFirst = true;

        GameObject wayPoint = _enemyMove.WayPoint;
        _wayPoints = new Transform[wayPoint.transform.childCount];

        for (int i = 0; i < wayPoint.transform.childCount; i++)
        {
            _wayPoints[i] = wayPoint.transform.GetChild(i).transform;
        }

        // WayPointひとつずつと現在地を比べ、いちばん近いWayPointをtargetとする
        foreach (Transform child in _wayPoints)
        {
            // 最初はとりあえず今のWayPointを入れる
            if (isFirst)
            {
                _nextWayPoint = child;
                _shortestDistance = Vector3.Distance
                    (_nextWayPoint.transform.position, _player.transform.position);
                isFirst = false;
            }

            // このWayPointと現在地の距離
            float thisWayPointDistance = Vector3.Distance
                (child.transform.position, _player.transform.position);

            if (Mathf.Abs(_shortestDistance) > Mathf.Abs(thisWayPointDistance))
            {
                _nextWayPoint = child;
                _shortestDistance = Vector3.Distance
                    (_nextWayPoint.transform.position, _player.transform.position);
            }
        }

        _isSearch = false;
    }

    /// <summary>
    /// キャラクターを動かす
    /// </summary>
    private void MoveCharactor()
    {
        _player.transform.position = Vector3.MoveTowards
            (_player.transform.position, _nextWayPoint.transform.position, _speed * Time.deltaTime);

        // 目的地の方向に向くように修正(回転はY軸のみ)
        Vector3 directionVector = _nextWayPoint.position - _player.transform.position;
        Quaternion directionQuaternion = Quaternion.LookRotation(directionVector, Vector3.up);
        _player.transform.rotation = Quaternion.Euler(0f, directionQuaternion.eulerAngles.y, 0f);

        Vector3 nowPos = new Vector3(_player.transform.position.x, 0f, _player.transform.position.z);
        Vector3 targetPos = new Vector3(_nextWayPoint.transform.position.x, 0f, _nextWayPoint.transform.position.z);

        // ある程度近くなったら次の目的地へ
        if (Vector3.SqrMagnitude(targetPos - nowPos) < Mathf.Pow(_followStopDistance, 2))
        {
            _currentWayPoint = _nextWayPoint;
            _isSearch = true;
        }

    }

    // 指定時間待ってからオブジェクトを非アクティブにするコルーチン
    IEnumerator DeactivateAfterOneSecond()
    {
        yield return new WaitForSeconds(0.25f);

        // オブジェクトを非アクティブにする
        this.gameObject.SetActive(false);
        
    }
}
