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
            modeSelect = 100;
        }
        private const int modeCount         = 5;

        // -------- scene -------->
        public  const int ScenePractice     = 0;         //Default Scene Selection
        public  const int SceneBrake        = modeCount;
        public  const int SceneLaneChange   = 2 * modeCount;
        public  const int SceneIntersection = 3 * modeCount;
        // -------- mode --------->
        public  const int NormalMode        = 100;            //Default Mode Selection
        public  const int DistractAMode     = 101;
        public  const int DistractBMode     = 102;
        public  const int DistractCMode     = 103;
        public  const int DistractDMode     = 104;

        public static List<int> SceneList  = new List<int>() { ScenePractice, SceneBrake, SceneLaneChange, SceneIntersection };
        public static List<int> ModeList = new List<int>() { NormalMode, DistractAMode, DistractBMode, DistractCMode, DistractDMode };
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
            if (scene == ScenePractice && (mode == DistractAMode || mode == DistractBMode))
                return -1;
            return scene + mode - NormalMode;
        }
    }
}
