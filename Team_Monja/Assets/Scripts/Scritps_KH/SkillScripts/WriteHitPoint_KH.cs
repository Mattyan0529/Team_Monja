using UnityEngine;

public class WriteHitPoint_KH : MonoBehaviour
{
    private CharacterAnim_MT _characterAnim;

    private GameObject _nowPlayer;
    private void Start()
    {
        _characterAnim = GetComponent<CharacterAnim_MT>();
        //_nowPlayer = GameObject.FindWithTag("Player");
    }

    /// <summary>
    /// ���̃L�����N�^�[��HP���X�V����i���炷�j
    /// </summary>
    /// <param name="afterAttackedHitPoint">�U�����ꂽ���HP</param>
    public void UpdateHitPoint(StatusManager_MT attackdStatus, int afterAttackedHitPoint)
    {
        attackdStatus.HP = afterAttackedHitPoint;
        _characterAnim.NowAnim = "Move";
    }
}
