using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccubusSkillAttack_KH : MonoBehaviour, IDamagable_KH
{
    [SerializeField]
    private GameObject _residentScript;

    private float _deleteTime = 2f;
    private float _elapsedTime = 0f;
    private float _skillResetTime = 5f;

    // 相手のステータスを何倍にするか（１未満に設定してね）
    private const float _statDecreaseRate = 0.9f;

    private GameObject _attackArea;

    private WriteHitPoint_KH _writeHitPoint = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private AudioSource _audioSource = default;
    private CreateDamageImage_KH _createDamageImage = default;
    private PlayerSkill_KH _playerSkill = default;
    private ChangeEnemyMoveType_KH _changeEnemyMoveType = default;

    // スキルで減少させているステータス
    private List<GameObject> _reducedStatus;
    // ステータスリストのリスト（減少リセットまでの間に複数回スキルを使う可能性があるため）
    private List<List<GameObject>> _statEachSkillTimes;

    private bool _isAttack = false;
    private bool _isMoveCoroutine = false;
    private bool _isSkipReset = true;     // リセットを飛ばしているときはtrue

    //松本
    private CharacterAnim_MT _characterAnim = default;

    void Start()
    {
        _writeHitPoint = _residentScript.GetComponent<WriteHitPoint_KH>();
        _soundEffectManagement = _residentScript.GetComponent<SoundEffectManagement_KH>();
        _createDamageImage = _residentScript.GetComponent<CreateDamageImage_KH>();
        _characterAnim = GetComponent<CharacterAnim_MT>();
        _audioSource = GetComponent<AudioSource>();
        _playerSkill = GetComponent<PlayerSkill_KH>();
        _changeEnemyMoveType = GetComponent<ChangeEnemyMoveType_KH>();

        // 子オブジェクトの中からAttackAreaを取得
        _attackArea = transform.Find("AttackArea").gameObject;
        _attackArea.SetActive(false);

        // リストを初期化
        _reducedStatus = new List<GameObject>();
        _statEachSkillTimes = new List<List<GameObject>>();
    }

    void Update()
    {
        UpdateTime();

        // スキップしたリセット処理を行う
        if(_isSkipReset && !_isMoveCoroutine)
        {
            // ステータスリセットの予約
            StartCoroutine(CountSecondsWaitReset());

            _isSkipReset = false;
        }
    }

    public void SpecialAttack()
    {  //松本
        _characterAnim.NowAnim = "Skill";

        _changeEnemyMoveType.IsMove = false;
        _soundEffectManagement.PlaySlimeSound(_audioSource);

        if (!_isMoveCoroutine)
        {
            // ステータスリセットの予約
            StartCoroutine(CountSecondsWaitReset());
        }
        else
        {
            // すでにコルーチンが動いているので一旦スキップ
            _isSkipReset = true;
        }
    }


    private void CreateAttackArea()
    {
        _isAttack = true;
        _attackArea.SetActive(true);
    }

    private void SuccubusDeleteAttackArea()
    {
        _attackArea.SetActive(false);
    }

    public void HitDecision(GameObject hitObj)
    {
        // 相手と自分のStatusManagerが両方必要
        StatusManager_MT targetStatusManager = hitObj.gameObject.GetComponent<StatusManager_MT>();
        StatusManager_MT myStatusManager = GetComponent<StatusManager_MT>();

        float strength = targetStatusManager.Strength * _statDecreaseRate;
        targetStatusManager.Strength = (int)strength;

        float defence = targetStatusManager.Defense * _statDecreaseRate;
        targetStatusManager.Defense = (int)defence;

        // 減少させているステータスリストに追加
        _reducedStatus.Add(targetStatusManager.gameObject);

        HitPointCalculation(myStatusManager, targetStatusManager);
    }

    /// <summary>
    /// ダメージ計算
    /// </summary>
    public void HitPointCalculation(StatusManager_MT myStatus, StatusManager_MT targetStatus)
    {
        int myAttackPower = myStatus.Strength;        // 自分の攻撃力をgetしてくる
        int targetDefensePower = targetStatus.Defense;        // 相手の防御力をgetしてくる
        int targetHitPoint = targetStatus.HP;        // 相手のHPをgetしてくる

        int damage = myAttackPower - targetDefensePower;

        if (myAttackPower <= targetDefensePower)
        {
            // 防御力のほうが高い場合はダメージを1とする
            int smallestDamage = 1;
            damage = smallestDamage;
        }

        int hitPointAfterDamage = targetHitPoint - damage;

        _createDamageImage.InstantiateDamageImage(targetStatus.gameObject, damage);
        _writeHitPoint.UpdateHitPoint(targetStatus, hitPointAfterDamage);      // targetStatusのHPを更新
    }

    private void ResetStatus()
    {
        if (_statEachSkillTimes.Count == 0) return;

        List<GameObject> list = new List<GameObject>(_statEachSkillTimes[0]);

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == null) continue;

            StatusManager_MT status = list[i].GetComponent<StatusManager_MT>();

            float strength = status.Strength / _statDecreaseRate;
            status.Strength = Mathf.CeilToInt(strength);

            float defence = status.Defense / _statDecreaseRate;
            status.Defense = Mathf.CeilToInt(defence);
        }
        _statEachSkillTimes.Remove(_statEachSkillTimes[0]);
    }

    /// <summary>
    /// 一定時間後攻撃範囲を削除する
    /// </summary>
    private void UpdateTime()
    {
        if (!_isAttack) return;     // 攻撃中以外は処理を行わない

        // 時間加算
        _elapsedTime += Time.deltaTime;

        // 規定時間に達していた場合
        if (_elapsedTime > _deleteTime)
        {
            // リストへの追加がここで締め切りなので、リストをリストへ追加する
            List<GameObject> list = new List<GameObject>(_reducedStatus);
            _statEachSkillTimes.Add(list);
            _reducedStatus.Clear();

            _characterAnim.NowAnim = "Idle";
            _attackArea.SetActive(false);
            _elapsedTime = 0f;

            _changeEnemyMoveType.IsMove = true;
            _isAttack = false;
            _playerSkill.IsUseSkill = false;
        }
    }

    // 減らしたステータスを数秒後にリセットする
    private IEnumerator CountSecondsWaitReset()
    {
        _isMoveCoroutine = true;

        yield return new WaitForSeconds(_skillResetTime);

        _isMoveCoroutine = false;

        List<List<GameObject>> list = _statEachSkillTimes;

        for (int i = 0; i < list.Count; i++)
        {
            ResetStatus();
        }
    }

    private void OnDisable()
    {
        ResetStatus();
    }
}