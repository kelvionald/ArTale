using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLoadTale : MonoBehaviour
{
    public string TaleName;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnMouseClick);
    }

    void OnMouseClick()
    {
        GameObject camera = GameObject.Find("ARCamera");
        TaleModel TaleModelObj = new TaleModel();
        TaleModelObj.Load(TaleName, camera.GetComponent<TaleManager>());
    }
}
