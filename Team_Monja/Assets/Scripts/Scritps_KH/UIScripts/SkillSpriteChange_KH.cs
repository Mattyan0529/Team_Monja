using UnityEngine;

public class SkillSpriteChange_KH : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer = default;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// �X�L���̃A�C�R����ύX����
    /// </summary>
    /// <param name="spriteNum"></param>
    private void ChangeSprite(Sprite skillIcon)
    {
        _spriteRenderer.sprite = skillIcon;
    }
}
