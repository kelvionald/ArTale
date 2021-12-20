using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model
{
    [Serializable]
    public class Scene
    {
        public List<Obj> objs = new List<Obj>();
        public int id;
        public string name;
        public Vector3 btnPosition;
    }
}
