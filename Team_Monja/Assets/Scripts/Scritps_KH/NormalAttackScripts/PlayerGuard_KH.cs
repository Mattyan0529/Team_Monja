using UnityEngine;

public class PlayerGuard_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _coolTimeObj = default;

    private bool _isGuard = false;
    private int _guardCount = default;
    private bool _isGuarded = false;//���͂̏d���h�~
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
    /// �K�[�h��Ԃ�؂�ւ���
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
                //�ŏ��̓X�N���v�g����U�����Ăяo��
                _characterAnim.NowAnim = "Attack";
                _guardCount--;
            }
        }
    }

    /// <summary>
    /// �A���U�������邩(�U���A�j���[�V�����̍Ō�
    /// </summary>
    private void ComboOrCoolTime()
    {
        if (_guardCount > 0)
        {
            //�A���U���̓��͂������
            _characterAnim.NowAnim = "Attack";
            _guardCount--;
        }
        else
        {
            //�A���U���̓��͂��Ȃ����
            StartCoolTime();
        }
    }

    /// <summary>
    /// �N�[���^�C�����X�^�[�g����(�U���A�j���[�V�����̍Ō�ɌĂяo��)
    /// </summary>
    private void StartCoolTime()
    {
        _coolTimeUI.StartCoolTime();
        _guardCount = 0;//�U���̓��͉񐔂����Z�b�g
        _canUseGuard = false;
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
