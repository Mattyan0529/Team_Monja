using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim_MT : MonoBehaviour
{
    Animator animator;


    private string _nowAnim = default;
    private string _currentAnim = default;

    private bool _isAttacking = false;

    public string NowAnim { get { return _nowAnim; } set { _nowAnim = value; } }


    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        NowAnim = "Idle";
    }

    private void LateUpdate()
    {

        AnimSwitch();

        _isAttacking = false;

        _currentAnim = NowAnim;
    }

    private void AnimSwitch()
    {
        switch (NowAnim)
        {
            case "Idle":
                animator.SetBool("Idle", true);
                animator.SetBool("Move", false);
                NowAnim = null;
                break;
            case "Move":
                animator.SetBool("Move", true);
                animator.SetBool("Idle", false);
                NowAnim = null;
                break;
            case "Attack":
                if (!_isAttacking)
                {
                    animator.SetTrigger("Attack");
                    _isAttacking = true;
                }
                break;
            case "Attack2":
                animator.SetTrigger("Attack2");

                break;
            case "Skill":
                animator.SetTrigger("Skill");

                break;
            case "GotDamage":
                animator.SetTrigger("GotDamage");

                break;
            case "Die":
                animator.SetTrigger("Die");
                animator.SetBool("Die", true);
                animator.SetBool("Idle", false);
                animator.SetBool("Move", false);
                NowAnim = null;
                break;
            case "Revive":
                animator.SetTrigger("Revive");
                animator.SetBool("Idle", false);
                animator.SetBool("Move", false);
                animator.SetBool("Die", false);
                NowAnim = null;
                break;
            case "Eat":
                animator.SetTrigger("Eat");
                break;
            case null:
                break;
        }
    }


}
