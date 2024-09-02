using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GODCAMERA : MonoBehaviour
{

    [SerializeField] private float _moveSpeed = 10f;  // �J�����̈ړ����x
    [SerializeField] private float _rotateSpeed = 100f;  // �J�����̉�]���x
    [SerializeField] private float _zoomSpeed = 10f;  // �J�����̃Y�[�����x
    [SerializeField] private float _minZoom = 5f;  // �ŏ��Y�[������
    [SerializeField] private float _maxZoom = 100f;  // �ő�Y�[������
    [SerializeField] private float _verticalSpeed = 10f;  // �㉺�ړ��̑��x

    private float _currentZoom = 10f;

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    // �J�����̈ړ�����
    private void HandleMovement()
    {
        // �O��̈ړ��̓J������forward�����Ɋ�Â�
        float moveZ = Input.GetAxis("Vertical");
        // ���E�̈ړ��̓J������right�����Ɋ�Â�
        float moveX = Input.GetAxis("Horizontal");

        // Q�L�[�ŏ㏸�AE�L�[�ŉ��~
        float moveY = 0f;
        if (Input.GetKey(KeyCode.Q))
        {
            moveY = _verticalSpeed * Time.deltaTime;  // �㏸
        }
        else if (Input.GetKey(KeyCode.E))
        {
            moveY = -_verticalSpeed * Time.deltaTime;  // ���~
        }

        // �J������forward��right�����ɉ������ړ��x�N�g�����v�Z
        Vector3 move = (transform.forward * moveZ + transform.right * moveX) * _moveSpeed * Time.deltaTime;

        // �㉺�ړ���ǉ�
        move.y += moveY;

        // �J�������ړ�
        transform.position += move;
    }

    // �J�����̉�]����
    private void HandleRotation()
    {
        if (Input.GetMouseButton(1)) // �E�N���b�N�������Ă���ԁA�J��������]
        {
            float rotateX = Input.GetAxis("Mouse X") * _rotateSpeed * Time.deltaTime;
            float rotateY = Input.GetAxis("Mouse Y") * _rotateSpeed * Time.deltaTime;

            // ���������̉�]�i�J������Y������̉�]�j
            transform.Rotate(Vector3.up, rotateX, Space.World);

            // ���������̉�]�i�J�����̃��[�J��X������̉�]�j
            transform.Rotate(Vector3.right, -rotateY, Space.Self);
        }
    }

    // �J�����̃Y�[������
    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        _currentZoom -= scroll * _zoomSpeed;
        _currentZoom = Mathf.Clamp(_currentZoom, _minZoom, _maxZoom);
        Camera.main.fieldOfView = _currentZoom;
    }
}
