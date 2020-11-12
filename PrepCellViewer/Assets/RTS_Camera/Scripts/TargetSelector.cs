using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using RTS_Cam;

[RequireComponent(typeof(RTS_Camera))]
public class TargetSelector : MonoBehaviour
{
    private RTS_Camera cam;
    private new Camera camera;
    public string targetsTag;

    private void Start()
    {
        cam = gameObject.GetComponent<RTS_Camera>();
        camera = gameObject.GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // cant click through UI now
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            Transform tmp = null;

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {

                tmp = FindParentWithTag2(hit.transform, targetsTag);
                if (tmp != null)
                    cam.SetTarget(tmp);
                else
                    cam.ResetTarget();
            }
        }
    }

    private static Transform FindParentWithTag2(Transform childObject, string tag)
    {
        Transform t = childObject.transform;
        Transform b = null;
        
        // chcek all parents
        while (t.parent != null)
        {
            b = FindGameObjectInChildWithTag(t, tag);
            if (b != null)
            {
                return b.transform;
            }
            t = t.parent.transform;
        }

        b = FindGameObjectInChildWithTag(t, tag);
        if (b != null)
        {
            return b.transform;
        }

        return null; // Could not find a parent with given tag.
    }

    public static Transform FindGameObjectInChildWithTag(Transform parent, string tag)
    {
        Transform t = parent.transform;

        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).transform.tag == tag)
            {
                return t.GetChild(i).transform;
            }

        }
        return null;
    }
}