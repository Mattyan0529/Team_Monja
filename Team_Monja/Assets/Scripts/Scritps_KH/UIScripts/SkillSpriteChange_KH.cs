using UnityEngine;
using UnityEngine.UI;

public class SkillSpriteChange_KH : MonoBehaviour
{
    private Image _image = default;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    /// <summary>
    /// スキルのアイコンを変更する
    /// </summary>
    /// <param name="spriteNum"></param>
    public void ChangeSprite(Sprite skillIcon)
    {
        _image.sprite = skillIcon;
    }
}
