using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryonisScript : MonoBehaviour
{
    public GameObject iceWallPrefab;

    public void StartCryonis(RaycastHit hit)
    {   
        // get rotation from the normal
        Quaternion rot = Quaternion.FromToRotation (Vector3.up, hit.normal);
        GameObject iceWall = Instantiate(iceWallPrefab, hit.point, rot);
    }
}
