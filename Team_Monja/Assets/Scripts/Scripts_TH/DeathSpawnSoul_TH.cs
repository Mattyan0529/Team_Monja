using UnityEngine;

public class DeathSpwanSoul_TH : MonoBehaviour
{
    [SerializeField]
    private GameObject _particleSystemObject;  // パーティクルシステムのGameObject

    private float _rotateSpeed = 1.5f;
    private float _pingPongValue = 0.3f;
    private float _nowPositionY = default;
    private string _targetTag = "TutorialUI";
    private GameObject _player;
    private GameObject _tutorialObj;
    private EnemyTriggerManager_MT _enemyTriggerManager;
    private CharacterDeadDecision_MT characterDeadDecision;
    private ClosestEnemyFinder_MT _closestEnemyFinder;

    void Start()
    {
      

        // 親オブジェクトから取得
        characterDeadDecision = GetComponent<CharacterDeadDecision_MT>();
        if (characterDeadDecision == null)
        {
            Debug.LogError("CharacterDeadDecision_MT component not found on this GameObject.");
        }

        // プレイヤーマネージャーから最も近い敵を探すスクリプトを取得
        GameObject playerManager = GameObject.FindWithTag("PlayerManager");
        if (playerManager != null)
        {
            _closestEnemyFinder = playerManager.GetComponent<ClosestEnemyFinder_MT>();
            if (_closestEnemyFinder == null)
            {
                Debug.LogError("ClosestEnemyFinder_MT component not found on PlayerManager.");
            }
        }
        else
        {
            Debug.LogError("PlayerManager with tag 'PlayerManager' not found.");
        }

        // 近くのトリガーから敵のトリガーマネージャーを取得
        GameObject nearTrigger = GameObject.FindWithTag("NearTrigger");
        if (nearTrigger != null)
        {
            _enemyTriggerManager = nearTrigger.GetComponent<EnemyTriggerManager_MT>();
            if (_enemyTriggerManager == null)
            {
                Debug.LogError("EnemyTriggerManager_MT component not found on NearTrigger.");
            }
        }
        else
        {
            Debug.LogError("NearTrigger with tag 'NearTrigger' not found.");
        }

        _nowPositionY = transform.position.y;
        // タグを持つ最初の子オブジェクトを検索する
        _tutorialObj = FindFirstObjectWithTag(transform, _targetTag);

        // _tutorialObjが見つからない場合の警告
        if (_tutorialObj == null)
        {
            Debug.LogWarning("Tag 'TutorialUI' not found in child objects.");
        }

        // 初期状態でパーティクルシステムを非表示にしておく
        if (_particleSystemObject != null)
        {
            _particleSystemObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("_particleSystemObject is not assigned.");
        }
    }

    public void SetPlayer(GameObject player)
    {
        _player = player;
    }

    void Update()
    {
        if (characterDeadDecision != null && characterDeadDecision.IsDeadDecision())
        {
            if (!CompareTag("Player"))
            {

                if (_player != null && _closestEnemyFinder != null && _enemyTriggerManager != null)
                {
                    var closestObject = _closestEnemyFinder.GetClosestObject(_enemyTriggerManager.objectsInTrigger, _player.transform);

                    if (closestObject != null)
                    {
                        if (this.gameObject == closestObject.gameObject)
                        {
                            if (_tutorialObj != null)
                            {
                                _tutorialObj.SetActive(true);
                            }
                            else
                            {
                                Debug.LogWarning("_tutorialObj is null.");
                            }
                        }
                    }
                    else
                    {
                        Debug.LogWarning("ClosestObject is null.");
                    }
                }
                else
                {
                    Debug.LogWarning("One of the required components is null.");
                }
                ToggleParticleSystem(true);  // 死亡状態でパーティクルシステムを表示
            }
        }
        else
        {
            if (_tutorialObj != null)
            {
                _tutorialObj.SetActive(false);
            }
            ToggleParticleSystem(false); // 生存状態でパーティクルシステムを非表示
        }
    }


    private void ToggleParticleSystem(bool isActive)
    {
        if (_particleSystemObject != null)
        {
            _particleSystemObject.SetActive(isActive);
        }
    }

    // 再帰的にタグを持つ最初のオブジェクトを検索するメソッド
    private GameObject FindFirstObjectWithTag(Transform parent, string tag)
    {
        // 子オブジェクトをすべて取得
        foreach (Transform child in parent)
        {
            // タグが一致する場合、オブジェクトを返す
            if (child.CompareTag(tag))
            {
                return child.gameObject;
            }

            // 子オブジェクトの子も再帰的に検索
            GameObject foundObject = FindFirstObjectWithTag(child, tag);
            if (foundObject != null)
            {
                return foundObject;
            }
        }

        // タグを持つオブジェクトが見つからない場合
        return null;
    }
}
