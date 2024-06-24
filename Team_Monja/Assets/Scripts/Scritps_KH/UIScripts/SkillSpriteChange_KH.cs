using UnityEngine;

public class SkillSpriteChange_KH : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer = default;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// スキルのアイコンを変更する
    /// </summary>
    /// <param name="spriteNum"></param>
    private void ChangeSprite(Sprite skillIcon)
    {
        _spriteRenderer.sprite = skillIcon;
    }
}
