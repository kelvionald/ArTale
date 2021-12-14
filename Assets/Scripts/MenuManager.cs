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

    public string TaleName;
    TaleModel TaleModelObj;

    public GameObject CurrentMoveObj = null;

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

        UpdateScrollLoadTale();
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
            //Vector3 a = btn.GetComponent<Button>()
            //btn.transform.localPosition = new Vector3(a.x, a.y, a.z - i * 35);
            btn.GetComponent<ButtonLoadTale>().TaleName = name;
            btn.GetComponentInChildren<Text>().text = name;


            btn.SetActive(false);
            RectTransform pos = btn.GetComponent<RectTransform>();
            Debug.Log(pos.anchoredPosition);
            btn.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.anchoredPosition.x, pos.anchoredPosition.y - i * 35);
            Debug.Log(btn.GetComponent<RectTransform>().anchoredPosition);
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

    private void OnClickLoadModels()
    {
        if (TaleName == null || TaleName.Length == 0)
        {
            PanelMessage.SetActive(true);
            LabelMessage.GetComponent<Text>().text = "Set a name for the tale and save it before doing so.";
            return;
        }

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
                GameObject model = Importer.LoadFromFile(path);
                model.transform.SetParent(ObjectsForScene.transform);
                //model.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                model.AddComponent<BoxCollider>();
                model.AddComponent<MoveObj>();
                model.GetComponent<MoveObj>().ModelFilename = Path.GetFileName(path);
                TaleModelObj.AddModels(path);
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

        TaleModelObj = new TaleModel();
        TaleModelObj.Save(TaleName, GetComponent<TaleManager>());

        // InputFieldTaleLinkOutput.GetComponent<InputField>().text = "1"; // TODO tale link in save on server

        UpdateScrollLoadTale();
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
