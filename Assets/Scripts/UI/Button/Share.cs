using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Share : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ShareHandler);
    }

    private void ShareHandler()
    {
        GameObject camera = GameObject.Find("ARCamera");
        string link = "";

        camera.GetComponent<MenuManager>().TaleLinkOutput.GetComponent<InputField>().text = link;
    }
}
