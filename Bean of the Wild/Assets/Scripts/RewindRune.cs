using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindRune : MonoBehaviour
{
    RewindMovementRecorder recorderScript;
    public List<Vector3> recordedPos;
    public List<Quaternion> recordedRots;
    public float curRecordIndex;
    float recordIncrement;
    Vector3 lerpTargetPos;
    Quaternion lerpTargetRot;
    Transform rewindObjTrans;
    // lerping
    Vector3 startPosition;
    Quaternion startRotation;
    float t;
    bool targetExists = false;
    float firstRecordIncrement;
    float curIncrement;

    // pretty stuff
    public Material defaultMat, rewindMat;

    public void DoRewind(RewindMovementRecorder script)
    {
        targetExists = true;
        // get the script
        recorderScript = script;
        rewindObjTrans = recorderScript.gameObject.transform;
        // take the list of transforms and then clear the list on the recorder
        recordedPos = recorderScript.recordedPos;
        recordedRots = recorderScript.recordedRots;
        //recorderScript.ClearRecords();
        // find how long the list of records is and how often it iterated
        recordIncrement = recorderScript.recordIncrement;
        curRecordIndex = recordedPos.Count-1;
        // setup the first step to account for the incorrect timing
        firstRecordIncrement = recordIncrement - recorderScript.curRecordIncrement;
        // disable the recorder of the object and make the rb kinematic
        recorderScript.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        recorderScript.doRecord = false;
        // set its material
        recorderScript.gameObject.GetComponent<MeshRenderer>().material = rewindMat;
        // begin lerping it to the first position
        StartRewindLerp(recordedPos[(int)curRecordIndex], recordedRots[(int)curRecordIndex], true);
    }

    private void StartRewindLerp(Vector3 pos, Quaternion rot, bool firstStep)
    {
        // remove the last transform from the list
        recordedPos.Remove(recordedPos[(int)curRecordIndex]);
        recordedRots.Remove(recordedRots[(int)curRecordIndex]);
        // set the target
        lerpTargetPos = pos;
        lerpTargetRot = rot;
        // set the starting rotation and position
        startPosition = recorderScript.gameObject.transform.position;
        startRotation = recorderScript.gameObject.transform.rotation;
        t = 0;
        // set the increment
        if(firstStep) 
        {
            curIncrement = firstRecordIncrement;
        }
        else
        {
            curIncrement = recordIncrement;
        }

        Debug.Log("startposition = " + startPosition.ToString() + " target position = " +lerpTargetPos.ToString());
    }

    private void DoRewindLerp()
    {
        // move position
        rewindObjTrans.position = Vector3.Lerp(startPosition, lerpTargetPos, t / curIncrement);
        // move rotation
        rewindObjTrans.rotation = Quaternion.Lerp(startRotation, lerpTargetRot, t / curIncrement);
        // change t
        t ++;
    }

    private void NextRewindLerp()
    {
        curRecordIndex--;
        if(curRecordIndex < 0)
        {
            EndRewind();
        }
        else
        {
            StartRewindLerp(recordedPos[(int)curRecordIndex], recordedRots[(int)curRecordIndex], false);
        }
    }

    private void EndRewind()
    {
        // remove the target and clear the list
        targetExists = false;
        recordedPos.Clear();
        recordedRots.Clear();
        //enable recorder and make rb not kinematic
        recorderScript.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        recorderScript.doRecord = true;
        // set its material
        recorderScript.gameObject.GetComponent<MeshRenderer>().material = defaultMat;
    }

    private void FixedUpdate() 
    {
        if(targetExists)
        {
            if(t < recordIncrement)
            {
                DoRewindLerp();
            }
            else
            {
                NextRewindLerp();
            }
        }
    }
}
