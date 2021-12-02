using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneRaycaster : MonoBehaviour
{
    public LayerMask runeInteractive;
    public Camera cam;
    public float activeRuneNumber; // 1=stasis, 2= magnesis, 3=cryonis, 4=circleBomb, 5=cubeBomb, 6=rewind
    public bool runeActive;
    bool activateInput, useInput, cycleForward, cycleBack;

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
                    if(activeRuneNumber == 3 && props.hasWater)
                    {
                        // cryonis rune
                        objectHit.GetComponent<ApplyForces>().StasisFreeze(stasisTime);
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
