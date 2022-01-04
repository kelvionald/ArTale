using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class Utils
    {
        public static string PathSaves = Application.persistentDataPath + "/Saves/";
        public static string PathModelsWindows = Application.persistentDataPath + "/Models/"; // for load models win

        public static string PathRootAndroid = "/storage/emulated/0/ArTale/"; // project android root
        public static string PathModelsAndroid = PathRootAndroid + "Models/"; // for load models android

        public static string HelpUrl = "https://nlix.ru/ArTale/Materials.pdf";

        public static void DisableSSL()
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(
                delegate
                { return true; }
            );
        }

        public static string UploadUrl = "https://nlix.ru/ArTale/upload.php";

        public static bool IsViewMode = false;

        public static void TapDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        internal static void Init()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {

            }
            else
            {
                PathSaves = PathRootAndroid + "Saves/";
            }

            TapDirectory(PathSaves);
            TapDirectory(PathRootAndroid);
            TapDirectory(CalcModelsLoadPath());
        }

        internal static string CalcModelsLoadPath()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                return PathModelsWindows;
            }
            else
            {
                return PathModelsAndroid;
            }
        }

        internal static void HideOtherPanels(GameObject panel)
        {
            Debug.Log("HIDE AND SHOW " + panel);
            GameObject canvas = GameObject.Find("Canvas").gameObject;
            foreach (Transform t in canvas.transform)
            {
                t.gameObject.SetActive(t.gameObject == panel);
                if (t.gameObject.GetComponent<Text>())
                {
                    t.gameObject.SetActive(true);
                }
            }
        }
    }
}
