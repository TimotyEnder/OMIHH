using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRotation : MonoBehaviour
{

    public GameObject aimPivot;
    public GameObject target;
    private Vector3 diff;

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    void Rotate(){
        diff = target.transform.position - aimPivot.transform.position;
        //normalize difference  
        diff.Normalize();
        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        //apply to object
        aimPivot.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }
}
