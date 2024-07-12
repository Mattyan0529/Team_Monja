using UnityEngine;

public class NormalAttackHitDecision_KH : MonoBehaviour
{
    private NormalAttack_KH _normalAttack;

    void Start()
    {
        // �e�I�u�W�F�N�g���� NormalAttack_KH �R���|�[�l���g���擾����
        _normalAttack = GetComponentInParent<NormalAttack_KH>();
        if (_normalAttack == null)
        {
            Debug.LogError("NormalAttack_KH script not found in parent GameObject.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���̃I�u�W�F�N�g���U���͈͂ɓ��������̏���
        // �U���͈͂ɓ������I�u�W�F�N�g���G�܂��̓{�X�ł���΁A�U��������s��
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boss"))
        {
            _normalAttack?.HitDecision(other.gameObject);
        }
    }
}
