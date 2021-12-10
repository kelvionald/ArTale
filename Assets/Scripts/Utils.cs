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
        //public static string PathModels = Application.persistentDataPath + "/Models/";

        public static void TapDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        internal static void Init()
        {
            //TapDirectory(PathModels);
            TapDirectory(PathSaves);
        }
    }
}
