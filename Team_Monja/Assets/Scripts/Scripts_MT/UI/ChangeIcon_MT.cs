using UnityEngine;
using UnityEngine.UI;

public class ChangeIcon_MT : MonoBehaviour
{
    // キャラクターアイコンの配列
    [SerializeField]
    private Sprite[] _characterIcons;

    // Imageコンポーネント
    private Image _iconImage;

    // 初期化
    private void Start()
    {
        // 同じオブジェクトにアタッチされたImageコンポーネントを取得
        _iconImage = GetComponent<Image>();

        // Imageコンポーネントが取得できなかった場合のエラーチェック
        if (_iconImage == null)
        {
            Debug.LogError("Imageコンポーネントが見つかりませんでした！");
        }
    }

    // アイコンを変更する関数
    public void IconChange(int spriteNum)
    {
        // インデックスの範囲チェック
        if (spriteNum < 0 || spriteNum >= _characterIcons.Length)
        {
            Debug.LogError("無効なインデックスです！");
            return;
        }

        // アイコンを変更
        if (_iconImage != null)
        {
            _iconImage.sprite = _characterIcons[spriteNum];
            Debug.Log($"アイコンが{spriteNum}番目のスプライトに変更されました！");
        }
    }
}
