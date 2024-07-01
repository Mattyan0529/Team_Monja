using UnityEngine;

public class PostSceneTransitionAnimation : MonoBehaviour
{
    [SerializeField] private Animator transitionAnimator; // �J�ڌ�ɍĐ�����A�j���[�V����

    void Start()
    {
        // �V�[���J�ڌ�ɃA�j���[�V�������Đ�
        PlayTransitionAnimation();
    }

    private void PlayTransitionAnimation()
    {
        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger("StartTransition");
        }
    }
}
