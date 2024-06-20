using UnityEngine;

public class PlayerRangeInJudge_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _followArea;

    private MonsterRandomWalk_KH _monsterRandomWalk = default;

    private float _speed = 2f;

    private float _followStopDistance = 1.1f;

    private Vector3 _beforePos;
    private Vector3 _afterPos;

    private float _updateTime = 0.5f;
    private float _elapsedTime = 0f;

    void Start()
    {
        _monsterRandomWalk = GetComponent<MonsterRandomWalk_KH>();
        _beforePos = gameObject.transform.position;
    }

    /// <summary>
    /// プレイヤー追従
    /// </summary>
    private void FollowPlayer()
    {
        // プレイヤーにくっついてるSphereの場所（プレイヤーの場所と同義）
        Vector3 _playerPos = _followArea.transform.position;

        // 規定値以上にプレイヤーと敵が近かったら追従をやめる
        if (Vector3.Distance(gameObject.transform.position, _playerPos) < _followStopDistance) return;

        // 追従
        transform.position = Vector3.MoveTowards(gameObject.transform.position, _playerPos, _speed * Time.deltaTime);
    }

    /// <summary>
    /// 自動追従時のモンスターの方向更新
    /// </summary>
    private void MonsterRotation()
    {
        _afterPos = gameObject.transform.position;
        Vector3 _difference = _afterPos - _beforePos;

        _difference.y = 0f;

        if (_difference == Vector3.zero) return;        // 動いていないときは処理をしない（_differenceが0だったらエラーになる）

        // 移動した方向に向くように修正(回転はY軸のみ)
        Quaternion newDirection = Quaternion.LookRotation(_difference, Vector3.up);
        gameObject.transform.rotation = Quaternion.Euler(0f, newDirection.eulerAngles.y, 0f);
        _beforePos = _afterPos;
    }

    /// <summary>
    /// 定期的に方向を変える
    /// </summary>
    private void UpdateTime()
    {
        // 時間加算
        _elapsedTime += Time.deltaTime;

        // 規定時間に達していた場合
        if (_elapsedTime > _updateTime)
        {
            MonsterRotation();
            _elapsedTime = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーが追従範囲に入ったらランダム移動をやめる
        if (other.gameObject == _followArea && this.enabled)
        {
            _monsterRandomWalk.enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // プレイヤーが追従範囲にいる間追従する
        if (other.gameObject == _followArea && this.enabled)
        {
            FollowPlayer();
            UpdateTime();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // プレイヤーが追従範囲から外れたらランダム移動を始める
        if (other.gameObject == _followArea && this.enabled)
        {
            _elapsedTime = 0f;
            _monsterRandomWalk.enabled = true;
        }
    }
}
