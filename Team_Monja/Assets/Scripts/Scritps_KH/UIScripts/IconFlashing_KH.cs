using UnityEngine;
using UnityEngine.UI;

public class IconFlashing_KH : MonoBehaviour
{
    private Image _image = default;

    // ���̒l���傫���قǑ����_�ł���
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
        // �����x���v�Z
        float alpha = Mathf.Abs(Mathf.Sin(Time.time * _flashingTime));

        // �����x�𔽉f
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, alpha);
    }

}
