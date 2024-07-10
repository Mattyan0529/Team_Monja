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
        cameraManager = GameObject.FindWithTag("CameraPos").GetComponent<CameraManager_MT>();
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
                //キャラクターがプレイヤーになったときの処理
                if (statusManager != null)
                {
                    enemyTriggerManager.SetToPlayer();

                    statusManager.ApplyMultipliers();
                    
                    enemyHP.TagCheck();
                    enemyHP.SetPlayerArea();

                }
            }
            _currentTag = this.gameObject.tag;
        }
    }
}
