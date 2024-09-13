using System.Diagnostics;
using UnityEngine;

public class WriteHitPoint_KH : MonoBehaviour
{    //コンポーネント
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
    /// そのキャラクターのHPを更新する（減らす）
    /// </summary>
    /// <param name="afterAttackedHitPoint">攻撃された後のHP</param>
    public void UpdateHitPoint(StatusManager_MT attackedStatus, int afterAttackedHitPoint)
    {
        //ダメージを受けたのがプレイヤーなら
        if (attackedStatus.gameObject.CompareTag("Player"))
        {
            _vibration.VibrateController(1f,1f,0.75f);
        }
        //ダメージを受けたのが敵なら
        else
        {
            _vibration.VibrateController(0.15f, 0.15f, 0.5f);
        }


        _characterAnim = attackedStatus.gameObject.GetComponent<CharacterAnim_MT>();

        _audioSource = attackedStatus.gameObject.GetComponent<AudioSource>();

        //ステータスを更新
        attackedStatus.HP = afterAttackedHitPoint;
        //ダメージくらったときのアニメ    
        _characterAnim.NowAnim = "GotDamage";

        _soundEffectManagement.PlayDamageSound(_audioSource);
    }
}
