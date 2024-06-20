using UnityEngine;

public class Petrification_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _residentScript = default;

    private PlayerMove_MT _playerMove = default;
    private MonsterRandomWalk_KH _monsterRandomWalk = default;
    private PlayerRangeInJudge_KH _playerRangeInJudge = default;
    private Rigidbody _rigidbody = default;
    private AudioSource _audioSource = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;

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
    }

    /// <summary>
    /// 移動系スクリプトを無効化する
    /// </summary>
    /// <param name="petrificationTarget">石化対象</param>
    public void Petrification(GameObject petrificationTarget)
    {
        if (petrificationTarget.CompareTag("Enemy") && gameObject.CompareTag("Player"))     // 敵を石化する場合
        {
            _monsterRandomWalk = petrificationTarget.GetComponent<MonsterRandomWalk_KH>();
            _playerRangeInJudge = petrificationTarget.GetComponent<PlayerRangeInJudge_KH>();
            _rigidbody = petrificationTarget.GetComponent<Rigidbody>();

            // 移動系のスクリプトを無効化
            _monsterRandomWalk.enabled = false;
            _playerRangeInJudge.enabled = false;
            _rigidbody.velocity = Vector3.zero;

            _isPetrification = true;
        }
        else if (petrificationTarget.CompareTag("Player") && gameObject.CompareTag("Enemy"))       // プレイヤーを石化する場合
        {
            _playerMove = petrificationTarget.GetComponent<PlayerMove_MT>();
            _rigidbody = petrificationTarget.GetComponent<Rigidbody>();

            // 移動系のスクリプトを無効化
            _playerMove.enabled = false;
            _rigidbody.velocity = Vector3.zero;

            _isPetrification = true;
        }
    }

    /// <summary>
    /// 移動系スクリプトを有効化する
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

            _isPetrification = false;
        }
        else if (gameObject.CompareTag("Enemy"))       // プレイヤーの石化を解除する場合
        {
            _playerMove.enabled = true;

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
            _petrificationArea.SetActive(false);
            PetrificationCancellation();
            _elapsedTime = 0f;
            _isSphereExists = false;
        }
    }

    private void OnDisable()
    {
        PetrificationCancellation();
    }
}
