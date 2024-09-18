using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim_MT : MonoBehaviour
{
    Animator _animator;

    private string _nowAnim = default;
    private string _currentAnim = default;

    private bool _isAttacking = false;

    public string NowAnim { get { return _nowAnim; } set { _nowAnim = value; } }

    private void Start()
    {
        _animator = GetComponent<Animator>();
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
                _animator.SetBool("Idle", true);
                _animator.SetBool("Move", false);
                NowAnim = null;
                break;
            case "Move":
                _animator.SetBool("Move", true);
                _animator.SetBool("Idle", false);
                NowAnim = null;
                break;
            case "Attack":
                if (!_isAttacking)
                {
                    _animator.SetTrigger("Attack");  // ここではリセットしない
                    NowAnim = null;
                    _isAttacking = true;
                }
                break;
            case "Attack2":
                _animator.SetTrigger("Attack2");  // トリガーをリセットしない
                NowAnim = null;
                break;
            case "Skill":
                _animator.SetTrigger("Skill");
                NowAnim = null;
                break;
            case "GotDamage":
                _animator.SetTrigger("GotDamage");
                NowAnim = null;
                break;
            case "Die":
                _animator.SetTrigger("Die");
                _animator.SetBool("Die", true);
                _animator.SetBool("Idle", false);
                _animator.SetBool("Move", false);
                NowAnim = null;
                break;
            case "Revive":
                _animator.SetTrigger("Revive");
                _animator.SetBool("Idle", false);
                _animator.SetBool("Move", false);
                _animator.SetBool("Die", false);
                NowAnim = null;
                break;
            case "Eat":
                _animator.SetTrigger("Eat");
                NowAnim = null;
                break;
            case null:
                break;
        }
    }
}
