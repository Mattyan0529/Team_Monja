using UnityEngine;

public class CameraBlock_TH : MonoBehaviour
{
    public Camera camera; // カメラの参照
    public float cameraDistance = 5.0f; // カメラとプレイヤーの距離
    public float smoothSpeed = 10.0f; // カメラのスムーズな動きのスピード
    public LayerMask collisionMask; // 衝突を検出するレイヤーマスク（壁など）

    private Vector3 desiredCameraPos; // カメラの目標位置
    private Vector3 smoothVelocity; // カメラのスムーズな動きを補助するための変数

    // カメラの最小高さを指定して、地面に埋まらないようにする
    public float cameraMinHeight = 1.0f;

    void LateUpdate()
    {
        if (camera == null)
        {
            Debug.LogWarning("カメラが割り当てられていません。");
            return;
        }

        // プレイヤーのTransformを取得
        Transform player = transform;

        // プレイヤーからカメラまでの方向を計算
        Vector3 directionToCamera = (camera.transform.position - player.position).normalized;

        // プレイヤーからカメラまでの通常の位置を計算
        desiredCameraPos = player.position - directionToCamera * cameraDistance;

        // 現在のカメラのY軸を一度保存
        float originalCameraHeight = desiredCameraPos.y;

        // プレイヤーからカメラに向けてレイキャストの方向ベクトルを修正
        Vector3 rayDirection = camera.transform.position - player.position;

        // レイキャストを可視化するために、Debug.DrawRayを追加
        Debug.DrawRay(player.position, rayDirection, Color.red);

        // プレイヤーからカメラに向けてレイを飛ばし、障害物があるかを確認
        RaycastHit hit;
        bool hitDetected = Physics.Raycast(player.position, rayDirection, out hit, rayDirection.magnitude, collisionMask);

        if (hitDetected)
        {
            // 障害物にヒットした場合、その手前にカメラを配置
            float distanceToHit = hit.distance;
            desiredCameraPos = player.position + rayDirection.normalized * (distanceToHit - 0.2f);

            // ヒットしたオブジェクトの名前をコンソールに出力
            Debug.Log("レイがヒットしたオブジェクト: " + hit.collider.gameObject.name);
        }
        else
        {
            // レイキャストがヒットしなかった場合の処理
            Debug.Log("レイが何にもヒットしませんでした。");
        }

        // カメラの高さは元の高さを維持する
        desiredCameraPos.y = Mathf.Max(desiredCameraPos.y, cameraMinHeight);

        // スムーズにカメラを目標位置に移動させる
        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, desiredCameraPos, ref smoothVelocity, 1 / smoothSpeed);
    }
}
