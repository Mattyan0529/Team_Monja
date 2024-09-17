using System.Runtime.CompilerServices;
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

    private bool _isRotation = false;

    private GameObject _player;

    private void Start()
    {
        if (_rotationMapToggle != null)
        {
            // トグルの状態が変更されたときに呼び出されるメソッドを登録
            _rotationMapToggle.onValueChanged.AddListener(OnToggleValueChanged);
            _isRotation = _rotationMapToggle.isOn; // トグルの初期状態をフラグに設定
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
        _isRotation = isOn;

        if (!_isRotation)
        {
            gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            _CompassImage.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }


    public void SetPlayer(GameObject player)
    {
        _player = player;
    }

    /// <summary>
    /// ミニマップやアイコンを回転させる
    /// </summary>
    private void UpdateOrientation()
    {
        // 自分の位置
        Vector3 myPos = _player.transform.position;
        // 目的地
        Vector3 target = _destinationObj.transform.position;

        // 方位磁石を向ける角度
        Vector3 direction = target-myPos;

        direction.y = 0f;

        if (direction == Vector3.zero) return;        // 動いていないときは処理をしない（_differenceが0だったらエラーになる）

        // Y軸の回転をZ軸の回転として反映する
        Quaternion newDirection = Quaternion.LookRotation(direction, Vector3.up);

        float angle;

        // プレイヤーの視点に依存して回転するか
        if (_isRotation)
        {
            angle = _player.transform.rotation.eulerAngles.y - newDirection.eulerAngles.y;
            _CompassImage.transform.parent = gameObject.transform;
        }
        else
        {
            angle = newDirection.eulerAngles.y;
            _CompassImage.transform.parent = gameObject.transform.parent;
        }

        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
