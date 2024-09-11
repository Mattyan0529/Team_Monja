using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    [SerializeField] private float lockOnRadius = 10f;
    private List<Transform> targets = new List<Transform>();
    private int currentTargetIndex = 0;
    public bool isLockedOn = false;
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

            // キャラクターをターゲット方向に向かせる
            Vector3 direction = currentTarget.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 4f);

            // ターゲットに向かってRayを飛ばす
            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, lockOnRadius))
            {
                // Rayが何かにヒットした場合
                if (hit.transform == currentTarget)
                {
                    Debug.Log("Ray hit the target!");
                }
                else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Building"))
                {
                    Debug.Log("Ray hit a building. Locking off.");
                    isLockedOn = false;
                    targets.Clear(); // ターゲットリストをクリア
                    _lockOnImage.SetActive(false); // ロックオンイメージを非表示
                    ResetRotation();
                }
                else
                {
                    Debug.Log("Ray hit something else.");
                }
            }

            // ターゲットが死んだ場合、リストから削除
            if (dead.IsDeadDecision())
            {
                targets.RemoveAt(currentTargetIndex);
                if (targets.Count > 0)
                {
                    currentTargetIndex = currentTargetIndex % targets.Count;
                }
                else
                {
                    isLockedOn = false;
                    ResetRotation();
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
            isLockedOn = false;
            ResetRotation();
            targets.Clear();
            _lockOnImage.SetActive(false);
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
}
