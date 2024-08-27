using UnityEngine;
using System.Collections;

public class CameraManager_MT : MonoBehaviour
{
    private Camera playerCamera;
    private GameObject playerObj;
    private Transform playerTransform;
    private CapsuleCollider playerCollider;

    private float mouseSensitivity = 100.0f; // マウス感度
    private bool _InWall = false; // カメラが壁の中に入っているか
    private Vector3 shakeOffset = Vector3.zero; // 揺れのオフセット
    private Vector3 cameraPos;//カメラの初期位置

    private void Awake()
    {
        FindPlayer();
    }

    void Start()
    {
        playerCamera = Camera.main.GetComponent<Camera>();
        playerCollider = playerObj.GetComponent<CapsuleCollider>();

        // カーソルをロックして画面中央に固定
        Cursor.lockState = CursorLockMode.Locked;

        cameraPos =playerObj.transform.position;
        Debug.Log(cameraPos);
    }

    // Update is called once per frame
    void Update()
    {
        CameraMove();
        CameraTransparent();

        cameraPos = playerObj.transform.position;

        //カメラ振動のサンプル
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    StartCoroutine(CameraShake(0.3f, 0.5f));
        //}
    }

    private void LateUpdate()
    {
        PlayerFollowing();
        PositionCalculator();
    }

    /// <summary>
    /// プレイヤーを取得
    /// </summary>
    public void FindPlayer()
    {
        playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
    }

    /// <summary>
    /// プレイヤーの座標にカメラを移動
    /// </summary>
    private void PlayerFollowing()
    {
        if (playerObj != null)
        {
            // プレイヤーに追従
            transform.position = playerTransform.position;
            // 追従後、揺れのオフセットを適用
            transform.position += shakeOffset;
        }
    }

    /// <summary>
    /// キャラクターに応じてカメラの高さを変える
    /// </summary>
    private void PositionCalculator()
    {
        transform.position = cameraPos + (Vector3.up * playerCollider.height);
    }

    /// <summary>
    /// マウスでカメラを動かす
    /// </summary>
    private void CameraMove()
    {
        // マウスの移動量を取得
        float mouseX = Input.GetAxis("Horizontal2") * mouseSensitivity * Time.deltaTime
         + Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //カメラを回転
        transform.Rotate(Vector3.up * mouseX, Space.World);
        //プレイヤーを移動
        playerObj.transform.Rotate(Vector3.up * mouseX, Space.World);
    }

    /// <summary>
    /// カメラを揺らす
    /// </summary>
    /// <param name="duration">時間</param>
    /// <param name="magnitude">強さ</param>
    /// <returns></returns>
    public IEnumerator CameraShake(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // 揺れのオフセットを計算
            shakeOffset = Random.insideUnitSphere * magnitude;
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 揺れ終了後、オフセットをリセット
        shakeOffset = Vector3.zero;
    }

    /// <summary>
    /// カメラの透過距離を変更
    /// </summary>
    private void CameraTransparent()
    {
        if (_InWall)
        {
            playerCamera.nearClipPlane = 7.25f; // 透過距離を設定
        }
        else
        {
            playerCamera.nearClipPlane = 0.03f; // 初期値
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
