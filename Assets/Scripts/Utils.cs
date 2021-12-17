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
        public static string PathModelsWindows = Application.persistentDataPath + "/Models/"; // for load models win
        public static string PathRootAndroid = "/storage/emulated/0/ArTale/"; // project android root
        public static string PathModelsAndroid = PathRootAndroid + "Models/"; // for load models android

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
    }
}
