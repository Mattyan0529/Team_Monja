using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSkillAreaMove_KH : MonoBehaviour
{
    private GameObject _slimeSphere = default;
    private SlimeLuncer _slimeLuncer = default;
    private Vector3 _initialPosition = new Vector3(0f, 2.11f, 2.53f);

    private void Start()
    {
        _slimeLuncer = transform.parent.GetComponent<SlimeLuncer>();
        if (GameObject.FindGameObjectWithTag("SlimeSphere") != null)
        {
            _slimeSphere = GameObject.FindGameObjectWithTag("SlimeSphere");
        }

        if (_slimeSphere == null)
        {
            gameObject.transform.position = _slimeLuncer.LastSpherePos.position;
        }
        else
        {
            gameObject.transform.position = _slimeSphere.transform.position;
        }
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

    private void OnDisable()
    {
        gameObject.transform.position = _initialPosition;
    }
}
