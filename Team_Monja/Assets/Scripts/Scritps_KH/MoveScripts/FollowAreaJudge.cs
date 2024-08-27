using UnityEngine;

public class FollowAreaJudge : MonoBehaviour
{
    private ChangeEnemyMoveType _changeEnemyMoveType = default;
    private GameObject _player = default;

    void Start()
    {
        _changeEnemyMoveType = gameObject.GetComponentInParent<ChangeEnemyMoveType>();
        if (gameObject.transform.parent.CompareTag("Boss"))
        {
            gameObject.transform.parent = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (other.gameObject != _player) return;
        if (_changeEnemyMoveType.NowState != ChangeEnemyMoveType.EnemyMoveState.InRandomMove) return;

        _changeEnemyMoveType.NowState = ChangeEnemyMoveType.EnemyMoveState.InFollow;
    }

    private void OnTriggerExit(Collider other)
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (other.gameObject != _player) return;
        if (_changeEnemyMoveType.NowState != ChangeEnemyMoveType.EnemyMoveState.InFollow) return;

        _changeEnemyMoveType.NowState = ChangeEnemyMoveType.EnemyMoveState.InRandomMove;
    }
}
