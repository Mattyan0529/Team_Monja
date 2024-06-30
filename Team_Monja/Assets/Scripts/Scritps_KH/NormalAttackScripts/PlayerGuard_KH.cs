using UnityEngine;

public class PlayerGuard_KH : MonoBehaviour
{
    [SerializeField]
    GameObject _coolTimeUIObj = default;

    private bool _isGuard = false;
    private bool _canUseGuard = true;

    private float _deleteTime = 0.5f;
    private float _elapsedTime = 0f;

    private float _coolTime = 0.5f;    // 通常攻撃を発動してから次に発動できるようになるまでの時間
    private float _coolTimeElapsedTime = 0f;

    private CoolTimeUI _coolTimeUI = default;

    public bool IsGuard
    {
        get { return _isGuard; }
    }

    private void Start()
    {
        _coolTimeUI = _coolTimeUIObj.GetComponent<CoolTimeUI>();
    }

    void Update()
    {
        GuardManagement();
        UpdateTime();
        UpdateCoolTime();
    }

    /// <summary>
    /// ガード状態を切り替える
    /// </summary>
    private void GuardManagement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_isGuard == false)
            {
                _canUseGuard = false;
                _elapsedTime = 0f;
                _coolTimeElapsedTime = 0f;
                _coolTimeUI.StartCoolTime();
                _isGuard = true;
            }
        }
    }

    /// <summary>
    /// 一定時間後ガードを自動で取り消す
    /// </summary>
    private void UpdateTime()
    {
        // 時間加算
        _elapsedTime += Time.deltaTime;

        // 規定時間に達していた場合
        if (_elapsedTime > _deleteTime)
        {
            _isGuard = false;
            _elapsedTime = 0f;
        }
    }

    /// <summary>
    /// クールタイム後通常攻撃を使えるようにする
    /// </summary>
    private void UpdateCoolTime()
    {
        if (_canUseGuard) return;     // 攻撃中以外は処理を行わない

        // 時間加算
        _coolTimeElapsedTime += Time.deltaTime;

        // 規定時間に達していた場合
        if (_coolTimeElapsedTime > _coolTime)
        {
            _coolTimeElapsedTime = 0f;
            _canUseGuard = true;
        }
    }
}
