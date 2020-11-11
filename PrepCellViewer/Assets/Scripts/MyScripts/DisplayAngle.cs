using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


public class DisplayAngle : MonoBehaviour
{

    public Text JointAngle;
    private float Zaxis;

    public float offset;
    public Transform Joint;
    // Start is called before the first frame update
    void Start()
    {
        JointAngle = GetComponent<Text>(); // initialize text

    }

    // Update is called once per frame
    void LateUpdate()
    {
        // last angle without offset is needed later
        float prewAngle = Zaxis - offset;

        //Vector3 X = UnityEditor.TransformUtils.GetInspectorRotation(Joint.transform);
        Vector3 X = Joint.localEulerAngles;

        
        Zaxis = X.z;

        // unity in inspect mode tries to keep angle between <0, 360> Here I reset that
        if (prewAngle + 200 < Zaxis)
            Zaxis -= 360;
        if (prewAngle - 200 > Zaxis)
            Zaxis += 360;

        // offset accordingly to robot documentation
        Zaxis += offset;

        Zaxis = Mathf.Round(Zaxis * 100f) / 100f;
        JointAngle.text = Zaxis.ToString();
    }
}
