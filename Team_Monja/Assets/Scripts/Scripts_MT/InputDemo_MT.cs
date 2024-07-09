using System.Collections.Generic;
using UnityEngine;
public class InputDemo_MT : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
     
        float Ltrigger = Input.GetAxis("skill");
        float Rtrigger = Input.GetAxis("attack");
        if(Ltrigger>0.3f)
        {
            Debug.Log("trigger" + Ltrigger);
        }
        if(Rtrigger>0.3f)
        {
            Debug.Log("trigger" + Rtrigger);
        }
    }
}