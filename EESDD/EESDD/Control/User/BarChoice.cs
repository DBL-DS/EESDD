using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Control.User
{
    class BarChoice
    {
        public static List<BarDetail> ScenePractice {
            get { return new List<BarDetail>() { }; }
        }
            
        public static List<BarDetail> SceneBrake {
            get { return new List<BarDetail>() { BarDetailCluster.BrakeDistance, BarDetailCluster.ReactTime.ChangeTitle("应对前车刹车反应时（ms）") };}
        }
        public static List<BarDetail> SceneLaneChange {
            get { return new List<BarDetail>() { BarDetailCluster.BrakeDistance, BarDetailCluster.ReactTime.ChangeTitle("应对车辆并线反应时（ms）") }; }
        }
        public static List<BarDetail> SceneIntersection {
            get { return new List<BarDetail>() { BarDetailCluster.AverageQueueLength, BarDetailCluster.ReactTime.ChangeTitle("应对红绿灯变绿反应时（ms）") }; }
        }

        public static List<BarDetail> SceneNavigator
        {
            get { return new List<BarDetail>() { }; }
        }

    }
}
