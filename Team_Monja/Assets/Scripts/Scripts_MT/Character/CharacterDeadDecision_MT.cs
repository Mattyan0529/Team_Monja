using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeadDecision_MT : MonoBehaviour
{
    private StatusManager_MT _statusManager;
    private CharacterAnim_MT _characterAnim;
    private GameEndCamera_MT _gameEndCamera;
    private LockOn _lockOn;

    [SerializeField]
    private ResultCounter _resultCounter;
    [SerializeField]
    private GameObject _whisperingWords = default;
    private MonsterSkill_KH _monsterSkill;
    private PlayerSkill_KH _playerSkill;
    private NormalAttack_KH _normalAttack;
    private PlayerGuard_KH _playerGuard;
    private EnemyMove_KH _enemyMove;
    private GameObject _tagJudgeObj;
    private TagJudge_MT _tagJudge;
    private DisplayWordInEvent_KH _displayWordInEvent;

    private bool _isAlive = true;
    private bool _coroutineSwitch = true;

    void Start()
    {
        _statusManager = GetComponent<StatusManager_MT>();
        _characterAnim = GetComponent<CharacterAnim_MT>();
        _gameEndCamera = GameObject.FindWithTag("CameraPos").GetComponent<GameEndCamera_MT>();
        _lockOn = GameObject.FindWithTag("CameraPos").GetComponent<LockOn>();

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
            if (CompareTag("Player") && _coroutineSwitch)
            {
                StartCoroutine(_gameEndCamera.GameOverCoroutine());
                _coroutineSwitch = false;
            }
            else if (CompareTag("Boss") && _coroutineSwitch)
            {
                StartCoroutine(_gameEndCamera.GameClearCoroutine());
                _coroutineSwitch = false;
            }
            if (_isAlive)
            {
                EnemyStop();
            }
        }

        if (_gameEndCamera.IsGameOver)
        {
            Debug.Log("ゲームオーバー処理が実行されました");
            EnemyStop();
            _lockOn.CancelLockOn();
        }
    }

    public bool IsDeadDecision()
    {
        _tagJudge.ChangeTagJudge();

        if (_statusManager.HP <= 0)
        {
            _displayWordInEvent.KillEnemyForFirstTime();
            return true;
        }
        else
        {
            _isAlive = true;
            return false;
        }
    }

    private void EnemyStop()
    {
        if (_enemyMove != null) _enemyMove.enabled = false;
        if (_monsterSkill != null) _monsterSkill.enabled = false;
        if (_playerSkill != null) _playerSkill.enabled = false;
        if (_normalAttack != null)
        {
            _normalAttack.enabled = false;
            _normalAttack.FinishNormalAttack();
        }
        if (_playerGuard != null) _playerGuard.enabled = false;
        if (IsDeadDecision())
        {
            _characterAnim.NowAnim = "Die";
        }
        else
        {
            _characterAnim.NowAnim = "Idle";
        }

        //増やすスクリプト予定地
        
        _isAlive = false;
    }
}
