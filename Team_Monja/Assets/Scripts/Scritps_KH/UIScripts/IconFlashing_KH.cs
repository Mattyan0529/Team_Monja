using UnityEngine;
using UnityEngine.UI;

public class IconFlashing_KH : MonoBehaviour
{
    private Image _image = default;

    // この値が大きいほど速く点滅する
    private float _flashingTime = 3f;

    void Start()
    {
        _image = GetComponent<Image>();
    }

    void Update()
    {
        Flashing();
    }

    private void Flashing()
    {
        // 透明度を計算
        float alpha = Mathf.Abs(Mathf.Sin(Time.time * _flashingTime));

        // 透明度を反映
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, alpha);
    }

}
