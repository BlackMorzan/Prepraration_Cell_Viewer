using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PlayButton;
using System.Linq;

public class SimpleRead : MonoBehaviour
{
    public PlayButtons Buttons;
    public TextMeshProUGUI NuberOfJoints;
    public Image LoadDataButton;

    public List<float> SolverTime;
    public List<float>[] OperatingValues;

    public TMP_InputField DataNameInput;

    private int JointNuber = 0;
    private string PathFileName = "data.txt";

    public void Awake()
    {
        NewPathFileName();
        NewPathJointCount();
        OperatingValues = new List<float>[0];
    }

    public void NewPathFileName()
    {
        PathFileName = DataNameInput.text;
    }

    public void NewPathJointCount()
    {
        NuberOfJoints.text = "Nuber of joints: " + JointNuber;
    }

    public void ReadNewDataFromFile()
    {
        GetMatlabData();
        Buttons.StopPlay();
    }

    private void GetMatlabData()
    {
        // Player needs to know too
        if (!File.Exists(PathFileName))
        {
            JointNuber = 0;
            NewPathJointCount();
            Debug.Log("read error - no file");
            LoadDataButton.color = Color.red;
            return;
        }

        SolverTime = new List<float>();
        OperatingValues = new List<float>[NuberOfWords(File.ReadLines(PathFileName).First())];

        for (int i = 0; i < OperatingValues.Length; i++)
        {
            OperatingValues[i] = new List<float>();
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

        JointNuber = OperatingValues.Length;
        NewPathJointCount();

        LoadDataButton.color = Color.green;
    }

    private float getDouble(string value)
    {
        float result;
        
        if (!float.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out result) &&
            //Then try in US english
            !float.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
            //Then in neutral language
            !float.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out result))
        {
            LoadDataButton.color = Color.red;
        }

        return result;
    }

    private int NuberOfWords(string text)
    {
        int index = 0;
        int wordCount = -1;
        while (index < text.Length && char.IsWhiteSpace(text[index]))
            index++;

        while (index < text.Length)
        {
            // check if current char is part of a word
            while (index < text.Length && !char.IsWhiteSpace(text[index]))
                index++;

            wordCount++;

            // skip whitespace until next word
            while (index < text.Length && char.IsWhiteSpace(text[index]))
                index++;
        }

        return wordCount;
    }
}


