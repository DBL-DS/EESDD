using EESDD.Control.DataModel;
using EESDD.Control.Operation;
using EESDD.Control.User;
using EESDD.UDP;
using EESDD.VISSIM;
using EESDD.Widgets.Chart;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using EESDD.Control.User;

namespace EESDD.Control.Player
{
     class Player
    {
         User.User user;
         Dispatcher dispatcher;

         SimulatedVehicle currentVehicle;
         List<SimulatedVehicle> vehicles;
         LinePlotter speed;
         LinePlotter offset;
         LinePlotter accelerate;
         LinePlotter brake;

         BrakeActivity brakeActivity;
         ReactActivity reactActivity;

         BJUTVissim vissim;
         VehicleUDP udp;

         bool braking = false;
         bool reacting = false;

         bool refreshing = false;

         int playTimes = 0;
         int scene;
         int mode;

         public Player() {
             init();
         }

         void init()
         {
             dispatcher = PageList.Main.Dispatcher;

             speed      = PageList.Experience.SpeedChart;
             offset     = PageList.Experience.OffsetChart;
             accelerate = PageList.Experience.AccelerationChart;
             brake      = PageList.Experience.BrakeChart;
         }
        
         private void reset() {
             user = PageList.Main.User;

             mode = PageList.Main.Selection.ModeSelect;
             scene = PageList.Main.Selection.SceneSelect;

             currentVehicle = null;
             vehicles = new List<SimulatedVehicle>();

             brakeActivity = new BrakeActivity();
             reactActivity = new ReactActivity();
        }

         private void initChart() {
             int index = PageList.Main.Selection.Index;           

             int normalIndex = UserSelections.getIndex(scene, UserSelections.NormalMode);
             if (normalIndex != index && user.Index[normalIndex] != -1)
                 plotExperience(UserSelections.NormalMode, normalIndex);

             int distractAIndex = UserSelections.getIndex(scene, UserSelections.LowDistractedMode);
             if (distractAIndex != index && user.Index[distractAIndex] != -1)
                 plotExperience(UserSelections.LowDistractedMode, distractAIndex);

             int distractBIndex = UserSelections.getIndex(scene, UserSelections.HighDistractedMode);
             if (distractBIndex != index && user.Index[distractBIndex] != -1)
                 plotExperience(UserSelections.HighDistractedMode, distractBIndex);
         }

