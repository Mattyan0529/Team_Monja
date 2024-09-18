using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim_MT : MonoBehaviour
{
    private Animator _animator;

    private string _nowAnim = default;
    private string _currentAnim = default;
    private bool _isAttacking = false;

    // プロパティでアニメーションの現在状態を管理
    public string NowAnim { get { return _nowAnim; } set { _nowAnim = value; } }

    // 初期化処理
    private void Start()
    {
        _animator = GetComponent<Animator>();
        NowAnim = "Idle"; // 初期アニメーション
    }

    // 毎フレームの処理
    private void LateUpdate()
    {
        AnimSwitch(); // アニメーションの切り替え

        // 攻撃が終わったらリセット
        if (_isAttacking && NowAnim != "Attack")
        {
            _isAttacking = false;
        }

        // 現在のアニメーションを保存
        _currentAnim = NowAnim;
    }

    // アニメーションの切り替えロジック
    private void AnimSwitch()
    {
        switch (NowAnim)
        {
            case "Idle":
                _animator.SetBool("Idle", true);
                _animator.SetBool("Move", false);
                ResetAnimTrigger();
                break;

            case "Move":
                _animator.SetBool("Move", true);
                _animator.SetBool("Idle", false);
                ResetAnimTrigger();
                break;

            case "Attack":
                if (!_isAttacking) // 攻撃中かどうかチェック
                {
                    _animator.SetTrigger("Attack");
                    _isAttacking = true;
                }
                break;

            case "Attack2":
                _animator.SetTrigger("Attack2");
                break;

            case "Skill":
                _animator.SetTrigger("Skill");
                break;

            case "GotDamage":
                _animator.SetTrigger("GotDamage");
                break;

            case "Die":
                _animator.SetTrigger("Die");
                _animator.SetBool("Die", true);
                _animator.SetBool("Idle", false);
                _animator.SetBool("Move", false);
                break;

            case "Revive":
                _animator.SetTrigger("Revive");
                _animator.SetBool("Idle", false);
                _animator.SetBool("Move", false);
                _animator.SetBool("Die", false);
                break;

            case "Eat":
                _animator.SetTrigger("Eat");
                break;

            case null:
                // アニメーションなし
                break;
        }
    }

    // アニメーションフラグのリセット用メソッド
    private void ResetAnimTrigger()
    {
        // 単発アニメーションのトリガーをリセット
        _animator.ResetTrigger("Attack");
        _animator.ResetTrigger("Attack2");
        _animator.ResetTrigger("Skill");
        _animator.ResetTrigger("GotDamage");
        _animator.ResetTrigger("Die");
        _animator.ResetTrigger("Revive");
        _animator.ResetTrigger("Eat");

        // NowAnimをリセット
        NowAnim = null;
    }
}
