using System;
using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        PanelMenu.SetActive(false);
        BtnMenu.GetComponent<Button>().onClick.AddListener(OnClickMenu);
        BtnCloseMenu.GetComponent<Button>().onClick.AddListener(OnClickMenuClose);

        BtnSaveTale.GetComponent<Button>().onClick.AddListener(OnClickSaveTale);
        ButtonCopyLink.GetComponent<Button>().onClick.AddListener(OnClickCopyLink);
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
        string sceneName = InputFieldTaleName.GetComponent<InputField>().text;
        if (sceneName.Length == 0)
        {
            return;
        }

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
