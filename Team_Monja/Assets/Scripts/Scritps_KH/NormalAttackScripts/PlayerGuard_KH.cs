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
    /// �K�[�h��Ԃ�؂�ւ���
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
}
