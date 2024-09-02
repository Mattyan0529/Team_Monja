using TMPro;
using UnityEngine;

public class CreateDamageImage_KH : MonoBehaviour
{
    [SerializeField]
    private GameObject _damageImage = default;

    private TextMeshPro _textMeshPro = default;

    private CapsuleCollider _targetCollider;

    private Vector3 _createAdd = new Vector3(0,3f,1f);

    private void Start()
    {
        _textMeshPro = _damageImage.GetComponentInChildren<TextMeshPro>();
        _damageImage.SetActive(false);
    }

    public void InstantiateDamageImage(GameObject target, int damage)
    {
        // 死んでたらダメージを表示しない
        if (target.GetComponent<CharacterDeadDecision_MT>().IsDeadDecision()) return;

        _targetCollider = target.GetComponent<CapsuleCollider>();


        Vector3 position = target.transform.position  + _targetCollider.center + _createAdd;

        _textMeshPro.text = damage.ToString();
        _damageImage.transform.position = position;
        _damageImage.SetActive(true);
    }
}
