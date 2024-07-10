using UnityEngine;
using UnityEngine.EventSystems;

public class MenuNavigation_SM : MonoBehaviour
{
    // 追記：北
    [SerializeField]
    private GameObject _audioObj = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private AudioSource[] _audioSource = default;

    private EventSystem eventSystem;   // EventSystemを格納するための変数
    private GameObject selectedObject; // 現在選択されているオブジェクトを格納するための変数

    void Start()
    {
        // 追記：北
        _soundEffectManagement = _audioObj.GetComponent<SoundEffectManagement_KH>();
        _audioSource = _audioObj.GetComponents<AudioSource>();

        eventSystem = EventSystem.current; // 現在のEventSystemを取得
        selectedObject = eventSystem.firstSelectedGameObject; // 最初に選択されるゲームオブジェクトを取得

        // 最初のボタンを明示的に選択状態にする
        eventSystem.SetSelectedGameObject(selectedObject);
    }

    void Update()
    {
        // 矢印キーの入力をチェック
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            // 現在選択されているオブジェクトがnullの場合
            if (eventSystem.currentSelectedGameObject == null)
            {
                // 最初に選択されるオブジェクトを再度選択
                eventSystem.SetSelectedGameObject(selectedObject);
            }
            else
            {
                // 現在選択されているオブジェクトを更新
                selectedObject = eventSystem.currentSelectedGameObject;
            }

            // 1つ目のAudioSourceからSEを鳴らす（AudioSourceは別にどっちでもいい）：北
            _soundEffectManagement.PlayBigSelectionSound(_audioSource[0]);
        }
    }
}