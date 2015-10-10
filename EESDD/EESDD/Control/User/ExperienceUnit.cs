using EESDD.Control.DataModel;
using EESDD.Control.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            set { 
                vehicles = value;
                setTopAndBottom();
            }
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

        private SimulatedVehicle top;
        public SimulatedVehicle Top
        {
            get {
                if (top == null)
                {
                    setTopAndBottom();
                }
                return top; 
            }
        }

        private SimulatedVehicle bottom;
        public SimulatedVehicle Bottom
        {
            get {
                if (bottom == null)
                {
                    setTopAndBottom();
                }
                return bottom;
            }
        }

        public SimulatedVehicle Right
        {
            get { return this.vehicles[vehicles.Count-1]; }
        }

        public ExperienceUnit()
        {
            time = DateTime.Now;
        }

        private void setTopAndBottom() {
            top = new SimulatedVehicle();
            bottom = new SimulatedVehicle();
            foreach (SimulatedVehicle vehicle in vehicles)
            {
                foreach (PropertyInfo p in vehicle.GetType().GetProperties())
                {
                    if ((float)p.GetValue(vehicle) > (float)p.GetValue(top))
                    {
                        p.SetValue(top, p.GetValue(vehicle));
                    }

                    if ((float)p.GetValue(vehicle) < (float)p.GetValue(bottom))
                    {
                        p.SetValue(bottom, p.GetValue(vehicle));
                    }
                }
            }
        }
    }
}
