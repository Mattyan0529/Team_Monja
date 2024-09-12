using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    [SerializeField] float lockOnRadius = 10f; // ロックオン範囲の半径
    private List<Transform> targets = new List<Transform>(); // ターゲット候補のリスト
    private int currentTargetIndex = 0; // 現在のターゲットのインデックス
    public bool isLockedOn = false; // ロックオン状態のフラグ
    private CharacterDeadDecision_MT dead;
    [SerializeField] private Transform childObject; // 子オブジェクト（メインカメラ）
    [SerializeField] private GameObject _lockOnImage; // ロックオンしたときに画面に表示するイメージ
    [SerializeField] private LayerMask obstacleLayerMask; // 障害物のレイヤーマスク

    private Camera _camera; // メインカメラ

    private void Start()
    {
        _camera = Camera.main; // メインカメラの取得
        _lockOnImage.SetActive(false); // 初期化時にロックオンイメージを非表示に
        isLockedOn = false;
    }

    void Update()
    {
        // "TargetButton" ボタンが押された瞬間を検出
        if (Input.GetButtonDown("TargetButton"))
        {
            if (!isLockedOn)
            {
                isLockedOn = true;
                FindTargets();
            }
            else
            {
                FindTargets();
                SwitchTarget(1);
            }
        }

        if (isLockedOn && targets.Count > 0)
        {
            Transform currentTarget = targets[currentTargetIndex]; // 現在ロックオンしているターゲット
            dead = currentTarget.GetComponent<CharacterDeadDecision_MT>();

            Vector3 direction = currentTarget.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 4f); // プレイヤーがターゲットを向く

            // ターゲットとプレイヤーの間に障害物があるか確認
            if (IsObstacleBetween(transform.position, currentTarget))
            {
                CancelLockOn(); // 障害物がある場合にロックオンを解除
                return;
            }

            if (dead != null && dead.IsDeadDecision())
            {
                targets.RemoveAt(currentTargetIndex);
                if (targets.Count > 0)
                {
                    currentTargetIndex = currentTargetIndex % targets.Count;
                }
                else
                {
                    CancelLockOn();
                }
            }

            Vector3 targetScreenPos = _camera.WorldToScreenPoint(currentTarget.position);
            if (targetScreenPos.z > 0)
            {
                _lockOnImage.transform.position = targetScreenPos;
                _lockOnImage.SetActive(true);
            }
            else
            {
                CancelLockOn(); // ターゲットが見えなくなった場合にロックオンを解除
            }
        }
        else
        {
            _lockOnImage.SetActive(false); // ターゲットがいない場合はイメージを非表示に
        }

        if (Input.GetButtonDown("TargetCancel"))
        {
            CancelLockOn();
        }
    }

    void FindTargets()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, lockOnRadius);
        Transform nearestTarget = null;
        float nearestDistance = Mathf.Infinity;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy") || hitCollider.CompareTag("Boss"))
            {
                if (!targets.Contains(hitCollider.transform))
                {
                    float distanceToTarget = Vector3.Distance(transform.position, hitCollider.transform.position);
                    if (distanceToTarget < nearestDistance)
                    {
                        nearestDistance = distanceToTarget;
                        nearestTarget = hitCollider.transform;
                    }
                    targets.Add(hitCollider.transform);
                }
            }
        }

        if (nearestTarget != null)
        {
            targets.Remove(nearestTarget);
            targets.Insert(0, nearestTarget);
            currentTargetIndex = 0;
        }
    }

    void SwitchTarget(int direction)
    {
        if (targets.Count == 0) return;

        currentTargetIndex += direction;
        if (currentTargetIndex < 0)
        {
            currentTargetIndex = targets.Count - 1;
        }
        else if (currentTargetIndex >= targets.Count)
        {
            currentTargetIndex = 0;
        }
    }

    public void CancelLockOn()
    {
        isLockedOn = false;
        targets.Clear();
        _lockOnImage.SetActive(false);
        ResetRotation();
    }

    void ResetRotation()
    {
        Vector3 newRotation = transform.localEulerAngles;
        newRotation.x = 0f;
        transform.localEulerAngles = newRotation;
    }

    bool IsObstacleBetween(Vector3 fromPosition, Transform target)
    {
        // プレイヤーとターゲットの間に障害物がないか確認するRaycast
        Vector3 directionToTarget = (target.position - fromPosition).normalized;
        float distanceToTarget = Vector3.Distance(fromPosition, target.position);

        // Raycastを使って障害物の判定
        if (Physics.Raycast(fromPosition, directionToTarget, distanceToTarget, obstacleLayerMask))
        {
            return true; // 障害物がある
        }

        return false; // 障害物がない
    }
}
