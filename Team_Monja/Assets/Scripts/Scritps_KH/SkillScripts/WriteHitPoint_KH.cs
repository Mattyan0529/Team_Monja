using UnityEngine;

public class WriteHitPoint_KH : MonoBehaviour
{    //�R���|�[�l���g
    private MoveSlider_MT _moveSlider;
    private CharacterAnim_MT _characterAnim;
    private GameObject _nowPlayer;

    //canvas
    [SerializeField] private GameObject _canvasObj;

    /// <summary>
    /// ���̃L�����N�^�[��HP���X�V����i���炷�j
    /// </summary>
    /// <param name="afterAttackedHitPoint">�U�����ꂽ���HP</param>
    public void UpdateHitPoint(StatusManager_MT attackdStatus, int afterAttackedHitPoint)
    {
        //���݂̃L�����N�^�[�擾
        _nowPlayer = GameObject.FindWithTag("Player");
        _characterAnim = _nowPlayer.GetComponent<CharacterAnim_MT>();
        _moveSlider = _canvasObj.GetComponentInChildren<MoveSlider_MT>();
        //HP�o�[�X�V
        if (_moveSlider != null)
        {
            if(CompareTag("Player"))
            {
                _moveSlider.SetCurrentHP(afterAttackedHitPoint);
            }
        }
        //�X�e�[�^�X���X�V
        attackdStatus.HP = afterAttackedHitPoint;
        //�_���[�W��������Ƃ��̃A�j��    
        _characterAnim.NowAnim = "GotDamage";
    }
}
