using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDDTEST.VISSIM
{
    class DataVehicle
    {
        private double speed;

        public double Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        private int lane;

        public int Lane
        {
            get { return lane; }
            set { lane = value; }
        }
        const double desirSpeed = -1000.0;

        public double DesirSpeed
        {
            get { return desirSpeed; }
        } 

    }
}
