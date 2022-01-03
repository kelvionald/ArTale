using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLoadTale : MonoBehaviour
{
    public string TaleName;
    public GameObject PanelTale;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnMouseClick);
    }

    void OnMouseClick()
    {
        GameObject camera = GameObject.Find("ARCamera");
        TaleModel TaleModelObj = new TaleModel();
        TaleModelObj.Load(TaleName, camera.GetComponent<TaleManager>());
        MenuManager mm = camera.GetComponent<MenuManager>();
        mm.InputFieldTaleName.GetComponent<InputField>().text = TaleName;
        mm.TaleName = TaleName;
        mm.LoadModels();

        Utils.HideOtherPanels(PanelTale);
    }
}
