using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colision : MonoBehaviour
{
    private Material ColiMaterial;
    private Material RoboOrange;
    private Material RoboBlack;
    private Material Floor;
    private Material Surroundings;
    private Material Fence;
    private Material Machine;
    //private Material GetMaterial();
    //public GameObject Robot;
    //private bool flag = false;



    private void Start()
    {
        RoboOrange = GameObject.Find("RoboOrange").GetComponent<Renderer>().material;
        Surroundings = GameObject.Find("Surroundings").GetComponent<Renderer>().material;
        Floor = GameObject.Find("Floor").GetComponent<Renderer>().material;
        RoboBlack = GameObject.Find("RoboBlack").GetComponent<Renderer>().material;
        ColiMaterial = GameObject.Find("Colision").GetComponent<Renderer>().material;
        Machine = GameObject.Find("Machine").GetComponent<Renderer>().material;
        Fence = GameObject.Find("Fence").GetComponent<Renderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {

        if ((other.tag == "RobotFront1" && tag == "Efector") || (tag == "RobotFront1" && other.tag == "Efector"))
            return;
        if ((other.tag == "RobotFront1" && tag == "RobotFront2") || (tag == "RobotFront1" && other.tag == "RobotFront2"))
            return;
        if ((other.tag == "RobotFront2" && tag == "RobotSafe") || (tag == "RobotFront2" && other.tag == "RobotSafe"))
            return;
        if ((other.tag == "RobotSafe" && tag == "RobotBase") || (tag == "RobotSafe" && other.tag == "RobotBase"))
            return;
        if ((other.tag == "RobotSafe" && tag == "RobotFront1") || (tag == "RobotSafe" && other.tag == "RobotFront1"))
            return;

        if (tag == other.tag)
            return;

        GetComponent<MeshRenderer>().material = ColiMaterial;
        other.GetComponent<MeshRenderer>().material = ColiMaterial;

        if(other.name == "chainlink_LOD1")
        {
            other.transform.parent.GetChild(0).GetComponent<Renderer>().material = ColiMaterial;
            other.transform.parent.GetChild(1).GetComponent<Renderer>().material = ColiMaterial;
        }




    }

    void OnTriggerExit(Collider other)
    {
        string SwitchSelf = "";
        string SwitchOther = "";

        SwitchOther = other.tag;
        SwitchSelf = tag;

        if (SwitchSelf == "Untagged")
            SwitchSelf = FindParent(this.transform, "ColiBound").name;
        if (SwitchOther == "Untagged")
            SwitchOther = FindParent(other.transform, "ColiBound").name;
        
        switch (SwitchSelf)
        {
            case "Efector":
            case "RobotBase":
                GetComponent<MeshRenderer>().material = RoboBlack;
                break;

            case "Robots":
            case "RobotSafe":
            case "RobotFront1":
            case "RobotFront2":
                GetComponent<MeshRenderer>().material = RoboOrange;
                break;
                
            case "Fences":
                transform.parent.GetChild(0).GetComponent<Renderer>().material = Fence;
                transform.parent.GetChild(1).GetComponent<Renderer>().material = Fence;
                break;

            case "Floor":
                GetComponent<MeshRenderer>().material = Floor;
                break;

            case "Machines":
                GetComponent<MeshRenderer>().material = Machine;
                break;

            default:
                GetComponent<MeshRenderer>().material = Surroundings;
                break;

        }

        switch (SwitchOther)
        {
            case "Efector":
            case "RobotBase":
                other.GetComponent<MeshRenderer>().material = RoboBlack;
                break;

            case "Robots":
            case "RobotSafe":
            case "RobotFront1":
            case "RobotFront2":
                other.GetComponent<MeshRenderer>().material = RoboOrange;
                break;

            case "Fences":
                other.transform.parent.GetChild(0).GetComponent<Renderer>().material = Fence;
                other.transform.parent.GetChild(1).GetComponent<Renderer>().material = Fence;
                break;

            case "Floor":
                other.GetComponent<MeshRenderer>().material = Floor;
                break;

            case "Machines":
                other.GetComponent<MeshRenderer>().material = Machine;
                break;

            default:
                other.GetComponent<MeshRenderer>().material = Surroundings;
                break;

        }

    }

        private static Transform FindParent(Transform childObject, string tag)
        {
            Transform t = childObject.transform;
            // chcek current transform
            if (t.tag == tag)
            {
                return t.transform;
            }
            // chcek all parents
            while (t.parent != null)
            {
                if (t.parent.tag == tag)
                {
                    return t.transform.parent.transform;
                }
                t = t.parent.transform;
            }
            return null; // Could not find a parent with given tag.
    }

}
