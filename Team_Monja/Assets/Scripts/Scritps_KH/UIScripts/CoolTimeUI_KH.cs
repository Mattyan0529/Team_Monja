using UnityEngine;
using UnityEngine.UI;

public class CoolTimeUI_KH : MonoBehaviour
{
    private Image _image = default;
    [SerializeField]
    private float _skillCoolTime = 2.0f;        // PlayerSkill��NormalAttack��_coolTime�Ɠ������Ԃɂ���
    private bool _isCoolTime = false;

    void Start()
    {
        _image = GetComponent<Image>();
        _image.fillAmount = 0f;
    }

    void Update()
    {
        if (_isCoolTime)
        {
            _image.fillAmount -= 1.0f / _skillCoolTime * Time.deltaTime;
        }

        if(_image.fillAmount < 0f)
        {
            _isCoolTime = false;
        }
    }

    /// <summary>
    /// �N�[���^�C����UI�\�����X�^�[�g����
    /// </summary>
    public void StartCoolTime()
    {
        _isCoolTime = true;
        _image.fillAmount = 1f;
    }
}
