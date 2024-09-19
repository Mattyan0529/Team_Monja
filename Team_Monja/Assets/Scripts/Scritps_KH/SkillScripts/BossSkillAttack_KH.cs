using UnityEngine;

public class BossSkillAttack_KH : MonoBehaviour, IDamagable_KH
{
    [SerializeField]
    private float _rangeSwitchAttack = 7f;

    [SerializeField]
    private GameObject[] _attackRangeImage = default;

    [SerializeField]
    private GameObject[] _pillarOfFire = default;

    [SerializeField]
    private GameObject _bossFollowArea = default;

    //���{
    private CharacterAnim_MT _characterAnim = default;

    private float _sphereDeleteTime = 1f;
    private float _hitAttackDeleteTime = 0.5f;
    private float _timeFromPredictionToAttack = 1.5f;
    private float _elapsedTime = 0f;

    private float _imagePositionY = 18.34f;

    private AudioSource _audioSource = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private WriteHitPoint_KH _writeHitPoint = default;
    private CreateDamageImage_KH _createDamageImage = default;
    private ChangeEnemyMoveType_KH _changeEnemyMoveType = default;
    private BoxCollider _pillarOfFireBounds = default;

    private GameObject _player = default;
    private GameObject _residentScript = default;

    #region FireSphere

    private bool _isShot = false;

    private GameObject[] _fireAttackAreas;

    private GameObject _underPlayerAttackRange = default;
    private GameObject _nearPlayerAttackRange = default;
    private GameObject _farPlayerAttackRange = default;

    private GameObject _underPlayerPillar = default;
    private GameObject _nearPlayerPillar = default;
    private GameObject _farPlayerPillar = default;

    private float _minimumNearPlayerAttackRange = -10f;
    private float _maximumNearPlayerAttackRange = 10f;

    private float _minimumFarPlayerAttackRange = -30f;
    private float _maximumFarPlayerAttackRange = 30f;

    #endregion

    #region HitAttack

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
        _changeEnemyMoveType = GetComponent<ChangeEnemyMoveType_KH>();
        _pillarOfFireBounds = _bossFollowArea.GetComponent<BoxCollider>();
   

        #region FireAttack

        _fireAttackAreas = GameObject.FindGameObjectsWithTag("FireAttackArea");
        _underPlayerAttackRange = _fireAttackAreas[0];
        _nearPlayerAttackRange = _fireAttackAreas[1];
        _farPlayerAttackRange = _fireAttackAreas[2];

        _underPlayerPillar = _pillarOfFire[0];
        _nearPlayerPillar = _pillarOfFire[1];
        _farPlayerPillar = _pillarOfFire[2];

        _underPlayerAttackRange.SetActive(false);
        _nearPlayerAttackRange.SetActive(false);
        _farPlayerAttackRange.SetActive(false);

        _underPlayerPillar.SetActive(false);
        _nearPlayerPillar.SetActive(false);
        _farPlayerPillar.SetActive(false);

        _attackRangeImage[0].SetActive(false);
        _attackRangeImage[1].SetActive(false);
        _attackRangeImage[2].SetActive(false);

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


    /// <summary>
    /// �v���C���[��ݒ�
    /// </summary>
    /// <param name="player"></param>
    public void SetPlayer(GameObject player)
    {
        _player = player;
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
        _elapsedTime = 0f;

        Vector3 playerPos = _player.transform.position;
        Vector3 myPos = gameObject.transform.position;

        playerPos.y = 0f;
        myPos.y = 0f;

        // Mathf.Pow(_rangeSwitchAttack, 2)��_rangeSwitchAttack��2��
        // ����l���v���C���[���߂�������
        if (Vector3.SqrMagnitude(playerPos - myPos) < Mathf.Pow(_rangeSwitchAttack, 2))
        {
            HitAttack();
        }
        // �v���C���[������l�O�̎�
        else if (Vector3.SqrMagnitude(playerPos - myPos) > Mathf.Pow(_rangeSwitchAttack, 2))
        {
            FireSphere();
        }
    }

