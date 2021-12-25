using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    [Serializable]
    class ScriptScene
    {
        public int sceneId;
        public string title;
        public List<ScriptPart> script;
    }
}
