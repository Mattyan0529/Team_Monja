using System.Collections;
using UnityEngine;

public class FlameDelay_TH : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _attackRangeImage = default;
    [SerializeField]
    private GameObject[] _attackRangeFrame = default;
    [SerializeField]
    private float _fireSphereDelay = 3f; // FireSphere������̒x�����ԁi�b�j
    [SerializeField]
    private float _frameDisplayTime = 2f; // �U���͈̓t���[�����\������鎞�ԁi�b�j
    [SerializeField]
    private float _sphereDeleteTime = 2f; // �U���͈͂��\������鎞�ԁi�b�j

    public void StartFireSphereCoroutine(Vector3 playerPos, Vector3 nearPlayerPos, Vector3 farPlayerPos, AudioSource audioSource, SoundEffectManagement_KH soundEffectManagement, ChangeEnemyMoveType_KH changeEnemyMoveType, CharacterAnim_MT characterAnim, WriteHitPoint_KH writeHitPoint, CreateDamageImage_KH createDamageImage)
    {
        StartCoroutine(FireSphereCoroutine(playerPos, nearPlayerPos, farPlayerPos, audioSource, soundEffectManagement, changeEnemyMoveType, characterAnim, writeHitPoint, createDamageImage));
    }

    private IEnumerator FireSphereCoroutine(Vector3 playerPos, Vector3 nearPlayerPos, Vector3 farPlayerPos, AudioSource audioSource, SoundEffectManagement_KH soundEffectManagement, ChangeEnemyMoveType_KH changeEnemyMoveType, CharacterAnim_MT characterAnim, WriteHitPoint_KH writeHitPoint, CreateDamageImage_KH createDamageImage)
    {
        // �w�肵�����ԑҋ@
        yield return new WaitForSeconds(_fireSphereDelay);

        // �U���͈͂̃t���[����\��
        _attackRangeFrame[0].SetActive(true);
        _attackRangeFrame[1].SetActive(true);
        _attackRangeFrame[2].SetActive(true);

        // �t���[���̈ʒu��ݒ�
        _attackRangeFrame[0].transform.position = playerPos;
        _attackRangeFrame[1].transform.position = nearPlayerPos;
        _attackRangeFrame[2].transform.position = farPlayerPos;

        // �t���[�����\������鎞�ԑҋ@
        yield return new WaitForSeconds(_frameDisplayTime);

        // �t���[�����A�N�e�B�u�ɂ���
        _attackRangeFrame[0].SetActive(false);
        _attackRangeFrame[1].SetActive(false);
        _attackRangeFrame[2].SetActive(false);

        // �U���͈͂̕\�����Ԃ��Ǘ�
        yield return new WaitForSeconds(_sphereDeleteTime);

        foreach (var attackRange in _attackRangeImage)
        {
            attackRange.SetActive(false);
        }

        // �U���������������Ƃ�����
        changeEnemyMoveType.IsMove = true;
    }
}
