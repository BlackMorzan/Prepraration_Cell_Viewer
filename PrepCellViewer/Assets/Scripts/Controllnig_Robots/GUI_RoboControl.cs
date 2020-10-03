using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;
/*
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
            
            GUILayout.Label("List od robots", EditorStyles.boldLabel);
            MainController.NuberOfRobots = EditorGUILayout.IntField("Nuber of robots: ", MainController.NuberOfRobots);
            for (int i = 0; i < MainController.NuberOfRobots; i++)
                MainController.RobotArray[i] = EditorGUILayout.ObjectField("Robot nr " + i, MainController.RobotArray[i], typeof(Robot), true) as Robot;

            // List of Sliders ---------------------------------------------------------------------

            GUILayout.Label("List od sliders", EditorStyles.boldLabel);
            MainController.NuberOfSliders = EditorGUILayout.IntField("Nuber of sliders: ", MainController.NuberOfSliders);
            for (int i = 0; i < MainController.NuberOfSliders; i++)
                MainController.SliderArray[i] = EditorGUILayout.ObjectField("Robot nr " + i, MainController.SliderArray[i], typeof(GameObject), true) as GameObject;

        }
    }
    
}
*/