         private void plotExperience(int _mode, int indexOfSelection)
         {
             ExperienceUnit unit = user.Experiences[user.Index[indexOfSelection]];
             List<SimulatedVehicle> list = unit.Vehicles;

             speed.Init.AppendAsync(dispatcher, new Point(0, unit.Top.Speed));
             offset.Init.AppendAsync(dispatcher, new Point(0, unit.Top.Offset));
             accelerate.Init.AppendAsync(dispatcher, new Point(0, unit.Top.Acceleration));
             brake.Init.AppendAsync(dispatcher, new Point(0, unit.Top.BrakePedal));
             speed.Init.AppendAsync(dispatcher, new Point(0, unit.Bottom.Speed));
             offset.Init.AppendAsync(dispatcher, new Point(0, unit.Bottom.Offset));
             accelerate.Init.AppendAsync(dispatcher, new Point(0, unit.Bottom.Acceleration));
             brake.Init.AppendAsync(dispatcher, new Point(0, unit.Bottom.BrakePedal));
             speed.Init.AppendAsync(dispatcher, new Point(unit.Right.SimulationTime, 0));
             offset.Init.AppendAsync(dispatcher, new Point(unit.Right.SimulationTime, 0));
             accelerate.Init.AppendAsync(dispatcher, new Point(unit.Right.SimulationTime, 0));
             brake.Init.AppendAsync(dispatcher, new Point(unit.Right.SimulationTime, 0));

             if (_mode == UserSelections.NormalMode)
             {
                 foreach (SimulatedVehicle vehicle in list)
                 {
                     speed.Normal.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Speed));
                     offset.Normal.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Offset));
                     accelerate.Normal.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Acceleration));
                     brake.Normal.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.BrakePedal));               
                 }
             }
             else if (_mode == UserSelections.LowDistractedMode)
             {
                 foreach (SimulatedVehicle vehicle in list)
                 {
                     speed.DistractA.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Speed));
                     offset.DistractA.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Offset));
                     accelerate.DistractA.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Acceleration));
                     brake.DistractA.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.BrakePedal));
                 }
             }
             else if (_mode == UserSelections.HighDistractedMode)
             {
                 foreach (SimulatedVehicle vehicle in list)
                 {
                     speed.DistractB.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Speed));
                     offset.DistractB.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Offset));
                     accelerate.DistractB.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Acceleration));
                     brake.DistractB.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.BrakePedal));
                 }
             }
         }

         public void play(SimulatedVehicle vehicle) {
             if (vehicle != null && vehicle.SimulationTime > 0.000001)
             {                
                 currentVehicle = vehicle;
                 vehicles.Add(vehicle);

                 if (mode == UserSelections.NormalMode)
                 {
                     speed.Normal.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Speed));
                     offset.Normal.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Offset));
                     accelerate.Normal.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Acceleration));
                     brake.Normal.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.BrakePedal));
                 }
                 else if (mode == UserSelections.LowDistractedMode)
                 {
                     speed.DistractA.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Speed));
                     offset.DistractA.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Offset));
                     accelerate.DistractA.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Acceleration));
                     brake.DistractA.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.BrakePedal));
                 }
                 else if (mode == UserSelections.HighDistractedMode)
                 {
                     speed.DistractB.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Speed));
                     offset.DistractB.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Offset));
                     accelerate.DistractB.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Acceleration));
                     brake.DistractB.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.BrakePedal));
                 }

                 setBrake(vehicle);
                 setReact(vehicle);
             }
         }

         public void UseVissim()
         {
             vissim.Run();
         }
         public void initVissim()
         {
             vissim = new BJUTVissim();
         }
         public void refreshDataSource()
         {
             initRefresh();
             skipUselessData();

             while (refreshing)
             {
                 play(udp.getData());
                 PageList.Experience.refreshTextBlocks();

                 if (vissim != null)
                 {
                    vissim.set(currentVehicle);
                 }
             }
         }
         private void skipUselessData() {
             SimulatedVehicle vehicleFirst = udp.getData();
             while (true)
             {
                 if (udp == null)
                     break;
                 SimulatedVehicle vehicleNext = udp.getData();
                 if (vehicleNext == null || vehicleNext.SimulationTime == vehicleFirst.SimulationTime)
                     continue;
                 else
                 {
                     play(vehicleNext);
                     PageList.Experience.refreshTextBlocks();

                     if (vissim != null)
                     {
                         vissim.set(currentVehicle);
                     }
                     break;
                 }
             }
         }
         public void endRefreshDataSource(bool state)
         {
             refreshing = false;
             udp.close();
             udp = null;

             if (vissim != null && PageList.Main.Selection.SceneSelect != UserSelections.SceneLaneChange)
                vissim.Stop();

             if (state)
             {
                 ExperienceUnit unit = new ExperienceUnit();
                 unit.SceneID = PageList.Main.Selection.SceneSelect;
                 unit.Mode = PageList.Main.Selection.ModeSelect;
                 unit.Vehicles = PageList.Main.Player.Vehicles;
                 unit.BrakeAct = this.brakeActivity;
                 unit.ReactAct = this.reactActivity;
                 Evaluation evaluation = new Evaluation(unit);
                 unit.Evaluation = evaluation;

                 PageList.Main.User.NewExperience = unit;
             }
         }
         private void initRefresh()
         {
             udp = new VehicleUDP(PageList.Main.UdpControl.Port);
             reset();
             initChart();
             refreshing = true;
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
