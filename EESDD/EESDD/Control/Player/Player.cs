using EESDD.Control.DataModel;
using EESDD.Control.Operation;
using EESDD.Control.User;
using EESDD.UDP;
using EESDD.VISSIM;
using EESDD.Widgets.Chart;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

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
         LinePlotter follow;

         BrakeActivity brakeActivity;
         ReactActivity reactActivity;
         float accident = 0;

         BJUTVissim vissim;
         VehicleUDP udp;

         bool braking = false;
         bool reacting = false;

         bool refreshing = false;

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
             follow     = PageList.Experience.FollowChart;
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

             //int normalIndex = UserSelections.getIndex(scene, UserSelections.NormalMode);
             //if (normalIndex != -1 && normalIndex != index && user.Index[normalIndex] != -1)
             //    plotExperience(UserSelections.NormalMode, normalIndex);

             //int distractAIndex = UserSelections.getIndex(scene, UserSelections.DistractAMode);
             //if (distractAIndex != -1 && distractAIndex != index && user.Index[distractAIndex] != -1)
             //    plotExperience(UserSelections.DistractAMode, distractAIndex);

             //int distractBIndex = UserSelections.getIndex(scene, UserSelections.DistractBMode);
             //if (distractBIndex != -1 && distractBIndex != index && user.Index[distractBIndex] != -1)
             //    plotExperience(UserSelections.DistractBMode, distractBIndex);

             //int distractCIndex = UserSelections.getIndex(scene, UserSelections.DistractCMode);
             //if (distractCIndex != -1 && distractCIndex != index && user.Index[distractCIndex] != -1)
             //    plotExperience(UserSelections.DistractCMode, distractCIndex);

             //int distractDIndex = UserSelections.getIndex(scene, UserSelections.DistractDMode);
             //if (distractDIndex != -1 && distractDIndex != index && user.Index[distractDIndex] != -1)
             //    plotExperience(UserSelections.DistractDMode, distractDIndex);
         }

         private void plotExperience(int _mode, int indexOfSelection)
         {
             ExperienceUnit unit = user.Experiences[user.Index[indexOfSelection]];
             List<SimulatedVehicle> list = unit.Vehicles;

             speed.addRealTimePoint(-1, new Point(0, unit.Top.Speed));
             offset.addRealTimePoint(-1, new Point(0, unit.Top.Offset));
             accelerate.addRealTimePoint(-1, new Point(0, unit.Top.Acceleration));
             brake.addRealTimePoint(-1, new Point(0, unit.Top.BrakePedal));
             follow.addRealTimePoint(-1, new Point(0, unit.Top.DistanceToNext));

             speed.addRealTimePoint(-1, new Point(0, unit.Bottom.Speed));
             offset.addRealTimePoint(-1, new Point(0, unit.Bottom.Offset));
             accelerate.addRealTimePoint(-1, new Point(0, unit.Bottom.Acceleration));
             brake.addRealTimePoint(-1, new Point(0, unit.Bottom.BrakePedal));
             follow.addRealTimePoint(-1, new Point(0, unit.Bottom.DistanceToNext));

             speed.addRealTimePoint(-1, new Point(unit.Right.SimulationTime, 0));
             offset.addRealTimePoint(-1, new Point(unit.Right.SimulationTime, 0));
             accelerate.addRealTimePoint(-1, new Point(unit.Right.SimulationTime, 0));
             brake.addRealTimePoint(-1, new Point(unit.Right.SimulationTime, 0));
             follow.addRealTimePoint(-1, new Point(unit.Right.SimulationTime, 0));

             foreach (SimulatedVehicle vehicle in list)
             {
                 speed.addRealTimePoint(_mode, new Point(vehicle.SimulationTime, vehicle.Speed));
                 offset.addRealTimePoint(_mode, new Point(vehicle.SimulationTime, vehicle.Offset));
                 accelerate.addRealTimePoint(_mode, new Point(vehicle.SimulationTime, vehicle.Acceleration));
                 brake.addRealTimePoint(_mode, new Point(vehicle.SimulationTime, vehicle.BrakePedal));
                 follow.addRealTimePoint(_mode, new Point(vehicle.SimulationTime, vehicle.DistanceToNext));
             }
             
         }

         public void play(SimulatedVehicle vehicle) {
             if (vehicle != null && vehicle.SimulationTime > 0.000001)
             {
                 preProcess(vehicle);

                 currentVehicle = vehicle;
                 vehicles.Add(vehicle);

                 speed.addRealTimePoint(mode, new Point(vehicle.SimulationTime, vehicle.Speed));
                 offset.addRealTimePoint(mode, new Point(vehicle.SimulationTime, vehicle.Offset));
                 accelerate.addRealTimePoint(mode, new Point(vehicle.SimulationTime, vehicle.Acceleration));
                 brake.addRealTimePoint(mode, new Point(vehicle.SimulationTime, vehicle.BrakePedal));
                 follow.addRealTimePoint(mode, new Point(vehicle.SimulationTime, vehicle.DistanceToNext));

                 setBrake(vehicle);
                 setReact(vehicle);
             }
         }

         public void preProcess(SimulatedVehicle vehicle)
         {
             if (vehicle.Area < 0)
             {
                 accident = 1;
             }

             if (PageList.Main.Selection.SceneSelect == UserSelections.SceneBrake ||
                 PageList.Main.Selection.SceneSelect == UserSelections.SceneLaneChange)
             {
                 float initLane = 4;
                 vehicle.Offset += (vehicle.Lane - initLane) * 4;
             }

             if (vehicle.DistanceToNext > 2000)
             {
                 float frontVehicleSpeed = (float)(70 / 3.6);
                 vehicle.DistanceToNext = currentVehicle.DistanceToNext
                     + vehicle.TotalDistance - currentVehicle.TotalDistance
                     - frontVehicleSpeed * (vehicle.SimulationTime - currentVehicle.SimulationTime);
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
                 unit.Accident = accident;
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
                 reactActivity.ReactTime = (vehicle.SimulationTime - reactActivity.TimeStart)*1000;
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
