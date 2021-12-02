using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFloat : MonoBehaviour
{
    public float floatForce; 
    public bool touchingWater = false;
    ApplyForces forceScript;

    private void Start() 
    {
        forceScript = gameObject.GetComponent<ApplyForces>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.layer == 4)
        {
            touchingWater = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.layer == 4)
        {
            touchingWater = false;
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if(other.gameObject.layer == 4) // if touching water
        {
            // find distance to the top of the water
            float dist = other.bounds.max.y - transform.position.y + 0.1f;
            // apply force to the forceScript
            forceScript.AddForce(-Vector3.up * floatForce * dist);
        }
    }
}
