using Assets.Scripts;
using Assets.Scripts.Model;
using System.Collections;
using System.Collections.Generic;
using System.Net;
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
        using (WebClient client = new WebClient())
        {
            GameObject camera = GameObject.Find("ARCamera");
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
        }
    }
}
