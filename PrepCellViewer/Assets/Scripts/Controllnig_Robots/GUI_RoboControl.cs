using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;


namespace RoboController
{

    [CustomEditor(typeof(RoboControl))]
    public class GUI_RoboControl : Editor
    {
        // controler of controllers - decide how robot can be controlled
        // get all sliders, data from Matalb, current robot (send data there) and type of control
        private RoboControl MainController { get { return target as RoboControl; } }

        public override void OnInspectorGUI()
        {
            // List of Robots ---------------------------------------------------------------------

            GUILayout.Label("List of robots: ", EditorStyles.boldLabel, GUILayout.Width(170f));
            MainController.NuberOfRobots = EditorGUILayout.IntField("Nuber of robots: ", MainController.NuberOfRobots);
            MainController.RobotArray = new Robot[MainController.NuberOfRobots];
            for (int i=0; i<MainController.RobotArray.Length; i++)
                MainController.RobotArray[i] = EditorGUILayout.ObjectField("Robot nr " + i, MainController.RobotArray[i], typeof(Robot), true) as Robot;
            
            // List of Sliders ---------------------------------------------------------------------

            GUILayout.Label("List of sliders: ", EditorStyles.boldLabel, GUILayout.Width(170f));
            MainController.NuberOfSliders = EditorGUILayout.IntField("Nuber of sliders: ", MainController.NuberOfSliders);
            MainController.RobotArray = new Robot[MainController.NuberOfSliders];
        }
    }
}

