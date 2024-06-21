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
            Debug.LogError("Slider�R���|�[�l���g��������܂���ł����B");
        }
    }

    /// <summary>
    /// HP�̍ő�l���X�V
    /// </summary>
    /// <param name="maxHP">�V�����ő�HP</param>
    public void SetMaxHP(int maxHP)
    {
        // Slider�̍ő�l��ݒ�
        _thisSlider.maxValue = maxHP;
    }

    /// <summary>
    /// ���݂�HP���X�V
    /// </summary>
    /// <param name="currentHP">���݂�HP</param>
    public void SetCurrentHP(int currentHP)
    {
        // Slider�̌��݂̒l��ݒ�
        _thisSlider.value = currentHP;
    }
}
