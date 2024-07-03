using UnityEngine;

public class CharacterComponentManager_MT : MonoBehaviour
{
    // Player��Enemy�ɂ���ĕύX��������R���|�[�l���g����
    private PlayerMove_MT playerMove;
    private CameraManager_MT cameraManager;
    private EatEnemy_MT eatEnemy;
    private EatOrChangeController_MT eatOrChangeController;
    private ClosestEnemyFinder_MT closestEnemyFinder;
    private ChangeCharacter_MT changeCharacter;
    private KillEnemy_MT killEnemy;

    private string _currentTag;

    // �L�����ύX�������ɔ{�����|���Ȃ���
    private StatusManager_MT statusManager;
    //�G��HP�o�[��ύX���ɍX�V
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
        //�q�I�u�W�F�N�g����R���|�[�l���g���擾
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
        
                //�L�����N�^�[���v���C���[�ɂȂ����Ƃ��̏���
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
