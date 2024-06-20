using UnityEngine;
using UnityEngine.UI;

public class ChangeIcon_MT : MonoBehaviour
{
    // �L�����N�^�[�A�C�R���̔z��
    [SerializeField]
    private Sprite[] _characterIcons;

    // Image�R���|�[�l���g
    private Image _iconImage;

    // ������
    private void Start()
    {
        // �����I�u�W�F�N�g�ɃA�^�b�`���ꂽImage�R���|�[�l���g���擾
        _iconImage = GetComponent<Image>();

        // Image�R���|�[�l���g���擾�ł��Ȃ������ꍇ�̃G���[�`�F�b�N
        if (_iconImage == null)
        {
            Debug.LogError("Image�R���|�[�l���g��������܂���ł����I");
        }
    }

    // �A�C�R����ύX����֐�
    public void IconChange(int spriteNum)
    {
        // �C���f�b�N�X�͈̔̓`�F�b�N
        if (spriteNum < 0 || spriteNum >= _characterIcons.Length)
        {
            Debug.LogError("�����ȃC���f�b�N�X�ł��I");
            return;
        }

        // �A�C�R����ύX
        if (_iconImage != null)
        {
            _iconImage.sprite = _characterIcons[spriteNum];
            Debug.Log($"�A�C�R����{spriteNum}�Ԗڂ̃X�v���C�g�ɕύX����܂����I");
        }
    }
}
