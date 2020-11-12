using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainControlButtons : MonoBehaviour
{
    public TextMeshProUGUI ShowChosenRobot;
    public TextMeshProUGUI ShowChosenMode;

    private string PrepRobot = "Robot: ";
    private string PrepMode = "Mode: ";

    private void Awake()
    {
        ShowChosenRobot.text = PrepRobot + "Agilus R6";
        ShowChosenMode.text = PrepMode + "Manual";
    }

    public void ChangeDisplayedRobotLabel(string Label)
    {
        ShowChosenRobot.text = PrepRobot + Label;
    }

    public void ChangeDisplayedModeLabel(int mode)
    {
        string label = String.Empty;
        if (mode == 0)
            label = "manual";
        if (mode == 1)
            label = "matlab";
        if (mode == 2)
            label = "none";
        ShowChosenMode.text = PrepMode + label;
    }
}
