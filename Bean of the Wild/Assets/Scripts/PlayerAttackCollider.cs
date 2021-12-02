using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
{
    public PlayerAttack attackScript;
    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Hit something" + other.gameObject.ToString());
        ApplyForces forceScript = other.gameObject.GetComponent<ApplyForces>();
        if(forceScript != null)
        {
            forceScript.AddForce(transform.forward * attackScript.attackForce);
        }
    }
}
