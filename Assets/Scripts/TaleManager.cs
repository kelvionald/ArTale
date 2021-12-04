using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaleManager : MonoBehaviour
{
    public GameObject TextSceneNumber;
    public GameObject CurrentTarget;
    public Color SelecredScene;
    public Color UnselecredScene;
    public Color CurrentScene;

    void Start()
    {
        ColorUtility.TryParseHtmlString("#92FF93", out CurrentScene);
        ColorUtility.TryParseHtmlString("#8AB6FF", out SelecredScene);
        ColorUtility.TryParseHtmlString("#FFFFFF", out UnselecredScene);

        RenderScene();
    }

    private void RenderScene()
    {
        TextSceneNumber.GetComponent<InputField>().text = "Scene 1";
    }

    internal void ChangeScene(int sceneNumber, GameObject Target)
    {
        CurrentTarget = Target;
        RenderScene();
    }
}
