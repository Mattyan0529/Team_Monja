using UnityEngine;

public class PlayerMove_MT : MonoBehaviour
{
    private float moveSpeed = 500f;  // 移動速度
    private float maxSpeed = 1000f;  // 最大速度
    private float slopeForce = 100f; // 坂を登る力
    private float groundCheckDistance = 0.1f; // 地面チェック距離

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

        // 移動ベクトルをローカル座標系に変換
        movement = transform.TransformDirection(movement);

        // 地面に近い場合、より滑らかな動きを得るためにY軸の速度を保持
        float yVelocity = rb.velocity.y;

        // 速度の制限
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        // 坂を登る処理
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance))
        {
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
            if (slopeAngle > 0f && slopeAngle <= 45f) // 坂の角度が45度以下の場合に坂を登る
            {
                Vector3 slopeDirection = Vector3.Cross(Vector3.Cross(hit.normal, Vector3.up), hit.normal);
                rb.AddForce(slopeDirection * slopeForce);
            }
        }

        // 地面に接触しているかを確認
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        // 地面に接触している場合、Y軸の速度をリセット
        if (isGrounded)
        {
            yVelocity = 0f;
        }

        // 移動
        rb.velocity = new Vector3(movement.x, yVelocity, movement.z);
    }
}
