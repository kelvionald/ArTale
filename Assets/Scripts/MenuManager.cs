using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject BtnCloseMenu;
    public GameObject BtnMenu;
    public GameObject PanelMenu;

    public GameObject InputFieldTaleName;
    public GameObject BtnSaveTale;

    public GameObject TalesList;
    public GameObject TalesListItem;

    public GameObject BtnSaveTaleOnServer;
    public GameObject InputFieldTaleLinkOutput;
    public GameObject ButtonCopyLink;

    public GameObject BtnLoadTaleFromServer;

    public GameObject ButtonLoadModels;

    public GameObject PanelMessage;
    public GameObject ButtonOk;
    public GameObject LabelMessage;

    public GameObject ObjectsForScene;

    public GameObject BtnRunView;
    public GameObject PanelTale;
    public GameObject PanelTaleView;

    public string TaleName;

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

        BtnMenu.GetComponent<Button>().onClick.AddListener(OnClickMenu);
        BtnCloseMenu.GetComponent<Button>().onClick.AddListener(MenuClose);

        BtnSaveTale.GetComponent<Button>().onClick.AddListener(OnClickSaveTale);
        ButtonCopyLink.GetComponent<Button>().onClick.AddListener(OnClickCopyLink);

        ButtonLoadModels.GetComponent<Button>().onClick.AddListener(OnClickLoadModels);

        BtnSaveTaleOnServer.GetComponent<Button>().onClick.AddListener(OnClickSaveOnServer);
        BtnLoadTaleFromServer.GetComponent<Button>().onClick.AddListener(OnClickLoadFromServer);

        ButtonOk.GetComponent<Button>().onClick.AddListener(OnClickOk);

        BtnRunView.GetComponent<Button>().onClick.AddListener(RunView);

        UpdateScrollLoadTale();
    }

    private void RunView()
    {
        MenuClose();
        PanelTale.SetActive(false);
        PanelTaleView.SetActive(true);
        GetComponent<ViewManager>().Run(InputFieldTaleName.GetComponent<InputField>().text);
    }

    internal void ShowEditor()
    {
        OnClickMenu();
        PanelTale.SetActive(true);
        PanelTaleView.SetActive(false);
    }

    void UpdateScrollLoadTale()
    {
        foreach (Transform child in TalesList.transform)
        {
            Destroy(child.gameObject);
        }

        List<string> talesNames = TaleModel.LoadTaleList();

        int i = 0;
        foreach (string name in talesNames)
        {
            GameObject btn = Instantiate(TalesListItem, TalesList.transform);
            btn.GetComponent<ButtonLoadTale>().TaleName = name;
            btn.GetComponentInChildren<Text>().text = name;

            btn.SetActive(false);
            RectTransform pos = btn.GetComponent<RectTransform>();
            btn.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.anchoredPosition.x, pos.anchoredPosition.y - i * 35);
            btn.SetActive(true);
            i++;
        }
    }

    private void OnClickLoadFromServer()
    {
        
    }

    private void OnClickSaveOnServer()
    {
        
    }

    private void OnClickOk()
    {
        PanelMessage.SetActive(false);
    }

    public void OnClickLoadModels()
    {
        if (TaleName == null || TaleName.Length == 0)
        {
            Debug.Log("Set a name for the tale and save it before doing so.");
            ShowMessage("Set a name for the tale and save it before doing so.");
            return;
        }

        try
        {
            string modelDir = Utils.CalcModelsLoadPath();
            if (modelDir.Length != 0)
            {
                foreach (Transform child in ObjectsForScene.transform)
                {
                    Destroy(child.gameObject);
                }

                Debug.Log(modelDir);
                var paths = Directory.GetFiles(modelDir, "*.gltf", SearchOption.TopDirectoryOnly);
                foreach (string path in paths)
                {
                    try
                    {
                        GameObject model = TaleModel.CreateObjFromFile(path);
                        model.transform.SetParent(ObjectsForScene.transform);
                        TaleModelObj.AddModel(path);
                    }
                    catch (Exception ex)
                    {
                        Debug.Log("1" + " " + ex.Message + " " + ex.Source + " " + ex.StackTrace);
                    }
                }
                GetComponent<DrawPreviewSceneObjects>().RenderObjectsPreview();
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
            text = InputFieldTaleLinkOutput.GetComponent<InputField>().text
        };
        editor.SelectAll();
        editor.Copy();
    }

    private void OnClickSaveTale()
    {
        string taleName = InputFieldTaleName.GetComponent<InputField>().text;
        if (taleName.Length == 0)
        {
            return;
        }
        TaleName = taleName;

        TaleModelObj = new TaleModel();
        TaleModelObj.Save(TaleName, GetComponent<TaleManager>());

        UpdateScrollLoadTale();
    }

    private void OnClickMenu()
    {
        PanelMenu.SetActive(true);
    }

    public void MenuClose()
    {
        PanelMenu.SetActive(false);
    }
}
