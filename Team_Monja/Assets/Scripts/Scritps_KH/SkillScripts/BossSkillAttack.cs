using Unity.VisualScripting;
using UnityEngine;

public class BossSkillAttack : MonoBehaviour, IDamagable
{
    [SerializeField]
    private float _rangeHaveAttackArea = 7f;

    [SerializeField]
    private GameObject[] _attackRangeImage = default;

    //���{
    private CharacterAnim_MT _characterAnim = default;

    private float _sphereDeleteTime = 2f;
    private float _hitAttackDeleteTime = 0.5f;
    private float _elapsedTime = 0f;

    private float _imagePositionY = 18.34f;

    private AudioSource _audioSource = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private WriteHitPoint_KH _writeHitPoint = default;
    private CreateDamageImage_KH _createDamageImage = default;
    private ChangeEnemyMoveType _changeEnemyMoveType = default;
    private PlayerManager_KH _playerManager = default;

    private GameObject _player = default;
    private GameObject _residentScript = default;

    #region FireSphere

    private bool _isShot = false;

    private GameObject[] _fireAttackAreas;

    private GameObject _underPlayerAttackRange = default;
    private GameObject _nearPlayerAttackRange = default;
    private GameObject _farPlayerAttackRange = default;

    float _minimumNearPlayerAttackRange = 0.5f;
    float _maximumNearPlayerAttackRange = 2f;

    float _minimumFarPlayerAttackRange = 2f;
    float _maximumFarPlayerAttackRange = 4f;

    #endregion

    #region HitAttack

    [SerializeField]
    private EffectManager _effectManager; // EffectManager�̎Q�Ƃ�ǉ�

    private bool _isAttack = false;
    private GameObject _attackArea;

    #endregion


    void Start()
    {
        _characterAnim = GetComponent<CharacterAnim_MT>();

        _residentScript = GameObject.Find("ResidentScripts");
        _writeHitPoint = _residentScript.GetComponent<WriteHitPoint_KH>();
        _createDamageImage = _residentScript.GetComponent<CreateDamageImage_KH>();
        _soundEffectManagement = _residentScript.GetComponent<SoundEffectManagement_KH>();
        _audioSource = GetComponent<AudioSource>();
        _changeEnemyMoveType = GetComponent<ChangeEnemyMoveType>();
        _playerManager = _residentScript.GetComponent<PlayerManager_KH>();
        _player = _playerManager.Player;

        #region FireAttack

        _fireAttackAreas = GameObject.FindGameObjectsWithTag("FireAttackArea");
        _underPlayerAttackRange = _fireAttackAreas[0];
        _nearPlayerAttackRange = _fireAttackAreas[1];
        _farPlayerAttackRange = _fireAttackAreas[2];

        _underPlayerAttackRange.SetActive(false);
        _nearPlayerAttackRange.SetActive(false);
        _farPlayerAttackRange.SetActive(false);

        #endregion

        #region HitAttack

        // HitAttackArea���擾
        _attackArea = GameObject.FindGameObjectWithTag("HitAttackArea");
        _attackArea.SetActive(false);

        #endregion
    }

    private void Update()
    {
        UpdateFireTime();
        UpdateHitTime();
    }

    public void SpecialAttack()
    {
        RangeMeasurementWithPlayer();
    }

    /// <summary>
    /// �v���C���[�Ƃ̋����𑪒肵�A����ɓK�����U��������
    /// </summary>
    private void RangeMeasurementWithPlayer()
    {
        // �v���C���[�X�V
        _player = _playerManager.Player;

        Vector3 playerPos = _player.transform.position;
        Vector3 myPos = gameObject.transform.position;

        playerPos.y = 0f;
        myPos.y = 0f;

        // Mathf.Pow(_rangeHaveAttackArea, 2)��_rangeHaveAttackArea��2��
        // ����l���v���C���[���߂�������
        if (Vector3.SqrMagnitude(playerPos - myPos) < Mathf.Pow(_rangeHaveAttackArea, 2))
        {
            HitAttack();
        }
        // �v���C���[������l�O�̎�
        else if (Vector3.SqrMagnitude(playerPos - myPos) > Mathf.Pow(_rangeHaveAttackArea, 2))
        {
            FireSphere();
        }
    }

    private void FireSphere()
    {
        if (_isShot) return;      // �d���ōU���͂��Ȃ�

        _characterAnim.NowAnim = "Skill";

        _changeEnemyMoveType.IsMove = false;

        // SE��炷
        _soundEffectManagement.PlayLongDistanceAttackSound(_audioSource);

        _isShot = true;
    }

