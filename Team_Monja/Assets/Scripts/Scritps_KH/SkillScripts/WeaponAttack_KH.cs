using UnityEngine;

public class WeaponAttack_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _residentScript;

    private float _deleteTime = 0.5f;
    private float _elapsedTime = 0f;

    private GameObject _attackArea;

    private WriteHitPoint_KH _writeHitPoint = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private AudioSource _audioSource = default;
    private MonsterRandomWalk_KH _monsterRandomWalk = default;
    private PlayerRangeInJudge_KH _playerRangeInJudge = default;
    private PlayerMove_MT _playerMove = default;

    private bool _isAttack = false;

    //���{
    private CharacterAnim_MT _characterAnim = default;

    private void Awake()
    {
        _monsterRandomWalk = GetComponent<MonsterRandomWalk_KH>();
        _playerRangeInJudge = GetComponent<PlayerRangeInJudge_KH>();
        _playerMove = GetComponent<PlayerMove_MT>();
    }

    void Start()
    {
        _writeHitPoint = _residentScript.GetComponent<WriteHitPoint_KH>();
        _soundEffectManagement = _residentScript.GetComponent<SoundEffectManagement_KH>();
        _characterAnim = GetComponent<CharacterAnim_MT>();
        _audioSource = GetComponent<AudioSource>();

        // �q�I�u�W�F�N�g�̒�����AttackArea���擾
        _attackArea = transform.Find("AttackArea").gameObject;
        _attackArea.SetActive(false);
    }

    void Update()
    {
        UpdateTime();
    }

    /// <summary>
    /// �U��������
    /// </summary>
    public void Attack()
    {
        if (_monsterRandomWalk.enabled) return;     // �����_���ړ����i�v���C���[���U���͈͊O�j�͏������Ȃ�

        //���{
        _characterAnim.NowAnim = "Skill";

        // �������~�߂�
        if (gameObject.CompareTag("Enemy"))
        {
            _playerRangeInJudge.enabled = false;
        }
        if (gameObject.CompareTag("Player"))
        {
            _playerMove.enabled = false;
        }

        _isAttack = true;
        _attackArea.SetActive(true);

        _soundEffectManagement.PlayStrongPunchSound(_audioSource);
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
            // �������ĊJ����
            if (gameObject.CompareTag("Enemy"))
            {
                _playerRangeInJudge.enabled = true;
            }
            if (gameObject.CompareTag("Player"))
            {
                _playerMove.enabled = true;
            }

            _attackArea.SetActive(false);
            _elapsedTime = 0f;
            _isAttack = false;
        }
    }
}
