using UnityEngine;

public class PlayerGuard_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _coolTimeObj = default;

    private bool _isGuard = false;
    private int _guardCount = default;
    private bool _isGuarded = false;//入力の重複防止
    private bool _canUseGuard = true;

    private float _deleteTime = 0.8f;
    private float _elapsedTime = 0f;

    private float _coolTime = 1f;
    private float _coolTimeElapsedTime = 0f;

    private CoolTimeUI_KH _coolTimeUI = default;
    private CharacterAnim_MT _characterAnim = default;

    public bool IsGuard
    {
        get { return _isGuard; }
    }

    private void Start()
    {
        _coolTimeUI = _coolTimeObj.GetComponent<CoolTimeUI_KH>();
        _characterAnim = GetComponent<CharacterAnim_MT>();
    }

    void Update()
    {
        GuardManagement();
        UpdateTime();
        UpdateCoolTime();
        _isGuarded = false;
    }

    /// <summary>
    /// ガード状態を切り替える
    /// </summary>
    private void GuardManagement()
    {
        if ((Input.GetMouseButtonDown(1) || Input.GetButtonDown("Submit")) && !_isGuarded)
        {
            if (!_canUseGuard) return;

            _isGuarded = true;

            if (_guardCount > 0)
            {
                _guardCount++;
            }
            else
            {
                //最初はスクリプトから攻撃を呼び出す
                _characterAnim.NowAnim = "Attack";
                _guardCount--;
            }
        }
    }

    /// <summary>
    /// 連続攻撃をするか(攻撃アニメーションの最後
    /// </summary>
    private void ComboOrCoolTime()
    {
        if (_guardCount > 0)
        {
            //連続攻撃の入力があれば
            _characterAnim.NowAnim = "Attack";
            _guardCount--;
        }
        else
        {
            //連続攻撃の入力がなければ
            StartCoolTime();
        }
    }

    /// <summary>
    /// クールタイムをスタートする(攻撃アニメーションの最後に呼び出す)
    /// </summary>
    private void StartCoolTime()
    {
        _coolTimeUI.StartCoolTime();
        _guardCount = 0;//攻撃の入力回数をリセット
        _canUseGuard = false;
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
    /// 再度ガードができるようにする
    /// </summary>
    private void UpdateCoolTime()
    {
        if (_canUseGuard) return;

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
