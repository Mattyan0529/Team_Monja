using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim_MT : MonoBehaviour
{
    Animator animator;


    private string _nowAnim = default;
    private string _currentAnim = default;

    public string NowAnim { get { return _nowAnim; } set { _nowAnim = value; } }


    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        if (NowAnim != _currentAnim)
        {
            AnimSwitch();
        }

        
        _currentAnim = NowAnim;
    }

    private void AnimSwitch()
    {
        switch (NowAnim)
        {
            case "Idle":
                animator.SetBool("Idle", true);
                animator.SetBool("Move", false);
                break;
            case "Move":
                animator.SetBool("Move", true);
                animator.SetBool("Idle", false);
                break;
            case "Attack":
                animator.SetTrigger("Attack");
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
                break;
            case "Revive":
                animator.SetTrigger("Revive");
                animator.SetBool("Idle", false);
                animator.SetBool("Move", false);
                animator.SetBool("Die", false);
                break;
        }
    }


}
