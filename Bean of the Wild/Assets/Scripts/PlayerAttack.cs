using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject swordObj;
    public float attackForce;
    bool attackInput;
    bool attacking;
    public float attackTime = 1f;

    private void Start() 
    {
        swordObj.SetActive(false);
    }
    
    private void Update() 
    {
        GetInput();
        DoAttacking();
    }

    private void GetInput()
    {
        attackInput = Input.GetMouseButtonDown(0);
    }

    private void DoAttacking()
    {
        if(attackInput && !attacking)
        {
            swordObj.SetActive(true);
            attacking = true;
            Invoke(nameof(EndAttacking), attackTime);
        }
    }

    private void EndAttacking()
    {
        attacking = false;
        swordObj.SetActive(false);
    }
}
