using UnityEngine;

public class AttackAreaJudge_KH : MonoBehaviour
{
    private ChangeEnemyMoveType_KH _changeEnemyMoveType = default;

    private GameObject _player = default;

    [SerializeField]
    private float _rangeHaveAttackArea = 3f;

    void Start()
    {
        _changeEnemyMoveType = gameObject.GetComponentInParent<ChangeEnemyMoveType_KH>();
    }

    private void Update()
    {
        PlayerInAttackAreaJudge();
    }

    /// <summary>
    /// プレイヤーを設定
    /// </summary>
    public void SetPlayer(GameObject player)
    {
        _player = player;
    }

    private void PlayerInAttackAreaJudge()
    {
        Vector3 playerPos = _player.transform.position;
        Vector3 myPos = gameObject.transform.position;

        playerPos.y = 0f;
        myPos.y = 0f;

        // Mathf.Pow(_rangeHaveAttackArea, 2)は_rangeHaveAttackAreaの2乗
        // 既定値よりプレイヤーが近かったら
        if (Vector3.SqrMagnitude(playerPos - myPos) < Mathf.Pow(_rangeHaveAttackArea, 2) &&
            _changeEnemyMoveType.NowState == ChangeEnemyMoveType_KH.EnemyMoveState.InFollow)
        {
            // プレイヤーが敵の前にいなければ攻撃しない
            float dotProduct = Vector3.Dot(transform.forward, playerPos - myPos);
            if (dotProduct <= 0) return;

            _changeEnemyMoveType.NowState = ChangeEnemyMoveType_KH.EnemyMoveState.InAttack;
        }
        // プレイヤーが既定値外で、攻撃状態の時
        else if (Vector3.SqrMagnitude(playerPos - myPos) > Mathf.Pow(_rangeHaveAttackArea, 2) &&
            _changeEnemyMoveType.NowState == ChangeEnemyMoveType_KH.EnemyMoveState.InAttack)
        {
            _changeEnemyMoveType.NowState = ChangeEnemyMoveType_KH.EnemyMoveState.InFollow;
        }
    }
}
