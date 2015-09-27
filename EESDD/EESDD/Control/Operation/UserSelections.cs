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
        public const int SceneBrake = 1;
        public const int SceneLaneChange = 4;
        public const int SceneIntersection = 7;
        public const int NormalMode = 10;            //Default Mode Selection
        public const int LowDistractedMode = 11;
        public const int HighDistractedMode = 12;

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

        public int Index {get {return sceneSelect + modeSelect - NormalMode;} }

        public static int getIndex(int scene, int mode)
        {
            return scene + mode - NormalMode;
        }
    }
}
