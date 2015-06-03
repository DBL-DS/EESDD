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
        SignalController signalController;
        SignalGroup signalGroup;

        //需要获取的变量
        private double speed;

        private int laneInVissim;           //初始化时获取这个参数，init中现默认4
        private int lane;
        private double coord;
        private int link;
        private int lastLane;
        private bool dataIn;
        private int scene;
        private bool running;

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
            initData();
        }

        private void initRoot() {
            scene = PageList.Main.Selection.SceneSelect;
            switch (scene)
            {
                case UserSelections.ScenePractice:
                    vissimMapFilePath = System.IO.Directory.GetCurrentDirectory() + "\\vissim\\map\\2015-01-31.inp";
                    break;
                case UserSelections.SceneBrake:
                    vissimMapFilePath = System.IO.Directory.GetCurrentDirectory() + "\\vissim\\map\\wangheqiao.inp";
                    break;
                case UserSelections.SceneIntersection:
                    vissimMapFilePath = System.IO.Directory.GetCurrentDirectory() + "\\vissim\\map\\zuixinlukou.inp";
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
            signalState = 0;
            coord = 1;
            link = 1;
        }
        void initNet()
        {
            vissim = new Vissim();
            vissim.LoadNet(vissimMapFilePath);
            net = vissim.Net;
            simulation = vissim.Simulation;
            simulation.Resolution = 4;         
            
            if (net.SignalControllers.Count!=0)
            {
                signalController = net.SignalControllers.GetSignalControllerByNumber(1);
                signalGroup = signalController.SignalGroups.GetSignalGroupByNumber(1);
            }


            check();
            while ((vehicle = getVehicle(1)) == null)
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
            running = false;
            simulation.Stop();
            vissim.Exit();
        }
        public void Run()
        {
            running = true;
            while (count!=-1 && running)
            {
                if (dataIn && getVehicle(1) != null)
                {
                    vehicle.set_AttValue("SPEED", speed);
                    //vehicle.set_AttValue("LANE", lane);
                    if (link != 2 && scene == UserSelections.SceneBrake)
                    {
                        vehicle.MoveToLinkCoordinate(link,1,coord);
                    }
                    vehicle.set_AttValue("DESIREDSPEED", -1000);

                    RunSingle();

                }

                if (signalState != 0 && net.SignalControllers.Count != 0)
                {

                    setSignal(signalState);
                }   
                Thread.Sleep(250);
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
            if (vehicle != null)
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
                if (vehicle.PositionX>1206.141 && vehicle.PositionX <= 1736.125)
                {
                    link = 1;
                    coord = 529.984-(vehicle.PositionX - 1206.720);
                }
                if (vehicle.PositionX > 1190.628 && vehicle.PositionX <= 1206.141)
                {
                    link = 10001;
                    coord = 15.00 - Math.Abs(vehicle.PositionX - 1190.628);
                }
                if (vehicle.PositionX > 1151.405 && vehicle.PositionX <= 1190.628)
                {
                    link = 3;
                    coord = 39.323 - Math.Abs(vehicle.PositionX - 1151.405);
                }
                if (vehicle.PositionX < 1151.405)
                {
                    link = 2;
                }
                signalState = (int)vehicle.TrafficLight;
                    
            }                       
        }

        private void setSignal(int state)
        {
            signalGroup.set_AttValue("STATE", state);

        }
    }
}
