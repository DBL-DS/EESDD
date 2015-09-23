using EESDD.Control.DataModel;
using EESDD.Control.Operation;
using EESDD.VISSIM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Control.User
{
    [Serializable]
    class Evaluation
    {
        private ExperienceUnit unit;
        public Evaluation(ExperienceUnit unit) {
            this.unit = unit;
            if (unit.Vehicles != null && unit.Vehicles.Count != 0) {
                this.cacuMean();
                this.cacuStdError();
            }
            if (unit.SceneID != UserSelections.SceneLaneChange)
                setDataFromVISSIM();
        }

        //**针对场景中所有车辆（VISSIM数据）
        // 平均延误
        private float averageDelay;
        public float AverageDelay
        {
            get { return averageDelay; }
            set { averageDelay = value; }
        }
        // 平均排队长度
        private float averageQueueLength;
        public float AverageQueueLength { get { return averageQueueLength; } set { averageQueueLength = value; } }
        // 平均速度
        private float averageSpeed;
        public float AverageSpeed { get { return averageSpeed; } set { averageSpeed = value; } }

        //**均值与标准差
        // 速度
        private float meanSpeed;
        public float MeanSpeed { get { return meanSpeed; } set { meanSpeed = value; } }

        private float stdErrorSpeed;
        public float StdErrorSpeed { get { return stdErrorSpeed; } set { stdErrorSpeed = value; } }
        // 加速度
        private float meanAcc;
        public float MeanAcc { get { return meanAcc; } set { meanAcc = value; } }

        private float stdErrorAcc;
        public float StdErrorAcc { get { return stdErrorAcc; } set { stdErrorAcc = value; } }
        // 方向盘转角
        private float meanSteeringWheel;
        public float MeanSteeringWheel { get { return meanSteeringWheel; } set { meanSteeringWheel = value; } }

        private float stdErrorSteeringWheel;
        public float StdErrorSteeringWheel { get { return stdErrorSteeringWheel; } set { stdErrorSteeringWheel = value; } }
        // 偏离道路中心线距离
        private float meanOffset;
        public float MeanOffset { get { return meanOffset; } set { meanOffset = value; } }

        private float stdErrorOffset;
        public float StdErrorOffset { get { return stdErrorOffset; } set { stdErrorOffset = value; } }
        // 与前车距离
        private float meanDistanceToNext;
        public float MeanDistanceToNext { get { return meanDistanceToNext; } set { meanDistanceToNext = value; } }

        private float stdErrorDistanceToNext;
        public float StdErrorDistanceToNext { get { return stdErrorDistanceToNext; } set { stdErrorDistanceToNext = value; } }

        //**特殊观察量（依赖于场景）
        // 反应时
        private float reactTime;
        public float ReactTime { get { return reactTime; } set { reactTime = value; } }
        // 刹车距离
        private float brakeDistance;
        public float BrakeDistance { get { return brakeDistance; } set { brakeDistance = value; } }

        private void initData() {
            this.meanSpeed = this.meanAcc = this.meanOffset 
                = this.meanSteeringWheel = this.meanDistanceToNext = 0;
            this.stdErrorSpeed = this.stdErrorAcc = this.stdErrorOffset
                = this.stdErrorSteeringWheel = this.stdErrorDistanceToNext = 0;
            averageDelay = 0;
            averageQueueLength = 0;
            averageSpeed = 0;
            reactTime = 0;
            brakeDistance = 0;
        }
        private void cacuMean() {
            int count = unit.Vehicles.Count;
            foreach (SimulatedVehicle item in unit.Vehicles)
            {
                meanSpeed           += item.Speed;
                meanAcc             += item.Acceleration;
                meanOffset          += item.Offset;
                meanSteeringWheel   += item.SteeringWheel;
                meanDistanceToNext  += item.DistanceToNext;
            }
            meanSpeed           /= count;
            meanAcc             /= count;
            meanOffset          /= count;
            meanSteeringWheel   /= count;
            meanDistanceToNext  /= count;
        }

        private void cacuStdError() {
            int count = unit.Vehicles.Count;
            foreach (SimulatedVehicle item in unit.Vehicles)
            {
                stdErrorSpeed           += (float)System.Math.Pow(item.Speed - meanSpeed, 2);
                stdErrorAcc             += (float)System.Math.Pow(item.Acceleration - meanAcc, 2);
                stdErrorOffset          += (float)System.Math.Pow(item.Offset - meanOffset, 2);
                stdErrorSteeringWheel   += (float)System.Math.Pow(item.SteeringWheel- meanSteeringWheel, 2);
                stdErrorDistanceToNext  += (float)System.Math.Pow(item.DistanceToNext - meanDistanceToNext, 2);
            }
            stdErrorSpeed /= count;
            stdErrorAcc /= count;
            stdErrorOffset /= count;
            stdErrorSteeringWheel /= count;
            stdErrorDistanceToNext /= count;
        }

        private void setDataFromVISSIM() {
            VissimDB vDB = new VissimDB();

            averageDelay = (float)vDB.getAvgDelayTime();
            if (unit.SceneID == UserSelections.SceneIntersection)
                averageQueueLength = (float)vDB.getAvgQueueLengh();
            averageSpeed = (float)vDB.getAvgSpeed();

            vDB.close();
        }
        private void setSpecialData() {
            if (unit.BrakeAct != null)
                this.brakeDistance = unit.BrakeAct.BrakeDistance;
            if (unit.ReactAct != null)
                this.reactTime = unit.ReactAct.ReactTime;
        }
    }
}
