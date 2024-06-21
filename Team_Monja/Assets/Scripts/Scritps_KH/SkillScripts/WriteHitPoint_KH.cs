using UnityEngine;

public class WriteHitPoint_KH : MonoBehaviour
{
    private CharacterAnim_MT _characterAnim;

    private GameObject _nowPlayer;

    /// <summary>
    /// ���̃L�����N�^�[��HP���X�V����i���炷�j
    /// </summary>
    /// <param name="afterAttackedHitPoint">�U�����ꂽ���HP</param>
    public void UpdateHitPoint(StatusManager_MT attackdStatus, int afterAttackedHitPoint)
    {
        _nowPlayer = GameObject.FindWithTag("Player");
        _characterAnim = _nowPlayer.GetComponent<CharacterAnim_MT>();
        attackdStatus.HP = afterAttackedHitPoint;
        _characterAnim.NowAnim = "GotDamage";
    }
}
