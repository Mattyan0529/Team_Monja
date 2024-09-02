using UnityEngine;

public class MoveCompass_KH : MonoBehaviour
{
    // 目的地となるオブジェクト
    [Header("ボスをいれてね")]
    [SerializeField]
    private GameObject _destinationObj = default;

    private GameObject _player;

    // 最大角度（ひっくり返って画面外に行かないように）
    private float _maxAngle = 70f;


    void Update()
    {
        UpdateOrientation();
    }


    public void SetPlayer(GameObject player)
    {
        _player = player;
    }

    /// <summary>
    /// 方位磁石の角度を変える
    /// </summary>
    private void UpdateOrientation()
    {
        // 自分の位置
        Vector3 myPos = _player.transform.position;
        // 目的地
        Vector3 target = _destinationObj.transform.position;

        // 方位磁石を向ける角度
        Vector3 direction = target-myPos;

        direction.y = 0f;

        if (direction == Vector3.zero) return;        // 動いていないときは処理をしない（_differenceが0だったらエラーになる）

        // Y軸の回転をZ軸の回転として反映する(プレイヤーの向いている方向も考慮する)
        Quaternion newDirection = Quaternion.LookRotation(direction, Vector3.up);

        float angle =_player.transform.rotation.eulerAngles.y - newDirection.eulerAngles.y;
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
