using UnityEngine;

public class MoveAndRotateObject : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform; // 移動先と回転先のTransform
    [SerializeField] private float _moveDuration = 3f; // 移動と回転にかかる秒数
    [SerializeField] private ParticleSystem[] _particleSystems; // 複数のパーティクルシステム
    [SerializeField] private float _startParticleSize = 1f; // 初期のパーティクルサイズ
    [SerializeField] private float _endParticleSize = 3f; // 最終的なパーティクルサイズ

    private Vector3 _startPosition; // 開始位置
    private Quaternion _startRotation; // 開始時の回転
    private Vector3 _startScale; // 開始時のスケール
    private float _elapsedTime; // 経過時間

    void Start()
    {
        // 開始位置、回転、スケールを保存
        _startPosition = transform.position;
        _startRotation = transform.rotation;
        _startScale = transform.localScale;
        _elapsedTime = 0f;
    }

    void Update()
    {
        // 経過時間を増加
        _elapsedTime += Time.deltaTime;

        // 進捗を計算
        float progress = Mathf.Clamp01(_elapsedTime / _moveDuration);

        // 位置の補間
        transform.position = Vector3.Lerp(_startPosition, _targetTransform.position, progress);

        // 回転の補間
        transform.rotation = Quaternion.Lerp(_startRotation, _targetTransform.rotation, progress);

        // スケールの補間
        transform.localScale = Vector3.Lerp(_startScale, _targetTransform.localScale, progress);

        // パーティクルのサイズも進捗に応じて変化させる
        for (int i = 0; i < _particleSystems.Length; i++)
        {
            var renderer = _particleSystems[i].GetComponent<ParticleSystemRenderer>();
            if (renderer != null)
            {
                float maxSize = Mathf.Lerp(_startParticleSize, _endParticleSize, progress);

                // 二つ目のパーティクルシステムにはサイズを小さく
                if (i == 1)
                {
                    maxSize *= 0.75f;
                }

                renderer.maxParticleSize = maxSize;
            }
        }
    }
}
