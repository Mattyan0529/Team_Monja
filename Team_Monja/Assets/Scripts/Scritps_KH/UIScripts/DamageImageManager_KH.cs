using UnityEditor;
using UnityEngine;

public class DamageImageManager_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _residentScript = default;

    [SerializeField]
    private GameObject _camera = default;

    private PlayerManager_KH _playerManager = default;

    // ダメージの値を表示しておく時間
    private float _displayTime = 0.8f;
    // ダメージの表示を上下に動かす時間（往復分）
    private float _moveTime = 0.1f;
    private float _elapsedTime = 0f;

    private float _speed = 20f;

    private void Start()
    {
        _playerManager = _residentScript.GetComponent<PlayerManager_KH>();
    }

    void Update()
    {
        UpdateTime();
        transform.LookAt(_camera.transform);
        transform.Rotate(0f, 180f, 0f);
    }

    private void UpdateTime()
    {
        _elapsedTime += Time.deltaTime;

        if(_moveTime / 2 > _elapsedTime)
        {
            UpImage();
        }
        else if (_moveTime > _elapsedTime)
        {
            DownImage();
        }
        else if(_elapsedTime > _displayTime)
        {
            gameObject.SetActive(false);
            _elapsedTime = 0f;
        }
    }

    private void UpImage()
    {
        gameObject.transform.position += Vector3.up * Time.deltaTime * _speed;
    }

    private void DownImage()
    {
        gameObject.transform.position += Vector3.down * Time.deltaTime * _speed;
    }
}
