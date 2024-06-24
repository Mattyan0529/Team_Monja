using UnityEngine;
using UnityEngine.UI;

public class SkillSpriteChange_KH : MonoBehaviour
{
    private Image _image = default;

    void Start()
    {
        _image = GetComponent<Image>();
    }

    /// <summary>
    /// �X�L���̃A�C�R����ύX����
    /// </summary>
    /// <param name="spriteNum"></param>
    public void ChangeSprite(Sprite skillIcon)
    {
        _image.sprite = skillIcon;
        Debug.Log("wa");
    }
}
