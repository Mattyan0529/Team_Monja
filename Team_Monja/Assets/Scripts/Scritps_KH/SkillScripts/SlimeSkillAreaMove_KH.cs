using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSkillAreaMove_KH : MonoBehaviour
{
    private GameObject _slimeSphere = default;

    void Update()
    {
        gameObject.transform.position = _slimeSphere.transform.position;
    }

    private void OnEnable()
    {
        _slimeSphere = GameObject.FindGameObjectWithTag("SlimeSphere");
    }
}