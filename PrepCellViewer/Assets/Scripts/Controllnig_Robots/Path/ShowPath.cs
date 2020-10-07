using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;

public class ShowPath : MonoBehaviour
{
    public GameObject PathPrefab;
    public GameObject PathParent;
    public float MaxSpeed;

    private GameObject RobotEndPoint;

    private Vector3 LastPosition;
    private Color PathPointColor;

    //PathPrefab.GetComponent<MeshRenderer>().material.color;
    public GameObject GetRobotEndPoint() { return RobotEndPoint; }
    public Vector3 GetLastPosition() { return LastPosition; }

    public void SetLastPosition(Vector3 newVector) { LastPosition = newVector; }

    private void Start()
    {
        LastPosition = RobotEndPoint.transform.position;
        PathPointColor = Color.green;
    }

    public void AddPathPoint()
    {
        Instantiate(PathPrefab, RobotEndPoint.transform.position, Quaternion.identity, PathParent.transform);
    }

    public void AddPathPoint(Color newColor)
    {
        GameObject Tmp = Instantiate(PathPrefab, RobotEndPoint.transform.position, Quaternion.identity, PathParent.transform);
        //Tmp.GetComponent<MeshRenderer>.
        //Tmp.GetComponent<MeshRenderer>().
        Tmp.GetComponent<MeshRenderer>().material.color = newColor;
    }

    public Color SpeedToColor(float SpeedModifier)
    {
        float newSpeed = (RobotEndPoint.transform.position - LastPosition).magnitude;
        LastPosition = RobotEndPoint.transform.position;

        float currentSpeed = (SpeedModifier*newSpeed) / (MaxSpeed);


        if (currentSpeed <= 0.5)
        {
            PathPointColor.g = 1;
            PathPointColor.r = currentSpeed*2; // needs to go twice fast - it would used just 0.5 of color 
            PathPointColor.b = 0;              // it would be (0, 0.5> we would use half spectrum
            PathPointColor.a = 1f;
        }

        if (currentSpeed > 0.5)
        {
            PathPointColor.g = 2-(currentSpeed*2); // goes from 0=2-(1*2) to 1=2-(0.5*2)
            PathPointColor.r = 1;
            PathPointColor.b = 0;
            PathPointColor.a = 1f;
        }

        return PathPointColor;
    }

    public void NewRobotEndpiont(GameObject NewEndpoint)
    {
        RobotEndPoint = NewEndpoint;
    }

    public void KillAllPathPoints()
    {
        foreach (Transform child in PathParent.transform)
            Destroy(child.gameObject);
    }

    public void FindAllPathPoints()
    {
        foreach (Transform child in PathParent.transform)
            Debug.Log(child.name);
    }

    public GameObject GetEfector(GameObject FindIn)
    {
        return GetChildRecursive(FindIn, "Efector");
    }

    private GameObject GetChildRecursive(GameObject obj, string searchTag)
    {
        GameObject foundIt;
        if (obj.tag == searchTag)
            return obj;

        if (null == obj)
            return null;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
                continue;

            if (child.tag == searchTag)
                return child.gameObject;

            foundIt = GetChildRecursive(child.gameObject, searchTag);
            if (foundIt != null)
                return foundIt;
        }

        return null;
    }
}


