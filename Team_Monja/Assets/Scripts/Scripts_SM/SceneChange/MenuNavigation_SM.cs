using UnityEngine;
using UnityEngine.EventSystems;

public class MenuNavigation_SM : MonoBehaviour
{
    // 追記：北
    [SerializeField]
    private GameObject _audioObj = default;
    private SoundEffectManagement_KH _soundEffectManagement = default;
    private AudioSource[] _audioSource = default;

    private EventSystem _eventSystem;   // EventSystemを格納するための変数
    private GameObject _selectedObject; // 現在選択されているオブジェクトを格納するための変数
    private GameObject _previousSelectedObject; // 追加：前回選択されていたオブジェクト

    void Start()
    {
        // 追記：北
        _soundEffectManagement = _audioObj.GetComponent<SoundEffectManagement_KH>();
        _audioSource = _audioObj.GetComponents<AudioSource>();

        _eventSystem = EventSystem.current; // 現在のEventSystemを取得
        _selectedObject = _eventSystem.firstSelectedGameObject; // 最初に選択されるゲームオブジェクトを取得
        _previousSelectedObject = _selectedObject; // 追加：初期化時に前回の選択も同じに設定

        // 最初のボタンを明示的に選択状態にする
        _eventSystem.SetSelectedGameObject(_selectedObject);
    }

    void Update()
    {
        // GetAxisを使って入力をチェック（ゲームパッド対応）：北
        float horizontal = Input.GetAxis("Horizontal"); // 水平方向の入力を取得
        float vertical = Input.GetAxis("Vertical");     // 垂直方向の入力を取得

        // 入力があった場合に処理を行う
        if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
        {
            // 現在選択されているオブジェクトがnullの場合
            if (_eventSystem.currentSelectedGameObject == null)
            {
                // 最初に選択されるオブジェクトを再度選択
                _eventSystem.SetSelectedGameObject(_selectedObject);
            }
            else
            {
                // 現在選択されているオブジェクトを更新
                _selectedObject = _eventSystem.currentSelectedGameObject;
            }

            // 選択オブジェクトが前回と異なる場合にのみ音を再生する
            if (_selectedObject != _previousSelectedObject)
            {
                // 1つ目のAudioSourceからSEを鳴らす
                _soundEffectManagement.PlayBigSelectionSound(_audioSource[0]);
                Debug.Log("SE");

                // 前回選択されていたオブジェクトを更新
                _previousSelectedObject = _selectedObject;
            }

            // マウスオーバー中のオブジェクトが選択されている場合、それをクリア
            if (HoverSelectable_SM._currentHoveredObject == _selectedObject)
            {
                HoverSelectable_SM._currentHoveredObject = null;
            }
        }
    }
}
