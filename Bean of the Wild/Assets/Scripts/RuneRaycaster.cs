using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneRaycaster : MonoBehaviour
{
    public LayerMask runeInteractive;
    public float activeRuneNumber; // 1=stasis, 2= magnesis, 3=cryonis, 4=circleBomb, 5=cubeBomb, 6=rewind
    public bool runeActive;
    bool activateInput, useInput, cycleForward, cycleBack;

    [Header("Refrences")]
    public Camera cam;
    public MagnesisRune magnesisScript;

    [Header("Properties")]
    public float stasisTime = 5f;

    void Update()
    {
        GetInput();
        CycleRunes();

        if(activateInput) { runeActive = !runeActive; }

        if(runeActive) { RuneLoop(); }
    }

    private void CycleRunes()
    {
        if(cycleForward)
        {
            activeRuneNumber += 1;
        } 
        else if(cycleBack)
        {
            activeRuneNumber -= 1;
        }
        activeRuneNumber = Mathf.Clamp(activeRuneNumber, 1, 6);
    }

    private void RuneLoop()
    {
        if(useInput)
        {
            RaycastHit hit;
            Ray ray =  cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            
            if (Physics.Raycast(ray, out hit, runeInteractive))
            {
                Transform objectHit = hit.transform;
                Debug.Log(objectHit);

                ObjectProperties props = objectHit.GetComponent<ObjectProperties>();
                
                if(props != null)
                {
                    if(activeRuneNumber == 1 && props.hasStasis)
                    {
                        // stasis rune
                        objectHit.GetComponent<ApplyForces>().StasisFreeze(stasisTime);
                    }
                    else if(activeRuneNumber == 2 && props.hasMagnet)
                    {
                        // magnesis rune
                        // change layer of object hit and disable gravity
                        objectHit.gameObject.layer = 6;
                        objectHit.GetComponent<Rigidbody>().useGravity = false;
                        // set position of magnesis point
                        magnesisScript.movePoint = true;
                        // move the hit object towards the point
                        magnesisScript.forceScript = objectHit.gameObject.GetComponent<ApplyForces>();
                        
                    }
                    else if(activeRuneNumber == 3 && props.hasWater)
                    {
                        // cryonis rune
                        // make ice block
                    }
                }
            }
        }
    }

    private void GetInput()
    {
        activateInput = Input.GetKeyDown(KeyCode.Tab);
        useInput = Input.GetMouseButtonDown(1);
        cycleForward = Input.GetKeyDown(KeyCode.E);
        cycleBack = Input.GetKeyDown(KeyCode.Q);
    }
}
