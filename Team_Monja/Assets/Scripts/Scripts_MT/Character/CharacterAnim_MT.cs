using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim_MT : MonoBehaviour
{
    private Animator _animator;

    private string _nowAnim = default;
    private string _currentAnim = default;
    private bool _isAttacking = false;

    // �v���p�e�B�ŃA�j���[�V�����̌��ݏ�Ԃ��Ǘ�
    public string NowAnim { get { return _nowAnim; } set { _nowAnim = value; } }

    // ����������
    private void Start()
    {
        _animator = GetComponent<Animator>();
        NowAnim = "Idle"; // �����A�j���[�V����
    }

    // ���t���[���̏���
    private void LateUpdate()
    {
        AnimSwitch(); // �A�j���[�V�����̐؂�ւ�

        // �U�����I������烊�Z�b�g
        if (_isAttacking && NowAnim != "Attack")
        {
            _isAttacking = false;
        }

        // ���݂̃A�j���[�V������ۑ�
        _currentAnim = NowAnim;
    }

    // �A�j���[�V�����̐؂�ւ����W�b�N
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
                if (!_isAttacking) // �U�������ǂ����`�F�b�N
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
                // �A�j���[�V�����Ȃ�
                break;
        }
    }

    // �A�j���[�V�����t���O�̃��Z�b�g�p���\�b�h
    private void ResetAnimTrigger()
    {
        // �P���A�j���[�V�����̃g���K�[�����Z�b�g
        _animator.ResetTrigger("Attack");
        _animator.ResetTrigger("Attack2");
        _animator.ResetTrigger("Skill");
        _animator.ResetTrigger("GotDamage");
        _animator.ResetTrigger("Die");
        _animator.ResetTrigger("Revive");
        _animator.ResetTrigger("Eat");

        // NowAnim�����Z�b�g
        NowAnim = null;
    }
}
