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
        private int sceneID;
        private int mode;
        private List<SimulatedVehicle> vehicles;
        private Evaluation evaluation;
        private DateTime time;
        private BrakeActivity brakeAct;
        private ReactActivity reactAct;
        private SimulatedVehicle top;
        private SimulatedVehicle bottom;
        private List<DistractMark> marks;
        private float accident;

        public float Accident
        {
            get { return accident; }
            set { accident = value; }
        }

        public int SceneID
        {
            get { return sceneID; }
            set { sceneID = value; }
        }

        public int Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        public List<SimulatedVehicle> Vehicles
        {
            get { return vehicles; }
            set { 
                vehicles = value;
                setTopAndBottom();
                scanInclusion();
            }
        }

        public Evaluation Evaluation
        {
            get { return evaluation; }
            set { evaluation = value; }
        }


        public BrakeActivity BrakeAct {
            get { return brakeAct; }
            set { brakeAct = value; }
        }

        public ReactActivity ReactAct {
            get { return reactAct; }
            set { reactAct = value; }
        }

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

        public List<DistractMark> Marks
        {
            get
            {
                if (marks == null)
                    setMarks();
                return marks;
            }
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
        public void scanInclusion()
        {
            if (vehicles == null || vehicles.Count == 0)
                return;

            // clear possible inclusion due to the feature of Simulator
            float firstTime = vehicles[0].SimulationTime;
            int count = 0;
            foreach (SimulatedVehicle vehicle in vehicles)
            {
                if (vehicle.SimulationTime < firstTime)
                {
                    vehicles.RemoveRange(0, count);
                    break;
                }
                ++count;
            }
        }

        private void setMarks()
        {
            marks = new List<DistractMark>();

            float normalMark = 0;
            float equal = 0.00001f;
            for (int i = 0; i < vehicles.Count; i++)
            {
                if (Math.Abs(vehicles[i].Area - normalMark) >= equal)
                {
                    DistractMark mark = new DistractMark();
                    mark.Start = vehicles[i];

                    mark.Top = new SimulatedVehicle();
                    mark.Bottom = new SimulatedVehicle();
                    while (++i < vehicles.Count - 1 && Math.Abs(vehicles[i].Area - normalMark) >= equal)
                    {
                        SimulatedVehicle vehicle = vehicles[i];
                        foreach (PropertyInfo p in vehicle.GetType().GetProperties())
                        {
                            if ((float)p.GetValue(vehicle) > (float)p.GetValue(mark.Top))
                            {
                                p.SetValue(mark.Top, p.GetValue(vehicle));
                            }

                            if ((float)p.GetValue(vehicle) < (float)p.GetValue(mark.Bottom))
                            {
                                p.SetValue(mark.Bottom, p.GetValue(vehicle));
                            }
                        }
                    }

                    mark.End = vehicles[i];

                    marks.Add(mark);
                }
            }
        }
    }

    [Serializable]
    class DistractMark
    {
        SimulatedVehicle start;
        SimulatedVehicle end;
        SimulatedVehicle top;
        SimulatedVehicle bottom;

        public SimulatedVehicle Start { get { return start; } set { start = value; } }
        public SimulatedVehicle End { get { return end; } set { end = value; } }
        public SimulatedVehicle Top { get { return top; } set { top = value; } }
        public SimulatedVehicle Bottom { get { return bottom; } set { bottom = value; } }
    }
}
