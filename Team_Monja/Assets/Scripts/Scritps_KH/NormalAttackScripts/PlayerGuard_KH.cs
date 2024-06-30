using UnityEngine;

public class PlayerGuard_KH : MonoBehaviour
{
    [SerializeField]
    GameObject _coolTimeUIObj = default;

    private bool _isGuard = false;
    private bool _canUseGuard = true;

    private float _deleteTime = 0.5f;
    private float _elapsedTime = 0f;

    private float _coolTime = 0.5f;    // �ʏ�U���𔭓����Ă��玟�ɔ����ł���悤�ɂȂ�܂ł̎���
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
    /// �K�[�h��Ԃ�؂�ւ���
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
    /// �N�[���^�C����ʏ�U�����g����悤�ɂ���
    /// </summary>
    private void UpdateCoolTime()
    {
        if (_canUseGuard) return;     // �U�����ȊO�͏������s��Ȃ�

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
