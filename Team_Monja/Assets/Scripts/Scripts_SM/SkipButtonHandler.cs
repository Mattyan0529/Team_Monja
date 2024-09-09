using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkipButtonHandler : MonoBehaviour
{
    [SerializeField] private string _sceneNameToLoad;  // 遷移先のシーン名
    [SerializeField] private float _holdDuration = 2.0f;  // 長押し時間の必要秒数
    [SerializeField] private Image _circularGauge;  // UIの円形ゲージ
    private float _holdTime = 0.0f;  // ボタンを押した時間をカウント
    private bool _isHolding = false;

    void Update()
    {
        // "MenuButton" を長押ししているか確認
        if (Input.GetButton("MenuButton"))
        {
            _holdTime += Time.deltaTime;
            _isHolding = true;

            // 円形ゲージの進捗率を更新
            _circularGauge.fillAmount = _holdTime / _holdDuration;

            // 長押しが完了したらシーン遷移
            if (_holdTime >= _holdDuration)
            {
                SceneManager.LoadScene(_sceneNameToLoad);
            }
        }
        else if (_isHolding)  // ボタンを離したらリセット
        {
            _holdTime = 0.0f;
            _isHolding = false;
            _circularGauge.fillAmount = 0.0f;  // ゲージをリセット
        }
    }
}
