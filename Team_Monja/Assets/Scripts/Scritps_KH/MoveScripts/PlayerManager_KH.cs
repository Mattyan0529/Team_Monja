using UnityEngine;

public class PlayerManager_KH : MonoBehaviour
{
    private GameObject _player = default;

    public GameObject Player
    {
        get { return _player; }
        set { _player = value; }
    }
}