    private void FireSphere()
    {
        // �d���ōU���͂��Ȃ�
        if (_isShot) return;

        // �v���C���[�����̍U���͈͂̈ʒu����
        _attackRangeImage[0].transform.position = new Vector3
            (_player.transform.position.x, _imagePositionY, _player.transform.position.z);
        _attackRangeImage[0].SetActive(true);

        _underPlayerPillar.transform.position = _player.transform.position;

        _underPlayerAttackRange.transform.position = _player.transform.position;
        _underPlayerAttackRange.transform.parent = null;

        Vector3 nearPlayerPos;

        // �U���͈͂�_pillarOfFireBounds�͈̔͊O�̊ԌJ��Ԃ�
        do
        {
            // �v���C���[����߂��U���͈͂̈ʒu����
            float nearPlayerX = Random.Range(_minimumNearPlayerAttackRange, _maximumNearPlayerAttackRange);
            float nearPlayerZ = Random.Range(_minimumNearPlayerAttackRange, _maximumNearPlayerAttackRange);
            nearPlayerPos = new Vector3
                (_player.transform.position.x + nearPlayerX, _player.transform.position.y, _player.transform.position.z + nearPlayerZ);
        } while (!_pillarOfFireBounds.bounds.Contains(nearPlayerPos));

        // �U���͈͂̈ʒu���f
        _attackRangeImage[1].transform.position = new Vector3(nearPlayerPos.x, _imagePositionY, nearPlayerPos.z);
        _attackRangeImage[1].SetActive(true);

        _nearPlayerPillar.transform.position = nearPlayerPos;

        _nearPlayerAttackRange.transform.position = nearPlayerPos;
        _nearPlayerAttackRange.transform.parent = null;

        Vector3 farPlayerPos;

        // �U���͈͂�_pillarOfFireBounds�͈̔͊O�̊ԌJ��Ԃ�
        do
        {
            // �v���C���[���牓���U���͈͂̈ʒu����
            float farPlayerX = Random.Range(_minimumFarPlayerAttackRange, _maximumFarPlayerAttackRange);
            float farPlayerZ = Random.Range(_minimumFarPlayerAttackRange, _maximumFarPlayerAttackRange);
            farPlayerPos = new Vector3
                (_player.transform.position.x + farPlayerX, _player.transform.position.y, _player.transform.position.z + farPlayerZ);
        } while (!_pillarOfFireBounds.bounds.Contains(farPlayerPos));

        // �U���͈͂̈ʒu���f
        _attackRangeImage[2].transform.position = new Vector3(farPlayerPos.x, _imagePositionY, farPlayerPos.z);
        _attackRangeImage[2].SetActive(true);

        _farPlayerPillar.transform.position = farPlayerPos;

        _farPlayerAttackRange.transform.position = farPlayerPos;
        _farPlayerAttackRange.transform.parent = null;

        _changeEnemyMoveType.IsMove = false;

        // SE��炷
        _soundEffectManagement.PlayLongDistanceAttackSound(_audioSource);

        _characterAnim.NowAnim = "Skill";
        Invoke("CreateFireAttackArea", _timeFromPredictionToAttack);
    }

    private void HitAttack()
    {
        _characterAnim.NowAnim = "Attack";

        _changeEnemyMoveType.IsMove = false;

        _soundEffectManagement.PlayStrongPunchSound(_audioSource);
    }

    private void BiteAttack()
    {
        _characterAnim.NowAnim = "Attack2";

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
        // �U���͈͗L����
        _underPlayerAttackRange.SetActive(true);
        _nearPlayerAttackRange.SetActive(true);
        _farPlayerAttackRange.SetActive(true);

        // �Β��I��
        _underPlayerPillar.SetActive(true);
        _nearPlayerPillar.SetActive(true);
        _farPlayerPillar.SetActive(true);

        _isShot = true;
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

        _createDamageImage.InstantiateDamageImage(targetStatus.gameObject, damage);
        _writeHitPoint.UpdateHitPoint(targetStatus, hitPointAfterDamage);      // targetStatus��HP���X�V
    }

    /// <summary>
    /// ��莞�Ԍ�͈͂��폜����
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

            _attackRangeImage[0].SetActive(false);
            _attackRangeImage[1].SetActive(false);
            _attackRangeImage[2].SetActive(false);

            // �Β��I�t
            _underPlayerPillar.SetActive(false);
            _nearPlayerPillar.SetActive(false);
            _farPlayerPillar.SetActive(false);

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