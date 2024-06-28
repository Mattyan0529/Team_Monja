using TMPro;
using UnityEngine;

public class CreateDamageImage : MonoBehaviour
{
    [SerializeField]
    private GameObject _damageImage = default;

    private TextMeshPro _textMeshPro = default;

    private float _createAddY = 2.5f;

    private void Start()
    {
        _textMeshPro = _damageImage.GetComponentInChildren<TextMeshPro>();
        _damageImage.SetActive(false);
    }

    public void InstantiateDamageImage(GameObject player, int damage)
    {
        Vector3 position = new Vector3
            (player.transform.position.x, player.transform.position.y + _createAddY, player.transform.position.z);

        _textMeshPro.text = damage.ToString();
        _damageImage.transform.position = position;
        _damageImage.SetActive(true);
    }
}
