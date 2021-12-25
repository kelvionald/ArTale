using Assets.Scripts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class Tale
    {
        public long Edited;
        public List<string> Links;
        public List<Scene> scenes = new List<Scene>();
    }
}