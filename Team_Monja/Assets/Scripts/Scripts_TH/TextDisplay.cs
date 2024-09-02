using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DemonTextDisplay : MonoBehaviour
{
    // セリフのリスト
    public string[] texts;
    // 現在表示しているセリフのインデックス
    int _textNumber;
    // 現在表示しているテキスト内容
    string _displayText;
    // 現在表示している文字のインデックス
    int _textCharNumber;
    // 表示速度を管理するカウンター
    private int _displayTextSpeed = 1000;
    // テキストの再生を制御するフラグ
    bool _speakBool = true;
    // クリック入力を検出するためのフラグ
    bool _click;
    // テキスト表示が終了したかどうかを管理するフラグ
    bool _textStop;
    // すべてのテキストが表示されたかを管理するフラグ
    bool _allTextsDisplayed = false;
    // TextMeshProUGUIコンポーネントの参照
    TextMeshProUGUI _textMeshPro;
    // セリフの最後の文字を表示し終えたかを判定するフラグ
    private bool _finishedSentence = false;
    // シーン遷移用オブジェクトの参照
    public GameObject sceneTransitionObject;

    void Start()
    {
        // TextMeshProUGUIコンポーネントを取得
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        // シーン遷移用オブジェクトを非アクティブに設定
        if (sceneTransitionObject != null)
        {
            sceneTransitionObject.SetActive(false);
        }
    }

    void Update()
    {
        // すべてのテキストが表示されていない場合に処理を行う
        if (!_allTextsDisplayed)
        {
            // テキスト表示が終了していない場合に表示を更新
            if (_textStop == false)
            {
                HandleTextDisplay();
                HandleInput();
            }
        }
        else
        {
            // すべてのテキストが表示された後、シーン遷移オブジェクトを有効にする
            if (sceneTransitionObject != null && !sceneTransitionObject.activeSelf)
            {
                sceneTransitionObject.SetActive(true);
                Debug.Log("Scene transition object activated.");
            }
        }
    }

    // テキスト表示の処理を行う
    void HandleTextDisplay()
    {
        // 表示速度のカウンターをインクリメント
        _displayTextSpeed++;
        if (_displayTextSpeed % 25 == 0) // 一定速度で文字を表示
        {
            if (_textCharNumber < texts[_textNumber].Length) // まだ表示する文字が残っている場合
            {
                // 次の文字を追加して表示
                _displayText += texts[_textNumber][_textCharNumber];
                _textCharNumber++;
                _finishedSentence = false; // セリフが完全に表示されていない
            }
            else // 現在のセリフが全て表示された場合
            {
                HandleTextEnd(); // テキストの終了処理を呼び出す
            }

            // 更新されたテキストを表示
            _textMeshPro.text = _displayText;
            _click = false; // クリックフラグをリセット
        }
    }

    // ユーザー入力を処理する
    void HandleInput()
    {
        // マウスクリックを検出した場合
        if (Input.GetMouseButtonDown(0))
        {
            _click = true; // クリックフラグをセット
            _finishedSentence = false; // セリフが完全に表示されていないことを明示
            Debug.Log(_finishedSentence); // デバッグ用にフラグの状態を出力
        }
    }

    // セリフが全て表示されたときの処理
    void HandleTextEnd()
    {
        if (_textNumber < texts.Length - 1) // 次のセリフが存在する場合
        {
            if (_click) // ユーザーがクリックした場合
            {
                ResetForNextText(); // 次のセリフを表示する準備を行う
            }
        }
        else // 最後のセリフが表示された場合
        {
            if (_click) // ユーザーがクリックした場合
            {
                _displayText = ""; // 表示テキストをリセット
                _textCharNumber = 0; // 文字インデックスをリセット
                _textStop = true; // テキスト表示終了フラグをセット
                _allTextsDisplayed = true; // すべてのテキストが表示されたことを示す
                Debug.Log("All texts have been displayed."); // デバッグ用にメッセージを出力
            }
        }
        _finishedSentence = true; // セリフの最後の文字を出し終えたことを示す
    }

    // 次のセリフを表示する準備を行う
    void ResetForNextText()
    {
        _displayText = ""; // 表示テキストをリセット
        _textCharNumber = 0; // 文字インデックスをリセット
        _textNumber++; // 次のセリフに進む
        _finishedSentence = false; // セリフの最後の文字が出し終わっていないことを示す
    }

    // すべてのテキストが表示されたかを確認するためのプロパティ
    public bool AllTextsDisplayed
    {
        get { return _allTextsDisplayed; }
    }

    // セリフの最後の文字が表示されたかを確認するためのプロパティ
    public bool FinishedSentence
    {
        get { return _finishedSentence; }
    }
}
