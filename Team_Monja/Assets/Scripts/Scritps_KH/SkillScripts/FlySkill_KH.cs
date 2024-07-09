using UnityEngine;

public class FlySkill_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _residentScript = default;

    [SerializeField]
    private EffectManager _effectManager; // EffectManagerの参照を追加

    private Rigidbody _rigidbody = default;
    private AudioSource _audioSource = default;

    private float _upSpeed = 3000f;
    private float _maxUpHeight = 3f;

    private float _startHeight = default;
    private float _currentMaxHeight = default;
    private float _rayLength = 10f;

    private bool _isFlying = false;

    [SerializeField]
    private LayerMask groundLayer;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_isFlying)
        {
            FlyHeightUpdate();
            FlyAddForce();
        }
    }

    public bool IsFlying
    {
        get { return _isFlying; }
    }

    /// <summary>
    /// プレイヤーからの入力に応じて状態変化
    /// </summary>
    public void PlayerFlyManager()
    {
        _isFlying = true;     // 飛んでいるときは降りる、飛んでいないときは飛び始める

        // 飛び始めの最高到達点を決める
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, _rayLength, groundLayer))
        {
            _startHeight = hit.point.y;
            _currentMaxHeight = _startHeight + _maxUpHeight;
        }
    }

    /// <summary>
    /// モンスターは常に飛んでいる状態にする
    /// </summary>
    public void MonsterStartFly()
    {
        _isFlying = true;

        if (_audioSource == null)
        {
            _audioSource = GetComponentInChildren<AudioSource>();
        }

        // 飛び始めの最高到達点を決める
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, _rayLength, groundLayer))
        {
            _startHeight = hit.point.y;
            _currentMaxHeight = _startHeight + _maxUpHeight;
        }
    }

    /// <summary>
    /// 飛び始めるために力を加える
    /// </summary>
    private void FlyAddForce()
    {
        if (gameObject.transform.position.y < _currentMaxHeight)
        {
            _rigidbody.AddForce(Vector3.up * _upSpeed * Time.deltaTime, ForceMode.Acceleration);
        }
        else if (_currentMaxHeight < gameObject.transform.position.y)
        {
            // 最大高度に達したら、その高さを維持する
            gameObject.transform.position = new Vector3(transform.position.x, _currentMaxHeight, transform.position.z);
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z); // 上昇を停止する
        }
    }

    /// <summary>
    /// 飛行中に地面の高さに合わせて最高到達点を調整する
    /// </summary>
    private void FlyHeightUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, _rayLength, groundLayer))
        {
            float groundHeight = hit.point.y;
            if (groundHeight != _startHeight)
            {
                _startHeight = groundHeight;
                _currentMaxHeight = _startHeight + _maxUpHeight;
            }
        }
    }

    public void StopFly()
    {
        _rigidbody.velocity = Vector3.zero;
        _isFlying = false;
    }

    private void OnDisable()
    {
        StopFly();
    }
}
