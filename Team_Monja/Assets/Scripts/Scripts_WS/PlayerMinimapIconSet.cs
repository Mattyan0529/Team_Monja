using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMinimapIconSet : MonoBehaviour
{
    private GameObject _player;

    // Update is called once per frame
    void Update()
    {
        // プレイヤーの位置にアイコンの位置を設定
        this.transform.position = new Vector3(_player.transform.position.x,
            _player.transform.position.y + 3,
            _player.transform.position.z);

        // プレイヤーの回転に合わせて、x軸を90度、z軸を180度に固定した回転を設定
        // y軸の回転に180度を加算して、プレイヤーの向きと真逆にならないようにする
        this.transform.rotation = Quaternion.Euler(90, _player.transform.rotation.eulerAngles.y + 180, 180);
    }

    // プレイヤーオブジェクトを取得するメソッド
    public void SetPlayer(GameObject player)
    {
        _player = player;
    }
}
