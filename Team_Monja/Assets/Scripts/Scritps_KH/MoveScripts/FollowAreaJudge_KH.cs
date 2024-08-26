using UnityEngine;

public class FollowAreaJudge_KH : MonoBehaviour
{
    private ChangeEnemyMoveType_KH _changeEnemyMoveType = default;
    private GameObject _player = default;

    void Start()
    {
        _changeEnemyMoveType = gameObject.GetComponentInParent<ChangeEnemyMoveType_KH>();
        if (gameObject.transform.parent.CompareTag("Boss"))
        {
            gameObject.transform.parent = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (other.gameObject != _player) return;
        if (_changeEnemyMoveType.NowState != ChangeEnemyMoveType_KH.EnemyMoveState.InRandomMove) return;

        _changeEnemyMoveType.NowState = ChangeEnemyMoveType_KH.EnemyMoveState.InFollow;
    }

    private void OnTriggerExit(Collider other)
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (other.gameObject != _player) return;
        if (_changeEnemyMoveType.NowState != ChangeEnemyMoveType_KH.EnemyMoveState.InFollow) return;

        _changeEnemyMoveType.NowState = ChangeEnemyMoveType_KH.EnemyMoveState.InRandomMove;
    }
}
