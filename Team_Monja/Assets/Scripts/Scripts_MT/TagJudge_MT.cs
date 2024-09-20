using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagJudge_MT : MonoBehaviour
{
    [Header("PlayerManager")]
    [SerializeField]
    private PlayerMove_MT _playerMove; 
    [SerializeField]
    private EatEnemy_MT _eatEnemy; 
    [SerializeField]
    private ChangeCharacter_MT _changeCharacter;
    [SerializeField]
    private EatOrChangeController_MT _eatOrChange;
    [Header("Camera")]
    [SerializeField]
    private CameraManager_MT _cameraManager;
    [SerializeField]
    private GameEndCamera_MT _gameEnd;
    [Header("ClosestObjectArea")]
    [SerializeField]
    private EnemyTriggerManager_MT _enemyTrigger;
    [Header("DamonHand")]
    [SerializeField]
    private DamonHandPos _damonHand;
    [SerializeField]
    private DragPlayerToBoss_KH _dragPlayer;
    [Header("StrengthBar")]
    [SerializeField]
    private StatusBar_MT _atkBar;
    [Header("DefenseBar")]
    [SerializeField]
    private StatusBar_MT _defBar;
    [Header("NearPlayerArea")]
    [SerializeField]
    private NearPlayerWayPointManager_KH _nearPlayer;
    [Header("MiniMapCamera")]
    [SerializeField]
    private MiniMapCameraMove_KH _miniMapCamera;
    [Header("CompassCenter")]
    [SerializeField]
    private MoveCompass_KH _moveCompass;
    [Header("InvisibleWall")]
    [SerializeField]
    private PlayerEnterWallJudge_KH _wallJudge; 
    [Header("NearPlayerAraの子オブジェクトのPinを入れる")]
    [SerializeField]
    private MiniMapPlayerIcon_KH _pin;
    [Header("VideoCanvasの中身")]
    [SerializeField]
    private VideoPlayerController_MT[] _videoControllers;

    [SerializeField,Header("シーン内の全キャラクター")]
    private GameObject[] _characters;
 



    private GameObject _playerObj;
    private GameObject _bossObj;

    private string _currentTag; //１フレーム前のタグ

   
    void Start()
    {
        SetPlayer();
        _currentTag = _playerObj.tag;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeTagJudge();
    }


    /// <summary>
    /// タグが変わったか
    /// </summary>
    public void ChangeTagJudge()
    {
        if (_playerObj.tag != _currentTag)
        {
            SetPlayer();
            return;
        }

        _currentTag = _playerObj.tag;
    }



    /// <summary>
    ///　各スクリプトのプレイヤーを再設定
    /// </summary>
    public void SetPlayer()
    {
        _playerObj = GameObject.FindWithTag("Player");
        
        _playerObj.GetComponent<StatusManager_MT>().ApplyMultipliers();
        _playerObj.GetComponent<StatusManager_MT>().HealHP(999999);
        _playerObj.GetComponentInChildren<EnemyHP_MT>().TagCheck();
        _playerObj.GetComponentInChildren<EnemyHP_MT>().SetPlayerArea();

        _playerMove.SetPlayer();
        _eatEnemy.SetPlayer();
        _changeCharacter.SetPlayer();
        _eatOrChange.SetPlayer(_playerObj);

        _gameEnd.SetPlayer(_playerObj);
        _cameraManager.FindPlayer(_playerObj);

        _enemyTrigger.SetToPlayer(_playerObj);

        _bossObj = GameObject.FindWithTag("Boss");
        
        _bossObj.GetComponent<BossSkillAttack_KH>().SetPlayer(_playerObj);

        _damonHand.SetPlayer(_playerObj);
        _dragPlayer.SetPlayer(_playerObj);

        _atkBar.SetPlayer(_playerObj);
        _defBar.SetPlayer(_playerObj);

        _nearPlayer.SetPlayer(_playerObj);

        _miniMapCamera.SetPlayer(_playerObj);

        _moveCompass.SetPlayer(_playerObj);

        _wallJudge.SetPlayer(_playerObj);

        _pin.SetPlayer(_playerObj);

        //全動画に対して
        foreach (VideoPlayerController_MT video in _videoControllers)
        {
            video.SetPlayer(_playerObj);
        }

        foreach (GameObject character in _characters)
        {
            character.GetComponent<ChangeEnemyMoveType_KH>().SetPlayer(_playerObj);
            character.GetComponent<AttackAreaJudge_KH>().SetPlayer(_playerObj);
            character.GetComponent<EnemyMove_KH>().SetPlayer(_playerObj);
            character.GetComponent<FollowPlayer_KH>().SetPlayer(_playerObj);

            if (CompareTag("Boss")) return;

            // DeathSpwanSoul_TH のコンポーネントがあるかチェック
            DeathSpwanSoul_TH deathSpwanSoul = character.GetComponent<DeathSpwanSoul_TH>();
            if (deathSpwanSoul != null)
            {
                deathSpwanSoul.SetPlayer(_playerObj);
            }
            else
            {
                Debug.LogWarning(character.name + " に DeathSpwanSoul_TH コンポーネントがありません。");
            }
        }

    }

}
