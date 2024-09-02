using UnityEngine;

public class PlayerMove_MT : MonoBehaviour
{
    private float moveSpeed = 10f;  // 移動速度を後でStatusManagerから取得する
    private float maxSpeed = 20f;  // 最大速度
    private float slopeForce = 10f; // 坂を登る力
    private float groundCheckDistance = 0.1f; // 地面チェック距離

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
    /// プレイヤーオブジェクトを設定する
    /// </summary>
    public void SetPlayer()
    {
        _playerObj = GameObject.FindWithTag("Player");

        rb = _playerObj.GetComponent<Rigidbody>();
        _characterAnim = _playerObj.GetComponent<CharacterAnim_MT>();
        _statusManager = _playerObj.GetComponent<StatusManager_MT>();  // StatusManagerを取得する
        moveSpeed = _statusManager.Speed;  // Speedプロパティから移動速度を設定
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // アニメーション
        if ((moveHorizontal + moveVertical) != 0)
        {
            _characterAnim.NowAnim = "Move";
        }
        else
        {
            _characterAnim.NowAnim = "Idle";
        }

        // カメラの方向から見たキャラクターの前方ベクトルを計算
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0f; // y軸の影響を無視（水平方向のみを考慮）

        // カメラの前方ベクトルを基準にした移動ベクトルを計算
        Vector3 movement = (cameraForward * moveVertical + Camera.main.transform.right * moveHorizontal).normalized * moveSpeed * Time.deltaTime;

        // そのままワールド座標系で移動
        rb.MovePosition(rb.position + movement);

        // キャラクターの向きを移動方向に合わせる
        if (movement.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            _playerObj.transform.rotation = Quaternion.Slerp(_playerObj.transform.rotation, targetRotation, 0.5f); // 補間の速度を速くする
        }

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
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        }
    }
}
