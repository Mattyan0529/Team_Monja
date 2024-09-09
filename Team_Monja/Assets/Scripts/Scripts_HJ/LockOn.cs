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
    [SerializeField] private Transform childObject; // 子オブジェクトの参照を事前にセット

    private void Start()
    {
        childObject = transform.GetChild(0); // 子オブジェクトをインデックスで取得する (インデックスを変更可能)
        isLockedOn = false;
    }

    void Update()
    {
        // "TargetButton" ボタンが押された瞬間を検出
        if (Input.GetButtonDown("TargetButton"))
        {
            //FindTargets();
            if (!isLockedOn)
            {
                // ロックオンを開始する場合、ターゲットをリストに追加して最初のターゲットをロックオン
                isLockedOn = true;
                FindTargets();
            }
            else
            {
                FindTargets();
                // ロックオン中の場合、次のターゲットに切り替える
                SwitchTarget(1);
            }
        }

        // ターゲットリストが空でない場合
        if (isLockedOn && targets.Count > 0)
        {
            // 現在ロックオンしているターゲットのTransformを取得
            Transform currentTarget = targets[currentTargetIndex];

            // 現在ターゲットしているオブジェクトのスクリプトを取得
           dead = currentTarget.GetComponent<CharacterDeadDecision_MT>();

            // キャラクターが現在のターゲットの方向を向くようにする
            Vector3 direction = currentTarget.position - transform.position; // ターゲットまでの方向ベクトルを計算
            Quaternion rotation = Quaternion.LookRotation(direction); // その方向を向く回転を計算

            // 現在の回転からターゲット方向の回転へ、スムーズに回転させる
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 4f);

            if (dead.IsDeadDecision())
            {
                targets.RemoveAt(currentTargetIndex); // 現在のターゲットをリストから削除

                // リストがまだ空でない場合、次のターゲットをロックオン
                if (targets.Count > 0)
                {
                    currentTargetIndex = currentTargetIndex % targets.Count; // 新しいインデックスがリストの範囲内に収まるように調整
                }
                else
                {
                    isLockedOn = false; // リストが空ならロックオンを解除
                    Vector3 newRotation = transform.localEulerAngles;
                    newRotation.x = 0f; // X軸のローテーションを0に戻す
                    transform.localEulerAngles = newRotation;

                }
            }

        }
        if(targets.Count <=0)
        {
            isLockedOn = false;
        }


        if (Input.GetButtonDown("TargetCancel"))
        {
            isLockedOn = false;
            Vector3 newRotation = transform.localEulerAngles;
            newRotation.x = 0f; // X軸のローテーションを0に戻す
            transform.localEulerAngles = newRotation;
            // 現在のターゲットリストをクリア
            targets.Clear();

        }

        Debug.Log(targets.Count);
    }

    void FindTargets()
    {
        // プレイヤーの位置を中心に、指定した半径内にあるすべてのコライダーを取得
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, lockOnRadius);

        Transform nearestTarget = null;
        float nearestDistance = Mathf.Infinity;

        // 取得したコライダーの中から、「Enemy」タグを持つものをターゲットリストに追加
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy") || hitCollider.CompareTag("Boss"))
            {
                // ターゲットリストにすでに含まれていないか確認
                if (!targets.Contains(hitCollider.transform))
                {
                    // プレイヤーとの距離を計算
                    float distanceToTarget = Vector3.Distance(transform.position, hitCollider.transform.position);

                    // 一番近い敵を更新
                    if (distanceToTarget < nearestDistance)
                    {
                        nearestDistance = distanceToTarget;
                        nearestTarget = hitCollider.transform;
                    }

                    // ターゲットリストに敵のTransformを追加
                    targets.Add(hitCollider.transform);
                }
            }
        }

        // 一番近いターゲットが存在する場合
        if (nearestTarget != null)
        {
            // 一番近いターゲットをリストの最初に移動
            targets.Remove(nearestTarget);
            targets.Insert(0, nearestTarget);

            // 現在のターゲットインデックスを0（最初のターゲット）に設定
            currentTargetIndex = 0;
        }
    }

    void SwitchTarget(int direction)
    {
        // ターゲットが1つもない場合は処理を終了
        if (targets.Count == 0) return;

        // 現在のターゲットインデックスを、方向に応じて更新
        currentTargetIndex += direction;

        // インデックスがリストの範囲を超えないように調整
        // インデックスが負の値になった場合（範囲外）、リストの最後のターゲットにループする
        if (currentTargetIndex < 0)
        {
            currentTargetIndex = targets.Count - 1;
        }
        // インデックスがリストのサイズ以上になった場合（範囲外）、リストの最初のターゲットにループする
        else if (currentTargetIndex >= targets.Count)
        {
            currentTargetIndex = 0;
        }
    }


}


