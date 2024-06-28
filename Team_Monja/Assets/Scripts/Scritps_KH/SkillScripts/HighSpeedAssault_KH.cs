using UnityEngine;

public class HighSpeedAssault_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _residentScript;

    private WriteHitPoint_KH _writeHitPoint = default;
    private MonsterRandomWalk_KH _monsterRandomWalk = default;
    private PlayerMove_MT _playerMove = default;
    private Rigidbody _rigidbody = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private AudioSource _audioSource = default;
    private CreateDamageImage _createDamageImage = default;

    private StatusManager_MT _myStatusManager = default;        // 自分のステータス
    private StatusManager_MT _targetStatusManager = default;    // 攻撃相手のステータス

    private bool _isSpeedUp = false;

    private float _addForce = 20f;

    private float _updateTime = 1f;
    private float _elapsedTime = 0f;

    void Start()
    {
        _writeHitPoint = _residentScript.GetComponent<WriteHitPoint_KH>();
        _createDamageImage = _residentScript.GetComponent<CreateDamageImage>();
        _soundEffectManagement = _residentScript.GetComponent<SoundEffectManagement_KH>();
        _rigidbody = GetComponent<Rigidbody>();

        if (gameObject.tag == "Enemy" || gameObject.tag == "Boss")
        {
            _monsterRandomWalk = GetComponent<MonsterRandomWalk_KH>();
        }
        else if (gameObject.tag == "Player")
        {
            _playerMove = GetComponent<PlayerMove_MT>();
        }
    }

    void Update()
    {
        UpdateTime();       // 一定時間経過後に速度を下げる
    }

    /// <summary>
    /// 突撃開始
    /// </summary>
    public void SpeedUp()
    {
        // すでに加速中のときと、ランダム移動中は加速しない
        if (_isSpeedUp) return;
        if ((gameObject.CompareTag("Enemy") || gameObject.CompareTag("Boss")) && _monsterRandomWalk.enabled) return;

        if (gameObject.CompareTag("Enemy") || gameObject.CompareTag("Boss"))
        {
            _monsterRandomWalk.enabled = false;     // 方向転換をオフ
            _rigidbody.AddForce(transform.forward * _addForce, ForceMode.Impulse);

            if (_audioSource == null)
            {
                _audioSource = GetComponentInChildren<AudioSource>();
            }

            // SEが鳴る
            _soundEffectManagement.PlaySlashAttackSound(_audioSource);
        }
        else if (gameObject.CompareTag("Player"))
        {
            if (_playerMove == null)
            {
                _playerMove = GetComponentInChildren<PlayerMove_MT>();
            }

            _playerMove.enabled = false;        // 方向転換をオフ
            _rigidbody.AddForce(transform.forward * _addForce, ForceMode.Impulse);

            if (_audioSource == null)
            {
                _audioSource = GetComponentInChildren<AudioSource>();
            }
            // SEを鳴らす
            _soundEffectManagement.PlaySlashAttackSound(_audioSource);
        }

        _isSpeedUp = true;
    }

    /// <summary>
    /// 突撃終了
    /// </summary>
    private void SpeedDown()
    {
        if (gameObject.CompareTag("Enemy") || gameObject.CompareTag("Boss"))
        {
            _monsterRandomWalk.enabled = true;     // 方向転換をオン
        }
        else if (gameObject.CompareTag("Player"))
        {
            _playerMove.enabled = true;        // 方向転換をオン
        }

        _soundEffectManagement.StopSound(_audioSource);
        _isSpeedUp = false;
    }

    /// <summary>
    /// スキル発動から一定時間経過後方向転換を許可する
    /// </summary>
    private void UpdateTime()
    {
        if (!_isSpeedUp)
        {
            _elapsedTime = 0f;
            return;
        }

        // 時間加算
        _elapsedTime += Time.deltaTime;

        // 規定時間に達していた場合
        if (_elapsedTime > _updateTime)
        {
            SpeedDown();
            _elapsedTime = 0f;
        }
    }

    /// <summary>
    /// 減らすHPの計算
    /// </summary>
    public void HitPointCalculation(StatusManager_MT myStatus, StatusManager_MT targetStatus)
    {
        int myAttackPower = myStatus.Strength;        // 自分の攻撃力をgetしてくる
        int targetDefensePower = targetStatus.Defense;        // 相手の防御力をgetしてくる
        int targetHitPoint = targetStatus.HP;        // 相手のHPをgetしてくる

        if (myAttackPower < targetDefensePower) return;        // 防御力のほうが高かったら0ダメージ

        int damage = targetHitPoint - (myAttackPower - targetDefensePower);
        _writeHitPoint.UpdateHitPoint(targetStatus, damage);      // targetStatusesのHPを更新
        _createDamageImage.InstantiateDamageImage(gameObject, myAttackPower - targetDefensePower);

        SpeedDown();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isSpeedUp) return;        // 高速突撃中でなければ処理を行わない
        if (collision.gameObject.GetComponent<PlayerGuard_KH>() && 
            !collision.gameObject.GetComponent<PlayerGuard_KH>().IsGuard) return;       // ガード中であれば攻撃無効

        if ((gameObject.CompareTag("Enemy") || gameObject.CompareTag("Boss")) && collision.gameObject.CompareTag("Player"))       // 自分がモンスターで相手がプレイヤーだった場合
        {
            _soundEffectManagement.StopSound(_audioSource);
            _soundEffectManagement.PlaySlowPunchSound(_audioSource);

            _myStatusManager = GetComponent<StatusManager_MT>();
            _targetStatusManager = collision.gameObject.GetComponent<StatusManager_MT>();
            HitPointCalculation(_myStatusManager, _targetStatusManager);
        }
        else if (gameObject.CompareTag("Player") && (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss")))       // 自分がプレイヤーで相手がモンスターだった場合
        {
            _soundEffectManagement.StopSound(_audioSource);
            _soundEffectManagement.PlaySlowPunchSound(_audioSource);

            _myStatusManager = GetComponent<StatusManager_MT>();
            _targetStatusManager = collision.gameObject.GetComponent<StatusManager_MT>();
            HitPointCalculation(_myStatusManager, _targetStatusManager);
        }
    }
}
