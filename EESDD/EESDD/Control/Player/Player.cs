using EESDD.Control.DataModel;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace EESDD.Control.Player
{
     class Player
    {
         SimulatedVehicle currentVehicle;
         List<SimulatedVehicle> vehicles;
         ObservableDataSource<Point> speed;
         ObservableDataSource<Point> coordinate;
         ObservableDataSource<Point> offset;
         ObservableDataSource<Point> steeringWheel;
         ObservableDataSource<Point> accelerate;
         ObservableDataSource<Point> brake;

         BrakeActivity brakeActivity;
         ReactActivity reactActivity;

         bool braking;
         bool reacting;

         public Player() {
             init();
         }

         void init()
         {
             vehicles = new List<SimulatedVehicle>();
             speed = new ObservableDataSource<Point>();
             coordinate = new ObservableDataSource<Point>();
             offset = new ObservableDataSource<Point>();
             steeringWheel = new ObservableDataSource<Point>();
             accelerate = new ObservableDataSource<Point>();
             brake = new ObservableDataSource<Point>();
             brakeActivity = new BrakeActivity();
             reactActivity = new ReactActivity();

             braking = false;
             reacting = false;
         }
        
         public void reset() {
             currentVehicle = null;
             vehicles = null;
             coordinate = null;
             offset = null;
             steeringWheel = null;
             accelerate = null;
             brake = null;
             brakeActivity = null;
             reactActivity = null;

             init();
        }

         public void play(SimulatedVehicle vehicle) {
             if (vehicle != null && vehicle.SimulationTime > 0.000001)
             {
                 //if (currentVehicle == null && vehicle.SimulationTime > 1)
                 //    return;

                 Dispatcher dispatcher = PageList.Main.Dispatcher;
                 currentVehicle = vehicle;
                 vehicles.Add(vehicle);
                 speed.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Speed));
                 coordinate.AppendAsync(dispatcher, new Point(vehicle.PositionX, vehicle.PositionY));
                 offset.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Offset));
                 steeringWheel.AppendAsync(dispatcher, new Point(vehicle.SimulationTime,vehicle.SteeringWheel));
                 accelerate.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Acceleration));
                 brake.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.BrakePedal));

                 setBrake(vehicle);
                 setReact(vehicle);
             }
         }

         private void setBrake(SimulatedVehicle vehicle)
         {
             if (vehicle.Braking > 0.000001)
             {
                 if (!braking)
                 {
                     braking = true;
                     brakeActivity.BrakeStart = vehicle.TotalDistance;
                 }
                 brakeActivity.BrakeDistance = vehicle.TotalDistance - brakeActivity.BrakeStart;
             }
             else
             {
                 if (braking)
                 {
                     braking = false;
                     brakeActivity.BrakeEnd = vehicle.TotalDistance;
                     brakeActivity.BrakeDistance = vehicle.TotalDistance - brakeActivity.BrakeStart;
                 }
             }
         }

         private void setReact(SimulatedVehicle vehicle)
         {
             if (vehicle.Reacting > 0.000001)
             {
                 if (!reacting)
                 {
                     reacting = true;
                     reactActivity.TimeStart = vehicle.SimulationTime;                     
                 }
                 reactActivity.ReactTime = vehicle.SimulationTime - reactActivity.TimeStart;
             }
             else
             {
                 if (reacting)
                 {
                     reacting = false;
                     reactActivity.TimeEnd = vehicle.SimulationTime;
                     reactActivity.ReactTime = vehicle.SimulationTime - reactActivity.TimeStart;
                 }
             }
         }

         internal SimulatedVehicle CurrentVehicle
         {
             get { return currentVehicle; }
             set { currentVehicle = value; }
         }


         internal List<SimulatedVehicle> Vehicles
         {
             get { return vehicles; }
             set { vehicles = value; }
         }

         public ObservableDataSource<Point> SpeedPoints
         {
             get { return speed; }
             set { speed = value; }
         }


         public ObservableDataSource<Point> CoordinatePoints
         {
             get { return coordinate; }
             set { coordinate = value; }
         }

         public ObservableDataSource<Point> OffsetPoints
         {
             get { return offset; }
             set { offset = value; }
         }

         public ObservableDataSource<Point> SteeringWheelPoints
         {
             get { return steeringWheel; }
             set { steeringWheel = value; }
         }

         public ObservableDataSource<Point> AcceleratePoints
         {
             get { return accelerate; }
             set { accelerate = value; }
         }

         public ObservableDataSource<Point> BrakePoints
         {
             get { return brake; }
             set { brake = value; }
         }

         public string Time
         {
             get
             {
                 if (currentVehicle != null)
                 {
                     float time = currentVehicle.SimulationTime;

                     return millsecondToClock(time);
                 }

                 return "00:00:00";
             }
         }
         public string Speed
         {
             get
             {
                 if (currentVehicle != null)
                 {
                     float speed = currentVehicle.Speed;
                     return speed.ToString("0.00");
                 }

                 return "0";
             }
         }
         public string Acceleration
         {
             get
             {
                 if (currentVehicle != null)
                 {
                     float acc = currentVehicle.Acceleration;
                     return acc.ToString("0.00");
                 }

                 return "0";
             }
         }

         public string TotalDistance
         {
             get
             {
                 if (currentVehicle != null)
                 {
                     return currentVehicle.TotalDistance.ToString("0.00");
                 }

                 return "0";
             }
         }

         public string BrakeDistance
         {
             get { return brakeActivity.BrakeDistance.ToString("0.00"); }
         }
         public string BrakeDistanceStart
         {
             get { return brakeActivity.BrakeStart.ToString("0.00"); }
         }
         public string BrakeDistanceEnd
         {
             get { return brakeActivity.BrakeEnd.ToString("0.00"); }
         }
         public string ReactTime
         {
             get { return reactActivity.ReactTime.ToString("0.00"); }
         }
         public string ReactTimeStart
         {
             get { return millsecondToClock(reactActivity.TimeStart); }
         }
         public string ReactTimeEnd
         {
             get { return millsecondToClock(reactActivity.TimeEnd); }
         }

         private string millsecondToClock(float time)
         {
             int ms = (int)((time - (int)time) * 100);
             int s = (int)time % 60;
             int m = ((int)time / 60) % 60;

             return standardTimeFomat(m) + ":"
                 + standardTimeFomat(s) + ":"
                 + standardTimeFomat(ms);
         }
         private string standardTimeFomat(int timeSlide)
         {
             if (timeSlide < 10)
                 return "0" + timeSlide;
             else
                 return timeSlide + "";
         }
    }
}
