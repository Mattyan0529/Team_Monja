using UnityEngine;
using TMPro;
using DG.Tweening;

public class StrengthStatusUI_MT : MonoBehaviour
{
    TextMeshProUGUI _textMeshProUGUI;
    float _currentStrength;  // float �^�ɕύX

    private void Start()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        _currentStrength = 0;  // �����l��ݒ�
        UpdateText();  // �����\����ݒ�
    }

    public void ChangeText(int targetStrength)
    {
        // DOTween.To �ŃA�j���[�V��������
        DOTween.To(() => _currentStrength, x => {
            _currentStrength = x;
            UpdateText();  // �A�j���[�V�������Ƀe�L�X�g���X�V
        }, targetStrength, 0.5f).SetEase(Ease.Linear);  // �A�j���[�V�����̎������Ԃ�0.5�b�ɕύX
    }

    private void UpdateText()
    {
        // _currentStrength �� 10 �{���A���̒l�𐮐��Ƃ��ĕ\��
        // �\���͏�� 10 �{�A�����_�ȉ��̒l��؂�̂Ă�
        int displayValue = Mathf.RoundToInt(_currentStrength * 10);
        _textMeshProUGUI.text = displayValue.ToString();
    }
}
