using UnityEngine;

public class WriteHitPoint_KH : MonoBehaviour
{    //コンポーネント
    private MoveSlider_MT _moveSlider;
    private CharacterAnim_MT _characterAnim;
    private GameObject _nowPlayer;

    private SoundEffectManagement_KH _soundEffectManagement = default;

    //canvas
    [SerializeField] private GameObject _canvasObj;

    private void Start()
    {
        _soundEffectManagement = GetComponent<SoundEffectManagement_KH>();
    }

    /// <summary>
    /// そのキャラクターのHPを更新する（減らす）
    /// </summary>
    /// <param name="afterAttackedHitPoint">攻撃された後のHP</param>
    public void UpdateHitPoint(StatusManager_MT attackdStatus, int afterAttackedHitPoint)
    {
        //現在のキャラクター取得
        _nowPlayer = GameObject.FindWithTag("Player");
        _characterAnim = _nowPlayer.GetComponent<CharacterAnim_MT>();
        _moveSlider = _canvasObj.GetComponentInChildren<MoveSlider_MT>();
        //HPバー更新
        if (_moveSlider != null)
        {
            if(CompareTag("Player"))
            {
                _moveSlider.SetCurrentHP(afterAttackedHitPoint);
            }
        }
        //ステータスを更新
        attackdStatus.HP = afterAttackedHitPoint;

        // SE
        _soundEffectManagement.PlayDamageSound(attackdStatus.gameObject.GetComponent<AudioSource>());

        //ダメージくらったときのアニメ    
        _characterAnim.NowAnim = "GotDamage";
    }
}
