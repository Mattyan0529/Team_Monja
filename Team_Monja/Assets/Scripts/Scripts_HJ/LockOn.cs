using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    [Header("ロックオン設定")]
    [SerializeField] private float lockOnRadius = 10f; // ロックオン範囲の半径 (インスペクタで編集可能)
    private List<Transform> targets = new List<Transform>(); // ターゲット候補のリスト
    private int currentTargetIndex = 0; // 現在のターゲットのインデックス
    public bool isLockedOn = false; // ロックオン状態のフラグ
    private CharacterDeadDecision_MT dead;
    [SerializeField] private Transform childObject; // メインカメラ
    [SerializeField] private GameObject _lockOnImage; // ロックオンイメージ

    private void Start()
    {
        childObject = Camera.main.transform;
        _lockOnImage.SetActive(false);
        isLockedOn = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("TargetButton"))
        {
            if (!isLockedOn)
            {
                isLockedOn = true;
                FindTargets();
            }
            else
            {
                SwitchTarget(1);
            }
        }

        if (isLockedOn && targets.Count > 0)
        {
            Transform currentTarget = targets[currentTargetIndex];
            if (dead == null || dead.gameObject != currentTarget.gameObject)
            {
                dead = currentTarget.GetComponent<CharacterDeadDecision_MT>();
            }

            Vector3 direction = currentTarget.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 4f);

            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, lockOnRadius))
            {
                if (hit.transform == currentTarget)
                {
                    Debug.Log("Ray hit the target!");
                }
                else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Building"))
                {
                    Debug.Log("Ray hit a building. Locking off.");
                    CancelLockOn();
                }
                else
                {
                    Debug.Log("Ray hit something else.");
                }
            }

            if (Vector3.Distance(transform.position, currentTarget.position) > lockOnRadius)
            {
                Debug.Log("Target is out of range.");
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

            Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(currentTarget.position);
            bool isTargetOnScreen = targetScreenPos.z > 0;
            _lockOnImage.SetActive(isTargetOnScreen);
            if (isTargetOnScreen)
            {
                _lockOnImage.transform.position = targetScreenPos;
            }
        }
        else
        {
            _lockOnImage.SetActive(false);
        }

        if (targets.Count <= 0)
        {
            isLockedOn = false;
        }

        if (Input.GetButtonDown("TargetCancel"))
        {
            CancelLockOn();
        }

        if (isLockedOn)
        {
            UpdateTargetList();
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

    void UpdateTargetList()
    {
        for (int i = targets.Count - 1; i >= 0; i--)
        {
            Transform target = targets[i];
            if (Vector3.Distance(transform.position, target.position) > lockOnRadius)
            {
                targets.RemoveAt(i);
                if (currentTargetIndex >= targets.Count)
                {
                    currentTargetIndex = 0;
                }
            }
        }
    }

    void SwitchTarget(int direction)
    {
        if (targets.Count == 0) return;

        currentTargetIndex = (currentTargetIndex + direction) % targets.Count;
        if (currentTargetIndex < 0)
        {
            currentTargetIndex += targets.Count;
        }
    }

    void ResetRotation()
    {
        Vector3 newRotation = transform.localEulerAngles;
        newRotation.x = 0f;
        transform.localEulerAngles = newRotation;
    }

    // ロックオンをキャンセルするメソッド
   public  void CancelLockOn()
    {
        isLockedOn = false;
        ResetRotation();
        targets.Clear();
        _lockOnImage.SetActive(false);
    }
}
