using UnityEngine;
using UnityEngine.UI;

public class MoveCompass_KH : MonoBehaviour
{
    // 目的地となるオブジェクト
    [Header("ボスをいれてね")]
    [SerializeField]
    private GameObject _destinationObj = default;

    [SerializeField]
    private GameObject _CompassImage = default;

    [SerializeField]
    private Toggle _rotationMapToggle;

    [SerializeField]
    private Scriptableobject_SM _settingsData; // ScriptableObjectの参照

    private GameObject _player;
    private Quaternion _initialPlayerRotation; // プレイヤーの初期回転

    private void Start()
    {
        if (_rotationMapToggle != null)
        {
            // トグルの初期状態をScriptableObjectの値で設定
            _rotationMapToggle.isOn = _settingsData.isMapRotationEnabled;

            // トグルの状態が変更されたときに呼び出されるメソッドを登録
            _rotationMapToggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        // ScriptableObjectの値をローカル変数に反映
        OnToggleValueChanged(_settingsData.isMapRotationEnabled);

        if (_player != null)
        {
            _initialPlayerRotation = _player.transform.rotation; // プレイヤーの初期回転を保存
        }
    }

    void Update()
    {
        UpdateOrientation();
    }

    /// <summary>
    /// トグルの値が切り替わったときに呼ばれるメソッド
    /// </summary>
    /// <param name="isOn"></param>
    private void OnToggleValueChanged(bool isOn)
    {
        _settingsData.isMapRotationEnabled = isOn; // ScriptableObjectの値を更新

        if (!isOn)
        {
            gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            _CompassImage.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }

    public void SetPlayer(GameObject player)
    {
        _player = player;
        _initialPlayerRotation = _player.transform.rotation; // プレイヤーが変更された時に初期回転を更新
    }

    /// <summary>
    /// ミニマップやアイコンを回転させる
    /// </summary>
    private void UpdateOrientation()
    {
        if (_player == null || _destinationObj == null) return;

        // プレイヤーの位置
        Vector3 myPos = _player.transform.position;
        // 目的地
        Vector3 target = _destinationObj.transform.position;

        // 方位磁石を向ける角度
        Vector3 direction = target - myPos;
        direction.y = 0f;

        if (direction == Vector3.zero) return; // 動いていないときは処理をしない

        // Y軸の回転をZ軸の回転として反映する
        Quaternion newDirection = Quaternion.LookRotation(direction, Vector3.up);

        float angle;

        // カメラの回転を基準にするかどうか
        if (_settingsData.isMapRotationEnabled)
        {
            // プレイヤーの初期回転を基準にカメラのY軸の回転を調整
            angle = Camera.main.transform.rotation.eulerAngles.y - _initialPlayerRotation.eulerAngles.y - newDirection.eulerAngles.y;
            _CompassImage.transform.parent = gameObject.transform;
        }
        else
        {
            // カメラの回転を考慮しない場合
            angle = newDirection.eulerAngles.y;
            _CompassImage.transform.parent = gameObject.transform.parent;
        }

        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
