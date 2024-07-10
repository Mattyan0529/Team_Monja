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
        Debug.Log("tag = " + _playerObj.tag);
    }


    /// <summary>
    /// タグが変わったか
    /// </summary>
    public void ChangeTagJudge()
    {
        if (_playerObj.tag != _currentTag)
        {
            SetPlayer();
            Debug.Log("tagchanged");
            return;
        }

        _currentTag = _playerObj.tag;
        Debug.Log("currentTag = " + _currentTag);
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

        _playerManager.GetComponent<PlayerMove_MT>().SetPlayer();
        _playerManager.GetComponent<EatEnemy_MT>().SetPlayer();
        _playerManager.GetComponent<ChangeCharacter_MT>().SetPlayer();

        _cameraObj.GetComponent<GameEndCamera_MT>().SetPlayer();
        _cameraObj.GetComponent<CameraManager_MT>().FindPlayer();
        _closeObjectArea.GetComponent<EnemyTriggerManager_MT>().SetToPlayer();

    }

}
