using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateJoint : MonoBehaviour
{
    private float angleZ = 0;

    //public EulerGet script;


    void Start()
    {
        transform.Rotate(0, 0, 0, Space.Self); // Set joint to base position
    }



    public void OnValueChangeZ(float newAngle)
    {
        transform.Rotate(0, 0, -angleZ, Space.Self); // reset position before turning
        
        angleZ = newAngle;
        transform.Rotate(0, 0, angleZ, Space.Self); // rotate to new point
    }

}