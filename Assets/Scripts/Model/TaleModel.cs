﻿using Assets.Scripts.Model;
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
        public void Save(string sceneName, TaleManager taleManager)
        {
            Tale tale = Serialize(taleManager);
            WriteFile(sceneName, tale);
        }

        private void WriteFile(string sceneName, Tale tale)
        {
            string savePath = Application.persistentDataPath + "/Saves/";
            Utils.TapDirectory(savePath);

            string path = savePath + sceneName + ".json";
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
                Obj obj = PackObj(_obj.gameObject);
                obj.children = SerializeObj(_obj);
                objs.Add(obj);
            }
            return objs;
        }

        private Obj PackObj(GameObject gameObject)
        {
            Obj obj = new Obj();
            
            // common
            obj.position = gameObject.transform.position;
            obj.rotation = gameObject.transform.rotation;
            obj.localScale = gameObject.transform.localScale;

            // mesh
            if (gameObject.GetComponent<MeshFilter>() != null)
            {
                obj.meshSer = MeshConverter.Serialize(gameObject.GetComponent<MeshFilter>());
                Debug.Log(JsonUtility.ToJson(obj.meshSer));
            }

            return obj;
        }
    }
}
