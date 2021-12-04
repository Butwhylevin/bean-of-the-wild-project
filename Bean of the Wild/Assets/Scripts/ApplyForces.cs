using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyForces : MonoBehaviour
{
    public GameObject stasisArrowPrefab;
    GameObject stasisArrowInstance;
    public Vector3 forces;
    public float frozenMultiplier = 2f;
    public bool frozen = false;
    Rigidbody rb;

    private void Start() 
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate() 
    {
        if(forces != Vector3.zero && !frozen)
        {
            rb.AddForce(forces);
            forces = Vector3.zero;
        }
    }

    public void AddForce(Vector3 force)
    {
        forces += (force * frozenMultiplier);

        // set direction of stasis arrow
        if(frozen)
        {
            stasisArrowInstance.SetActive(true);
            stasisArrowInstance.transform.rotation = Quaternion.LookRotation(forces);
        }
    }

    public void MultiplyVelocity(float vel)
    {
        rb.velocity = (rb.velocity * vel);
    }

    public void StasisFreeze(float stasisTime)
    {
        frozen = true;
        rb.constraints = RigidbodyConstraints.FreezeAll;

        stasisArrowInstance = Instantiate(stasisArrowPrefab, transform);
        stasisArrowInstance.SetActive(false);

        Invoke(nameof(EndStasis), stasisTime);
    }

    public void EndStasis()
    {
        // destroy arrow and disable freezing
        frozen = false;
        rb.constraints = RigidbodyConstraints.None;
        Destroy(stasisArrowInstance);

        // apply built-up forces
        rb.AddForce(forces);
        forces = Vector3.zero;
    }
}
