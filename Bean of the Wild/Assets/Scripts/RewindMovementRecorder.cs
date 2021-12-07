using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindMovementRecorder : MonoBehaviour
{
    public float recordIncrement = 10f;
    public float curRecordIncrement;
    public float recordAmount = 20f;
    public List<Vector3> recordedPos;
    public List<Quaternion> recordedRots;
    public bool doRecord = true;

    private void Start() 
    {
        RecordTransform(transform);
    }

    private void FixedUpdate() 
    {
        if(curRecordIncrement > 0 && doRecord)
        {
            curRecordIncrement --;
            if(curRecordIncrement == 0)
            {
                RecordTransform(transform);
            }
        }
    }

    public void RecordTransform(Transform trans)
    {
        // add latest transform
        recordedPos.Add(trans.position);
        recordedRots.Add(trans.rotation);
        // remove oldest transform
        if(recordedPos.Count > recordAmount)
        {
            recordedPos.RemoveAt(0);
            recordedRots.RemoveAt(0);
        }
        curRecordIncrement = recordIncrement;
    }

    public void ClearRecords()
    {
        recordedPos.Clear();
        recordedRots.Clear();
    }
}
