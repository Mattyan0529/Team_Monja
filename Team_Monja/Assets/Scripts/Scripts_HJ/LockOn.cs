using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    [SerializeField] float lockOnRadius = 10f; // ロックオン範囲の半径
    private List<Transform> targets = new List<Transform>(); // ターゲット候補のリスト
    private int currentTargetIndex = 0; // 現在のターゲットのインデックス
    private bool isLockedOn = false; // ロックオン状態のフラグ
    [SerializeField] CharacterDeadDecision_MT dead;

    void Update()
    {
        // "TargetButton" ボタンが押された瞬間を検出
        if (Input.GetButtonDown("TargetButton"))
        {
            if (!isLockedOn)
            {
                // ロックオンを開始する場合、ターゲットをリストに追加して最初のターゲットをロックオン
                isLockedOn = true;
                FindTargets();
            }
            else
            {
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
                }
            }

        }


        if (Input.GetButtonDown("TargetCancel"))
        {
            isLockedOn = false;
        }
    }

    void FindTargets()
    {
        // プレイヤーの位置を中心に、指定した半径内にあるすべてのコライダーを取得
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, lockOnRadius);

        // 現在のターゲットリストをクリア
        targets.Clear();

        // 取得したコライダーの中から、「Enemy」タグを持つものをターゲットリストに追加
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                // ターゲットリストに敵のTransformを追加
                targets.Add(hitCollider.transform);
            }
        }

        // ターゲットリストが空でなければ、最初のターゲットにロックオン
        if (targets.Count > 0)
        {
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


