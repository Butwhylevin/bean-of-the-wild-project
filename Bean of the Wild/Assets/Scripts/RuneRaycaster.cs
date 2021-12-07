using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneRaycaster : MonoBehaviour
{
    public LayerMask runeInteractive;
    public float activeRuneNumber; // 1=stasis, 2= magnesis, 3=cryonis, 4=circleBomb, 5=cubeBomb, 6=rewind
    public bool runeActive;
    bool activateInput, useInput, cycleForward, cycleBack, endInput;

    [Header("Refrences")]
    public Camera cam;
    public MagnesisRune magnesisScript;
    public RewindRune rewindScript;
    public CryonisScript cryonisScript;

    [Header("Properties")]
    public float stasisTime = 5f;

    void Update()
    {
        GetInput();
        CycleRunes();
        
        if(endInput) { EndRunes(); }

        if(activateInput) { runeActive = !runeActive; }

        if(runeActive) { RuneLoop(); }
    }

    private void EndRunes()
    {
        if(activeRuneNumber == 2 && magnesisScript.movePoint)
        {
            magnesisScript.EndMagnesis();
        }
    }

    private void CycleRunes()
    {
        if(cycleForward)
        {
            EndRunes();
            activeRuneNumber += 1;
        } 
        else if(cycleBack)
        {
            EndRunes();
            activeRuneNumber -= 1;
        }
        activeRuneNumber = Mathf.Clamp(activeRuneNumber, 1, 6);
    }

    private void RuneLoop()
    {
        if(useInput)
        {
            RaycastHit hit;
            //Ray ray =  cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            
            if (Physics.Raycast(cam.gameObject.transform.position, cam.gameObject.transform.forward, out hit, runeInteractive))
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
                        magnesisScript.StartMagnesis(objectHit.gameObject);
                        
                    }
                    else if(activeRuneNumber == 3 && props.hasWater)
                    {
                        // cryonis rune
                        // make ice block
                        cryonisScript.StartCryonis(hit);
                    }
                    else if(activeRuneNumber == 6 && props.hasRewind)
                    {
                        // rewind rune
                        rewindScript.DoRewind(objectHit.GetComponent<RewindMovementRecorder>());
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
        endInput = Input.GetKeyDown(KeyCode.Escape);
    }
}
