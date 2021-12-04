using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaleManager : MonoBehaviour
{
    public GameObject TextSceneNumber;
    public GameObject CurrentTarget;
    public int CurrentScene = 1;
    public int MaxScene = 4;

    void Start()
    {
        RenderScene();
    }

    private void RenderScene()
    {
        TextSceneNumber.GetComponent<InputField>().text = "Scene " + CurrentScene + "/" + MaxScene;
    }

    internal void ChangeScene(int sceneNumber, GameObject Target)
    {
        CurrentTarget = Target;
        CurrentScene = sceneNumber;
        RenderScene();
    }
}
