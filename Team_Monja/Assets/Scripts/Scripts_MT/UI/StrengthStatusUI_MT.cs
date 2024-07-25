using UnityEngine;
using TMPro;
using DG.Tweening;

public class StrengthStatusUI_MT : MonoBehaviour
{
    TextMeshProUGUI _textMeshProUGUI;
    float _currentStrength;  // float 型に変更

    private void Start()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        _currentStrength = 0;  // 初期値を設定
        UpdateText();  // 初期表示を設定
    }

    public void ChangeText(int targetStrength)
    {
        // DOTween.To でアニメーションする
        DOTween.To(() => _currentStrength, x => {
            _currentStrength = x;
            UpdateText();  // アニメーション中にテキストを更新
        }, targetStrength, 0.5f).SetEase(Ease.Linear);  // アニメーションの持続時間を0.5秒に変更
    }

    private void UpdateText()
    {
        // _currentStrength を 10 倍し、その値を整数として表示
        // 表示は常に 10 倍、小数点以下の値を切り捨てる
        int displayValue = Mathf.RoundToInt(_currentStrength * 10);
        _textMeshProUGUI.text = displayValue.ToString();
    }
}
