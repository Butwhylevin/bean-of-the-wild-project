using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbing : MonoBehaviour
{
    public Rigidbody rb;
    public bool climbing;
    public LayerMask climbable;
    public float climbSpeed, raycastDist;
    public Transform raycastPoint;
    float x, y;
    bool jump, quit;

    private void FixedUpdate() 
    {
        if(!climbing)
        {
            CheckForClimb();
        }
        else
        {
            ClimbMovement();
        }
    }

    private void Update()
    {
        GetInput();
    }

    private void ClimbMovement()
    {
        // point at the wall
        
        // move from right to left

        // move up and down

        // edge detection?
    }

    private void GetInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        jump = Input.GetButton("Jump");
        quit = Input.GetKeyDown(KeyCode.LeftControl);
    }

    private void CheckForClimb()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward,  out hit, raycastDist, climbable))
        {
            climbing = true;
        }
    }

}
