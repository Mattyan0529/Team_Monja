using UnityEngine;

public class CreateDamageImage : MonoBehaviour
{
    [SerializeField]
    private GameObject _damageImage = default;

    private float _createAddY = 1.0f;

    public void InstantiateDamageImage(GameObject player)
    {
        Vector3 position = new Vector3
            (player.transform.position.x, player.transform.position.y + _createAddY, player.transform.position.z);

        Instantiate(_damageImage, position, player.transform.rotation);
    }
}
