using UnityEngine;
using TMPro;
using DG.Tweening;

public class DefenseStatusUI_MT : MonoBehaviour
{
    TextMeshProUGUI _textMeshProUGUI;
    float _currentDefense;  // float �^�ɕύX

    private void Start()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        _currentDefense = 0;  // �����l��ݒ�
        UpdateText();  // �����\����ݒ�
    }

    public void ChangeText(int targetDefense)
    {
        // DOTween.To �ŃA�j���[�V��������
        DOTween.To(() => _currentDefense, x => {
            _currentDefense = x;
            UpdateText();  // �A�j���[�V�������Ƀe�L�X�g���X�V
        }, targetDefense, 0.5f).SetEase(Ease.Linear);  // �A�j���[�V�����̎������Ԃ�0.5�b�ɕύX
    }

    private void UpdateText()
    {
        // _currentStrength �� 10 �{���A���̒l�𐮐��Ƃ��ĕ\��
        // �\���͏�� 10 �{�A�����_�ȉ��̒l��؂�̂Ă�
        int displayValue = Mathf.RoundToInt(_currentDefense * 10);
        _textMeshProUGUI.text = displayValue.ToString();
    }
}
