using Assets.Scripts.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Siccity.GLTFUtility;
using UnityEngine.UI;
using System.IO.Compression;
using System.Net;

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

        public void Create(string taleName, TaleManager taleManager)
        {
            taleManager.ClearTale();
            taleManager.BtnAddOnClick();
            taleManager.UpdateVisibleScenes();
        }

        public void Load(string taleName, TaleManager taleManager)
        {
            Tale tale = ReadFile(taleName);
            Unserialize(taleManager, taleName, tale);
        }

        public void Save(string taleName, TaleManager taleManager)
        {
            TaleName = taleName;
            Tale tale = Serialize(taleManager);
            WriteFile(taleName, tale);
        }

        private Tale ReadFile(string taleName)
        {
            string pathTaleRoot = Utils.PathSaves + taleName + "/";
            string pathTale = pathTaleRoot + "tale.json";
            string json = File.ReadAllText(pathTale);
            return JsonUtility.FromJson<Tale>(json);
        }

        public static string ZipTale(string taleName)
        {
            string pathTale = Utils.PathSaves + taleName + "/";
            string pathTaleZip = Utils.PathSaves + taleName + ".zip";
            if (File.Exists(pathTaleZip))
            {
                File.Delete(pathTaleZip);
            }
            ZipFile.CreateFromDirectory(pathTale, pathTaleZip);
            return pathTaleZip;
        }

        private void WriteFile(string taleName, Tale tale)
        {
            string pathTaleRoot = Utils.PathSaves + taleName + "/";
            Utils.TapDirectory(pathTaleRoot);
            string pathTale = pathTaleRoot + "tale.json";
            string pathModels = pathTaleRoot + "Models/";
            Utils.TapDirectory(pathModels);
            string json = JsonUtility.ToJson(tale, true);
            Debug.Log(pathTale);
            File.WriteAllText(pathTale, json);
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

        private void Unserialize(TaleManager taleManager, string taleName, Tale tale)
        {
            string pathTaleRoot = Utils.PathSaves + taleName + "/";
            string pathModels = pathTaleRoot + "Models/";

            taleManager.ClearTale();

            foreach (Scene scene in tale.scenes)
            {
                ButtonScene bs = taleManager.CreateScene(scene.name);
                bs.SceneId = scene.id;
                bs.gameObject.transform.position = scene.btnPosition;
                foreach (Obj obj in scene.objs)
                {
                    GameObject objModel = UnserializeObj(obj, pathModels);
                    objModel.transform.SetParent(bs.Scene.transform);
                }
            }
            taleManager.UpdateVisibleScenes();
            LoadModels(pathModels, taleManager.GetComponent<DrawPreviewSceneObjects>());
            // set links
            taleManager.Links = new Dictionary<int, List<int>>();
            foreach (string kv in tale.Links)
            {
                string[] ab = kv.Split(new char[] { ':' });
                List<int> b = ab[1].Split(new char[] { ',' }).Select(x => Convert.ToInt32(x)).ToList();
                taleManager.Links.Add(Convert.ToInt32(ab[0]), b);
            }
            taleManager.RenderLinks();
        }

        public static string Download(string link)
        {
            string[] parts = link.Split(new char[] { '/' });
            string taleNameZip = parts[parts.Length - 1];
            string taleName = taleNameZip.Split(new char[] { '.' })[0];
            string pathTaleZip = Utils.PathSaves + taleNameZip;
            WebClient Client = new WebClient();
            if (File.Exists(pathTaleZip))
            {
                File.Delete(pathTaleZip);
            }
            Client.DownloadFile(link, pathTaleZip);

            string pathTale = Utils.PathSaves + taleName + "/";
            if (Directory.Exists(pathTale))
            {
                Directory.Delete(pathTale, true);
            }
            ZipFile.ExtractToDirectory(pathTaleZip, pathTale);

            return taleName;
        }

        private void LoadModels(string modelDir, DrawPreviewSceneObjects drawerPreview)
        {
            var paths = Directory.GetFiles(modelDir, "*.gltf", SearchOption.TopDirectoryOnly);
            foreach (string path in paths)
            {
                try
                {
                    GameObject model = CreateObjFromFile(path);
                    model.transform.SetParent(drawerPreview.ObjectsForScene.transform);
                }
                catch (Exception ex)
                {
                    Debug.Log("1" + " " + ex.Message + " " + ex.Source + " " + ex.StackTrace);
                }
            }
            drawerPreview.RenderObjectsPreview();
        }

        private Tale Serialize(TaleManager taleManager)
        {
            Tale tale = new Tale();
            tale.Edited = ConvertToUnixTime(DateTime.Now);
            foreach (Transform _scene in taleManager.ImgTarget.transform)
            {
                Scene scene = new Scene();
                scene.objs = SerializeObj(_scene);
                ButtonScene bs = FindButtonScene(taleManager, _scene);
                scene.name = bs.GetComponentInChildren<Text>().text;
                scene.id = bs.SceneId;
                scene.btnPosition = bs.gameObject.transform.position;
                tale.scenes.Add(scene);
            }
            tale.Links = new List<string>();
            foreach (var kv in taleManager.Links)
            {
                List<string> ls = kv.Value.Select(x => x.ToString()).ToList();
                tale.Links.Add(kv.Key + ":" + string.Join(",", ls));
            }
            return tale;
        }

        private ButtonScene FindButtonScene(TaleManager taleManager, Transform scene)
        {
            foreach (Transform t in taleManager.PanelScenesGraph.transform)
            {
                var bs = t.GetComponent<ButtonScene>();
                if (bs  && bs.Scene == scene.gameObject)
                {
                    return bs;
                }
            }
            return null;
        }

        private GameObject UnserializeObj(Obj obj, string pathModels)
        {
            GameObject objModel = CreateObjFromFile(pathModels + obj.modelFilename);

            objModel.transform.position = obj.position;
            objModel.transform.rotation = obj.rotation;
            objModel.transform.localScale = obj.localScale;

            return objModel;
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

        public static GameObject CreateObjFromFile(string path)
        {
            GameObject model = Importer.LoadFromFile(path);
            model.AddComponent<BoxCollider>();
            model.AddComponent<MoveObj>();
            model.GetComponent<MoveObj>().ModelFilename = Path.GetFileName(path);
            return model;
        }

        internal void AddModel(string path)
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

        public ScriptScenes LoadScript(string taleName)
        {
            string pathTaleRoot = Utils.PathSaves + taleName + "/";
            string pathTale = pathTaleRoot + "script.json";
            string json = File.ReadAllText(pathTale);
            return JsonUtility.FromJson<ScriptScenes>(json);
        }
    }
}
