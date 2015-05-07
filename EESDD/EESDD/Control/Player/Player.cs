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

         public Player() {
             vehicles = new List<SimulatedVehicle>();
             speed = new ObservableDataSource<Point>();
             coordinate = new ObservableDataSource<Point>();
             offset = new ObservableDataSource<Point>();
             steeringWheel = new ObservableDataSource<Point>();
             accelerate = new ObservableDataSource<Point>();           
         }
        
         public void reset() {
             currentVehicle = null;
             vehicles = null;
             coordinate = null;
             offset = null;
             steeringWheel = null;
             accelerate = null;
             vehicles = new List<SimulatedVehicle>();
             speed   = new ObservableDataSource<Point>();
             coordinate = new ObservableDataSource<Point>();
             offset = new ObservableDataSource<Point>();
             steeringWheel = new ObservableDataSource<Point>();
             accelerate = new ObservableDataSource<Point>();
        }

         public void play(SimulatedVehicle vehicle) {
             if (vehicle != null)
             {
                 Dispatcher dispatcher = PageList.Main.Dispatcher;
                 currentVehicle = vehicle;
                 vehicles.Add(vehicle);
                 speed.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Speed));
                 coordinate.AppendAsync(dispatcher, new Point(vehicle.PositionX, vehicle.PositionY));
                 offset.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Offset));
                 steeringWheel.AppendAsync(dispatcher, new Point(vehicle.SimulationTime,vehicle.WheelSpeed));
                 accelerate.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Acceleration));
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

         public ObservableDataSource<Point> Speed
         {
             get { return speed; }
             set { speed = value; }
         }


         public ObservableDataSource<Point> Coordinate
         {
             get { return coordinate; }
             set { coordinate = value; }
         }

         public ObservableDataSource<Point> Offset
         {
             get { return offset; }
             set { offset = value; }
         }

         public ObservableDataSource<Point> SteeringWheel
         {
             get { return steeringWheel; }
             set { steeringWheel = value; }
         }

         public ObservableDataSource<Point> Accelerate
         {
             get { return accelerate; }
             set { accelerate = value; }
         }

         public string Time
         {
             get
             {
                 if (currentVehicle != null)
                 {
                     float time = currentVehicle.SimulationTime;

                     int ms = (int)((time - (int)time) * 100);
                     int s = (int)time % 60;
                     int m = ((int)time / 60) % 60;

                     return standardTimeFomat(m) + ":"
                         + standardTimeFomat(s) + ":"
                         + standardTimeFomat(ms);
                 }

                 return "00:00:00";
             }
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
