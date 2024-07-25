using UnityEngine;
using TMPro;
using DG.Tweening;

public class DefenseStatusUI_MT : MonoBehaviour
{
    TextMeshProUGUI _textMeshProUGUI;
    float _currentDefense;  // float 型に変更

    private void Start()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        _currentDefense = 0;  // 初期値を設定
        UpdateText();  // 初期表示を設定
    }

    public void ChangeText(int targetDefense)
    {
        // DOTween.To でアニメーションする
        DOTween.To(() => _currentDefense, x => {
            _currentDefense = x;
            UpdateText();  // アニメーション中にテキストを更新
        }, targetDefense, 0.5f).SetEase(Ease.Linear);  // アニメーションの持続時間を0.5秒に変更
    }

    private void UpdateText()
    {
        // _currentStrength を 10 倍し、その値を整数として表示
        // 表示は常に 10 倍、小数点以下の値を切り捨てる
        int displayValue = Mathf.RoundToInt(_currentDefense * 10);
        _textMeshProUGUI.text = displayValue.ToString();
    }
}
