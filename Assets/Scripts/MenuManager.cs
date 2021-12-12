using Assets.Scripts;
using Siccity.GLTFUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject BtnCloseMenu;
    public GameObject BtnMenu;
    public GameObject PanelMenu;

    public GameObject InputFieldTaleName;
    public GameObject BtnSaveTale;
    public GameObject InputFieldTaleLinkOutput;
    public GameObject ButtonCopyLink;

    public GameObject BtnSaveTaleOnServer;
    public GameObject BtnLoadTaleFromServer;

    public GameObject ButtonLoadModels;

    public GameObject PanelMessage;
    public GameObject ButtonOk;
    public GameObject LabelMessage;

    public GameObject ObjectsForScene;

    public string TaleName;

    void Start()
    {
        Utils.Init();

        PanelMenu.SetActive(false);
        PanelMessage.SetActive(false);

        BtnMenu.GetComponent<Button>().onClick.AddListener(OnClickMenu);
        BtnCloseMenu.GetComponent<Button>().onClick.AddListener(OnClickMenuClose);

        BtnSaveTale.GetComponent<Button>().onClick.AddListener(OnClickSaveTale);
        ButtonCopyLink.GetComponent<Button>().onClick.AddListener(OnClickCopyLink);

        ButtonLoadModels.GetComponent<Button>().onClick.AddListener(OnClickLoadModels);

        BtnSaveTaleOnServer.GetComponent<Button>().onClick.AddListener(OnClickSaveOnServer);
        BtnLoadTaleFromServer.GetComponent<Button>().onClick.AddListener(OnClickLoadFromServer);

        ButtonOk.GetComponent<Button>().onClick.AddListener(OnClickOk);
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

    private void OnClickLoadModels()
    {
        if (TaleName == null || TaleName.Length == 0)
        {
            PanelMessage.SetActive(true);
            LabelMessage.GetComponent<Text>().text = "Set a name for the tale and save it before doing so.";
            return;
        }

        //string modelDir = EditorUtility.OpenFolderPanel("Folder with *.gltf models", "", "");
        string modelDir = Utils.CalcModelsLoadPath();
        if (modelDir.Length != 0)
        {
            Debug.Log(modelDir);
            var paths = Directory.GetFiles(modelDir, "*.gltf", SearchOption.TopDirectoryOnly);
            foreach (string path in paths)
            {
                GameObject model = Importer.LoadFromFile(path);
                model.transform.SetParent(ObjectsForScene.transform);
                //model.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                model.AddComponent<BoxCollider>();
                model.AddComponent<MoveObj>();
            }
            GetComponent<DrawPreviewSceneObjects>().RenderObjectsPreview();
        }
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

        TaleModel taleModel = new TaleModel();
        taleModel.Save(TaleName, GetComponent<TaleManager>());

        InputFieldTaleLinkOutput.GetComponent<InputField>().text = "1"; // TODO tale link 
    }

    private void OnClickMenu()
    {
        PanelMenu.SetActive(true);
    }

    private void OnClickMenuClose()
    {
        PanelMenu.SetActive(false);
    }
}
