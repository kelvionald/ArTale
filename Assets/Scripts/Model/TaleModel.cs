using Assets.Scripts.Model;
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
        public void Save(string sceneName, TaleManager taleManager)
        {
            Tale tale = Serialize(taleManager);
            WriteFile(sceneName, tale);
        }

        private void WriteFile(string sceneName, Tale tale)
        {
            string path = Application.persistentDataPath + "/" + sceneName + ".json";
            string json = JsonUtility.ToJson(tale);
            Debug.Log(path);
            File.WriteAllText(path, json);

            /*BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/" + sceneName + ".dat");
            bf.Serialize(file, tale);
            file.Close();*/
        }

        private Tale Serialize(TaleManager taleManager)
        {
            Tale tale = new Tale();
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
                Obj obj = new Obj();
                obj.position = _obj.gameObject.transform.position;
                obj.rotation = _obj.gameObject.transform.rotation;
                obj.localScale = _obj.gameObject.transform.localScale;
                objs.Add(obj);
            }
            return objs;
        }
    }
}
