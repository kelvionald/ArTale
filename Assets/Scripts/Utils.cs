using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class Utils
    {
        public static string PathSaves = Application.persistentDataPath + "/Saves/";
        public static string PathModelsWindows = "/storage/emulated/0/ArTale/"; // for load models win
        public static string PathModelsAndroid = "/storage/emulated/0/ArTale/"; // for load models android

        public static void TapDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        internal static void Init()
        {
            TapDirectory(PathSaves);
            TapDirectory(PathModelsAndroid);
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
    }
}
