using UnityEngine;

public class PlayerGuard_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _coolTimeObj = default;

    private bool _isGuard = false;
    private bool _canUseGuard = true;

    private float _deleteTime = 0.5f;
    private float _elapsedTime = 0f;

    private float _coolTime = 0.5f;
    private float _coolTimeElapsedTime = 0f;

    private CoolTimeUI _coolTimeUI = default;

    public bool IsGuard
    {
        get { return _isGuard; }
    }

    private void Start()
    {
        _coolTimeUI = _coolTimeObj.GetComponent<CoolTimeUI>();
    }

    void Update()
    {
        GuardManagement();
        UpdateTime();
        UpdateCoolTime();
    }

    /// <summary>
    /// �K�[�h��Ԃ�؂�ւ���
    /// </summary>
    private void GuardManagement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_isGuard == true) return;

            _canUseGuard = false;
            _elapsedTime = 0f;
            _coolTimeElapsedTime = 0f;
            _coolTimeUI.StartCoolTime();
            _isGuard = true;
        }
    }

    /// <summary>
    /// ��莞�Ԍ�K�[�h�������Ŏ�����
    /// </summary>
    private void UpdateTime()
    {
        // ���ԉ��Z
        _elapsedTime += Time.deltaTime;

        // �K�莞�ԂɒB���Ă����ꍇ
        if (_elapsedTime > _deleteTime)
        {
            _isGuard = false;
            _elapsedTime = 0f;
        }
    }

    /// <summary>
    /// �ēx�K�[�h���ł���悤�ɂ���
    /// </summary>
    private void UpdateCoolTime()
    {
        if (_canUseGuard) return;

        // ���ԉ��Z
        _coolTimeElapsedTime += Time.deltaTime;

        // �K�莞�ԂɒB���Ă����ꍇ
        if (_coolTimeElapsedTime > _coolTime)
        {
            _coolTimeElapsedTime = 0f;
            _canUseGuard = true;
        }
    }
}
