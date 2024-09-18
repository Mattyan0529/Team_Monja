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


    [SerializeField] private GameObject _sourceObject;
    [SerializeField] private GameObject _targetObject;
    [SerializeField] private float _rayDistance = 10f; // ���C�̒���
    [SerializeField] private LayerMask _layerMask; // ���C���[�}�X�N
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

    }

    // Update is called once per frame
    void Update()
    {
        CastRayFromSourceToTarget();
        CameraMove();
        //CameraTransparent();

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

    // �\�[�X�I�u�W�F�N�g����^�[�Q�b�g�I�u�W�F�N�g�����Ƀ��C���΂����\�b�h
    private void CastRayFromSourceToTarget()
    {
        if (_sourceObject == null || _targetObject == null)
        {
            Debug.LogError("Source Object or Target Object is not assigned.");
            return;
        }

        // �\�[�X�I�u�W�F�N�g�ƃ^�[�Q�b�g�I�u�W�F�N�g��Transform���擾
        Transform sourceTransform = _sourceObject.transform;
        Transform targetTransform = _targetObject.transform;

        // �\�[�X�I�u�W�F�N�g�̈ʒu����^�[�Q�b�g�I�u�W�F�N�g�̕����Ɍ������ă��C���΂�
        Vector3 direction = (targetTransform.position - sourceTransform.position).normalized;
        Ray ray = new Ray(sourceTransform.position, direction);
        RaycastHit hit;

        // ���C���w�肵���������ɁA�w�肵�����C���[�ɑ�����I�u�W�F�N�g�ɓ����������`�F�b�N
        if (Physics.Raycast(ray, out hit, _rayDistance, _layerMask))
        {
            // �q�b�g�����I�u�W�F�N�g�̃|�W�V�����ɃJ�������ړ�
          Camera.main.transform.position = hit.point;
        }
        else
        {
            Debug.Log("Missed.");
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
