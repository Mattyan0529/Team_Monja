using UnityEngine;

public class PlayerMove_MT : MonoBehaviour
{
    private float moveSpeed = 500f;  // �ړ����x
    private float maxSpeed = 1000f;  // �ő呬�x
    private float slopeForce = 100f; // ���o���
    private float groundCheckDistance = 0.1f; // �n�ʃ`�F�b�N����

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical).normalized * moveSpeed * Time.deltaTime;

        // �ړ��x�N�g�������[�J�����W�n�ɕϊ�
        movement = transform.TransformDirection(movement);

        // �n�ʂɋ߂��ꍇ�A��芊�炩�ȓ����𓾂邽�߂�Y���̑��x��ێ�
        float yVelocity = rb.velocity.y;

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
            yVelocity = 0f;
        }

        // �ړ�
        rb.velocity = new Vector3(movement.x, yVelocity, movement.z);
    }
}
