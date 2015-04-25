//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using VISSIM_COMSERVERLib;
//using EESDDTEST.VISSIM;
//namespace EESDDTEST.VISSIM
//{
//    class BJUTVissim
//    {
//        private Vissim vissim;
//        private Net net;
//        private DataVehicle data;
//        private bool over;


//        public bool Over
//        {
//            get { return over; }
//            set { over = value; }
//        }
//        public DataVehicle Data
//        {
//            get { return data; }
//            set { data = value; }
//        }
//        private Vehicle vehicle;

//        public Vehicle Vehicle
//        {
//            get { return vehicle; }
//            set { vehicle = value; }
//        }

       
       
//        public Net Net
//        {
//            get { return net; }
//            set { net = value; }
//        }
//        private Simulation simulation;

//        public Simulation Simulation
//        {
//            get { return simulation; }
//            set { simulation = value; }
//        }
//        public Vissim Vissim
//        {
//            get { return vissim; }
//            set { vissim = value; }
//        }
//        public BJUTVissim(String path)
//        {
//            over = false;
//            initNet(path);
//        }

//        void initNet(String path)
//        {
//            vissim.LoadNet(path);
//            net = vissim.Net;
//            simulation = vissim.Simulation;
//        }
        
//        public Simulation getSimulation()
//        {
//            return vissim.Simulation;
//        }
//        public Vehicle getVehicle(int i)
//        {
//            Vehicles vehicles = net.Vehicles;
//            return vehicles.GetVehicleByNumber(i);
//        }
//        public void RunSingle()
//        {
//            simulation.RunSingleStep();
//        }
//        public void Stop()
//        {
//            simulation.Stop();
//            vissim.Exit();
//        }
//        public void Run()
//        {
//            while((vehicle = getVehicle(3)) == null)
//                RunSingle();
//            while ((vehicle = getVehicle(3)) != null && over==false)
//            {
//                vehicle.set_AttValue("SPEED", 0);
//                vehicle.set_AttValue("LANE", 1);
//                vehicle.set_AttValue("DESIREDSPEED", -1000);
//                Console.WriteLine(vehicle.get_AttValue("LENGTH"));
//                RunSingle();
//            }
//            Stop();
//        }
//    }
//}
