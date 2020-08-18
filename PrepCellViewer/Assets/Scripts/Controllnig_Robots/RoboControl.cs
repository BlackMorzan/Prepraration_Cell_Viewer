using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboController
{
    [RequireComponent(typeof(GameObject))]
    [AddComponentMenu("Main Controller")]

    public class RoboControl : MonoBehaviour
    {
        public int NuberOfRobots = 5;
        public Robot[] RobotArray;

        public int NuberOfSliders = 5;
        //public Robot[] RobotArray;

    }
}