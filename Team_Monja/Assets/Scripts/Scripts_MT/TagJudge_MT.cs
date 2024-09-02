using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagJudge_MT : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerManager;
    [SerializeField]
    private GameObject _cameraObj;
    [SerializeField]
    private GameObject _closeObjectArea;
    [SerializeField]
    private GameObject _MiniMapIcon;
    [SerializeField]
    private GameObject _damonHand; 
    [SerializeField]
    private GameObject _atkBar;  
    [SerializeField]
    private GameObject _defBar;
    [SerializeField]
    private GameObject _nearPlayerArea;
    [SerializeField]
    private GameObject _miniMapCamera;  
    [SerializeField]
    private GameObject _compassCenter;
    [SerializeField,Header("シーン内の全キャラクター")]
    private GameObject[] _characters;


    private GameObject _playerObj;

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


        _playerManager.GetComponent<PlayerMove_MT>().SetPlayer();
        _playerManager.GetComponent<EatEnemy_MT>().SetPlayer();
        _playerManager.GetComponent<ChangeCharacter_MT>().SetPlayer();

        _cameraObj.GetComponent<GameEndCamera_MT>().SetPlayer(_playerObj);
        _cameraObj.GetComponent<CameraManager_MT>().FindPlayer(_playerObj);
        _closeObjectArea.GetComponent<EnemyTriggerManager_MT>().SetToPlayer(_playerObj);


        _playerObj.GetComponent<StatusManager_MT>().ApplyMultipliers();
        _playerObj.GetComponent<StatusManager_MT>().HealHP(999999);
        _playerObj.GetComponentInChildren<EnemyHP_MT>().TagCheck();
        _playerObj.GetComponentInChildren<EnemyHP_MT>().SetPlayerArea();


        _MiniMapIcon.GetComponent<PlayerMinimapIconSet>().SetPlayer(_playerObj);

        _damonHand.GetComponent<DamonHandPos>().SetPlayer(_playerObj);

        _atkBar.GetComponent<StatusBar_MT>().SetPlayer(_playerObj);
        _defBar.GetComponent<StatusBar_MT>().SetPlayer(_playerObj);

        _nearPlayerArea.GetComponent<NearPlayerWayPointManager_KH>().SetPlayer(_playerObj);

        _miniMapCamera.GetComponent<MiniMapCameraMove_KH>().SetPlayer(_playerObj);

        _compassCenter.GetComponent<MoveCompass_KH>().SetPlayer(_playerObj);

        //キャラクター全員に対して行う
        foreach (GameObject character in _characters)
        {
            character.GetComponent<ChangeEnemyMoveType_KH>().SetPlayer(_playerObj);
            character.GetComponent<AttackAreaJudge_KH>().SetPlayer(_playerObj);
            character.GetComponent<EnemyMove_KH>().SetPlayer(_playerObj);
            character.GetComponent<DragPlayerToBoss_KH>().SetPlayer(_playerObj);
        }
    }

}
