using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour {

    // Use this for initialization


    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector3.down);
        Debug.DrawRay(gameObject.transform.position, Vector3.down);
        //Debug.DrawRay(hitpoint.transform.localPosition, Vector3.down);
        if (hit.collider != null)
        {
            print("dsadsa");
        }
    }
}
