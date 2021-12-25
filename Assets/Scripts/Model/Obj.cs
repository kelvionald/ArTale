using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model
{
    [Serializable]
    public class Obj
    {
        public Vector3 position;
        public Vector3 localScale;
        public Quaternion rotation;
        
        public string modelFilename;
    }
}
