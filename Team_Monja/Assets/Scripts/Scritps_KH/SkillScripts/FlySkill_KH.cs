using UnityEngine;

public class FlySkill_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _residentScript = default;

    [SerializeField]
    private EffectManager _effectManager; // EffectManager�̎Q�Ƃ�ǉ�

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
    /// �v���C���[����̓��͂ɉ����ď�ԕω�
    /// </summary>
    public void PlayerFlyManager()
    {
        _isFlying = true;     // ���ł���Ƃ��͍~���A���ł��Ȃ��Ƃ��͔�юn�߂�

        // ��юn�߂̍ō����B�_�����߂�
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, _rayLength, groundLayer))
        {
            _startHeight = hit.point.y;
            _currentMaxHeight = _startHeight + _maxUpHeight;
        }
    }

    /// <summary>
    /// �����X�^�[�͏�ɔ��ł����Ԃɂ���
    /// </summary>
    public void MonsterStartFly()
    {
        _isFlying = true;

        if (_audioSource == null)
        {
            _audioSource = GetComponentInChildren<AudioSource>();
        }

        // ��юn�߂̍ō����B�_�����߂�
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, _rayLength, groundLayer))
        {
            _startHeight = hit.point.y;
            _currentMaxHeight = _startHeight + _maxUpHeight;
        }
    }

    /// <summary>
    /// ��юn�߂邽�߂ɗ͂�������
    /// </summary>
    private void FlyAddForce()
    {
        if (gameObject.transform.position.y < _currentMaxHeight)
        {
            _rigidbody.AddForce(Vector3.up * _upSpeed * Time.deltaTime, ForceMode.Acceleration);
        }
        else if (_currentMaxHeight < gameObject.transform.position.y)
        {
            // �ő卂�x�ɒB������A���̍������ێ�����
            gameObject.transform.position = new Vector3(transform.position.x, _currentMaxHeight, transform.position.z);
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z); // �㏸���~����
        }
    }

    /// <summary>
    /// ��s���ɒn�ʂ̍����ɍ��킹�čō����B�_�𒲐�����
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
