using UnityEngine;

public class StartAnimationOnSceneLoad : MonoBehaviour
{
    [SerializeField]
    private Animator animator; // �A�j���[�^�[�̎Q�Ƃ������ɐݒ肷��

    void Start()
    {
        Time.timeScale = 1; //�{�X��Ŏ~�߂����Ԃ�߂�
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (animator != null)
        {
            animator.Play("Stamp_Anim"); // �Đ��������A�j���[�V�����̖��O��ݒ肷��
        }
        else
        {
            Debug.LogWarning("Animator��������Ȃ���B�����Ɛݒ肵�Ȃ�����I");
        }
    }
}
