using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationRotate : MonoBehaviour
{
    public Transform playerTransform, lookTrans;
    public float xRotSpd, yRotSpd;
    public Vector2 look;
    
    private void Update() 
    {
        look = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
    }
    
    private void FixedUpdate() 
    {   
        transform.rotation *= Quaternion.AngleAxis(look.x * yRotSpd, Vector3.up);
        lookTrans.rotation *= Quaternion.AngleAxis(-look.y * xRotSpd, Vector3.right);
        transform.position = playerTransform.position;
    }
}
