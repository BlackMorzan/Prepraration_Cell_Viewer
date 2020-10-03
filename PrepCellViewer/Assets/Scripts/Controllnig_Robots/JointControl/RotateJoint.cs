using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateJoint : ControlJoint
{
    public override void JointControl(float newAngle)
    {
        transform.Rotate(0, 0, -prevValue, Space.Self); // reset position before turning

        prevValue = newAngle;
        transform.Rotate(0, 0, prevValue, Space.Self); // rotate to new point
    }
}
