using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatEnemy_MT : MonoBehaviour
{
    // 追記：北
    [SerializeField]
    private GameObject _residentScript = default;
    private AudioSource _audioSource = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private CharacterAnim_MT characterAnim;
    StatusManager_MT statusManagerEnemy;
    StatusManager_MT statusManagerPlayer;
    ClosestEnemyFinder_MT closestEnemyFinder;
    GameObject _player;

    private void Start()
    {
        closestEnemyFinder = GameObject.FindWithTag("PlayerManager").GetComponent<ClosestEnemyFinder_MT>();
        _soundEffectManagement = _residentScript.GetComponent<SoundEffectManagement_KH>();
    }

    public void RemoveClosestObject(List<Collider> objectsInTrigger, Transform referencePoint)
    {
        if (objectsInTrigger == null)
        {
            Debug.LogError("objectsInTriggerが空です");
            return;
        }

        if (objectsInTrigger.Count == 0)
        {
            return;
        }

        // ClosestEnemyFinder_MTを使用して最も近い敵を取得
        Collider closestObject = closestEnemyFinder.GetClosestObject(objectsInTrigger, referencePoint);

        if (closestObject != null && closestObject.gameObject.activeSelf &&( closestObject.CompareTag("Enemy") || closestObject.CompareTag("Boss")))
        {
            statusManagerEnemy = closestObject.GetComponent<StatusManager_MT>();
            if (statusManagerEnemy != null && statusManagerEnemy.HP <= 0 )
            {
                characterAnim.NowAnim = "Eat";
                //現在MaxHPを記録
                int currentMaxHP = statusManagerPlayer.MaxHP;

                // 現在のステータスの倍率をリセット
                statusManagerPlayer.ResetMultipliers();

                // プラスステータスを加算
                statusManagerPlayer.PlusHP += statusManagerEnemy.PlusHP;
                statusManagerPlayer.PlusStrength += statusManagerEnemy.PlusStrength;
                statusManagerPlayer.PlusDefense += statusManagerEnemy.PlusDefense;

                // 最も近い敵をリストから削除し、非アクティブ化
                objectsInTrigger.Remove(closestObject);
                closestObject.gameObject.SetActive(false);

                // ステータスの倍率を再適用
                statusManagerPlayer.ApplyMultipliers();
                //食べてMaxHPが増えた分だけ回復
                statusManagerPlayer.HealHP(statusManagerPlayer.MaxHP - currentMaxHP);

                _soundEffectManagement.PlayEatSound(_audioSource);
            }
            else
            {
                Debug.LogError("一番近い敵が死んでいないか、StatusManager_MTを持っていない。");
            }
        }
    }
    public void SetPlayer(GameObject player)
    {
        statusManagerPlayer = player.GetComponent<StatusManager_MT>();
        _audioSource = player.GetComponent<AudioSource>();
        characterAnim = player.GetComponent<CharacterAnim_MT>();
    }
}