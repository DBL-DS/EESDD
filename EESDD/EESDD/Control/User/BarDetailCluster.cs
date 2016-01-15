using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Control.User
{
    class BarDetailCluster
    {
        public static BarDetail AverageDelay = new BarDetail("AverageDelay", "平均延误（车流）");
        public static BarDetail AverageQueueLength = new BarDetail("AverageQueueLength", "平均排队长度（车流）");
        public static BarDetail AverageSpeed = new BarDetail("AverageSpeed", "平均车速（车流）");

        public static BarDetail MeanSpeed = new BarDetail("MeanSpeed", "车速均值");
        public static BarDetail VarianceSpeed = new BarDetail("VarianceSpeed", "车速标准差");
        public static BarDetail MeanAcc = new BarDetail("MeanAcc", "加速度均值");
        public static BarDetail VarianceAcc = new BarDetail("VarianceAcc", "加速度标准差");
        public static BarDetail MeanSteeringWheel = new BarDetail("MeanSteeringWheel", "方向盘转角均值");
        public static BarDetail VarianceSteeringWheel = new BarDetail("VarianceSteeringWheel", "方向盘转角标准差");
        public static BarDetail MeanOffset = new BarDetail("MeanOffset", "偏离道路中心线距离均值");
        public static BarDetail VarianceOffset = new BarDetail("VarianceOffset", "偏离道路中心线距离标准差");
        public static BarDetail MeanDistanceToNext = new BarDetail("MeanDistanceToNext", "跟驰距离均值");
        public static BarDetail VarianceDistanceToNext = new BarDetail("VarianceDistanceToNext", "跟驰距离标准差");

        public static BarDetail ReactTime = new BarDetail("ReactTime", "反应时");
        public static BarDetail BrakeDistance = new BarDetail("BrakeDistance", "刹车距离");


        public static BarDetail MeanSpeedEx = new BarDetail("MeanSpeedEx", "刹车阶段-车速均值");
        public static BarDetail VarianceSpeedEx = new BarDetail("VarianceSpeedEx", "刹车阶段-车速标准差");
        public static BarDetail MeanAccEx = new BarDetail("MeanAccEx", "刹车阶段-加速度均值");
        public static BarDetail VarianceAccEx = new BarDetail("VarianceAccEx", "刹车阶段-加速度标准差");
        public static BarDetail MeanSteeringWheelEx = new BarDetail("MeanSteeringWheelEx", "刹车阶段-方向盘转角均值");
        public static BarDetail VarianceSteeringWheelEx = new BarDetail("VarianceSteeringWheelEx", "刹车阶段-方向盘转角标准差");
        public static BarDetail MeanOffsetEx = new BarDetail("MeanOffsetEx", "刹车阶段-偏离道路中心线距离均值");
        public static BarDetail VarianceOffsetEx = new BarDetail("VarianceOffsetEx", "刹车阶段-偏离道路中心线距离标准差");
        public static BarDetail MeanDistanceToNextEx = new BarDetail("MeanDistanceToNextEx", "刹车阶段-跟驰距离均值");
        public static BarDetail VarianceDistanceToNextEx = new BarDetail("VarianceDistanceToNextEx", "刹车阶段-跟驰距离标准差");

    }
}
