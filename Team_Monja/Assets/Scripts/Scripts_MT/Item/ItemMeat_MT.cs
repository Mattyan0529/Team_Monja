using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMeat_MT : MonoBehaviour
{
    StatusManager_MT _statusManager;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, 3, 0,Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            _statusManager = other.GetComponent<StatusManager_MT>();
            //Œ»İ‚ÌMaxHP‚Ì”¼•ª‚ğ‰ñ•œ‚·‚é
            _statusManager.HealHP(_statusManager.MaxHP / 2);
        }
    }

}
