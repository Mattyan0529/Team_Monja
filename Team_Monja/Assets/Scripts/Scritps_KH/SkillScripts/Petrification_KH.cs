using UnityEngine;

public class Petrification_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _residentScript = default;

    [SerializeField]
    private EffectManager _effectManager; // EffectManagerの参照を追加

    private PlayerMove_MT _playerMove = default;
    private MonsterRandomWalk_KH _monsterRandomWalk = default;
    private PlayerRangeInJudge_KH _playerRangeInJudge = default;
    private Rigidbody _rigidbody = default;
    private AudioSource _audioSource = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private PlayerSkill_KH _playerSkill = default;
    private PlayerSkill_KH _myPlayerSkill = default;
    private MonsterSkill_KH _monsterSkill = default;
    private NormalAttack_KH _normalAttack = default;
    private PlayerGuard_KH _playerGuard = default;

    private GameObject _petrificationArea;


    private float _deleteTime = 1f;
    private float _elapsedTime = 0f;

    private bool _isPetrification = false;
    private bool _isSphereExists = false;

    void Start()
    {
        // 子オブジェクトの中からPetrificationAreaを取得
        _petrificationArea = transform.Find("PetrificationArea").gameObject;
        _soundEffectManagement = _residentScript.GetComponent<SoundEffectManagement_KH>();
        _myPlayerSkill = GetComponent<PlayerSkill_KH>();

    }

    void Update()
    {
        UpdateTime();
    }

    /// <summary>
    /// 石化範囲のSphereを有効化
    /// </summary>
    public void CreatePetrificationArea()
    {
        _isSphereExists = true;
        _petrificationArea.SetActive(true);

        if (_audioSource == null)
        {
            _audioSource = GetComponentInChildren<AudioSource>();
        }
        // SEを鳴らす
        _soundEffectManagement.PlayMagicSound(_audioSource);

        //スキルエフェクト
        if (_effectManager != null)
        {
            _effectManager.ShowSpecialAttackEffect(transform);
        }
        else
        {
            Debug.LogError("EffectManager component is not found.");
        }
    }

    /// <summary>
    /// 移動系スクリプトと攻撃系スクリプトを無効化する
    /// </summary>
    /// <param name="petrificationTarget">石化対象</param>
    public void Petrification(GameObject petrificationTarget)
    {
        if ((petrificationTarget.CompareTag("Enemy") || petrificationTarget.CompareTag("Boss")) && gameObject.CompareTag("Player"))     // 敵を石化する場合
        {
            _monsterRandomWalk = petrificationTarget.GetComponent<MonsterRandomWalk_KH>();
            _playerRangeInJudge = petrificationTarget.GetComponent<PlayerRangeInJudge_KH>();
            _rigidbody = petrificationTarget.GetComponent<Rigidbody>();
            _monsterSkill = petrificationTarget.GetComponent<MonsterSkill_KH>();

            // 移動系のスクリプトを無効化
            _monsterRandomWalk.enabled = false;
            _playerRangeInJudge.enabled = false;
            _rigidbody.velocity = Vector3.zero;

            // 攻撃系のスクリプトを無効化
            _monsterSkill.enabled = false;

            _isPetrification = true;
        }
        else if (petrificationTarget.CompareTag("Player") &&( gameObject.CompareTag("Enemy") || gameObject.CompareTag("Boss")))       // プレイヤーを石化する場合
        {
            _playerMove = petrificationTarget.GetComponent<PlayerMove_MT>();
            _playerSkill = petrificationTarget.GetComponent<PlayerSkill_KH>();
            petrificationTarget.TryGetComponent<NormalAttack_KH>(out _normalAttack);
            petrificationTarget.TryGetComponent<PlayerGuard_KH>(out _playerGuard);
            _rigidbody = petrificationTarget.GetComponent<Rigidbody>();

            // 移動系のスクリプトを無効化
            _playerMove.enabled = false;
            _rigidbody.velocity = Vector3.zero;

            // 攻撃系のスクリプトを無効化
            _playerSkill.enabled = false;
            if (_normalAttack != null)
            {
                _normalAttack.enabled = false;
            }
            if (_playerGuard != null)
            {
                _playerGuard.enabled = false;
            }

            _isPetrification = true;
        }
    }

    /// <summary>
    /// 移動系スクリプトと攻撃系スクリプトを有効化する
    /// </summary>
    private void PetrificationCancellation()
    {
        if (!_isPetrification) return;

        if (gameObject.CompareTag("Player"))     // 敵の石化を解除する
        {
            if (_monsterRandomWalk != null)
            {
                _monsterRandomWalk.enabled = true;
            }
            else if (_playerRangeInJudge != null)
            {
                _playerRangeInJudge.enabled = true;
            }

            _monsterSkill.enabled = true;

            _isPetrification = false;
        }
        else if (gameObject.CompareTag("Enemy") || gameObject.CompareTag("Boss"))       // プレイヤーの石化を解除する場合
        {
            _playerMove.enabled = true;

            _playerSkill.enabled = true;
            if (_normalAttack != null)
            {
                _normalAttack.enabled = true;
            }
            if (_playerGuard != null)
            {
                _playerGuard.enabled = true;
            }

            _isPetrification = false;
        }
    }

    /// <summary>
    /// 一定時間後石化範囲を削除する
    /// </summary>
    private void UpdateTime()
    {
        if (!_isSphereExists) return;     // Sphereがあるとき以外は処理を行わない

        // 時間加算
        _elapsedTime += Time.deltaTime;

        // 規定時間に達していた場合
        if (_elapsedTime > _deleteTime)
        {
            AreaDestroy();
        }
    }

    private void AreaDestroy()
    {
        _petrificationArea.SetActive(false);
        PetrificationCancellation();
        _elapsedTime = 0f;
        _isSphereExists = false;
        _myPlayerSkill.IsUseSkill = false;
    }

    private void OnDisable()
    {
        AreaDestroy();
        PetrificationCancellation();
    }

    private void OnDestroy()
    {
        AreaDestroy();
        PetrificationCancellation();
    }
}
