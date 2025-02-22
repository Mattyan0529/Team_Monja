using UnityEngine;

public class GhostFloat : MonoBehaviour
{
    // 揺れる振幅と速度を設定する
    [SerializeField] private float _amplitude = 0.5f; // 振幅
    [SerializeField] private float _frequency = 1f; // 周波数

    private Vector3 _startPos;

    void Start()
    {
        // 初期位置を保存する
        _startPos = transform.position;
    }

    void Update()
    {
        // Y軸方向の揺れを計算する
        float yOffset = Mathf.Sin(Time.time * _frequency) * _amplitude;

        // 新しい位置を設定する
        transform.position = new Vector3(_startPos.x, _startPos.y + yOffset, _startPos.z);
    }
}
