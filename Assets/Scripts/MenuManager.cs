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

    void Start()
    {
        PanelMenu.SetActive(false);
        BtnMenu.GetComponent<Button>().onClick.AddListener(OnClickMenu);
        BtnCloseMenu.GetComponent<Button>().onClick.AddListener(OnClickMenuClose);
    }

    private void OnClickMenu()
    {
        PanelMenu.SetActive(true);
    }

    private void OnClickMenuClose()
    {
        PanelMenu.SetActive(false);
    }

    void Update()
    {
        
    }
}
