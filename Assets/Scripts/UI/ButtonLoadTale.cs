using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLoadTale : MonoBehaviour
{
    public string TaleName;
    public GameObject PanelEditOrView;
    public GameObject PanelMainMenu;

    public bool IsEdit = true;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnMouseClick);
    }

    void OnMouseClick()
    {
        GameObject camera = GameObject.Find("ARCamera");
        MenuManager mm = camera.GetComponent<MenuManager>();
        if (IsEdit)
        {
            TaleModel TaleModelObj = new TaleModel();
            TaleManager taleManager = camera.GetComponent<TaleManager>();
            taleManager.TaleName = TaleName;
            TaleModelObj.Load(TaleName, taleManager);
        }
        else
        {
            mm.PanelMenu = PanelMainMenu;
            mm.GetComponent<ViewManager>().Run(TaleName);
        }

        Utils.HideOtherPanels(PanelEditOrView);
    }
}
