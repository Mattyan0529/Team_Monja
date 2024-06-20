using UnityEngine;
using UnityEngine.UI;

public class ChangeIcon_MT : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _characterIcons;

    private Image _iconImage;
    public void IconChange(int SpriteNum)
    {
        _iconImage.sprite = _characterIcons[SpriteNum];
    }
}
