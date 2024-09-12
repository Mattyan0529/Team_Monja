using UnityEngine;

public class DropDawnSoul_TH : MonoBehaviour
{
    [SerializeField]
    private GameObject _particleSystemObject;  // パーティクルシステムのGameObject

    private float _rotateSpeed = 1.5f;
    private float _pingPongValue = 0.3f;
    private float _nowPositionY = default;

    private CharacterDeadDecision_MT characterDeadDecision;

    void Start()
    {
        // 親オブジェクトから取得
        characterDeadDecision = GetComponentInParent<CharacterDeadDecision_MT>();

        _nowPositionY = transform.position.y;

        // 初期状態でパーティクルシステムを非表示にしておく
        if (_particleSystemObject != null)
        {
            _particleSystemObject.SetActive(false);
        }
    }

    void Update()
    {
        if (characterDeadDecision.IsDeadDecision())
        {
            if (!CompareTag("Player"))
            {
                ToggleParticleSystem(true);  // 死亡状態でパーティクルシステムを表示
            }
        }
        else
        {
            ToggleParticleSystem(false); // 生存状態でパーティクルシステムを非表示
        }
    }
    private void ToggleParticleSystem(bool isActive)
    {
        if (_particleSystemObject != null)
        {
            _particleSystemObject.SetActive(isActive);
        }
    }
}