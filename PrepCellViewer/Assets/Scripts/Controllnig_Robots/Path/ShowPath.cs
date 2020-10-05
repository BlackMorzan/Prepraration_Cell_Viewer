using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPath : MonoBehaviour
{
    public GameObject PathPrefab;
    public GameObject RobotEndPoint;
    public GameObject PathParent;
    public void AddPathPoint()
    {
        Instantiate(PathPrefab, RobotEndPoint.transform.position, Quaternion.identity);
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

    private void Start()
    {
        FindAllPathPoints();
    }

    private static GameObject FindInChild(Transform startPonit, string tag)
    {
        // chcek current transform
        if (startPonit.tag == tag)
        {
            return startPonit.transform.gameObject;
        }

        // chcek all child
        while (startPonit.parent != null)
        {
            if (startPonit.parent.tag == tag)
            {
                return startPonit.transform.parent.transform.gameObject;
            }
            startPonit = startPonit.parent.transform;
        }

        return null; // Could not find a parent with given tag.
    }
}


