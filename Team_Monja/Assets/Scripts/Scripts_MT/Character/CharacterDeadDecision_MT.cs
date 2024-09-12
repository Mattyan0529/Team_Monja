using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeadDecision_MT : MonoBehaviour
{
    private StatusManager_MT _statusManager;
    private CharacterAnim_MT _characterAnim;
    private GameEndCamera_MT _gameEndCamera;
    private LockOn _lockOn;

    // 追記：北
    [SerializeField]
    private GameObject _whisperingWords = default;
    private MonsterSkill_KH _monsterSkill = default;
    private PlayerSkill_KH _playerSkill = default;
    private NormalAttack_KH _normalAttack = default;
    private PlayerGuard_KH _playerGuard = default;
    private EnemyMove_KH _enemyMove = default;
    private GameObject _tagJudgeObj = default;
    private TagJudge_MT _tagJudge = default;
    private DisplayWordInEvent_KH _displayWordInEvent = default;

    private bool _isAlive = true;
    private bool _coroutineSwitch = true;


    void Start()
    {
        _statusManager = GetComponent<StatusManager_MT>();
        _characterAnim = GetComponent<CharacterAnim_MT>();
        _gameEndCamera = GameObject.FindWithTag("CameraPos").GetComponent<GameEndCamera_MT>();
        _lockOn = GameObject.FindWithTag("CameraPos").GetComponent<LockOn>();
        // 追記：北
        _monsterSkill = GetComponent<MonsterSkill_KH>();
        _playerSkill = GetComponent<PlayerSkill_KH>();
        _normalAttack = GetComponent<NormalAttack_KH>();
        _playerGuard = GetComponent<PlayerGuard_KH>();
        _enemyMove = GetComponent<EnemyMove_KH>();
        _tagJudgeObj = GameObject.FindGameObjectWithTag("TagJudge");
        _tagJudge = _tagJudgeObj.GetComponent<TagJudge_MT>();
        _displayWordInEvent = _whisperingWords.GetComponent<DisplayWordInEvent_KH>();

    }

    void LateUpdate()
    {
        if (IsDeadDecision())
        {
            //プレイヤーなら死んだときにスローモーションにする
            if (CompareTag("Player") && _coroutineSwitch)
            {
                StartCoroutine(_gameEndCamera.GameOverCoroutine());
                _coroutineSwitch = false;
            }
            if (_isAlive)
            {
                EnemyStop();
            }
        }

        //ゲームオーバーになったら全キャラクターの動きを止める
        if (_gameEndCamera.IsGameOver)
        {
            Debug.Log(123123123);
            EnemyStop();
            //ロックオンを解除
            _lockOn.CancelLockOn();
        }
    }

    /// <summary>
    /// 死んでいるか
    /// </summary>
    /// <returns></returns>
    public bool IsDeadDecision()
    {
        _tagJudge.ChangeTagJudge();

        if (_statusManager.HP <= 0)
        {
            // 追記：北
            _displayWordInEvent.KillEnemyForFirstTime();

            return true;

        }
        else
        {
            _isAlive = true;
            return false;
        }
    }

    /// <summary>
    /// 食べられたモンスターとプレイヤーの動きを止める　追記：北
    /// </summary>
    private void EnemyStop()
    {
        _enemyMove.enabled = false;
        _monsterSkill.enabled = false;
        _playerSkill.enabled = false;
        if (_normalAttack != null)
        {
            _normalAttack.enabled = false;
            _normalAttack.FinishNormalAttack();
        }
        if (_playerGuard != null)
        {
            _playerGuard.enabled = false;
        }
        if (IsDeadDecision())
        {
            _characterAnim.NowAnim = "Die";
        }
        else
        {
            _characterAnim.NowAnim = "Idle";
        }

        _isAlive = false;
    }


}
