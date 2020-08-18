using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MatlabMove : MonoBehaviour
{
    // Start is called before the first frame update

    public int JointNumber = 0;
    private int CurrentTime = 0;
    private float angleZ = 0.0f;
    public SimpleRead X;

    public float Speed = 1;

    private IEnumerator coroutine;

    private bool CanMove = true;

    private void Start()
    {

    }

    void Update()
    {
        if (CanMove)
            RobotRotate();
    }

    IEnumerator CoWaitToMove(float waitDuation)
    {
        CanMove = false;

        yield return new WaitForSeconds(waitDuation);
        CanMove = true;
    }


    private void RobotRotate()
    {
        this.transform.Rotate(0, 0, -angleZ, Space.Self); // reset position before turning

        angleZ = Convert.ToSingle(X.OperatingValues[JointNumber].ElementAt(CurrentTime++));

        transform.Rotate(0, 0, angleZ, Space.Self); // rotate to new point

        //Debug.Log("Current angle: " + X.OperatingValues[JointNumber]);
        //Debug.Log("Current Time: " + X.SolverTime[CurrentTime++] + " | ");

        Debug.Log("Current angle: " + X.OperatingValues[JointNumber].ElementAt(CurrentTime));

        coroutine = CoWaitToMove(Convert.ToSingle(X.SolverTime[CurrentTime+1] - X.SolverTime[CurrentTime + 1])+Speed);

        StartCoroutine(coroutine);
    }
}
