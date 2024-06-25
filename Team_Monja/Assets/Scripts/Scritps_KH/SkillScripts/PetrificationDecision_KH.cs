using UnityEngine;

public class PetrificationDecision_KH : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if ((gameObject.transform.parent.tag == "Enemy" || gameObject.transform.parent.tag == "Boss") && other.gameObject.tag == "Player")
        {
            transform.parent.GetComponent<Petrification_KH>().Petrification(other.gameObject);
        }
        else if (gameObject.transform.parent.tag == "Player" && (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss"))
        {
            transform.parent.GetComponent<Petrification_KH>().Petrification(other.gameObject);
        }
    }
}
