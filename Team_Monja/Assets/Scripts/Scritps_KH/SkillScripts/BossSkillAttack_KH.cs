using Unity.VisualScripting;
using UnityEngine;

public class BossSkillAttack_KH : MonoBehaviour, IDamagable_KH
{
    [SerializeField]
    private float _rangeSwitchAttack = 7f;

    [SerializeField]
    private GameObject[] _attackRangeImage = default;

    //武田
    [SerializeField]
    private FlameDelay_TH _FlameDelay_TH; // FlameDelay_THの参照を追加しました。

    //松本
    private CharacterAnim_MT _characterAnim = default;

    private float _sphereDeleteTime = 2f;
    private float _hitAttackDeleteTime = 0.5f;
    private float _elapsedTime = 0f;

    private float _imagePositionY = 18.34f;

    private AudioSource _audioSource = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private WriteHitPoint_KH _writeHitPoint = default;
    private CreateDamageImage_KH _createDamageImage = default;
    private ChangeEnemyMoveType_KH _changeEnemyMoveType = default;

    private GameObject _player = default;
    private GameObject _residentScript = default;

    #region FireSphere

    private bool _isShot = false;

    private GameObject[] _fireAttackAreas;

    private GameObject _underPlayerAttackRange = default;
    private GameObject _nearPlayerAttackRange = default;
    private GameObject _farPlayerAttackRange = default;

    float _minimumNearPlayerAttackRange = -10f;
    float _maximumNearPlayerAttackRange = 10f;

    float _minimumFarPlayerAttackRange = -30f;
    float _maximumFarPlayerAttackRange = 30f;

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

        // HitAttackAreaを取得
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
    /// プレイヤーを設定
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
    /// プレイヤーとの距離を測定し、それに適した攻撃をする
    /// </summary>
    private void RangeMeasurementWithPlayer()
    {
        Vector3 playerPos = _player.transform.position;
        Vector3 myPos = gameObject.transform.position;

        playerPos.y = 0f;
        myPos.y = 0f;

        // Mathf.Pow(_rangeSwitchAttack, 2)は_rangeSwitchAttackの2乗
        // 既定値よりプレイヤーが近かったら
        if (Vector3.SqrMagnitude(playerPos - myPos) < Mathf.Pow(_rangeSwitchAttack, 2))
        {
            HitAttack();
        }
        // プレイヤーが既定値外の時
        else if (Vector3.SqrMagnitude(playerPos - myPos) > Mathf.Pow(_rangeSwitchAttack, 2))
        {
            FireSphere();
        }
    }

    private void FireSphere()
    {
        // 重複で攻撃はしない
        if (_isShot) return;   

        // プレイヤー直下の攻撃範囲の位置決定
        _attackRangeImage[0].transform.position = new Vector3
            (_player.transform.position.x, _imagePositionY, _player.transform.position.z);
        _attackRangeImage[0].SetActive(true);
        _underPlayerAttackRange.transform.position = _player.transform.position;

        // プレイヤーから近い攻撃範囲の位置決定
        float nearPlayerX = Random.Range(_minimumNearPlayerAttackRange, _maximumNearPlayerAttackRange);
        float nearPlayerZ = Random.Range(_minimumNearPlayerAttackRange, _maximumNearPlayerAttackRange);
        Vector3 nearPlayerPos = new Vector3
            (_player.transform.position.x + nearPlayerX, _player.transform.position.y, _player.transform.position.z + nearPlayerZ);
        _attackRangeImage[1].transform.position = new Vector3(nearPlayerPos.x, _imagePositionY, nearPlayerPos.z);
        _attackRangeImage[1].SetActive(true);
        _nearPlayerAttackRange.transform.position = nearPlayerPos;

        // プレイヤーから遠い攻撃範囲の位置決定
        float farPlayerX = Random.Range(_minimumFarPlayerAttackRange, _maximumFarPlayerAttackRange);
        float farPlayerZ = Random.Range(_minimumFarPlayerAttackRange, _maximumFarPlayerAttackRange);
        Vector3 farPlayerPos = new Vector3
            (_player.transform.position.x + farPlayerX, _player.transform.position.y, _player.transform.position.z + farPlayerZ);
        _attackRangeImage[2].transform.position = new Vector3(farPlayerPos.x, _imagePositionY, farPlayerPos.z);
        _attackRangeImage[2].SetActive(true);
        _farPlayerAttackRange.transform.position = farPlayerPos;

        // ボスの動きに合わせて火が動かないように
        _underPlayerAttackRange.transform.parent = null;
        _nearPlayerAttackRange.transform.parent = null;
        _farPlayerAttackRange.transform.parent = null;

        _characterAnim.NowAnim = "Skill";

        _changeEnemyMoveType.IsMove = false;

        // SEを鳴らす
        _soundEffectManagement.PlayLongDistanceAttackSound(_audioSource);

        _isShot = true;

        //コルーチンを開始して指定時間後に処理を実行
        _FlameDelay_TH.StartFireSphereCoroutine(_player.transform.position, nearPlayerPos, farPlayerPos, _audioSource, _soundEffectManagement, _changeEnemyMoveType, _characterAnim, _writeHitPoint, _createDamageImage);
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
        _isShot = true;

        // 攻撃範囲有効化
        _underPlayerAttackRange.SetActive(true);
        _nearPlayerAttackRange.SetActive(true);
        _farPlayerAttackRange.SetActive(true);
    }

