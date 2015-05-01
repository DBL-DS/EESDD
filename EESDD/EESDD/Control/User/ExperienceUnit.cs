using EESDD.Control.DataModel;
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

        internal List<SimulatedVehicle> Vehicles
        {
            get { return vehicles; }
            set { vehicles = value; }
        }
        Evaluation evaluation;

        internal Evaluation Evaluation
        {
            get { return evaluation; }
            set { evaluation = value; }
        }
        DateTime time;

        public ExperienceUnit()
        {
            time = DateTime.Now;
        }
    }
}
