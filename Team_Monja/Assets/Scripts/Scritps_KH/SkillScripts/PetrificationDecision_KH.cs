using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetrificationDecision_KH : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.transform.parent.tag == "Enemy" && other.gameObject.tag == "Player")
        {
            transform.parent.GetComponent<Petrification_KH>().Petrification(other.gameObject);
        }
        else if (gameObject.transform.parent.tag == "Player" && other.gameObject.tag == "Enemy")
        {
            transform.parent.GetComponent<Petrification_KH>().Petrification(other.gameObject);
        }
    }
}
