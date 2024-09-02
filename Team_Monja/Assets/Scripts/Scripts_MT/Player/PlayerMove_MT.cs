using UnityEngine;

public class PlayerMove_MT : MonoBehaviour
{
    private float moveSpeed = 10f;  // �ړ����x�����StatusManager����擾����
    private float maxSpeed = 20f;  // �ő呬�x
    private float slopeForce = 10f; // ���o���
    private float groundCheckDistance = 0.1f; // �n�ʃ`�F�b�N����

    private GameObject _playerObj;
    private Rigidbody rb;
    private CharacterAnim_MT _characterAnim;
    private StatusManager_MT _statusManager;
    private bool isGrounded;

    void Start()
    {
        SetPlayer();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    /// <summary>
    /// �v���C���[�I�u�W�F�N�g��ݒ肷��
    /// </summary>
    public void SetPlayer()
    {
        _playerObj = GameObject.FindWithTag("Player");

        rb = _playerObj.GetComponent<Rigidbody>();
        _characterAnim = _playerObj.GetComponent<CharacterAnim_MT>();
        _statusManager = _playerObj.GetComponent<StatusManager_MT>();  // StatusManager���擾����
        moveSpeed = _statusManager.Speed;  // Speed�v���p�e�B����ړ����x��ݒ�
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // �A�j���[�V����
        if ((moveHorizontal + moveVertical) != 0)
        {
            _characterAnim.NowAnim = "Move";
        }
        else
        {
            _characterAnim.NowAnim = "Idle";
        }

        // �J�����̕������猩���L�����N�^�[�̑O���x�N�g�����v�Z
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0f; // y���̉e���𖳎��i���������݂̂��l���j

        // �J�����̑O���x�N�g������ɂ����ړ��x�N�g�����v�Z
        Vector3 movement = (cameraForward * moveVertical + Camera.main.transform.right * moveHorizontal).normalized * moveSpeed * Time.deltaTime;

        // ���̂܂܃��[���h���W�n�ňړ�
        rb.MovePosition(rb.position + movement);

        // �L�����N�^�[�̌������ړ������ɍ��킹��
        if (movement.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            _playerObj.transform.rotation = Quaternion.Slerp(_playerObj.transform.rotation, targetRotation, 0.5f); // ��Ԃ̑��x�𑬂�����
        }

        // ���x�̐���
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        // ���o�鏈��
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance))
        {
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
            if (slopeAngle > 0f && slopeAngle <= 45f) // ��̊p�x��45�x�ȉ��̏ꍇ�ɍ��o��
            {
                Vector3 slopeDirection = Vector3.Cross(Vector3.Cross(hit.normal, Vector3.up), hit.normal);
                rb.AddForce(slopeDirection * slopeForce);
            }
        }

        // �n�ʂɐڐG���Ă��邩���m�F
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        // �n�ʂɐڐG���Ă���ꍇ�AY���̑��x�����Z�b�g
        if (isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        }
    }
}
