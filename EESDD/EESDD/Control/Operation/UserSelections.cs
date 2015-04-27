using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Control.Operation
{
    class UserSelections
    {
        int sceneSelect;
        int modeSelect;

        public UserSelections()
        {
            sceneSelect = 0;
            modeSelect = 0;
        }

        public const int ScenePractice = 0;         //Default Scene Selection
        public const int SceneSecurityOne = 1;
        public const int SceneSecurityTwo = 2;
        public const int SceneSecurityThree = 3;
        public const int SceneSmoothOne = 4;
        public const int NormalMode = 0;            //Default Mode Selection
        public const int DistractedMode = 1;

        public int SceneSelect
        {
            get { return sceneSelect; }
            set { sceneSelect = value; }
        }

        public int ModeSelect
        {
            get { return modeSelect; }
            set { modeSelect = value; }
        }
    }
}
