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
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        // 親オブジェクトから取得
        characterDeadDecision = GetComponent<CharacterDeadDecision_MT>();
        _closestEnemyFinder = GameObject.FindWithTag("PlayerManager").GetComponent<ClosestEnemyFinder_MT>();
        _enemyTriggerManager = GameObject.FindWithTag("NearTrigger").GetComponent<EnemyTriggerManager_MT>();
        
        _nowPositionY = transform.position.y;
        // タグを持つ最初の子オブジェクトを検索する
        _tutorialObj = FindFirstObjectWithTag(transform, _targetTag);

        // 初期状態でパーティクルシステムを非表示にしておく
        if (_particleSystemObject != null)
        {
            _particleSystemObject.SetActive(false);
        }
    }

    void Update()
    {
        if (characterDeadDecision.IsDeadDecision())
        {
            if (!CompareTag("Player"))
            {
                Debug.Log(_closestEnemyFinder.GetClosestObject(_enemyTriggerManager.objectsInTrigger, _player.transform).gameObject);
                //プレイヤーから一番近い場合
                if (_enemyTriggerManager.objectsInTrigger != null && _player != null &&
                    (this.gameObject == _closestEnemyFinder.GetClosestObject(_enemyTriggerManager.objectsInTrigger, _player.transform)))
                {
                    _tutorialObj.SetActive(true);
                }

                ToggleParticleSystem(true);  // 死亡状態でパーティクルシステムを表示
            }
        }
        else
        {
            _tutorialObj.SetActive(false);
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

    public void SetPlayer(GameObject player)
    {
        _player = player;
    }
}