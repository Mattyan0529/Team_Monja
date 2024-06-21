using UnityEngine;
using UnityEngine.UI;

public class MoveSlider_MT : MonoBehaviour
{
    private Slider _thisSlider;

    // Start is called before the first frame update
    void Start()
    {
        _thisSlider = GetComponent<Slider>();
        if (_thisSlider == null)
        {
            Debug.LogError("Sliderコンポーネントが見つかりませんでした。");
        }
    }

    /// <summary>
    /// HPの最大値を更新
    /// </summary>
    /// <param name="maxHP">新しい最大HP</param>
    public void SetMaxHP(int maxHP)
    {
        // Sliderの最大値を設定
        _thisSlider.maxValue = maxHP;
    }

    /// <summary>
    /// 現在のHPを更新
    /// </summary>
    /// <param name="currentHP">現在のHP</param>
    public void SetCurrentHP(int currentHP)
    {
        // Sliderの現在の値を設定
        _thisSlider.value = currentHP;
    }
}
