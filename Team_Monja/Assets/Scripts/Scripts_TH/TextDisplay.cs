using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DemonTextDisplay : MonoBehaviour
{
    public string[] texts;
    int textNumber;
    string displayText;
    int textCharNumber;
    private int displayTextSpeed = 1000;
    bool _SpeakBool = true;
    bool click;
    bool textStop;
    TextMeshProUGUI textMeshPro;
    public bool finishedSentence = false; // �Z���t�̍Ō�̕������o���I��������Ƃ𔻒肷��BOOL�l

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (textStop == false)
        {
            displayTextSpeed++;
            if (displayTextSpeed % 25 == 0)
            {
                if (textCharNumber != texts[textNumber].Length)
                {
                    displayText += texts[textNumber][textCharNumber];
                    textCharNumber++;
                    finishedSentence = false;
                }
                else
                {
                    if (textNumber != texts.Length - 1)
                    {
                        if (click == true)
                        {
                            displayText = "";
                            textCharNumber = 0;
                            textNumber++;
                            print(finishedSentence);
                            finishedSentence = false; // Reset the flag
                        }
                    }
                    else
                    {
                        if (click == true)
                        {
                            displayText = "";
                            textCharNumber = 0;
                            textStop = true;
                            print(finishedSentence);
                            finishedSentence = false; // Reset the flag
                        }
                    }
                    finishedSentence = true; // �Z���t�̍Ō�̕������o���I��������Ƃ�����
                }

                textMeshPro.text = displayText;
                click = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                click = true;
                finishedSentence = false;
                print(finishedSentence);
            }
        }
    }
}
