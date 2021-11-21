using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTargetTrackableEventHandler : DefaultTrackableEventHandler
{
    public int SceneNumber;

    protected override void OnTrackingFound()
    {
        Debug.Log("SCENE FOUND " + SceneNumber);
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        foreach (var component in rendererComponents)
        {
            component.enabled = true;
        }
        ChangeScene();
    }

    protected override void OnTrackingLost()
    {
        Debug.Log("SCENE LOST " + SceneNumber);
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        foreach (var component in rendererComponents)
        {
            component.enabled = false;
        }
    }

    private void ChangeScene()
    {
        Camera.main.GetComponent<TaleManager>().ChangeScene(SceneNumber, this.gameObject);
    }
}
