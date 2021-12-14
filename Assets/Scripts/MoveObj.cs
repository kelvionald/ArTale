using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObj : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    private Vector3 rot = new Vector3(0, 0, 0);
    private Camera camera;

    public string ModelFilename;

    void Start()
    {
        camera = GameObject.Find("ARCamera").GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (camera == null)
            {
                return;
            }

            if (GetMenuManager().CurrentMoveObj != gameObject)
            {
                return;
            }

            ray = camera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                rot.z = hit.point.z;
                rot.x = hit.point.x;
                transform.position = new Vector3(rot.x, transform.position.y, rot.z);
            }
        }
    }

    void OnMouseDown()
    {
        if (GetMenuManager().CurrentMoveObj == null)
        {
            GetMenuManager().CurrentMoveObj = gameObject;
        }
    }

    void OnMouseUp()
    {
        GetMenuManager().CurrentMoveObj = null;
    }

    MenuManager GetMenuManager()
    {
        return camera.gameObject.GetComponent<MenuManager>();
    }
}
