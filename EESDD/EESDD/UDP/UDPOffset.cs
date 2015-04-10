using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.UDP
{
    /// <summary>
    /// 本类中设置数据在UDP数据块中的偏移量
    /// </summary>
    class UDPOffset
    {
        public static int SimulationTime = 124;
        public static int PositionX = 128;
        public static int PositionY = 132;
        public static int PositionZ = 136;
        public static int Speed = 140;
        public static int Acceleration = 144;
        public static int DriftAngle = 148;
        public static int Offset = 152;
        public static int SteeringWheel = 156;
        public static int RotationalSpeed = 160;
        public static int GasPedal = 164;
        public static int BrakePedal = 168;
        public static int ClutchPedal = 172;
        public static int Gear = 176;
        public static int Rpm = 180;
    }
}
