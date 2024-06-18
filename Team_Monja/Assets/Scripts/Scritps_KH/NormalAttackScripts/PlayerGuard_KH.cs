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

    void Start()
    {
        
    }

    void Update()
    {
        GuardManagement();
        UpdateTime();
    }

    /// <summary>
    /// ƒK[ƒhó‘Ô‚ğØ‚è‘Ö‚¦‚é
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
    /// ˆê’èŠÔŒãƒK[ƒh‚ğ©“®‚Åæ‚èÁ‚·
    /// </summary>
    private void UpdateTime()
    {
        // ŠÔ‰ÁZ
        _elapsedTime += Time.deltaTime;

        // ‹K’èŠÔ‚É’B‚µ‚Ä‚¢‚½ê‡
        if (_elapsedTime > _deleteTime)
        {
            _isGuard = false;
            _elapsedTime = 0f;
        }
    }
}
