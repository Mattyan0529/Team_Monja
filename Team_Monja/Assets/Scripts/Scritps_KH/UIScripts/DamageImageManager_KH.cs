using UnityEngine;

public class DamageImageManager_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _residentScript = default;

    private PlayerManager_KH _playerManager = default;

    // ダメージの値を表示しておく時間
    private float _displayTime = 0.8f;
    private float _elapsedTime = 0f;

    private void Start()
    {
        _playerManager = _residentScript.GetComponent<PlayerManager_KH>();
    }

    void Update()
    {
        UpdateTime();
        transform.LookAt(_playerManager.Player.transform.Find("camera"));
        transform.Rotate(0f, 180f, 0f);
    }

    private void UpdateTime()
    {
        _elapsedTime += Time.deltaTime;

        if(_elapsedTime > _displayTime)
        {
            gameObject.SetActive(false);
            _elapsedTime = 0f;
        }
    }
}
