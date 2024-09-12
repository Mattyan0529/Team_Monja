using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSkillAreaMove_KH : MonoBehaviour
{
    private GameObject _slimeSphere = default;
    private SlimeLuncer _slimeLuncer = default;

    private void Start()
    {
        _slimeLuncer = transform.parent.GetComponent<SlimeLuncer>();
        gameObject.transform.position = _slimeSphere.transform.position;
    }

    void Update()
    {
        if (_slimeSphere == null) 
        {
            gameObject.transform.position = _slimeLuncer.LastSpherePos.position;
        }
        else
        {
            gameObject.transform.position = _slimeSphere.transform.position;
        }
    }

    private void OnEnable()
    {
        _slimeSphere = GameObject.FindGameObjectWithTag("SlimeSphere");
    }
}
