using UnityEngine;

public class AttackAreaJudge : MonoBehaviour
{
    private ChangeEnemyMoveType _changeEnemyMoveType = default;
    private GameObject _player = default;

    void Start()
    {
        _changeEnemyMoveType = gameObject.GetComponentInParent<ChangeEnemyMoveType>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (other != _player) return;

        _changeEnemyMoveType.NowState = ChangeEnemyMoveType.EnemyMoveState.InAttack;
    }

    private void OnTriggerExit(Collider other)
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (other != _player) return;

        _changeEnemyMoveType.NowState = ChangeEnemyMoveType.EnemyMoveState.InFollow;
    }
}
