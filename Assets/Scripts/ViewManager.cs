using Assets.Scripts;
using Assets.Scripts.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    public GameObject BtnNext;
    public GameObject TextTitle;
    public GameObject TextDescription;

    public void Run(string taleName)
    {
        TaleModel tm = new TaleModel();
        ScriptScenes script = tm.LoadScript(taleName);
        Debug.Log(JsonUtility.ToJson(script, true));
    }
}
