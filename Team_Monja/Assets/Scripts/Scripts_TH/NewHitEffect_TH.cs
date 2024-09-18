//using UnityEngine;
//using System.Collections;

//public class HitEffect2_TH : MonoBehaviour
//{
//    [SerializeField] private GameObject[] _HitEffects; // アクティブにしたいエフェクトオブジェクトの配列
//    [SerializeField] private float _activationDuration = 3f; // アクティブにする時間
//    [SerializeField] private CapsuleCollider _capsuleCollider; // Inspectorで設定するCapsuleCollider

//    private Vector3 _debugClosestPoint = Vector3.zero; // デバッグ用変数
//    private GameObject _currentObjectToActivate; // 今回のトリガーに対応するオブジェクト
//    private StatusManager_MT _StatusManager;

//    private void Start()
//    {
//        _StatusManager = GetComponent<StatusManager_MT>();
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        //子オブジェクトにつければ解決するかも

//        if ((other.CompareTag("NormalHit") || other.CompareTag("SlashHit") || other.CompareTag("HeartHit") || other.CompareTag("ThunderHit")) && _StatusManager.HP > 0 )
//        {
//            if (gameObject.CompareTag("Enemy") && other.gameObject.transform.parent.CompareTag("Enemy"))
//            {
//                return;
//            }
//            else
//            {
//                Debug.Log(gameObject.name + other.name);

//                // エフェクトを選択
//                if (other.CompareTag("NormalHit"))
//                {
//                    _currentObjectToActivate = _HitEffects[0]; // NormalHit用のエフェクト
//                }
//                else if (other.CompareTag("SlashHit"))
//                {
//                    _currentObjectToActivate = _HitEffects[1]; // SlashHit用のエフェクト
//                }
//                else if (other.CompareTag("HeartHit"))
//                {
//                    _currentObjectToActivate = _HitEffects[2]; // HeartHit用のエフェクト
//                }
//                else if (other.CompareTag("ThunderHit"))
//                {
//                    _currentObjectToActivate = _HitEffects[3]; // ThunderHit用のエフェクト
//                }

//                // 親オブジェクトを取得し、CapsuleCollider上の最も近い位置にエフェクトをアクティブ化
//                Transform parentTransform = other.transform.parent;
//                if (parentTransform != null)
//                {
//                    _debugClosestPoint = GetClosestPointOnSideOfCapsule(parentTransform.position);
//                    if (_debugClosestPoint != Vector3.zero)
//                    {
//                        ActivateObjectAtPosition(_debugClosestPoint);
//                        StartCoroutine(DeactivateAfterDelay(_activationDuration));
//                    }
//                }
//                else
//                {
//                    Debug.Log("親オブジェクトがありません");
//                }
//            }
//        }
//    }

//    private Vector3 GetClosestPointOnSideOfCapsule(Vector3 targetPosition)
//    {
//        if (_capsuleCollider != null)
//        {
//            // カプセルコライダーの高さと半径を取得
//            float height = _capsuleCollider.height;
//            float radius = _capsuleCollider.radius;
//            // カプセルコライダーの中心座標をワールド座標に変換
//            Vector3 center = _capsuleCollider.center;
//            Vector3 capsuleCenter = _capsuleCollider.transform.TransformPoint(center);
//            // カプセルコライダーの上方向ベクトルを取得
//            Vector3 upDirection = _capsuleCollider.transform.up;
//            // ターゲット位置へのベクトルを計算
//            Vector3 toTarget = targetPosition - capsuleCenter;
//            // 上方向への射影ベクトルを計算
//            Vector3 projection = Vector3.Project(toTarget, upDirection);
//            // カプセルの半分の高さを計算
//            float halfHeight = height / 2;
//            // カプセルの軸上で最も近い点を計算
//            Vector3 closestPointOnAxis = capsuleCenter + Vector3.ClampMagnitude(projection, halfHeight);
//            // 軸上の最も近い点からターゲットへの方向を正規化
//            Vector3 directionToTarget = (targetPosition - closestPointOnAxis).normalized;
//            // カプセルの側面上でターゲットに最も近い点を返す
//            return closestPointOnAxis + directionToTarget * radius;
//        }

//        // カプセルコライダーが見つからない場合のエラーログ
//        Debug.LogError("カプセルコライダーが見つかりません");
//        return Vector3.zero;
//    }

//    private void ActivateObjectAtPosition(Vector3 position)
//    {
//        if (_currentObjectToActivate != null)
//        {
//            _currentObjectToActivate.transform.position = position;
//            _currentObjectToActivate.SetActive(true);
//        }
//    }

