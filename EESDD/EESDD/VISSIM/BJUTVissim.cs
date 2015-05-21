using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VISSIM_COMSERVERLib;
using EESDD.VISSIM;
using EESDD.Control.DataModel;
using EESDD.Control.Operation;

namespace EESDD.VISSIM
{
    class BJUTVissim
    {

        //系统所需控件
        private Vissim vissim;
        private Net net;
        private DataVehicle data;
        private bool over;
        SignalHead head;
        SignalController signalcontroller;
        SignalGroup signalgroup;

        //需要获取的变量
        private double speed;

        private int laneInVissim;           //初始化时获取这个参数，init中现默认4
        private int lane;
        private int lastLane;
        private bool dataIn;

        //路灯1为红色，3为绿灯，4为黄灯
        private int signalState;


        //计时器
        private double count = 0;

        private string vissimMapFilePath;

        public double Count
        {
            get { return count; }
            set { count = value; }
        }

        public int SignalState
        {
            get { return signalState; }
            set { signalState = value; }
        }

        
        
        public double Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public int Lane
        {
            get { return lane; }
            set { lane = value; }
        }
        public bool Over
        {
            get { return over; }
            set { over = value; }
        }
        public DataVehicle Data
        {
            get { return data; }
            set { data = value; }
        }
        private Vehicle vehicle;

        public Vehicle Vehicle
        {
            get { return vehicle; }
            set { vehicle = value; }
        }
         
        private Simulation simulation;

        public Simulation Simulation
        {
            get { return simulation; }
            set { simulation = value; }
        }
        public Vissim Vissim
        {
            get { return vissim; }
            set { vissim = value; }
        }
        public BJUTVissim()
        {
            over = false;
            initRoot();
            initNet();
        }

        private void initRoot() {
            int scene = PageList.Main.Selection.SceneSelect;
            switch (scene)
            {
                case UserSelections.ScenePractice:
                    vissimMapFilePath = System.IO.Directory.GetCurrentDirectory() + "\\vissim\\map\\2015-01-31.inp";
                    break;
                case UserSelections.SceneBrake:
                    vissimMapFilePath = System.IO.Directory.GetCurrentDirectory() + "\\vissim\\map\\2015-01-31.inp";
                    break;
                case UserSelections.SceneIntersection:
                    vissimMapFilePath = System.IO.Directory.GetCurrentDirectory() + "\\vissim\\map\\2015-01-31.inp";
                    break;
            }
        }

        private void initData()
        {
            speed = 0;
            lane = 1;
            dataIn = false;
            lastLane = 1;
            laneInVissim = 4;
        }
        void initNet()
        {
            vissim = new Vissim();
            vissim.LoadNet(vissimMapFilePath);
            net = vissim.Net;
            simulation = vissim.Simulation;
            //signalcontroller = net.SignalControllers.GetSignalControllerByNumber(5);
            //signalgroup = signalcontroller.SignalGroups.GetSignalGroupByNumber(6);
            //head = signalgroup.SignalHeads.GetSignalHeadByNumber(9);
            check();
            while ((vehicle = getVehicle(3)) == null)
            {
                RunSingle();
            }
        }
        
        public Simulation getSimulation()
        {
            return vissim.Simulation;
        }
        public Vehicle getVehicle(int i)
        {
            Vehicles vehicles = net.Vehicles;
            return vehicles.GetVehicleByNumber(i);
        }
        public void RunSingle()
        {
            if (count < simulation.Period * simulation.Resolution)
            {
                simulation.RunSingleStep();
                count++;
            }
            else 
            {
                Stop();
                count = -1;
            }
            
        }
        public void Stop()
        {
            simulation.Stop();
            vissim.Exit();
        }
        public void Run()
        {
            //signalgroup.set_AttValue("State", 1);
            while (/*(vehicle = getVehicle(3)) != null && */count!=-1 && dataIn)
            {
                vehicle.set_AttValue("SPEED", speed);
                vehicle.set_AttValue("LANE", lane);
                vehicle.set_AttValue("DESIREDSPEED", -1000);
                //Console.WriteLine(vehicle.get_AttValue("LENGTH"));
                RunSingle();
            }
            
        }
        public void check()
        {
            Evaluation eval = vissim.Evaluation;
            eval.set_AttValue("DELAY", true);
            eval.set_AttValue("QUEUECOUNTER", true);
            eval.set_AttValue("VEHICLERECORD", true);
            //eval.DelayEvaluation.set_AttValue("FILE", true);
        }

        public void set(SimulatedVehicle vehicle)
        {
            if (!dataIn)
            {
                lastLane = (int)vehicle.Lane;
            }
            else
            {
                int laneToChange = (int)vehicle.Lane - lastLane + lane;
                lane = laneToChange <= laneInVissim && laneToChange > 0 ? laneToChange : lane;
                lastLane = (int)vehicle.Lane;
            }

            dataIn = true;
            speed = vehicle.Speed;
            
        }
    }
}
