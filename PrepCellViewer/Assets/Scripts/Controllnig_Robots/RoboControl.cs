using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RoboController
{

    public class RoboControl : MonoBehaviour
    {
        public Robot[] RobotArray;

        public GameObject[] SliderArray;

        public SimpleRead MatlabInput;

        public ShowPath CurrentPath;

        public int CurrentRobot = 0;
        public int TypeOfControl = 0;
        public float Speed = 1;

        private float[] angleZ = new float[7];

        private bool PlayMatlabFlag = false;
        private IEnumerator coroutine;
        private bool CanMoveCorutine = true;
        private int CurrentTime = 0;
        
        public void Awake()
        {

            foreach (var i in RobotArray) // disable all robots but Agilus
                i.gameObject.SetActive(false);

            RobotArray[0].gameObject.SetActive(true);
            // Find active robot on awake
            CurrentPath.NewRobotEndpiont(CurrentPath.GetEfector(RobotArray[0].gameObject));
        }
        
        private void Update()
        {
            if (CanMoveCorutine && PlayMatlabFlag && MatlabInput.SolverTime.Count() > CurrentTime) 
            { // play form matlab file if corutine ended & called for & withing array 
                PlayMatlab();
                CurrentTime++;
            }
            if (MatlabInput.SolverTime.Count() - 1 <= CurrentTime) // reset timer
            {
                CurrentTime = 0;
                CurrentPath.KillAllPathPoints();
            }
        }

        public void ChangeRobotOnClick()
        {
            RobotArray[CurrentRobot].gameObject.SetActive(false); // activate new robot and store last
            CurrentRobot++;

            if (CurrentRobot >= RobotArray.Length)
                CurrentRobot = 0;

            RobotArray[CurrentRobot].gameObject.SetActive(true);

            for (int i = 0; RobotArray[CurrentRobot].Joints.Length > i; i++) // stting max & min to sliders
            {
                SliderArray[i].GetComponent<Slider>().maxValue = RobotArray[CurrentRobot].Joints[i].MaxValue;
                SliderArray[i].GetComponent<Slider>().minValue = RobotArray[CurrentRobot].Joints[i].MinValue;
            }

            CurrentTime = 0; // to reset timer after changing robot
            DisplaySliders();

            // Change showed path
            CurrentPath.KillAllPathPoints();
            CurrentPath.NewRobotEndpiont(CurrentPath.GetEfector(RobotArray[CurrentRobot].gameObject));
        }

        public void ChangeControlOnClick()
        {
            TypeOfControl++; // new control

            if (TypeOfControl >= 3) // reset control 
                TypeOfControl = 0;

            if (TypeOfControl == 1) // check if is 
                PlayMatlabFlag = true;
            else
                PlayMatlabFlag = false;

            DisplaySliders();
        }

        public void OnValueChangeZ(float newAngle) // change joint by slider
        {
            for (int i = 0; i <= SliderArray.Length; i++)
            {
                if (EventSystem.current.currentSelectedGameObject.name == ("K" + (i + 2) + "Slider")
                && RobotArray[CurrentRobot].Joints.Length > i)
                    RobotArray[CurrentRobot].Joints[i].JointControl(newAngle);
            }
        }

        
        private void PlayMatlab()
        {
            if (RobotArray[CurrentRobot].Joints.Length != MatlabInput.OperatingValues.Length)
                return;

            // send from MatlabInput[i+1] to joint[i] (0 would be time))
            for (int i = 0; i <= RobotArray[CurrentRobot].Joints.Length-1; i++)
                RobotArray[CurrentRobot].Joints[i].JointControl(Convert.ToSingle(MatlabInput.OperatingValues[i].ElementAt(CurrentTime)));

            float coroutineWait = Convert.ToSingle(MatlabInput.SolverTime[CurrentTime+1]
                - MatlabInput.SolverTime[CurrentTime]) * 1/Speed;
            coroutine = CoWaitToMove(coroutineWait); // count corutine timer

            StartCoroutine(coroutine);
        }

        private void DisplaySliders()
        {
            for (int i = 0; SliderArray.Length > i; i++)
            {
                if (TypeOfControl != 0) // sliders only if controlled maually
                {
                    SliderArray[i].gameObject.SetActive(false);
                    continue;
                }
                else if (i >= RobotArray[CurrentRobot].Joints.Length) // disable sliders if is bigger than joints
                {
                    SliderArray[i].gameObject.SetActive(false);
                    continue;
                }
                else if (SliderArray[i].activeSelf != true) // others on
                {
                    SliderArray[i].SetActive(true);
                }
            }
        }

        IEnumerator CoWaitToMove(float waitDuation)
        {
            CanMoveCorutine = false;
            // IT IS VERY TEMPORARY!!!!
            float newSpeed = (CurrentPath.GetRobotEndPoint().transform.position - CurrentPath.GetLastPosition()).magnitude;
            CurrentPath.SetLastPosition(CurrentPath.GetRobotEndPoint().transform.position);

            Debug.Log(newSpeed*Speed);

            if (CurrentTime != 0)
                CurrentPath.AddPathPoint(CurrentPath.SpeedToColor(newSpeed*Speed));

            yield return new WaitForSeconds(waitDuation);
            CanMoveCorutine = true;
        }

    }

}