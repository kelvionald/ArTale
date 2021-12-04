using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaleManager : MonoBehaviour
{
    public GameObject TextSceneNumber;
    public GameObject CurrentScene;

    public ButtonScene SelectedBtnScene;

    public GameObject ImgTarget;
    public GameObject BtnScenes;

    public GameObject BtnBack;
    public GameObject BtnAdd;
    public GameObject BtnShow;
    public GameObject BtnRemove;
    public GameObject PanelScenesManager;
    public GameObject PanelScenesGraph;
    public GameObject TmplBtnScene;

    public int LastSceneNumber;

    void Start()
    {
        LastSceneNumber = 1;

        //PanelScenesManager.SetActive(false);

        BtnScenes.GetComponent<Button>().onClick.AddListener(BtnScenesOnClick);
        BtnBack.GetComponent<Button>().onClick.AddListener(BtnBackOnClick);
        BtnAdd.GetComponent<Button>().onClick.AddListener(BtnAddOnClick);
        BtnShow.GetComponent<Button>().onClick.AddListener(BtnShowOnClick);
        BtnRemove.GetComponent<Button>().onClick.AddListener(BtnRemoveOnClick);

        BtnAddOnClick(); // first scene

        RenderScene(1);
    }

    private void BtnScenesOnClick()
    {
        PanelScenesManager.SetActive(true);
    }

    private void BtnBackOnClick()
    {
        PanelScenesManager.SetActive(false);
    }

    private void BtnAddOnClick()
    {
        GameObject btnScene = Instantiate(TmplBtnScene, PanelScenesGraph.transform);
        btnScene.GetComponentInChildren<Text>().text = "Scene " + LastSceneNumber;

        GameObject scene = Instantiate(new GameObject(), ImgTarget.transform);

        bool currentSceneIsNull = CurrentScene == null;
        if (currentSceneIsNull)
        {
            CurrentScene = scene;
        }

        btnScene.GetComponent<ButtonScene>().Init(scene, this, currentSceneIsNull, LastSceneNumber);

        LastSceneNumber++;
    }

    private void BtnShowOnClick()
    {
        if (SelectedBtnScene == null)
        {
            BtnBackOnClick();
            return;
        }
        Debug.Log("SelectedId " + SelectedBtnScene.SceneId);
        CurrentScene = SelectedBtnScene.Scene;
        RenderScene(SelectedBtnScene.SceneId);
        foreach (Transform scene in ImgTarget.transform)
        {
            scene.gameObject.SetActive(scene.gameObject == SelectedBtnScene.Scene);
        }
        foreach (Transform sceneBtn in PanelScenesGraph.transform)
        {
            var tmpBtnScene = sceneBtn.GetComponent<ButtonScene>();
            tmpBtnScene.IsCurrent = tmpBtnScene == SelectedBtnScene;
            tmpBtnScene.toggleSelection(false);
        }
        BtnBackOnClick();
    }

    private void BtnRemoveOnClick()
    {
        if (SelectedBtnScene == null)
        {
            return;
        }
        Destroy(SelectedBtnScene.Scene);
        Destroy(SelectedBtnScene.gameObject);
    }

    private void RenderScene(int id)
    {
        TextSceneNumber.GetComponent<InputField>().text = "Scene " + id;
    }

    internal void SelectSceneBtn(ButtonScene buttonScene, GameObject sceneParent)
    {
        SelectedBtnScene = buttonScene;
        foreach (Transform btn in PanelScenesGraph.transform)
        {
            var btnScene = btn.gameObject.GetComponent<ButtonScene>();
            if (btnScene != null)
            {
                btnScene.toggleSelection(btnScene == buttonScene);
            }
        }
    }
}
