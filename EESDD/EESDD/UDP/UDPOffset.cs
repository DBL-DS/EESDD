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
        public static int Speed = 136;
        public static int Acceleration = 140;
        public static int SteeringWheel = 144;
        public static int Offset = 148;
        public static int BrakePedal = 152;
        public static int TotalDistance = 156;
        public static int Braking = 160;
        public static int Reacting = 164;
        public static int Area = 168;
        public static int DistanceToNext = 172;
        public static int Lane = 176;
        public static int TrafficLight = 180;
    }
}
