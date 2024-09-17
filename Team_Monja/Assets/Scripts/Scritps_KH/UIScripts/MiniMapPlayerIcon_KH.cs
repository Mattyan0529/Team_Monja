using UnityEngine;

public class MiniMapPlayerIcon_KH : MonoBehaviour
{
    private GameObject _player = default;

    /// <summary>
    /// ÉvÉåÉCÉÑÅ[Çê›íË
    /// </summary>
    /// <param name="player"></param>
    public void SetPlayer(GameObject player)
    {
        _player = player;
    }

    void FixedUpdate()
    {
        Vector3 newRotation = gameObject.transform.rotation.eulerAngles;
        newRotation.y = _player.transform.rotation.eulerAngles.y;

        gameObject.transform.rotation = Quaternion.Euler(newRotation);
    }
}
