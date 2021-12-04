using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTargetTrackableEventHandler : DefaultTrackableEventHandler
{
    protected override void OnTrackingFound()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        foreach (var component in rendererComponents)
        {
            component.enabled = true;
        }
        ChangeScene();
    }

    protected override void OnTrackingLost()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        foreach (var component in rendererComponents)
        {
            component.enabled = false;
        }
    }

    private void ChangeScene()
    {
        //Camera.main.GetComponent<TaleManager>().ChangeScene(SceneNumber, this.gameObject);
    }
}
