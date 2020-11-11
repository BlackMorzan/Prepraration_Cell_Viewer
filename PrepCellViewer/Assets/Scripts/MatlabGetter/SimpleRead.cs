using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PlayButton;

public class SimpleRead : MonoBehaviour
{

    public List<double> SolverTime;
    public List<double>[] OperatingValues;
    public PlayButtons Buttons;

    public TMP_InputField DataNameInput;
    public Slider JointCountInput;

    private int JointNuber = 2;
    private string PathFileName = "data.txt";

    public void Awake()
    {
        NewPathFileName();
        NewPathJointCount();
    }

    public void NewPathFileName()
    {
        PathFileName = DataNameInput.text;
    }

    public void NewPathJointCount()
    {
        JointNuber = (int)JointCountInput.value;
    }

    public void ReadNewDataFromFile()
    {
        Debug.Log(JointNuber + " : " + PathFileName);
        GetMatlabData();
        Buttons.StopPlay();
    }

    private void GetMatlabData()
    {

        SolverTime = new List<double>();
        OperatingValues = new List<double>[JointNuber];

        for (int i = 0; i < OperatingValues.Length; i++)
        {
            OperatingValues[i] = new List<double>();
        }

        // Player needs to know too
        if (!File.Exists(PathFileName))
        {
            Debug.Log("read error - no file");
            return;
        }

        foreach (string line in File.ReadLines(PathFileName, Encoding.UTF8))
        {
            string[] parts = line.Split(' ');

            for (int i = 0; i <= parts.Length; i++)
            {
                if (parts[i].Length <= 0)
                    break;

                if (i == 0)
                    SolverTime.Add(getDouble(parts[i]));
                else
                    OperatingValues[i-1].Add(getDouble(parts[i]) * Mathf.Rad2Deg);
            }
        }

        /*
        int counter = 0;
        foreach (var i in OperatingValues)
        {
            counter++;
            foreach (double j in i)
                Debug.Log(counter + " OperatingValues: " + j);
        }

        counter = 0;
        foreach (var i in SolverTime)
            Debug.Log(counter++ + " Time: " + i);
        */
        //double tmpTime = Convert.ToDouble(readText);

        //Debug.Log(String.Format(text));

    }

    private double getDouble(string value)
    {
        double result;
        
        if (!double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out result) &&
            //Then try in US english
            !double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
            //Then in neutral language
            !double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out result))
        {
            Debug.LogError("error - cant read double |" + value + "|");
        }

        return result;
    }
}