    public void HitDecision(GameObject hitObj)
    {
        // 相手と自分のStatusManagerが両方必要
        StatusManager_MT targetStatusManager = hitObj.gameObject.GetComponent<StatusManager_MT>();
        StatusManager_MT myStatusManager = GetComponent<StatusManager_MT>();

        HitPointCalculation(myStatusManager, targetStatusManager);
    }

    /// <summary>
    /// ダメージ計算
    /// </summary>
    public void HitPointCalculation(StatusManager_MT myStatus, StatusManager_MT targetStatus)
    {
        int myAttackPower = myStatus.Strength;        // 自分の攻撃力をgetしてくる
        int targetDefensePower = targetStatus.Defense;        // 相手の防御力をgetしてくる
        int targetHitPoint = targetStatus.HP;        // 相手のHPをgetしてくる

        int damage = myAttackPower - targetDefensePower;

        if (myAttackPower < targetDefensePower)
        {
            // 防御力のほうが高い場合はダメージを1とする
            int smallestDamage = 1;
            damage = smallestDamage;
        }

        int hitPointAfterDamage = targetHitPoint - damage;

        _createDamageImage.InstantiateDamageImage(targetStatus.gameObject, damage);
        _writeHitPoint.UpdateHitPoint(targetStatus, hitPointAfterDamage);      // targetStatusのHPを更新
    }

    /// <summary>
    /// 一定時間後範囲を削除する
    /// </summary>
    private void UpdateFireTime()
    {
        if (!_isShot) return;     // 攻撃中以外は処理を行わない
        // 時間加算
        _elapsedTime += Time.deltaTime;

        // 規定時間に達していた場合
        if (_elapsedTime > _sphereDeleteTime)
        {
            _characterAnim.NowAnim = "Idle";
            _underPlayerAttackRange.SetActive(false);
            _nearPlayerAttackRange.SetActive(false);
            _farPlayerAttackRange.SetActive(false);

            _underPlayerAttackRange.transform.parent = gameObject.transform;
            _nearPlayerAttackRange.transform.parent = gameObject.transform;
            _farPlayerAttackRange.transform.parent = gameObject.transform;

            _attackRangeImage[0].SetActive(true);
            _attackRangeImage[1].SetActive(true);
            _attackRangeImage[2].SetActive(true);

            _elapsedTime = 0f;
            _changeEnemyMoveType.IsMove = true;
            _isShot = false;
        }
    }

    /// <summary>
    /// 一定時間後攻撃範囲を削除する
    /// </summary>
    private void UpdateHitTime()
    {
        if (!_isAttack) return;     // 攻撃中以外は処理を行わない

        // 時間加算
        _elapsedTime += Time.deltaTime;

        // 規定時間に達していた場合
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