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

        public const int ScenePractice = 3;         //Default Scene Selection
        public const int SceneBrake = 4;
        public const int SceneLaneChange = 5;
        public const int SceneIntersection = 6;
        public const int NormalMode = 0;            //Default Mode Selection
        public const int LowDistractedMode = 1;
        public const int HighDistractedMode = 2;

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
