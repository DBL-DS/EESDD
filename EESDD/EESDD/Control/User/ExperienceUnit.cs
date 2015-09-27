using EESDD.Control.DataModel;
using EESDD.Control.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Control.User
{
    [Serializable]
    class ExperienceUnit
    {
        int sceneID;
        public int SceneID
        {
            get { return sceneID; }
            set { sceneID = value; }
        }

        int mode;
        public int Mode
        {
            get { return mode; }
            set { mode = value; }
        }
        List<SimulatedVehicle> vehicles;

        public List<SimulatedVehicle> Vehicles
        {
            get { return vehicles; }
            set { vehicles = value; }
        }

        Evaluation evaluation;
        public Evaluation Evaluation
        {
            get { return evaluation; }
            set { evaluation = value; }
        }

        DateTime time;

        private BrakeActivity brakeAct;
        public BrakeActivity BrakeAct {
            get { return brakeAct; }
            set { brakeAct = value; }
        }

        private ReactActivity reactAct;
        public ReactActivity ReactAct {
            get { return reactAct; }
            set { reactAct = value; }
        }

        public ExperienceUnit()
        {
            time = DateTime.Now;
        }
    }
}
