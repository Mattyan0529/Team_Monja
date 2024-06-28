using UnityEngine;

public class MoveCompass_KH : MonoBehaviour
{
    // 目的地となるオブジェクト
    [Header("ボスをいれてね")]
    [SerializeField]
    private GameObject _destinationObj = default;

    [SerializeField]
    private GameObject _residentScript = default;

    private PlayerManager_KH _playerManager = default;

    // 最大角度（ひっくり返って画面外に行かないように）
    private float _maxAngle = 70f;

    void Start()
    {
        _playerManager = _residentScript.GetComponent<PlayerManager_KH>();
    }

    void Update()
    {
        UpdateOrientation();
    }

    /// <summary>
    /// 方位磁石の角度を変える
    /// </summary>
    private void UpdateOrientation()
    {
        GameObject player = _playerManager.Player;

        // 自分の位置
        Vector3 myPos = player.transform.position;
        // 目的地
        Vector3 target = _destinationObj.transform.position;

        // 方位磁石を向ける角度
        Vector3 direction = target-myPos;

        direction.y = 0f;

        if (direction == Vector3.zero) return;        // 動いていないときは処理をしない（_differenceが0だったらエラーになる）

        // Y軸の回転をZ軸の回転として反映する(プレイヤーの向いている方向も考慮する)
        Quaternion newDirection = Quaternion.LookRotation(direction, Vector3.up);

        float angle = player.transform.rotation.eulerAngles.y - newDirection.eulerAngles.y;
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
