using System.Collections;
using UnityEngine;

public class FlameDelay_TH : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _attackRangeImage = default;
    [SerializeField]
    private GameObject[] _attackRangeFrame = default;
    [SerializeField]
    private float _fireSphereDelay = 3f; // FireSphere発動後の遅延時間（秒）
    [SerializeField]
    private float _frameDisplayTime = 2f; // 攻撃範囲フレームが表示される時間（秒）
    [SerializeField]
    private float _sphereDeleteTime = 2f; // 攻撃範囲が表示される時間（秒）

    public void StartFireSphereCoroutine(Vector3 playerPos, Vector3 nearPlayerPos, Vector3 farPlayerPos, AudioSource audioSource, SoundEffectManagement_KH soundEffectManagement, ChangeEnemyMoveType_KH changeEnemyMoveType, CharacterAnim_MT characterAnim, WriteHitPoint_KH writeHitPoint, CreateDamageImage_KH createDamageImage)
    {
        StartCoroutine(FireSphereCoroutine(playerPos, nearPlayerPos, farPlayerPos, audioSource, soundEffectManagement, changeEnemyMoveType, characterAnim, writeHitPoint, createDamageImage));
    }

    private IEnumerator FireSphereCoroutine(Vector3 playerPos, Vector3 nearPlayerPos, Vector3 farPlayerPos, AudioSource audioSource, SoundEffectManagement_KH soundEffectManagement, ChangeEnemyMoveType_KH changeEnemyMoveType, CharacterAnim_MT characterAnim, WriteHitPoint_KH writeHitPoint, CreateDamageImage_KH createDamageImage)
    {
        // 指定した時間待機
        yield return new WaitForSeconds(_fireSphereDelay);

        // 攻撃範囲のフレームを表示
        _attackRangeFrame[0].SetActive(true);
        _attackRangeFrame[1].SetActive(true);
        _attackRangeFrame[2].SetActive(true);

        // フレームの位置を設定
        _attackRangeFrame[0].transform.position = playerPos;
        _attackRangeFrame[1].transform.position = nearPlayerPos;
        _attackRangeFrame[2].transform.position = farPlayerPos;

        // フレームが表示される時間待機
        yield return new WaitForSeconds(_frameDisplayTime);

        // フレームを非アクティブにする
        _attackRangeFrame[0].SetActive(false);
        _attackRangeFrame[1].SetActive(false);
        _attackRangeFrame[2].SetActive(false);

        // 攻撃範囲の表示時間を管理
        yield return new WaitForSeconds(_sphereDeleteTime);

        foreach (var attackRange in _attackRangeImage)
        {
            attackRange.SetActive(false);
        }

        // 攻撃が完了したことを示す
        changeEnemyMoveType.IsMove = true;
    }
}
