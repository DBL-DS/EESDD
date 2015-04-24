using EESDD.Control.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Control.User
{
    class ExperienceUnit
    {
        int sceneID;
        int mode;
        List<SimulatedVehicle> vehicles;
        Evaluation evaluation;
        DateTime time;
    }
}
