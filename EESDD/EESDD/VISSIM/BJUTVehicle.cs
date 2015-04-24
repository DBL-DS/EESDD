using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VISSIM_COMSERVERLib;
namespace EESDDTEST.VISSIM
{
    class BJUTVehicle
    {
        private Vehicle vehicle;

        public Vehicle Vehicle
        {
          get { return vehicle; }
          set { vehicle = value; }
        }

        public BJUTVehicle(Vehicle inVehicle)
        {
            vehicle = inVehicle;
        }

        public void setSpeed(double needSpeed)
        {
            vehicle.set_AttValue("Speed",needSpeed);
            vehicle.set_AttValue("DESIREDSPEED",-1000);
        }

        public void setLane(int needLane)
        {
            vehicle.set_AttValue("Lane",needLane);
        }
        public void checkAttValue(String Att)
        {
            Console.WriteLine(vehicle.get_AttValue(Att));
        }
      
    }
}
