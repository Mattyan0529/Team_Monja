using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillEnemy_MT : MonoBehaviour
{
    // �ł��߂��I�u�W�F�N�g���擾����X�N���v�g
    private ClosestEnemyFinder_MT _closestEnemyFinder;
    // �g���K�[���̓G���Ǘ�����X�N���v�g
    private EnemyTriggerManager_MT _enemyTriggerManager;
    //���񂾂Ƃ��̃J�����𐧌䂷��X�N���v�g
    private GameEndCamera_MT _gameEndCamera;

    private bool _coroutineSwitch = true;

    private void Start()
    {
        _closestEnemyFinder = GetComponent<ClosestEnemyFinder_MT>();
        _enemyTriggerManager = GetComponent<EnemyTriggerManager_MT>();
        _gameEndCamera = GetComponent<GameEndCamera_MT>();
    }

    // Update is called once per frame
    void Update()
    {
        // �v���C���[�̎���̓G���擾
        Collider closestEnemy = _closestEnemyFinder.GetClosestObject(_enemyTriggerManager.objectsInTrigger, transform);

        // �߂��̓G�����݂���ꍇ
        if (closestEnemy != null)
        {
            // �G��StatusManager_MT�R���|�[�l���g���擾
            StatusManager_MT enemyStatus = closestEnemy.GetComponent<StatusManager_MT>();

            // �߂��̓G���{�X�ł���AHP��0�ȉ��̏ꍇ
            if (enemyStatus != null && closestEnemy.CompareTag("Boss") && enemyStatus.HP <= 0)
            {
                if(_coroutineSwitch)
                {
                    // �{�X�����񂾂Ƃ��̏���
                    StartCoroutine(_gameEndCamera.GameClearCoroutine());
                    _coroutineSwitch = false;
                }
          
            }
        }
    }

}