using UnityEngine;
using System.Collections;

public class CameraManager_MT : MonoBehaviour
{
    private Camera playerCamera;
    private GameObject playerObj;
    private Transform playerTransform;
    private CapsuleCollider playerCollider;

    private float mouseSensitivity = 100.0f; // �}�E�X���x
    private bool _InWall = false; // �J�������ǂ̒��ɓ����Ă��邩
    private Vector3 shakeOffset = Vector3.zero; // �h��̃I�t�Z�b�g
    private Vector3 cameraPos;//�J�����̏����ʒu

    private void Awake()
    {
        FindPlayer(GameObject.FindWithTag("Player"));
    }

    void Start()
    {
        playerCamera = Camera.main.GetComponent<Camera>();
    

        // �J�[�\�������b�N���ĉ�ʒ����ɌŒ�
        Cursor.lockState = CursorLockMode.Locked;

        cameraPos = playerObj.transform.position;
        Debug.Log(cameraPos);
    }

    // Update is called once per frame
    void Update()
    {
        CameraMove();
        CameraTransparent();

        cameraPos = playerObj.transform.position;


    }

    private void LateUpdate()
    {
        PlayerFollowing();

    }

    /// <summary>
    /// �v���C���[���擾
    /// </summary>
    public void FindPlayer(GameObject player)
    {
        playerObj = player;

        playerCollider = playerObj.GetComponent<CapsuleCollider>();
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
    }

    /// <summary>
    /// �v���C���[�̍��W�ɃJ�������ړ�
    /// </summary>
    private void PlayerFollowing()
    {
        if (playerObj != null)
        {
            // �v���C���[�ɒǏ]
            transform.position = playerTransform.position;
            // �Ǐ]��A�h��̃I�t�Z�b�g��K�p
            transform.position += shakeOffset;
        }
    }

    /// <summary>
    /// �L�����N�^�[�ɉ����ăJ�����̍�����ς���
    /// </summary>
    private void PositionCalculator()
    {
        transform.position = cameraPos + (Vector3.up * playerCollider.height);
    }

    /// <summary>
    /// �}�E�X�ŃJ�����𓮂���
    /// </summary>
    private void CameraMove()
    {
        //��������
        PositionCalculator();
        // �}�E�X�̈ړ��ʂ��擾
        float mouseX = Input.GetAxis("Horizontal2") * mouseSensitivity * Time.deltaTime
         + Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //�J��������]
        transform.Rotate(Vector3.up * mouseX, Space.World);
        //�v���C���[���ړ�
        playerObj.transform.Rotate(Vector3.up * mouseX, Space.World);
    }

    /// <summary>
    /// �J������h�炷
    /// </summary>
    /// <param name="duration">����</param>
    /// <param name="magnitude">����</param>
    /// <returns></returns>
    public IEnumerator CameraShake(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // �h��̃I�t�Z�b�g���v�Z
            shakeOffset = Random.insideUnitSphere * magnitude;
            elapsed += Time.deltaTime;
            yield return null;
        }

        // �h��I����A�I�t�Z�b�g�����Z�b�g
        shakeOffset = Vector3.zero;
    }

    /// <summary>
    /// �J�����̓��ߋ�����ύX
    /// </summary>
    private void CameraTransparent()
    {
        if (_InWall)
        {
            playerCamera.nearClipPlane = 7.25f; // ���ߋ�����ݒ�
        }
        else
        {
            playerCamera.nearClipPlane = 0.03f; // �����l
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _InWall = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _InWall = false;
    }
}
