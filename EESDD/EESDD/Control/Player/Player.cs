using EESDD.Control.DataModel;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace EESDD.Control.Player
{
     class Player
    {
         SimulatedVehicle currentVehicle;
         List<SimulatedVehicle> vehicles = new List<SimulatedVehicle>();
         ObservableDataSource<Point> coordinate = new ObservableDataSource<Point>();
         ObservableDataSource<Point> offset = new ObservableDataSource<Point>();
         ObservableDataSource<Point> steeringWheel = new ObservableDataSource<Point>();
         ObservableDataSource<Point> accelerate = new ObservableDataSource<Point>();

         public Player() {
             vehicles = new List<SimulatedVehicle>();
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
             coordinate = new ObservableDataSource<Point>();
             offset = new ObservableDataSource<Point>();
             steeringWheel = new ObservableDataSource<Point>();
             accelerate = new ObservableDataSource<Point>();
        }

         public void play(SimulatedVehicle vehicle) {
             Dispatcher dispatcher = PageList.Main.Dispatcher;
             currentVehicle = vehicle;
             vehicles.Add(vehicle);
             coordinate.AppendAsync(dispatcher, new Point(vehicle.PositionX, vehicle.PositionY));
             offset.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Offset));
             steeringWheel.AppendAsync(dispatcher, new Point(vehicle.SimulationTime,vehicle.WheelSpeed));
             accelerate.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Acceleration));
         }
    }
}
