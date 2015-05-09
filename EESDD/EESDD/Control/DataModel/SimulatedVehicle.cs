using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Control.DataModel
{
    [Serializable]
    class SimulatedVehicle
    {
        float simulationTime;
        float positionX;
        float positionY;
        float speed;
        float acceleration;
        float steeringWheel;
        float offset;
        float brakePedal;
        float totalDistance;
        float braking;
        float reacting;
        float gear;

        public float SimulationTime
        {
            get { return simulationTime; }
            set { simulationTime = value; }
        }

        public float PositionX
        {
            get { return positionX; }
            set { positionX = value; }
        }
        public float PositionY
        {
            get { return positionY; }
            set { positionY = value; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public float Acceleration
        {
            get { return acceleration; }
            set { acceleration = value; }
        }

        public float SteeringWheel
        {
            get { return steeringWheel; }
            set { steeringWheel = value; }
        }

        public float Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        public float BrakePedal
        {
            get { return brakePedal; }
            set { brakePedal = value; }
        }

        public float TotalDistance
        {
            get { return totalDistance; }
            set { totalDistance = value; }
        }
        

        public float Braking
        {
            get { return braking; }
            set { braking = value; }
        }

        public float Reacting
        {
            get { return reacting; }
            set { reacting = value; }
        }

        public float Gear
        {
            get { return gear; }
            set { gear = value; }
        }
    }
}
