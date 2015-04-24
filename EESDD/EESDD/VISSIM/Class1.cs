using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VISSIM_COMSERVERLib;
namespace EESDDTEST.VISSIM
{
    class Class1
    {
        Vissim vis = new Vissim();
        void main()
        {
            vis.LoadNet();
            Simulation sim = vis.Simulation;
            Net ne = vis.Net;
            Vehicles ve = ne.Vehicles;
            Vehicle vesingle = ve.GetVehicleByNumber(101);
            vesingle.set_AttValue("Speed","101");
            vesingle.MoveToLinkCoordinate(1,1,1);
        }
    }
}
