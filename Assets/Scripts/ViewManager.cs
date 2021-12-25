using Assets.Scripts;
using Assets.Scripts.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewManager : MonoBehaviour
{
    public GameObject BtnNext;
    public GameObject TextTitle;
    public GameObject TextDescription;

    private TaleManager _TaleManager;
    private int CurrentSceneId = 1;
    private int CurrentScriptIndex = 1;

    ScriptScenes Script;

    void Start()
    {
        _TaleManager = GetComponent<TaleManager>();
        BtnNext.GetComponent<Button>().onClick.AddListener(Next);
    }

    public void Run(string taleName)
    {
        CurrentSceneId = 1;
        CurrentScriptIndex = 0;

        TaleModel tm = new TaleModel();
        Script = tm.LoadScript(taleName);
        Debug.Log(JsonUtility.ToJson(Script, true));

        ShowScript(CurrentSceneId, CurrentScriptIndex);
    }

    private void Next()
    {
        ScriptScene ss = FindScene(CurrentSceneId);
        if (CurrentScriptIndex < ss.script.Count - 1)
        {
            CurrentScriptIndex++;
            ShowScript(CurrentSceneId, CurrentScriptIndex);
            return;
        }
        else
        {
            CurrentSceneId++;
            CurrentScriptIndex = 0;
        }

        ScriptScene ssNext = FindScene(CurrentSceneId);
        if (ssNext != null)
        {
            ShowScript(CurrentSceneId, CurrentScriptIndex);
        } 
        else
        {
            Debug.Log("end");
        }
    }

    private void ShowScript(int sceneId, int scriptIndex)
    {
        Debug.Log(sceneId + " " + scriptIndex);
        _TaleManager.ShowSceneById(CurrentSceneId);

        ScriptScene ss = FindScene(sceneId);
        TextTitle.GetComponent<Text>().text = ss.title;
        TextDescription.GetComponent<Text>().text = ss.script[scriptIndex].text;
        if (ss.script[scriptIndex].song != null)
        {
            Debug.Log("play song " + ss.script[scriptIndex].song);
        }
    }

    private ScriptScene FindScene(int sceneId)
    {
        return Script.scenes.Find(x => x.sceneId == sceneId);
    }
}
