using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJoint : MonoBehaviour
{
    public float MaxValue = 0;
    public float MinValue = 0;

    protected float prevValue = 0;

    public virtual void JointControl(float newAngle)
    {

    }
}
