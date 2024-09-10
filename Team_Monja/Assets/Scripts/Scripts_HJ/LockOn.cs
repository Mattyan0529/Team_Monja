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

    private void Start()
    {
        // 子オブジェクトをインデックスで取得する (ここではメインカメラを想定)
        childObject = Camera.main.transform; // メインカメラとして初期化
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
                // ロックオンを開始し、ターゲットをリストに追加
                isLockedOn = true;
                FindTargets();
            }
            else
            {
                FindTargets();
                // ロックオン中なら次のターゲットに切り替える
                SwitchTarget(1);
            }
        }

        // ターゲットリストが空でない場合
        if (isLockedOn && targets.Count > 0)
        {
            Transform currentTarget = targets[currentTargetIndex]; // 現在ロックオンしているターゲット
            dead = currentTarget.GetComponent<CharacterDeadDecision_MT>();

            // キャラクターをターゲット方向に向かせる
            Vector3 direction = currentTarget.position - transform.position; // ターゲットまでの方向ベクトルを計算
            Quaternion rotation = Quaternion.LookRotation(direction); // その方向を向く回転を計算
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 4f); // スムーズに回転させる

            // ターゲットが死んだ場合、リストから削除して次のターゲットに切り替える
            if (dead.IsDeadDecision())
            {
                targets.RemoveAt(currentTargetIndex); // 現在のターゲットをリストから削除
                if (targets.Count > 0)
                {
                    currentTargetIndex = currentTargetIndex % targets.Count; // リストが空でない場合は次のターゲットへ
                }
                else
                {
                    isLockedOn = false; // リストが空ならロックオンを解除
                    ResetRotation();
                }
            }

            // ターゲットが存在する間、ロックオンイメージを追従させる
            Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(currentTarget.position); // ターゲットのスクリーン座標
            if (targetScreenPos.z > 0)
            {
                _lockOnImage.transform.position = targetScreenPos; // イメージをスクリーン座標に配置
                _lockOnImage.SetActive(true); // ロックオンイメージを表示
            }
            else
            {
                _lockOnImage.SetActive(false); // カメラ外にある場合は非表示
            }
        }
        else
        {
            _lockOnImage.SetActive(false); // ロックオン解除時、イメージを非表示
        }

        // ターゲットがいない場合はロックオンを解除
        if (targets.Count <= 0)
        {
            isLockedOn = false;
        }

        // ターゲットキャンセルボタンが押されたらロックオン解除
        if (Input.GetButtonDown("TargetCancel"))
        {
            isLockedOn = false;
            ResetRotation();
            targets.Clear(); // ターゲットリストをクリア
            _lockOnImage.SetActive(false); // ロックオンイメージを非表示
        }
    }

    // ターゲット候補を探す
    void FindTargets()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, lockOnRadius); // プレイヤー周辺のコライダーを取得
        Transform nearestTarget = null;
        float nearestDistance = Mathf.Infinity;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy") || hitCollider.CompareTag("Boss"))
            {
                if (!targets.Contains(hitCollider.transform))
                {
                    float distanceToTarget = Vector3.Distance(transform.position, hitCollider.transform.position); // プレイヤーとの距離を計算
                    if (distanceToTarget < nearestDistance)
                    {
                        nearestDistance = distanceToTarget;
                        nearestTarget = hitCollider.transform; // 最も近いターゲットを記録
                    }
                    targets.Add(hitCollider.transform); // ターゲットリストに追加
                }
            }
        }

        if (nearestTarget != null)
        {
            targets.Remove(nearestTarget); // 最も近いターゲットをリストの最初に移動
            targets.Insert(0, nearestTarget);
            currentTargetIndex = 0; // 最初のターゲットを現在のターゲットに設定
        }
    }

    // ターゲットを切り替える
    void SwitchTarget(int direction)
    {
        if (targets.Count == 0) return;

        currentTargetIndex += direction; // ターゲットインデックスを更新
        if (currentTargetIndex < 0)
        {
            currentTargetIndex = targets.Count - 1; // インデックスが負の場合、リストの最後に戻る
        }
        else if (currentTargetIndex >= targets.Count)
        {
            currentTargetIndex = 0; // インデックスが範囲外の場合、リストの最初に戻る
        }
    }

    // キャラクターの回転をリセットする
    void ResetRotation()
    {
        Vector3 newRotation = transform.localEulerAngles;
        newRotation.x = 0f; // X軸のローテーションを0に戻す
        transform.localEulerAngles = newRotation;
    }
}
