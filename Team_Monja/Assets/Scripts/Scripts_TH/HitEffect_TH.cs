
using UnityEngine;
using System.Collections;

public class TriggerHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] _HitEffects; // アクティブにしたいオブジェクト
    private GameObject _objectToActivate; // アクティブにしたいオブジェクト
    [SerializeField] private float _activationDuration = 3f; // オブジェクトがアクティブでいる時間（秒）
    
    [SerializeField] private CapsuleCollider _capsuleCollider; // インスペクター上で設定するCapsuleCollider
    private Vector3 _debugClosestPoint = Vector3.zero; // デバッグ用の変数

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NormalHit") || other.CompareTag("SlashHit") || other.CompareTag("HeartHit") || other.CompareTag("ThunderHit"))    
        {
            print("攻撃当たった: " + other);

            if(other.CompareTag("NormalHit"))
            {
                _objectToActivate = _HitEffects[0];
            }
            if (other.CompareTag("SlashHit"))
            {
                _objectToActivate = _HitEffects[1];
            }
            if (other.CompareTag("HeartHit"))
            {
                _objectToActivate = _HitEffects[2];
            }
            if (other.CompareTag("ThunderHit"))
            {
                _objectToActivate = _HitEffects[4];
            }

            // コリジョンしたオブジェクトの親を取得
            Transform parentTransform = other.transform.parent;

            if (parentTransform != null)
            {
                // 親オブジェクトとの距離を基にカプセルの側面の最も近い点を取得
                _debugClosestPoint = GetClosestPointOnSideOfCapsule(parentTransform.position);

                if (_debugClosestPoint != Vector3.zero)
                {
                    // デバッグ用に計算結果を出力
                    Debug.Log("最も近いカプセルの側面の点: " + _debugClosestPoint);

                    // その座標にオブジェクトを移動してアクティブ化
                    ActivateObjectAtPosition(_debugClosestPoint);
                    StartCoroutine(DeactivateAfterDelay(_activationDuration));
                }
            }
            else
            {
                Debug.Log("親オブジェクトがありません");
            }
        }
    }

    private Vector3 GetClosestPointOnSideOfCapsule(Vector3 targetPosition)
    {
        if (_capsuleCollider != null)
        {
            // カプセルコライダーの高さ、半径、ローカル空間での中心を取得
            float height = _capsuleCollider.height;
            float radius = _capsuleCollider.radius;
            Vector3 center = _capsuleCollider.center;

            // ローカル座標系からワールド座標系に変換
            Vector3 capsuleCenter = _capsuleCollider.transform.TransformPoint(center);

            // カプセルの軸の方向（ローカルのY軸に沿っていると仮定）
            Vector3 upDirection = _capsuleCollider.transform.up;

            // カプセルの中心軸に沿った最も近い点を求める（高さ方向に制限）
            Vector3 toTarget = targetPosition - capsuleCenter;
            Vector3 projection = Vector3.Project(toTarget, upDirection);

            // 高さ方向の制限を設定（カプセルの上下限を計算）
            float halfHeight = height;
            Vector3 closestPointOnAxis = capsuleCenter + Vector3.ClampMagnitude(projection, halfHeight);

            // デバッグ用に軸方向の点をログに表示
            Debug.Log("カプセルの中心軸に沿った最も近い点: " + closestPointOnAxis);

            // カプセルの側面上で最も近い点を計算するため、カプセルの中心軸からの距離を半径に制限
            Vector3 directionToTarget = (targetPosition - closestPointOnAxis).normalized;
            Vector3 closestPointOnSide = closestPointOnAxis + directionToTarget * radius;

            // デバッグ用に側面の最も近い点をログに表示
            Debug.Log("カプセルの側面上で最も近い点: " + closestPointOnSide);

            return closestPointOnSide;
        }
        Debug.LogError("カプセルコライダーが見つからなかった");
        return Vector3.zero; // カプセルコライダーが見つからなかった場合
    }


    private void ActivateObjectAtPosition(Vector3 position)
    {
        if (_objectToActivate != null)
        {
            Debug.Log("オブジェクトの位置を " + position + " に移動");
            _objectToActivate.transform.position = position;
            _objectToActivate.SetActive(true);
        }
    }

    private IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (_objectToActivate != null)
        {
            _objectToActivate.SetActive(false);
            Debug.Log("オブジェクトを非アクティブ化");
        }
    }

    // シーンビューにデバッグ情報を描画
    void OnDrawGizmos()
    {
        if (_debugClosestPoint != Vector3.zero)
        {
            // カプセルの最も近い側面の点を赤い球で表示
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_debugClosestPoint, 0.1f);
        }
    }
}
