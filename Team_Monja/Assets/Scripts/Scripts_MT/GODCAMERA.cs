using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GODCAMERA : MonoBehaviour
{

    [SerializeField] private float _moveSpeed = 10f;  // カメラの移動速度
    [SerializeField] private float _rotateSpeed = 100f;  // カメラの回転速度
    [SerializeField] private float _zoomSpeed = 10f;  // カメラのズーム速度
    [SerializeField] private float _minZoom = 5f;  // 最小ズーム距離
    [SerializeField] private float _maxZoom = 100f;  // 最大ズーム距離
    [SerializeField] private float _verticalSpeed = 10f;  // 上下移動の速度

    private float _currentZoom = 10f;

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    // カメラの移動処理
    private void HandleMovement()
    {
        // 前後の移動はカメラのforward方向に基づく
        float moveZ = Input.GetAxis("Vertical");
        // 左右の移動はカメラのright方向に基づく
        float moveX = Input.GetAxis("Horizontal");

        // Qキーで上昇、Eキーで下降
        float moveY = 0f;
        if (Input.GetKey(KeyCode.Q))
        {
            moveY = _verticalSpeed * Time.deltaTime;  // 上昇
        }
        else if (Input.GetKey(KeyCode.E))
        {
            moveY = -_verticalSpeed * Time.deltaTime;  // 下降
        }

        // カメラのforwardとright方向に沿った移動ベクトルを計算
        Vector3 move = (transform.forward * moveZ + transform.right * moveX) * _moveSpeed * Time.deltaTime;

        // 上下移動を追加
        move.y += moveY;

        // カメラを移動
        transform.position += move;
    }

    // カメラの回転処理
    private void HandleRotation()
    {
        if (Input.GetMouseButton(1)) // 右クリックを押している間、カメラを回転
        {
            float rotateX = Input.GetAxis("Mouse X") * _rotateSpeed * Time.deltaTime;
            float rotateY = Input.GetAxis("Mouse Y") * _rotateSpeed * Time.deltaTime;

            // 水平方向の回転（カメラのY軸周りの回転）
            transform.Rotate(Vector3.up, rotateX, Space.World);

            // 垂直方向の回転（カメラのローカルX軸周りの回転）
            transform.Rotate(Vector3.right, -rotateY, Space.Self);
        }
    }

    // カメラのズーム処理
    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        _currentZoom -= scroll * _zoomSpeed;
        _currentZoom = Mathf.Clamp(_currentZoom, _minZoom, _maxZoom);
        Camera.main.fieldOfView = _currentZoom;
    }
}
