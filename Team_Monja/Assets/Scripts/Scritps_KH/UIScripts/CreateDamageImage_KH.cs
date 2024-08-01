using TMPro;
using UnityEngine;

public class CreateDamageImage_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _damageImage = default;

    private TextMeshPro _textMeshPro = default;

    private float _createAddY = 3f;

    private void Start()
    {
        _textMeshPro = _damageImage.GetComponentInChildren<TextMeshPro>();
        _damageImage.SetActive(false);
    }

    public void InstantiateDamageImage(GameObject player, GameObject target, int damage)
    {
        // 死んでたらダメージを表示しない
        if (target.GetComponent<CharacterDeadDecision_MT>().IsDeadDecision()) return;

        Vector3 position = new Vector3
            (target.transform.position.x, target.transform.position.y + _createAddY * target.transform.localScale.y,
            target.transform.position.z);

        _textMeshPro.text = damage.ToString();
        _damageImage.transform.position = position;
        _damageImage.SetActive(true);
    }
}
