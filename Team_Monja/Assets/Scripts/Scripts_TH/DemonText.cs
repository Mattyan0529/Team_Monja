using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonText : MonoBehaviour
{
    DemonTextDisplay text;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.FindGameObjectWithTag("DemonText").GetComponent<DemonTextDisplay>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(text.FinishedSentence == false)
        {
            anim.SetBool("Speak",true);
        }
        else if(text.FinishedSentence == true)
        {
            anim.SetBool("Speak", false);
        }
    }
}
