using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPath : MonoBehaviour
{
    public GameObject PathPrefab;
    private GameObject RobotEndPoint;
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


