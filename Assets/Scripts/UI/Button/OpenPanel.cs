using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenPanel : MonoBehaviour
{
    public GameObject panel;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OpenPanelAndHideOther);
    }

    private void OpenPanelAndHideOther()
    {
        Utils.HideOtherPanels(panel);
    }
}
