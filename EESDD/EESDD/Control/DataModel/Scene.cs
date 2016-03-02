using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Control.DataModel
{
    class Scene
    {
        public Scene(string sceneName, int sceneValue)
        {
            this.SceneName = sceneName;
            this.SceneValue = sceneValue;
        }

        public string SceneName { get; set; }
        public int SceneValue { get; set; }
    }
}
