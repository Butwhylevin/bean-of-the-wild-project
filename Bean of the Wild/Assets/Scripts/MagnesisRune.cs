using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnesisRune : MonoBehaviour
{
    RuneRaycaster runeScript;
    [HideInInspector] public ApplyForces forceScript = null;
    public bool movePoint = false;
    public GameObject playerObj;
    Transform pointTransform;
    public LayerMask layerMask;
    public float minDist, maxDist, curDist;
    public float forceAmount;
    public float maxForceApplyDist;
    public float freezeDist = 1, freezeDampen = 0.5f;

    //input
    bool increaseDist, decreaseDist;

    private void Start() 
    {
        pointTransform = gameObject.transform.GetChild(0).gameObject.transform;
        runeScript = transform.parent.gameObject.GetComponent<RuneRaycaster>();
    }

    private void Update() 
    {
        ChangeDistance();
    }

    private void ChangeDistance()
    {
        // get input
        increaseDist = Input.GetKey(KeyCode.R);
        decreaseDist = Input.GetKey(KeyCode.F);

        // apply change
        float distChange = 0;
        if(increaseDist)
        {
            distChange += 1;
        }
        if(decreaseDist)
        {
            distChange -= 1;
        }

        curDist += distChange;

        // clamp distance
        curDist = Mathf.Clamp(curDist, minDist, maxDist);
    }

    private void FixedUpdate() 
    {
        if(movePoint) { MoveMagnesisPoint(); }
        if(forceScript != null) { ForceTowardsPoint(); }
    }

    private void MoveMagnesisPoint()
    {
        // raycast out
        Vector3 setPosition;
        RaycastHit hit;
        Ray ray = runeScript.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        
        if(Physics.Raycast(ray, out hit, curDist, layerMask))
        {
            setPosition = hit.point;
        }
        else
        {
            // set it to a position cur dist out
            setPosition = (runeScript.cam.gameObject.transform.position + (runeScript.cam.gameObject.transform.forward * curDist));
        }
        
        // set position
        pointTransform.position = setPosition;

        // point it at the player
        pointTransform.LookAt(playerObj.transform);
    }

    public void StartMagnesis(GameObject objectHit)
    {
        // change layer of object hit and disable gravity
        objectHit.gameObject.layer = 6;
        objectHit.GetComponent<Rigidbody>().useGravity = false;
        // set position of magnesis point
        movePoint = true;
        // move the hit object towards the point
        forceScript = objectHit.gameObject.GetComponent<ApplyForces>();
    }

    public void EndMagnesis()
    {
        // change layer of object hit and enable gravity
        forceScript.gameObject.layer = 3;
        forceScript.gameObject.GetComponent<Rigidbody>().useGravity = true;
        
        movePoint = false;
        forceScript = null; 
    }

    private void ForceTowardsPoint()
    {
        // move the rigidbody of the object towards the point
        Vector3 dir = pointTransform.position - forceScript.gameObject.transform.position;
        dir = dir.normalized;

        // dampen the force amount if it gets closer
        float dist = Vector3.Distance(pointTransform.position, forceScript.gameObject.transform.position);
        
        float distMultiplier = 1;

        if(dist < maxForceApplyDist)
        {
            distMultiplier = dist / maxForceApplyDist;
        }

        if(dist < freezeDist)
        {
            forceScript.MultiplyVelocity(0.5f);
        }

        forceScript.AddForce(dir * forceAmount * distMultiplier);
    }
}
