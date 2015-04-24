using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Control.DataModel
{
    class SimulatedVehicle
    {
        float simulationTime;

        public float SimulationTime
        {
            get { return simulationTime; }
            set { simulationTime = value; }
        }
        float positionX;

        public float PositionX
        {
            get { return positionX; }
            set { positionX = value; }
        }
        float positionY;

        public float PositionY
        {
            get { return positionY; }
            set { positionY = value; }
        }
        float speed;

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        float acceleration;

        public float Acceleration
        {
            get { return acceleration; }
            set { acceleration = value; }
        }
        float driftAngle;

        public float DriftAngle
        {
            get { return driftAngle; }
            set { driftAngle = value; }
        }
        float offset;

        public float Offset
        {
            get { return offset; }
            set { offset = value; }
        }
        float wheelSpeed;

        public float WheelSpeed
        {
            get { return wheelSpeed; }
            set { wheelSpeed = value; }
        }
        float gasPedal;

        public float GasPedal
        {
            get { return gasPedal; }
            set { gasPedal = value; }
        }
        float brakePedal;

        public float BrakePedal
        {
            get { return brakePedal; }
            set { brakePedal = value; }
        }
        float gear;

        public float Gear
        {
            get { return gear; }
            set { gear = value; }
        }
        float rpm;

        public float Rpm
        {
            get { return rpm; }
            set { rpm = value; }
        }
    }
}
