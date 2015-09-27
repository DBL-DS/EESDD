using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Control.Player
{
    [Serializable]
    class BrakeActivity
    {
        float brakeDistance;
        float brakeStart;
        float brakeEnd;

        public BrakeActivity()
        {
            brakeDistance = 0;
            brakeStart = 0;
            brakeEnd = 0;
        }

        public float BrakeDistance
        {
            get { return brakeDistance; }
            set { brakeDistance = value; }
        }

        public float BrakeStart
        {
            get { return brakeStart; }
            set { brakeStart = value; }
        }

        public float BrakeEnd
        {
            get { return brakeEnd; }
            set { brakeEnd = value; }
        }
    }
}
