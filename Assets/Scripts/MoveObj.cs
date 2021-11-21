using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObj : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    private Vector3 rot = new Vector3(0, 0, 0);

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Camera ar = Camera.current;
            if (ar == null)
            {
                return;
            }
            ray = ar.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == this.gameObject)
            {
                rot.z = hit.point.z;
                rot.x = hit.point.x;
                transform.position = new Vector3(rot.x, transform.position.y, rot.z);
            }
        }
    }
}
