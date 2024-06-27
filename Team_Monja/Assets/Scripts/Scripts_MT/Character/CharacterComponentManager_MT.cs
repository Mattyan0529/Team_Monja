using UnityEngine;

public class CharacterComponentManager_MT : MonoBehaviour
{
    // PlayerとEnemyによって変更が生じるコンポーネントたち
    private PlayerMove_MT playerMove;
    private EnemyTriggerManager_MT enemyTriggerManager;
    private CameraManager_MT cameraManager;
    private EatEnemy_MT eatEnemy;
    private EatOrChangeController_MT eatOrChangeController;
    private ClosestEnemyFinder_MT closestEnemyFinder;
    private ChangeCharacter_MT changeCharacter;
    private GameEndCamera_MT gameEndCamera;
    private KillEnemy_MT killEnemy;

    private string _currentTag;

    // キャラ変更した時に倍率を掛けなおす
    private StatusManager_MT statusManager;

    // Start is called before the first frame update
    void Start()
    {
        playerMove = GetComponent<PlayerMove_MT>();
        enemyTriggerManager = GetComponent<EnemyTriggerManager_MT>();
        cameraManager = GetComponent<CameraManager_MT>();
        eatEnemy = GetComponent<EatEnemy_MT>();
        eatOrChangeController = GetComponent<EatOrChangeController_MT>();
        closestEnemyFinder = GetComponent<ClosestEnemyFinder_MT>();
        changeCharacter = GetComponent<ChangeCharacter_MT>();
        gameEndCamera = GetComponent<GameEndCamera_MT>();
        killEnemy = GetComponent<KillEnemy_MT>();


        _currentTag = this.gameObject.tag;

        statusManager = GetComponent<StatusManager_MT>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.tag != _currentTag)
        {
            if (this.gameObject.CompareTag("Player"))
            {

                EnablePlayerComponents();
        
                // ステータスの倍率を掛けなおして、HPをMaxに設定
                if (statusManager != null)
                {
                    statusManager.ApplyMultipliers();
                    statusManager.HealHP(9999999);
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
        enemyTriggerManager.enabled = true;
        eatEnemy.enabled = true;
        eatOrChangeController.enabled = true;
        closestEnemyFinder.enabled = true;
        changeCharacter.enabled = true;
        gameEndCamera.enabled = true;
        killEnemy.enabled = true;
    }

    void DisablePlayerComponents()
    {
        playerMove.enabled = false;
        enemyTriggerManager.enabled = false;
        eatEnemy.enabled = false;
        eatOrChangeController.enabled = false;
        closestEnemyFinder.enabled = false;
        changeCharacter.enabled = false;
        gameEndCamera.enabled = false;
        killEnemy.enabled = false;
    }
}
