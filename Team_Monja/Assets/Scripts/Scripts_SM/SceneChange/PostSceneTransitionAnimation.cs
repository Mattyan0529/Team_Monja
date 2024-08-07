using UnityEngine;

public class PostSceneTransitionAnimation : MonoBehaviour
{
    [SerializeField] private Animator transitionAnimator; // 遷移後に再生するアニメーション

    void Start()
    {
        Time.timeScale = 1;
        // シーン遷移後にアニメーションを再生
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
