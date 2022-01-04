using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject BtnCloseMenu;
    public GameObject PanelMenu;
    public GameObject PanelEditorMenu;

    public GameObject BtnSaveTale;

    public GameObject TalesList;
    public GameObject TalesListView;

    public GameObject TalesListItem;

    public GameObject TaleLinkOutput;
    public GameObject ButtonCopyLink;

    public GameObject TaleLinkInput;
    public GameObject BtnLoadTaleFromServer;

    public GameObject ButtonLoadModels;

    public GameObject PanelMessage;
    public GameObject ButtonOk;
    public GameObject LabelMessage;

    public GameObject BtnRunView;
    public GameObject PanelTale;
    public GameObject PanelTaleView;

    TaleModel TaleModelObj;

    public GameObject CurrentMoveObj = null;
    public GameObject PanelMainMenu;

    void Start()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }

        Utils.Init();
        Utils.HideOtherPanels(PanelMainMenu);

        BtnCloseMenu.GetComponent<Button>().onClick.AddListener(MenuClose);

        BtnSaveTale.GetComponent<Button>().onClick.AddListener(OnClickSaveTale);
        ButtonCopyLink.GetComponent<Button>().onClick.AddListener(OnClickCopyLink);

        ButtonLoadModels.GetComponent<Button>().onClick.AddListener(LoadModels);

        BtnLoadTaleFromServer.GetComponent<Button>().onClick.AddListener(OnClickLoadFromServer);

        ButtonOk.GetComponent<Button>().onClick.AddListener(OnClickOk);

        BtnRunView.GetComponent<Button>().onClick.AddListener(RunView);

        UpdateScrollLoadTale();

        TaleModelObj = new TaleModel();
    }

    private void RunView()
    {
        GetComponent<MenuManager>().PanelMenu = PanelEditorMenu;
        GetComponent<ViewManager>().Run(GetComponent<TaleManager>().TaleName);
    }

    void UpdateScrollLoadTale()
    {
        foreach (Transform child in TalesList.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in TalesListView.transform)
        {
            Destroy(child.gameObject);
        }

        List<string> talesNames = TaleModel.LoadTaleList();

        int i = 0;
        foreach (string name in talesNames)
        {
            GameObject btn = Instantiate(TalesListItem, TalesList.transform);
            btn.GetComponent<ButtonLoadTale>().TaleName = name;
            btn.GetComponent<ButtonLoadTale>().PanelEditOrView = PanelTale;
            btn.GetComponent<ButtonLoadTale>().IsEdit = true;
            btn.GetComponentInChildren<Text>().text = name;
            btn.SetActive(false);
            RectTransform pos = btn.GetComponent<RectTransform>();
            btn.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.anchoredPosition.x, pos.anchoredPosition.y - i * 35);
            btn.SetActive(true);

            GameObject btnView = Instantiate(TalesListItem, TalesListView.transform);
            btnView.GetComponent<ButtonLoadTale>().TaleName = name;
            btnView.GetComponent<ButtonLoadTale>().PanelEditOrView = PanelTaleView;
            btnView.GetComponent<ButtonLoadTale>().PanelMainMenu = PanelMainMenu;
            btnView.GetComponent<ButtonLoadTale>().IsEdit = false;
            btnView.GetComponentInChildren<Text>().text = name;
            btnView.SetActive(false);
            RectTransform posView = btnView.GetComponent<RectTransform>();
            btnView.GetComponent<RectTransform>().anchoredPosition = new Vector2(posView.anchoredPosition.x, posView.anchoredPosition.y - i * 35);
            btnView.SetActive(true);

            i++;
        }
    }

    private void OnClickLoadFromServer()
    {
        try
        {
            Utils.DisableSSL();
            string link = TaleLinkInput.GetComponent<InputField>().text;
            string taleName = TaleModel.Download(link);

            // load scenes
            TaleModel TaleModelObj = new TaleModel();
            TaleManager taleManager = GetComponent<TaleManager>();
            taleManager.TaleName = taleName;
            taleManager.CurrentScene = null;
            TaleModelObj.Load(taleName, taleManager);

            RunView();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message + "  " + ex.StackTrace);
        }
    }

    private void OnClickOk()
    {
        PanelMessage.SetActive(false);
    }

    public void LoadModels()
    {
        string TaleName = GetComponent<TaleManager>().TaleName;
        if (TaleName == null || TaleName.Length == 0)
        {
            Debug.Log("Set a name for the tale and save it before doing so.");
            ShowMessage("Set a name for the tale and save it before doing so.");
            return;
        }

        TaleModelObj.TaleName = TaleName;
        DrawPreviewSceneObjects drawerPreview = GetComponent<DrawPreviewSceneObjects>();

        try
        {
            string modelDir = Utils.CalcModelsLoadPath();
            if (modelDir.Length != 0)
            {
                drawerPreview.ClearObjectsForScene();

                Debug.Log(modelDir);
                var paths = Directory.GetFiles(modelDir, "*.gltf", SearchOption.TopDirectoryOnly);
                foreach (string path in paths)
                {
                    try
                    {
                        GameObject model = TaleModel.CreateObjFromFile(path);
                        model.transform.SetParent(drawerPreview.ObjectsForScene.transform);
                        TaleModelObj.AddModel(path);
                    }
                    catch (Exception ex)
                    {
                        Debug.Log("1" + " " + ex.Message + " " + ex.Source + " " + ex.StackTrace);
                    }
                }

                drawerPreview.RenderObjectsPreview();
            }
        }
        catch (Exception ex)
        {
            Debug.Log("2" + " " + ex.Message + " " + ex.Source + " " + ex.StackTrace);
        }
    }

    public void ShowMessage(string v)
    {
        PanelMessage.SetActive(true);
        LabelMessage.GetComponent<Text>().text = v;
    }

    private void OnClickCopyLink()
    {
        TextEditor editor = new TextEditor
        {
            text = TaleLinkOutput.GetComponent<InputField>().text
        };
        editor.SelectAll();
        editor.Copy();
    }

    public void OnClickSaveTale()
    {
        string taleName = GetComponent<TaleManager>().TaleName;
        if (taleName.Length == 0)
        {
            return;
        }

        TaleModelObj.Save(taleName, GetComponent<TaleManager>());

        UpdateScrollLoadTale();
    }

    public void MenuClose()
    {
        PanelMenu.SetActive(false);
    }
}
