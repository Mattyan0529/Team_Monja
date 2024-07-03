using UnityEngine;

public class CharacterComponentManager_MT : MonoBehaviour
{
    // PlayerとEnemyによって変更が生じるコンポーネントたち
    private PlayerMove_MT playerMove;
    private CameraManager_MT cameraManager;
    private EatEnemy_MT eatEnemy;
    private EatOrChangeController_MT eatOrChangeController;
    private ClosestEnemyFinder_MT closestEnemyFinder;
    private ChangeCharacter_MT changeCharacter;
    private KillEnemy_MT killEnemy;

    private string _currentTag;

    // キャラ変更した時に倍率を掛けなおす
    private StatusManager_MT statusManager;
    //敵のHPバーを変更時に更新
    private EnemyHP_MT enemyHP;

    private EnemyTriggerManager_MT enemyTriggerManager;

    // Start is called before the first frame update
    void Start()
    {
        playerMove = GetComponent<PlayerMove_MT>();     
        cameraManager = GetComponent<CameraManager_MT>();
        eatEnemy = GetComponent<EatEnemy_MT>();
        eatOrChangeController = GetComponent<EatOrChangeController_MT>();
        closestEnemyFinder = GetComponent<ClosestEnemyFinder_MT>();
        changeCharacter = GetComponent<ChangeCharacter_MT>();
        killEnemy = GetComponent<KillEnemy_MT>();


        _currentTag = this.gameObject.tag;

        statusManager = GetComponent<StatusManager_MT>();
        //子オブジェクトからコンポーネントを取得
        enemyHP = GetComponentInChildren<EnemyHP_MT>();

        enemyTriggerManager = GameObject.FindWithTag("NearTrigger").GetComponent<EnemyTriggerManager_MT>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.tag != _currentTag)
        {
            if (this.gameObject.CompareTag("Player"))
            {

                EnablePlayerComponents();
        
                //キャラクターがプレイヤーになったときの処理
                if (statusManager != null)
                {
                    cameraManager = Camera.main.GetComponent<CameraManager_MT>();
                    cameraManager.CameraSwitch();
                    enemyTriggerManager.SetToPlayer();

                    statusManager.ApplyMultipliers();
                    statusManager.HealHP(9999999);
                    enemyHP.TagCheck();
                    enemyHP.SetPlayerArea();

                }
            }
            else if (this.gameObject.CompareTag("Enemy")|| this.gameObject.CompareTag("Boss"))
            {
                DisablePlayerComponents();
            }
            _currentTag = this.gameObject.tag;
        }
    }

    void EnablePlayerComponents()
    {
        playerMove.enabled = true;
        eatEnemy.enabled = true;
        eatOrChangeController.enabled = true;
        closestEnemyFinder.enabled = true;
        changeCharacter.enabled = true;
        killEnemy.enabled = true;
    }

    void DisablePlayerComponents()
    {
        playerMove.enabled = false;
        eatEnemy.enabled = false;
        eatOrChangeController.enabled = false;
        closestEnemyFinder.enabled = false;
        changeCharacter.enabled = false;
        killEnemy.enabled = false;
    }
}
