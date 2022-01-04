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

    public GameObject PanelEnd;
    public GameObject BtnEnd;

    public AudioSource audioSource;
    public AudioClip audioClip;
    public string taleName;

    private TaleManager _TaleManager;
    private int CurrentSceneId = 1;
    private int CurrentScriptIndex = 1;

    ScriptScenes Script;

    void Start()
    {
        _TaleManager = GetComponent<TaleManager>();
        BtnNext.GetComponent<Button>().onClick.AddListener(Next);
        audioSource = GetComponent<AudioSource>();

        PanelEnd.SetActive(false);
        BtnEnd.GetComponent<Button>().onClick.AddListener(End);
    }

    private void End()
    {
        Utils.IsViewMode = false;

        Utils.HideOtherPanels(GetComponent<MenuManager>().PanelMenu);
    }

    public void Run(string taleName)
    {
        Utils.IsViewMode = true;

        Utils.HideOtherPanels(GetComponent<MenuManager>().PanelTaleView);
        PanelEnd.SetActive(false);

        this.taleName = taleName;

        CurrentSceneId = 1;
        CurrentScriptIndex = 0;

        TaleModel tm = new TaleModel();
        Script = tm.LoadScript(taleName);
        Debug.Log(JsonUtility.ToJson(Script, true));

        ShowScript(CurrentSceneId, CurrentScriptIndex);
    }

    private void Next()
    {
        Debug.Log("Next");
        Utils.HideOtherPanels(GetComponent<MenuManager>().PanelTaleView);
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
            CurrentSceneId--;
            PanelEnd.SetActive(true);
        }
    }

    private void ShowScript(int sceneId, int scriptIndex)
    {
        Debug.Log(sceneId + " " + scriptIndex);
        _TaleManager.ShowSceneById(CurrentSceneId);
        Utils.HideOtherPanels(GetComponent<MenuManager>().PanelTaleView);

        ScriptScene ss = FindScene(sceneId);
        TextTitle.GetComponent<Text>().text = "<b>" + ss.title + "</b>";
        ScriptPart sp = ss.script[scriptIndex];
        TextDescription.GetComponent<Text>().text = sp.text;
        if (sp.song != null)
        {
            string wwwPre = "file://";//file:/
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                wwwPre = "file:///";
            }
            string wwwPath = wwwPre + Utils.PathSaves + taleName + "/" + sp.song;
            //GetComponent<MenuManager>().ShowMessage(wwwPath);
            StartCoroutine(LoadAudio(wwwPath, sp.song));
        }
    }

    private IEnumerator LoadAudio(string soundPath, string audioName)
    {
        WWW request = new WWW(soundPath);
        yield return request;

        audioClip = request.GetAudioClip();
        audioClip.name = audioName;

        PlayAudioFile();
    }

    private void PlayAudioFile()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    private ScriptScene FindScene(int sceneId)
    {
        return Script.scenes.Find(x => x.sceneId == sceneId);
    }
}