//    private IEnumerator DeactivateAfterDelay(float delay)
//    {
//        yield return new WaitForSeconds(delay);
//        if (_currentObjectToActivate != null)
//        {
//            _currentObjectToActivate.SetActive(false);
//        }
//    }
//}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HitEffect2_TH : MonoBehaviour
{
    [SerializeField] private GameObject[] _HitEffects; // アクティブにしたいエフェクトオブジェクトの配列
    [SerializeField] private float _activationDuration = 3f; // アクティブにする時間
    [SerializeField] private CapsuleCollider _capsuleCollider; // Inspectorで設定するCapsuleCollider

    private Vector3 _debugClosestPoint = Vector3.zero; // デバッグ用変数
    private GameObject _currentObjectToActivate; // 今回のトリガーに対応するオブジェクト
    private StatusManager_MT _StatusManager;
    private Dictionary<string, GameObject> _hitEffectMap; // タグとエフェクトのマッピング用
    private bool _hasTriggeredAtZeroHP = false; // HPがゼロのときにエフェクトが表示されたかを確認するフラグ

    private void Start()
    {
        _StatusManager = GetComponent<StatusManager_MT>();

        // 各タグに対応するエフェクトをマッピング
        _hitEffectMap = new Dictionary<string, GameObject>()
        {
            { "NormalHit", _HitEffects[0] },
            { "SlashHit", _HitEffects[1] },
            { "HeartHit", _HitEffects[2] },
            { "ThunderHit", _HitEffects[3] }
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        // 自分自身の子オブジェクトまたは自身である場合は無視
        if (other.transform.IsChildOf(transform))
        {
            return;
        }

        // HPが0以下の場合でも、一回だけエフェクトを発生させる
        if (_StatusManager.HP <= 0 && _hasTriggeredAtZeroHP)
        {
            return; // すでに0HP時のエフェクトが発生した場合は何もしない
        }

        // タグに対応するエフェクトが存在するか確認
        if (_hitEffectMap.ContainsKey(other.tag) && !IsSameEnemy(other))
        {
            _currentObjectToActivate = _hitEffectMap[other.tag];

            // 親オブジェクトを取得し、CapsuleCollider上の最も近い位置にエフェクトをアクティブ化
            Transform parentTransform = other.transform.parent;
            if (parentTransform != null)
            {
                _debugClosestPoint = GetClosestPointOnSideOfCapsule(parentTransform.position);
                if (_debugClosestPoint != Vector3.zero)
                {
                    ActivateObjectAtPosition(_debugClosestPoint);
                    StartCoroutine(DeactivateAfterDelay(_activationDuration));

                    // HPが0の場合、一度だけフラグを立てる
                    if (_StatusManager.HP <= 0)
                    {
                        _hasTriggeredAtZeroHP = true;
                    }
                }
            }
            else
            {
                Debug.LogWarning("親オブジェクトが見つかりません: " + other.name);
            }
        }
    }

    // 同じエネミー同士のヒットを無視
    private bool IsSameEnemy(Collider other)
    {
        return gameObject.CompareTag("Enemy") && other.gameObject.transform.parent.CompareTag("Enemy");
    }

    private Vector3 GetClosestPointOnSideOfCapsule(Vector3 targetPosition)
    {
        if (_capsuleCollider == null)
        {
            Debug.LogError("カプセルコライダーが見つかりません");
            return Vector3.zero;
        }

        // カプセルコライダーのプロパティを取得
        float height = _capsuleCollider.height;
        float radius = _capsuleCollider.radius;
        Vector3 center = _capsuleCollider.center;
        Vector3 capsuleCenter = _capsuleCollider.transform.TransformPoint(center);
        Vector3 upDirection = _capsuleCollider.transform.up;

        // ターゲットへのベクトルを計算
        Vector3 toTarget = targetPosition - capsuleCenter;
        Vector3 projection = Vector3.Project(toTarget, upDirection);
        float halfHeight = height / 2;

        // 軸上の最も近い点を計算
        Vector3 closestPointOnAxis = capsuleCenter + Vector3.ClampMagnitude(projection, halfHeight);
        Vector3 directionToTarget = (targetPosition - closestPointOnAxis).normalized;

        // カプセルの側面上で最も近い点を返す
        return closestPointOnAxis + directionToTarget * radius;
    }

    private void ActivateObjectAtPosition(Vector3 position)
    {
        if (_currentObjectToActivate != null)
        {
            _currentObjectToActivate.transform.position = position;
            _currentObjectToActivate.SetActive(true);
        }
        else
        {
            Debug.LogWarning("アクティベートするオブジェクトが設定されていません");
        }
    }

    private IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (_currentObjectToActivate != null)
        {
            _currentObjectToActivate.SetActive(false);
        }
    }
}
