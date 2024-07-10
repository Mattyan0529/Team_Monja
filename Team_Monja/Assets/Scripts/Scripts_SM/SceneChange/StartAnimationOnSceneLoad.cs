using UnityEngine;

public class StartAnimationOnSceneLoad : MonoBehaviour
{
    [SerializeField]
    private Animator animator; // アニメーターの参照をここに設定する

    void Start()
    {
        Time.timeScale = 1; //ボス戦で止めた時間を戻す
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (animator != null)
        {
            animator.Play("Stamp_Anim"); // 再生したいアニメーションの名前を設定する
        }
        else
        {
            Debug.LogWarning("Animatorが見つからないわ。ちゃんと設定しなさいよ！");
        }
    }
}
