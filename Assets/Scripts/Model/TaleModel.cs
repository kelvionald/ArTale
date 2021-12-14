using Assets.Scripts.Model;
using Assets.Scripts.Model.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class TaleModel
    {
        public string TaleName;

        public static List<string> LoadTaleList()
        {
            List<string> talesNames = new List<string>();
            var paths = Directory.GetDirectories(Utils.PathSaves);
            foreach (string path in paths)
            {
                talesNames.Add(Path.GetFileName(path));
            }
            return talesNames;
        }

        public void Save(string taleName, TaleManager taleManager)
        {
            TaleName = taleName;
            Tale tale = Serialize(taleManager);
            WriteFile(taleName, tale);
        }

        private void WriteFile(string sceneName, Tale tale)
        {
            string pathTaleRoot = Utils.PathSaves + sceneName + "/";
            Utils.TapDirectory(pathTaleRoot);
            string pathTale = pathTaleRoot + "tale.json";
            string pathModels = pathTaleRoot + "Models/";
            Utils.TapDirectory(pathModels);
            string json = JsonUtility.ToJson(tale, true);
            Debug.Log(pathTale);
            File.WriteAllText(pathTale, json);

            /*BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/" + sceneName + ".dat");
            bf.Serialize(file, tale);
            file.Close();*/
        }

        public static long ConvertToUnixTime(DateTime datetime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(datetime - sTime).TotalSeconds;
        }

        public static DateTime UnixTimeToDateTime(long unixtime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return sTime.AddSeconds(unixtime);
        }

        private Tale Serialize(TaleManager taleManager)
        {
            Tale tale = new Tale();
            tale.Edited = ConvertToUnixTime(DateTime.Now);
            foreach (Transform _scene in taleManager.ImgTarget.transform)
            {
                Scene scene = new Scene();
                List<Obj> tmp = SerializeObj(_scene);
                scene.objs = tmp;
                tale.scenes.Add(scene);
            }
            return tale;
        }

        private List<Obj> SerializeObj(Transform _scene)
        {
            List<Obj> objs = new List<Obj>();
            foreach (Transform _obj in _scene.transform)
            {
                if (_obj.GetComponent<MoveObj>() == null)
                {
                    continue;
                }

                Obj obj = new Obj();

                // common
                obj.position = _obj.transform.position;
                obj.rotation = _obj.transform.rotation;
                obj.localScale = _obj.transform.localScale;

                obj.modelFilename = _obj.GetComponent<MoveObj>().ModelFilename;

                objs.Add(obj);
            }
            return objs;
        }

        internal void AddModels(string path)
        {
            string pathTaleRoot = Utils.PathSaves + TaleName+ "/";
            Utils.TapDirectory(pathTaleRoot);
            string pathModels = pathTaleRoot + "Models/";
            Utils.TapDirectory(pathModels);
            string dest = pathModels + Path.GetFileName(path);
            if (File.Exists(dest))
            {
                File.Delete(dest);
            }
            File.Copy(path, dest);
        }
    }
}
