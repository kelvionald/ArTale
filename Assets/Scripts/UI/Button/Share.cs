using Assets.Scripts;
using Assets.Scripts.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Text;
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
        camera.GetComponent<MenuManager>().TaleLinkOutput.GetComponent<InputField>().text = "";
        Utils.DisableSSL();
        using (WebClient client = new WebClient())
        {
            try
            {
                string taleName = camera.GetComponent<TaleManager>().TaleName;
                string filepath = TaleModel.ZipTale(taleName);

                byte[] responseB = client.UploadFile(Utils.UploadUrl + "?taleName=" + taleName, filepath);
                string response = Encoding.Default.GetString(responseB);
                Debug.Log(response);

                ShareResponse shareResp = JsonUtility.FromJson<ShareResponse>(response);

                if (shareResp.status)
                {
                    camera.GetComponent<MenuManager>().TaleLinkOutput.GetComponent<InputField>().text = shareResp.link;
                }
                else
                {
                    camera.GetComponent<MenuManager>().ShowMessage(response);
                }
            }
            catch (Exception ex)
            {
                camera.GetComponent<MenuManager>().ShowMessage(ex.Message + "  " + ex.StackTrace);
            }
        }
    }
}
