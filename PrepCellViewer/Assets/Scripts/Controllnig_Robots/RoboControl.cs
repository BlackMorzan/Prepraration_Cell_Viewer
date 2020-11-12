using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using PlayButton;

namespace RoboController
{

    public class RoboControl : MonoBehaviour
    {
        public Robot[] RobotArray;

        public GameObject[] SliderArray;

        public SimpleRead MatlabInput;

        public ShowPath CurrentPath;
        public float Speed = 1;

        public MainControlButtons MControlButtons;

        private int CurrentRobot = 0;
        private int TypeOfControl = 0;

        // connected to corutine
        private bool PlayMatlabFlag = false;
        private IEnumerator coroutine;
        private bool CanMoveCorutine = true;
        private int CurrentTime = 0;

        private bool PlayRobotMove = false;
        private bool StopRobotMove = true;

        public void Awake()
        {
            foreach (var i in RobotArray) // disable all robots but Agilus
                i.gameObject.SetActive(false);

            RobotArray[0].gameObject.SetActive(true);
            // Find active robot on awake
            CurrentPath.NewRobotEndpiont(CurrentPath.GetEfector(RobotArray[0].gameObject));
            // hide play UI
            MatlabInput.Buttons.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (CanMoveCorutine && PlayMatlabFlag && MatlabInput.SolverTime.Count() > CurrentTime && PlayRobotMove)
            {
                // play form matlab file if corutine ended & called for & withing array 
                PlayMatlab();
                CurrentTime++;
            }
            if (MatlabInput.SolverTime.Count() - 1 <= CurrentTime || StopRobotMove) // reset timer
            {
                CurrentTime = 0;
                CurrentPath.KillAllPathPoints();
            }
        }

        public void StopPathPlay()
        {
            StopRobotMove = true;
            PlayRobotMove = false;
            MatlabInput.Buttons.StopPlay();
        }

        public void PausePathPlay()
        {
            PlayRobotMove = !PlayRobotMove;
            if (StopRobotMove)
                StopRobotMove = false;

            if (PlayRobotMove)
                MatlabInput.Buttons.StartPlay();
            else
                MatlabInput.Buttons.PausePlay();
        }

        public void ChangeRobotOnClick()
        {
            RobotArray[CurrentRobot].gameObject.SetActive(false); // activate new robot and store last
            CurrentRobot++;

            if (CurrentRobot >= RobotArray.Length)
                CurrentRobot = 0;

            RobotArray[CurrentRobot].gameObject.SetActive(true);


            for (int i = 0; RobotArray[CurrentRobot].Joints.Length > i; i++) // setting max & min to sliders
            {
                SliderArray[i].GetComponent<Slider>().maxValue = RobotArray[CurrentRobot].Joints[i].MaxValue;
                SliderArray[i].GetComponent<Slider>().minValue = RobotArray[CurrentRobot].Joints[i].MinValue;
            }

            CurrentTime = 0; // to reset timer after changing robot
            DisplaySliders();

            // Change showed path
            CurrentPath.KillAllPathPoints();
            CurrentPath.NewRobotEndpiont(CurrentPath.GetEfector(RobotArray[CurrentRobot].gameObject));

            // Stop play
            PlayRobotMove = false;
            StopRobotMove = true;
            MatlabInput.Buttons.StopPlay();

            // Hide buttons if cant use robot
            DisplayPlayButtons();

            // Change robot label
            MControlButtons.ChangeDisplayedRobotLabel(RobotArray[CurrentRobot].name);
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
            DisplayPlayButtons();
            MControlButtons.ChangeDisplayedModeLabel(TypeOfControl);
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

        public void NewDataLoaded()
        {
            DisplayPlayButtons();
        }
        
        private void PlayMatlab()
        {
            if (MatlabInput.SolverTime[CurrentTime] == MatlabInput.SolverTime[CurrentTime + 1] && CurrentTime != 0)
            {
                return;
            }

            for (int i = 0; i <= RobotArray[CurrentRobot].Joints.Length - 1; i++)
            {
                RobotArray[CurrentRobot].Joints[i].JointControl(MatlabInput.OperatingValues[i].ElementAt(CurrentTime));
            }

            // send from MatlabInput[i+1] to joint[i] (0 would be time))
            for (int i = 0; i <= RobotArray[CurrentRobot].Joints.Length - 1; i++)
            {
                RobotArray[CurrentRobot].Joints[i].JointControl(MatlabInput.OperatingValues[i].ElementAt(CurrentTime));
            }

            float coroutineWait = (MatlabInput.SolverTime[CurrentTime+1]
                - MatlabInput.SolverTime[CurrentTime]) * 1/Speed;

            coroutine = CoWaitToMove(coroutineWait); // count corutine timer

            StartCoroutine(coroutine);
        }

        IEnumerator CoWaitToMove(float waitDuation)
        {
            CanMoveCorutine = false;

            if (CurrentTime == 0)
                CurrentPath.StartPath();
            else
                CurrentPath.AddPathPoint(CurrentPath.SpeedToColor(Speed));

            yield return new WaitForSeconds(waitDuation);
            CanMoveCorutine = true;
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

        private void DisplayPlayButtons()
        {
            
            if (TypeOfControl == 1 && RobotArray[CurrentRobot].Joints.Length == MatlabInput.OperatingValues.Length)
            {
                MatlabInput.Buttons.gameObject.SetActive(true);
            }
            else
            {
                // also stop robot play
                StopRobotMove = true;
                PlayRobotMove = false;
                MatlabInput.Buttons.StopPlay();
                MatlabInput.Buttons.gameObject.SetActive(false);
            }
        }
    }

}