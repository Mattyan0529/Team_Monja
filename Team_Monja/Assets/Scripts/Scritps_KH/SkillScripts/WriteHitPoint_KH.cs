using System.Diagnostics;
using UnityEngine;

public class WriteHitPoint_KH : MonoBehaviour
{    //�R���|�[�l���g
    private ControllerVibration_MT _vibration;
    private CharacterAnim_MT _characterAnim;
    private GameObject _nowPlayer;

    private SoundEffectManagement_KH _soundEffectManagement;
    private AudioSource _audioSource;

    //canvas
    [SerializeField] private GameObject _canvasObj;

    private void Start()
    {
        _soundEffectManagement = GetComponent<SoundEffectManagement_KH>();
        _vibration = GetComponent<ControllerVibration_MT>();
    }

    /// <summary>
    /// ���̃L�����N�^�[��HP���X�V����i���炷�j
    /// </summary>
    /// <param name="afterAttackedHitPoint">�U�����ꂽ���HP</param>
    public void UpdateHitPoint(StatusManager_MT attackedStatus, int afterAttackedHitPoint)
    {
        //�_���[�W���󂯂��̂��v���C���[�Ȃ�
        if (attackedStatus.gameObject.CompareTag("Player"))
        {
            _vibration.VibrateController(1f,1f,0.75f);
        }
        //�_���[�W���󂯂��̂��G�Ȃ�
        else
        {
            _vibration.VibrateController(0.15f, 0.15f, 0.5f);
        }


        _characterAnim = attackedStatus.gameObject.GetComponent<CharacterAnim_MT>();

        _audioSource = attackedStatus.gameObject.GetComponent<AudioSource>();

        //�X�e�[�^�X���X�V
        attackedStatus.HP = afterAttackedHitPoint;
        //�_���[�W��������Ƃ��̃A�j��    
        _characterAnim.NowAnim = "GotDamage";

        _soundEffectManagement.PlayDamageSound(_audioSource);
    }
}
