using UnityEngine;

public class PlayerGuard_KH : MonoBehaviour
{
    private bool _isGuard = false;

    private float _deleteTime = 1f;
    private float _elapsedTime = 0f;

    public bool IsGuard
    {
        get { return _isGuard; }
    }

    void Update()
    {
        GuardManagement();
        UpdateTime();
    }

    /// <summary>
    /// ガード状態を切り替える
    /// </summary>
    private void GuardManagement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(_isGuard == false)
            {
                _elapsedTime = 0f;
            }

            _isGuard = !_isGuard;
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
}
