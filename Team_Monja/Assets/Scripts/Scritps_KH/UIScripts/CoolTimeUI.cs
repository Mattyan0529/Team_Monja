using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CoolTimeUI : MonoBehaviour
{
    private Image _image = default;
    private float _skillCoolTime = 2.0f;        // PlayerSkillの_coolTimeと同じ時間にする
    private bool _isCoolTime = false;

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        _image.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isCoolTime)
        {
            _image.fillAmount -= 1.0f / _skillCoolTime * Time.deltaTime;
        }

        if(_image.fillAmount < 0)
        {
            _isCoolTime = false;
        }
    }

    /// <summary>
    /// クールタイムのUI表示をスタートする
    /// </summary>
    public void StartCoolTime()
    {
        _isCoolTime = true;
        _image.fillAmount = 1f;
    }
}
