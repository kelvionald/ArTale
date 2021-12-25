using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTargetTrackableEventHandler : DefaultTrackableEventHandler
{
    public GameObject TargetStatus;

    protected override void OnTrackingFound()
    {
        /*var rendererComponents = GetComponentsInChildren<Renderer>(true);
        foreach (var component in rendererComponents)
        {
            component.enabled = true;
        }*/
        TargetStatus.GetComponent<Text>().text = "<color=#4caf50>Lost</color>";
    }

    protected override void OnTrackingLost()
    {
        TargetStatus.GetComponent<Text>().text = "<color=#f44336>Lost</color>";
    }
}
