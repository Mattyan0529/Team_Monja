using UnityEngine;
using UnityEngine.UI;

public class MoveSlider_MT : MonoBehaviour
{
    Slider _thisSlider;

    // Start is called before the first frame update
    void Start()
    {

        _thisSlider = GetComponent<Slider>();
    }

    /// <summary>
    /// HP�̍ő�l���X�V
    /// </summary>
    /// <param name="HP"></param>
    public void NewValue(int MaxHP)
    {
        _thisSlider.maxValue = MaxHP;
    }
    /// <summary>
    /// ���݂�HP���X�V
    /// </summary>
    /// <param name="HP"></param>
    public void NowValue(int HP)
    {
        _thisSlider.value = HP;
    }

}
