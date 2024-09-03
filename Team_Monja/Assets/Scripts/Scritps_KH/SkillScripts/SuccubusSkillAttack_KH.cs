using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccubusSkillAttack_KH : MonoBehaviour, IDamagable_KH
{
    [SerializeField]
    private GameObject _residentScript;

    [SerializeField]
    private EffectManager _effectManager; // EffectManagerの参照を追加


    private float _deleteTime = 0.5f;
    private float _elapsedTime = 0f;
    private float _skillResetTime = 5f;

    // 相手のステータスを何倍にするか（１未満に設定してね）
    private const float _statDecreaseRate = 0.7f;

    private GameObject _attackArea;

    private WriteHitPoint_KH _writeHitPoint = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private AudioSource _audioSource = default;
    private CreateDamageImage_KH _createDamageImage = default;
    private PlayerSkill_KH _playerSkill = default;
    private ChangeEnemyMoveType_KH _changeEnemyMoveType = default;

    // スキルで減少させているステータス
    private List<StatusManager_MT> _reducedStatus;
    // ステータスリストのリスト（減少リセットまでの間に複数回スキルを使う可能性があるため）
    private List<List<StatusManager_MT>> _statEachSkillTimes;

    private bool _isAttack = false;

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
        _reducedStatus = new List<StatusManager_MT>();
        _statEachSkillTimes = new List<List<StatusManager_MT>>();
    }

    void Update()
    {
        UpdateTime();
    }

    public void SpecialAttack()
    {  //松本
        _characterAnim.NowAnim = "Skill";

        _changeEnemyMoveType.IsMove = false;
        _soundEffectManagement.PlaySlimeSound(_audioSource);
        Invoke("ResetStatus", _skillResetTime);
    }


    private void CreateAttackArea()
    {
        _isAttack = true;
        _attackArea.SetActive(true);
        //スキルエフェクト
        if (_effectManager != null)
        {
            _effectManager.ShowSpecialAttackEffect(transform);
        }
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
        _reducedStatus.Add(targetStatusManager);

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
        if (_statEachSkillTimes == null)
        {
            return;
        }

        if (_statEachSkillTimes.Count == 0) return;
        List<StatusManager_MT> list = new List<StatusManager_MT>(_statEachSkillTimes[0]);

        for (int i = 0; i < list.Count; i++)
        {
            float strength = list[i].Strength / _statDecreaseRate;
            list[i].Strength = (int)strength;

            float defence = list[i].Defense / _statDecreaseRate;
            list[i].Defense = (int)defence;
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
            List<StatusManager_MT> list = new List<StatusManager_MT>(_reducedStatus);
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

    private void OnDisable()
    {
        List<List<StatusManager_MT>> list = _statEachSkillTimes;

        for (int i = 0; i < list.Count; i++)
        {
            ResetStatus();
        }
    }
}