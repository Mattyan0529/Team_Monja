using UnityEngine;

public class NormalAttack_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _residentScript;

    private float _radius = 4f;      // �U���͈͂Ƃ��Đ��ݏo��Sphere�̔��a
    private Vector3 _position = Vector3.zero;       // Sphere�̈ʒu

    private float _deleteTime = 0.2f;
    private float _elapsedTime = 0f;

    private GameObject _attackArea;
    private bool _isAttack = false;

    private WriteHitPoint_KH _writeHitPoint = default;

    void Start()
    {
        _writeHitPoint = _residentScript.GetComponent<WriteHitPoint_KH>();
    }

    void Update()
    {
        UpdateTime();
        AttackInputManager();
    }

    private void AttackInputManager()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    /// <summary>
    /// �U���͈͂�Sphere�𐶐�
    /// </summary>
    private void Attack()
    {
        _isAttack = true;

        if (_attackArea != null)        // Sphere�����łɂ���Ƃ��͍쐬���Ȃ�
        {
            _attackArea.SetActive(true);
            return;
        }

        _attackArea = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        // Sphere�����̃X�N���v�g���A�^�b�`����Ă���I�u�W�F�N�g�̎q�ɐݒ�
        _attackArea.transform.parent = transform;

        // Sphere��Transform��ݒ�
        _attackArea.transform.localPosition = _position;
        _attackArea.transform.localScale = new Vector3(_radius, _radius, _radius);

        // Sphere�̃}�e���A����ݒ�i�����Ɂj
        Renderer renderer = _attackArea.GetComponent<Renderer>();
        renderer.enabled = false;

        _attackArea.GetComponent<SphereCollider>().isTrigger = true;

        _attackArea.AddComponent<NormalAttackHitDecision_KH>();
    }

    /// <summary>
    /// ��������������擾
    /// </summary>
    public void HitDecision(GameObject hitObj)
    {
        // ����Ǝ�����StatusManager�������K�v
        StatusManager_MT targetStatusManager = hitObj.gameObject.GetComponent<StatusManager_MT>();
        StatusManager_MT myStatusManager = GetComponent<StatusManager_MT>();

        HitPointCalculation(myStatusManager, targetStatusManager);
    }

    /// <summary>
    /// �_���[�W�v�Z
    /// </summary>
    public void HitPointCalculation(StatusManager_MT myStatus, StatusManager_MT targetStatus)
    {
        int myAttackPower = myStatus.Strength;        // �����̍U���͂�get���Ă���
        int targetDefensePower = targetStatus.Defense;        // ����̖h��͂�get���Ă���
        int targetHitPoint = targetStatus.HP;        // �����HP��get���Ă���

        if (myAttackPower < targetDefensePower) return;        // �h��͂̂ق�������������0�_���[�W

        int damage = targetHitPoint - (myAttackPower - targetDefensePower);
        _writeHitPoint.UpdateHitPoint(targetStatus, damage);      // targetStatus��HP���X�V
    }

    /// <summary>
    /// ��莞�Ԍ�U���͈͂��폜����
    /// </summary>
    private void UpdateTime()
    {
        if (!_isAttack) return;     // �U�����ȊO�͏������s��Ȃ�

        // ���ԉ��Z
        _elapsedTime += Time.deltaTime;

        // �K�莞�ԂɒB���Ă����ꍇ
        if (_elapsedTime > _deleteTime)
        {
            _attackArea.SetActive(false);
            _elapsedTime = 0f;
            _isAttack = false;
        }
    }
}
