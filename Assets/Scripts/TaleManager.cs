using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaleManager : MonoBehaviour
{
    public GameObject[] ImageTargets;
    public GameObject CurrentTarget;
    public int CurrentScene = 1;
    public int MaxScene = 4;

    void Start()
    {
        CurrentScene = 1;
        RenderScene();
    }

    private void RenderScene()
    {
        CurrentTarget.GetComponent<Text>().text = "Scene " + CurrentScene + "/" + MaxScene;
    }
}