    private void HitAttack()
    {
        _characterAnim.NowAnim = "Attack";

        //�X�L���G�t�F�N�g
        if (_effectManager != null)
        {
            _effectManager.ShowSpecialAttackEffect(transform);
        }

        _changeEnemyMoveType.IsMove = false;

        _soundEffectManagement.PlayStrongPunchSound(_audioSource);
    }

    private void BiteAttack()
    {
        _characterAnim.NowAnim = "Attack2";

        // �v���C���[�����̍U���͈͂̈ʒu����
        _underPlayerAttackRange.transform.position = _player.transform.position;
        _attackRangeImage[0].transform.position = new Vector3
            (_player.transform.position.x, _imagePositionY, _player.transform.position.z);

        // �v���C���[����߂��U���͈͂̈ʒu����
        float nearPlayerX = Random.Range(_minimumNearPlayerAttackRange, _maximumNearPlayerAttackRange);
        float nearPlayerZ = Random.Range(_minimumNearPlayerAttackRange, _maximumNearPlayerAttackRange);
        Vector3 nearPlayerPos = new Vector3(nearPlayerX, _player.transform.position.y, nearPlayerZ);
        _nearPlayerAttackRange.transform.position = nearPlayerPos;
        _attackRangeImage[1].transform.position = new Vector3(nearPlayerPos.x, _imagePositionY, nearPlayerPos.z);

        // �v���C���[���牓���U���͈͂̈ʒu����
        float farPlayerX = Random.Range(_minimumFarPlayerAttackRange, _maximumFarPlayerAttackRange);
        float farPlayerZ = Random.Range(_minimumFarPlayerAttackRange, _maximumFarPlayerAttackRange);
        Vector3 farPlayerPos = new Vector3(farPlayerX, _player.transform.position.y, farPlayerZ);
        _farPlayerAttackRange.transform.position = farPlayerPos;
        _attackRangeImage[2].transform.position = new Vector3(farPlayerPos.x, _imagePositionY, farPlayerPos.z);


        //�X�L���G�t�F�N�g
        if (_effectManager != null)
        {
            _effectManager.ShowSpecialAttackEffect(transform);
        }

        _changeEnemyMoveType.IsMove = false;

        _soundEffectManagement.PlayStrongPunchSound(_audioSource);
    }

    private void CreateHitAttackArea()
    {
        _isAttack = true;
        _attackArea.SetActive(true);
    }

    private void CreateFireAttackArea()
    {
        _isShot = true;

        // �U���͈͗L����
        _underPlayerAttackRange.SetActive(true);
        _nearPlayerAttackRange.SetActive(true);
        _farPlayerAttackRange.SetActive(true);
    }

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

        int damage = myAttackPower - targetDefensePower;

        if (myAttackPower < targetDefensePower)
        {
            // �h��͂̂ق��������ꍇ�̓_���[�W��1�Ƃ���
            int smallestDamage = 1;
            damage = smallestDamage;
        }

        int hitPointAfterDamage = targetHitPoint - damage;

        _createDamageImage.InstantiateDamageImage(gameObject, targetStatus.gameObject, damage);
        _writeHitPoint.UpdateHitPoint(targetStatus, hitPointAfterDamage);      // targetStatus��HP���X�V
    }

    /// <summary>
    /// ��莞�Ԍ�e���폜����
    /// </summary>
    private void UpdateFireTime()
    {
        if (!_isShot) return;     // �U�����ȊO�͏������s��Ȃ�
        // ���ԉ��Z
        _elapsedTime += Time.deltaTime;

        // �K�莞�ԂɒB���Ă����ꍇ
        if (_elapsedTime > _sphereDeleteTime)
        {
            _characterAnim.NowAnim = "Idle";
            _underPlayerAttackRange.SetActive(false);
            _nearPlayerAttackRange.SetActive(false);
            _farPlayerAttackRange.SetActive(false);
            _elapsedTime = 0f;
            _changeEnemyMoveType.IsMove = true;
            _isShot = false;
        }
    }

    /// <summary>
    /// ��莞�Ԍ�U���͈͂��폜����
    /// </summary>
    private void UpdateHitTime()
    {
        if (!_isAttack) return;     // �U�����ȊO�͏������s��Ȃ�

        // ���ԉ��Z
        _elapsedTime += Time.deltaTime;

        // �K�莞�ԂɒB���Ă����ꍇ
        if (_elapsedTime > _hitAttackDeleteTime)
        {
            _characterAnim.NowAnim = "Idle";
            _attackArea.SetActive(false);
            _elapsedTime = 0f;
            _changeEnemyMoveType.IsMove = true;
            _isAttack = false;
        }
    }
}