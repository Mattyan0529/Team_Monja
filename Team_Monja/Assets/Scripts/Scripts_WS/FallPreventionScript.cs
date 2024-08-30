using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPreventionScript : MonoBehaviour
{
    private GameObject playableMonstor;
    private Vector3 stopPoint;
    private float rayDistance = 1f;
    private RaycastHit rayHit;
    private LayerMask layer;

    // Start is called before the first frame update
    void Start()
    {
        stopPoint = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(stopPoint, -transform.up,rayDistance, layer);
        Debug.DrawLine(stopPoint,-transform.up, Color.red);
    }
}
