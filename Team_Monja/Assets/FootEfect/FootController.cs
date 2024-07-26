using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootController : MonoBehaviour
{
    public GameObject footPrintPrefab;
    float time = 0;

    void Update()
    {
        this.time += Time.deltaTime;
        if (this.time > 0.35f)
        {
            this.time = 0;
            GameObject footPrint = Instantiate(footPrintPrefab, transform.position, transform.rotation);
            Destroy(footPrint, 2.0f); // 2•bŒã‚É‘«Õ‚ğ”jŠü‚·‚é—á
        }
    }

}