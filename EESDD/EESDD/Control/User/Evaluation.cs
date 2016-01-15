﻿using EESDD.Control.DataModel;
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
        protected ExperienceUnit unit;
        public Evaluation(ExperienceUnit unit) {
            this.unit = unit;
            if (unit.Vehicles != null && unit.Vehicles.Count != 0) {
                this.cacuMean();
                this.cacuVariance();
            }
            if (unit.SceneID != UserSelections.SceneLaneChange)
                setDataFromVISSIM();
            setSpecialData();
        }

        //**针对场景中所有车辆（VISSIM数据）
        // 平均延误
        protected float averageDelay;
        public float AverageDelay
        {
            get { return averageDelay; }
            set { averageDelay = value; }
        }
        // 平均排队长度
        protected float averageQueueLength;
        public float AverageQueueLength { get { return averageQueueLength; } set { averageQueueLength = value; } }
        // 平均速度
        protected float averageSpeed;   
        public float AverageSpeed { get { return averageSpeed; } set { averageSpeed = value; } }

        //**均值与标准差
        // 速度
        protected float meanSpeed;
        public float MeanSpeed { get { return meanSpeed; } set { meanSpeed = value; } }

        protected float varianceSpeed;
        public float VarianceSpeed { get { return varianceSpeed; } set { varianceSpeed = value; } }
        // 加速度
        protected float meanAcc;
        public float MeanAcc { get { return meanAcc; } set { meanAcc = value; } }

        protected float varianceAcc;
        public float VarianceAcc { get { return varianceAcc; } set { varianceAcc = value; } }
        // 方向盘转角
        protected float meanSteeringWheel;
        public float MeanSteeringWheel { get { return meanSteeringWheel; } set { meanSteeringWheel = value; } }

        protected float varianceSteeringWheel;
        public float VarianceSteeringWheel { get { return varianceSteeringWheel; } set { varianceSteeringWheel = value; } }
        // 偏离道路中心线距离
        protected float meanOffset;
        public float MeanOffset { get { return meanOffset; } set { meanOffset = value; } }

        protected float varianceOffset;
        public float VarianceOffset { get { return varianceOffset; } set { varianceOffset = value; } }
        // 与前车距离
        protected float meanDistanceToNext;
        public float MeanDistanceToNext { get { return meanDistanceToNext; } set { meanDistanceToNext = value; } }

        protected float varianceDistanceToNext;
        public float VarianceDistanceToNext { get { return varianceDistanceToNext; } set { varianceDistanceToNext = value; } }

        //**特殊观察量（依赖于场景）
        // 反应时
        protected float reactTime;
        public float ReactTime { get { return reactTime; } set { reactTime = value; } }
        // 刹车距离
        protected float brakeDistance;
        public float BrakeDistance { get { return brakeDistance; } set { brakeDistance = value; } }
        //**均值与标准差
        // 速度
        protected float meanSpeedEx;
        public float MeanSpeedEx { get { return meanSpeedEx; } set { meanSpeedEx = value; } }

        protected float varianceSpeedEx;
        public float VarianceSpeedEx { get { return varianceSpeedEx; } set { varianceSpeedEx = value; } }
        // 加速度
        protected float meanAccEx;
        public float MeanAccEx { get { return meanAccEx; } set { meanAccEx = value; } }

        protected float varianceAccEx;
        public float VarianceAccEx { get { return varianceAccEx; } set { varianceAccEx = value; } }
        // 方向盘转角
        protected float meanSteeringWheelEx;
        public float MeanSteeringWheelEx { get { return meanSteeringWheelEx; } set { meanSteeringWheelEx = value; } }

        protected float varianceSteeringWheelEx;
        public float VarianceSteeringWheelEx { get { return varianceSteeringWheelEx; } set { varianceSteeringWheelEx = value; } }
        // 偏离道路中心线距离
        protected float meanOffsetEx;
        public float MeanOffsetEx { get { return meanOffsetEx; } set { meanOffsetEx = value; } }

        protected float varianceOffsetEx;
        public float VarianceOffsetEx { get { return varianceOffsetEx; } set { varianceOffsetEx = value; } }
        // 与前车距离
        protected float meanDistanceToNextEx;
        public float MeanDistanceToNextEx { get { return meanDistanceToNextEx; } set { meanDistanceToNextEx = value; } }

        protected float varianceDistanceToNextEx;
        public float VarianceDistanceToNextEx { get { return varianceDistanceToNextEx; } set { varianceDistanceToNextEx = value; } }
        protected void initData() {
            this.meanSpeed = this.meanAcc = this.meanOffset 
                = this.meanSteeringWheel = this.meanDistanceToNext = 0;
            this.varianceSpeed = this.varianceAcc = this.varianceOffset
                = this.varianceSteeringWheel = this.varianceDistanceToNext = 0;
            this.meanSpeedEx = this.meanAccEx = this.meanOffsetEx
                = this.meanSteeringWheelEx = this.meanDistanceToNextEx = 0;
            this.varianceSpeedEx = this.varianceAccEx = this.varianceOffsetEx
                = this.varianceSteeringWheelEx = this.varianceDistanceToNextEx = 0;
            averageDelay = 0;
            averageQueueLength = 0;
            averageSpeed = 0;
            reactTime = 0;
            brakeDistance = 0;
        }
        protected void cacuMean()
        {
            int count = 0, countEx = 0;
            foreach (SimulatedVehicle item in unit.Vehicles)
            {
                if (item.Area == 1)
                {
                    meanSpeed += item.Speed;
                    meanAcc += item.Acceleration;
                    meanOffset += item.Offset;
                    meanSteeringWheel += item.SteeringWheel;
                    meanDistanceToNext += item.DistanceToNext;
                    count++;
                }
                else if (item.Area == 2)
                {
                    meanSpeedEx += item.Speed;
                    meanAccEx += item.Acceleration;
                    meanOffsetEx += item.Offset;
                    meanSteeringWheelEx += item.SteeringWheel;
                    meanDistanceToNextEx += item.DistanceToNext;
                    countEx++;
                }
            }

            if (count != 0)
            {
                meanSpeed /= count;
                meanAcc /= count;
                meanOffset /= count;
                meanSteeringWheel /= count;
                meanDistanceToNext /= count;
            }
            if (countEx != 0)
            {
                meanSpeedEx /= countEx;
                meanAccEx /= countEx;
                meanOffsetEx /= countEx;
                meanSteeringWheelEx /= countEx;
                meanDistanceToNextEx /= countEx;
            }
        }

        protected void cacuVariance()
        {
            int count = 0, countEx = 0;

            foreach (SimulatedVehicle item in unit.Vehicles)
            {
                if (item.Area == 1)
                {
                    varianceSpeed += (float)System.Math.Pow(item.Speed - meanSpeed, 2);
                    varianceAcc += (float)System.Math.Pow(item.Acceleration - meanAcc, 2);
                    varianceOffset += (float)System.Math.Pow(item.Offset - meanOffset, 2);
                    varianceSteeringWheel += (float)System.Math.Pow(item.SteeringWheel - meanSteeringWheel, 2);
                    varianceDistanceToNext += (float)System.Math.Pow(item.DistanceToNext - meanDistanceToNext, 2);
                    count++;
                }
                else if (item.Area == 2)
                {
                    varianceSpeedEx += (float)System.Math.Pow(item.Speed - meanSpeed, 2);
                    varianceAccEx += (float)System.Math.Pow(item.Acceleration - meanAcc, 2);
                    varianceOffsetEx += (float)System.Math.Pow(item.Offset - meanOffset, 2);
                    varianceSteeringWheelEx += (float)System.Math.Pow(item.SteeringWheel - meanSteeringWheel, 2);
                    varianceDistanceToNextEx += (float)System.Math.Pow(item.DistanceToNext - meanDistanceToNext, 2);
                    countEx++;
                }
            }

            if (count != 0)
            {
                varianceSpeed /= count;
                varianceAcc /= count;
                varianceOffset /= count;
                varianceSteeringWheel /= count;
                varianceDistanceToNext /= count;
            }
            if (countEx != 0)
            {
                varianceSpeedEx /= count;
                varianceAccEx /= count;
                varianceOffsetEx /= count;
                varianceSteeringWheelEx /= count;
                varianceDistanceToNextEx /= count;
            }
        }

        protected void setDataFromVISSIM() {
            VissimDB vDB = new VissimDB();

            averageDelay = (float)vDB.getAvgDelayTime();
            if (unit.SceneID == UserSelections.SceneIntersection)
                averageQueueLength = (float)vDB.getAvgQueueLengh();
            averageSpeed = (float)vDB.getAvgSpeed();

            vDB.close();
        }
        protected void setSpecialData() {
            if (unit.BrakeAct != null)
                this.brakeDistance = unit.BrakeAct.BrakeDistance;
            if (unit.ReactAct != null)
                this.reactTime = unit.ReactAct.ReactTime;
        }
    }
}
