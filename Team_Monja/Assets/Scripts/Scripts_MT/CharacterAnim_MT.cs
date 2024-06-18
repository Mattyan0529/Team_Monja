using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim_MT : MonoBehaviour
{
    Animator animator;


    private string _nowAnim = default;

    public string NowAnim { get { return _nowAnim; } set { _nowAnim = value; } }


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void AnimSwitch()
    {
        switch (NowAnim)
        {
            case "Move":
                //trigger
                break;
            case "Attack":
                //trigger
                break;
            case "Skill":
                //trigger
                break;
            case "GotDamage":
                //trigger
                break;
            case "Die":
                //bool
                break;
        }
    }


}
