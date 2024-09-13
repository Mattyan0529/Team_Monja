using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEnemyUI_MT : MonoBehaviour
{

    private CharacterDeadDecision_MT _characterDeadDecision;

    

    private GameObject _player;

    void Start()
    {
        _characterDeadDecision = GetComponentInParent<CharacterDeadDecision_MT>();
       
    }

    // Update is called once per frame
    void Update()
    {
      
    }


}
