using UnityEngine;

public class AttackAreaJudge : MonoBehaviour
{
    private ChangeEnemyMoveType _changeEnemyMoveType = default;
    private GameObject _player = default;

    private float _rangeHaveAttackArea = 3f;

    void Start()
    {
        _changeEnemyMoveType = gameObject.GetComponentInParent<ChangeEnemyMoveType>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(_changeEnemyMoveType.NowState == ChangeEnemyMoveType.EnemyMoveState.InFollow)
        {
            PlayerInAttackAreaJudge();
        }
    }

    private void PlayerInAttackAreaJudge()
    {
        Vector3 playerPos = _player.transform.position;
        Vector3 myPos = gameObject.transform.position;

        playerPos.y = 0f;
        myPos.y = 0f;

        // Mathf.Pow(_rangeHaveAttackArea, 2)は_rangeHaveAttackAreaの2乗
        // 既定値よりプレイヤーが近かったら
        if (Vector3.SqrMagnitude(playerPos - myPos) < Mathf.Pow(_rangeHaveAttackArea, 2)
            && _changeEnemyMoveType.NowState == ChangeEnemyMoveType.EnemyMoveState.InFollow)
        {
            _changeEnemyMoveType.NowState = ChangeEnemyMoveType.EnemyMoveState.InAttack;
        }
        else if(_changeEnemyMoveType.NowState == ChangeEnemyMoveType.EnemyMoveState.InAttack)
        {
            _changeEnemyMoveType.NowState = ChangeEnemyMoveType.EnemyMoveState.InFollow;
        }

    }
}
