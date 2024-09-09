using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkipButtonHandler : MonoBehaviour
{
    [SerializeField] private string _sceneNameToLoad;  // �J�ڐ�̃V�[����
    [SerializeField] private float _holdDuration = 2.0f;  // ���������Ԃ̕K�v�b��
    [SerializeField] private Image _circularGauge;  // UI�̉~�`�Q�[�W
    private float _holdTime = 0.0f;  // �{�^�������������Ԃ��J�E���g
    private bool _isHolding = false;

    void Update()
    {
        // "MenuButton" �𒷉������Ă��邩�m�F
        if (Input.GetButton("MenuButton"))
        {
            _holdTime += Time.deltaTime;
            _isHolding = true;

            // �~�`�Q�[�W�̐i�������X�V
            _circularGauge.fillAmount = _holdTime / _holdDuration;

            // ������������������V�[���J��
            if (_holdTime >= _holdDuration)
            {
                SceneManager.LoadScene(_sceneNameToLoad);
            }
        }
        else if (_isHolding)  // �{�^���𗣂����烊�Z�b�g
        {
            _holdTime = 0.0f;
            _isHolding = false;
            _circularGauge.fillAmount = 0.0f;  // �Q�[�W�����Z�b�g
        }
    }
}
