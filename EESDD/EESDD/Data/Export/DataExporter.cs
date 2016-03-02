using EESDD.Control.DataModel;
using EESDD.Control.Operation;
using EESDD.Control.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EESDD.Data.Export
{
    class DataExporter
    {
        public static void exportExcel(){
            ExcelManger manger = new ExcelManger(PageList.Main.User.LoginName + ".xlsx");
            manger.openTemplate();
            int[] index = PageList.Main.User.Index;
            int count = 0;

            exportUserInfo(manger);
            for (int i = 0; i < index.Length; i++)
            {
                if (index[i] != -1)
                {
                    showTip(++count);
                    ExperienceUnit unit = PageList.Main.User.Experiences[index[i]];
                    exportAExperienceUnit(manger, unit);
                }
            }

            manger.saveFileAndExit();
        }

        public static void showTip(int count)
        {
            Application.Current.Dispatcher.BeginInvoke((Action)delegate()
            {
                PageList.DataExport.ExportTip.Text = "数据导出中，正在导出第" + count + "组数据...";
            });
        }

        private static void exportUserInfo(ExcelManger manger)
        {
            manger.createNewSheet("个人信息");
            setInfoValues(manger);
        }

        private static void setInfoValues(ExcelManger manger)
        {
            User user = PageList.Main.User;
            manger.setCellValue(3, 2, user.RealName);
            manger.setCellValue(3, 4, user.Gender);
            manger.setCellValue(3, 6, user.Age + "");
            manger.setCellValue(3, 8, user.DrivingAge + "");
            manger.setCellValue(3, 10, user.Height + "");
            manger.setCellValue(4, 2, user.Weight + "");
            manger.setCellValue(4, 4, user.Career);
            manger.setCellValue(4, 6, user.Contact);
        }

        private static void exportAExperienceUnit(ExcelManger manger, ExperienceUnit unit)
        {
            manger.createNewSheet(getSheetName(unit));
            setAllValues(manger, unit);
        }

        private static string getSheetName(ExperienceUnit unit)
        {
            string sheetName = "";
            switch (unit.SceneID)
            {
                case UserSelections.ScenePractice:
                    sheetName += "#0";
                    return sheetName;
                case UserSelections.SceneBrake:
                    sheetName += "#1";
                    break;
                case UserSelections.SceneLaneChange:
                    sheetName += "#2";
                    break;
                case UserSelections.SceneIntersection:
                    sheetName += "#3";
                    break;
            }

            switch (unit.Mode)
            {
                case UserSelections.NormalMode:
                    sheetName += " - mode = 0";
                    break;
                case UserSelections.DistractAMode:
                    sheetName += " - mode = 1";
                    break;
                case UserSelections.DistractBMode:
                    sheetName += " - mode = 2";
                    break;
                case UserSelections.DistractCMode:
                    sheetName += " - mode = 3";
                    break;
                case UserSelections.DistractDMode:
                    sheetName += " - mode = 4";
                    break;
            }

            return sheetName;
        }

        private static string getSheetTitle(ExperienceUnit unit)
        {
            string sheetTitle = PageList.Main.User.LoginName;
            switch (unit.SceneID)
            {
                case UserSelections.ScenePractice:
                    sheetTitle += " - 练习场景";
                    return sheetTitle;
                case UserSelections.SceneBrake:
                    sheetTitle += " - 跟驰刹车";
                    break;
                case UserSelections.SceneLaneChange:
                    sheetTitle += " - 前车并线";
                    break;
                case UserSelections.SceneIntersection:
                    sheetTitle += " - 路口等灯";
                    break;
            }

            switch (unit.Mode)
            {
                case UserSelections.NormalMode:
                    sheetTitle += " - 正常模式";
                    break;
                case UserSelections.DistractAMode:
                    sheetTitle += " - 微信语音";
                    break;
                case UserSelections.DistractBMode:
                    sheetTitle += " - 微信短信";
                    break;
                case UserSelections.DistractCMode:
                    sheetTitle += " - 调节收音机";
                    break;
                case UserSelections.DistractDMode:
                    sheetTitle += " - 行车导航";
                    break;
            }

            return sheetTitle;
        }

        private static void setAllValues(ExcelManger manger, ExperienceUnit unit)
        {
            manger.setCellValue(1, 1, getSheetTitle(unit));

            manger.setCellValue(4, 3, unit.Evaluation.AverageDelay.ToString("0.00"));
            manger.setCellValue(4, 5, unit.Evaluation.AverageQueueLength.ToString("0.00"));
            manger.setCellValue(4, 7, unit.Evaluation.AverageSpeed.ToString("0.00"));

            manger.setCellValue(6, 4, unit.Evaluation.MeanSpeed.ToString("0.00"));
            manger.setCellValue(7, 4, unit.Evaluation.VarianceSpeed.ToString("0.00"));
            manger.setCellValue(6, 7, unit.Evaluation.MeanAcc.ToString("0.00"));
            manger.setCellValue(7, 7, unit.Evaluation.VarianceAcc.ToString("0.00"));
            manger.setCellValue(6, 10, unit.Evaluation.MeanSteeringWheel.ToString("0.00"));
            manger.setCellValue(7, 10, unit.Evaluation.VarianceSteeringWheel.ToString("0.00"));
            manger.setCellValue(8, 4, unit.Evaluation.MeanOffset.ToString("0.00"));
            manger.setCellValue(9, 4, unit.Evaluation.VarianceOffset.ToString("0.00"));
            manger.setCellValue(8, 7, unit.Evaluation.MeanDistanceToNext.ToString("0.00"));
            manger.setCellValue(9, 7, unit.Evaluation.VarianceDistanceToNext.ToString("0.00"));

            manger.setCellValue(10, 4, unit.Evaluation.MeanSpeedEx.ToString("0.00"));
            manger.setCellValue(11, 4, unit.Evaluation.VarianceSpeedEx.ToString("0.00"));
            manger.setCellValue(10, 7, unit.Evaluation.MeanAccEx.ToString("0.00"));
            manger.setCellValue(11, 7, unit.Evaluation.VarianceAccEx.ToString("0.00"));
            manger.setCellValue(10, 10, unit.Evaluation.MeanSteeringWheelEx.ToString("0.00"));
            manger.setCellValue(11, 10, unit.Evaluation.VarianceSteeringWheelEx.ToString("0.00"));
            manger.setCellValue(12, 4, unit.Evaluation.MeanOffsetEx.ToString("0.00"));
            manger.setCellValue(13, 4, unit.Evaluation.VarianceOffsetEx.ToString("0.00"));
            manger.setCellValue(12, 7, unit.Evaluation.MeanDistanceToNextEx.ToString("0.00"));
            manger.setCellValue(13, 7, unit.Evaluation.VarianceDistanceToNextEx.ToString("0.00"));

            manger.setCellValue(15, 3, unit.Evaluation.ReactTime.ToString("0.00"));
            manger.setCellValue(15, 5, unit.Evaluation.BrakeDistance.ToString("0.00"));

            int insertRow = 18;
            foreach (SimulatedVehicle vehicle in unit.Vehicles)
            {
                manger.setCellValue(insertRow, 1, vehicle.SimulationTime.ToString("0.00"));
                manger.setCellValue(insertRow, 2, vehicle.PositionX.ToString("0.00"));
                manger.setCellValue(insertRow, 3, vehicle.PositionY.ToString("0.00"));
                manger.setCellValue(insertRow, 4, vehicle.Speed.ToString("0.00"));
                manger.setCellValue(insertRow, 5, vehicle.Acceleration.ToString("0.00"));
                manger.setCellValue(insertRow, 6, vehicle.SteeringWheel.ToString("0.00"));
                manger.setCellValue(insertRow, 7, vehicle.Offset.ToString("0.00"));
                manger.setCellValue(insertRow, 8, vehicle.BrakePedal.ToString("0.00"));
                manger.setCellValue(insertRow, 9, vehicle.TotalDistance.ToString("0.00"));
                manger.setCellValue(insertRow, 10, vehicle.DistanceToNext.ToString("0.00"));
                manger.setCellValue(insertRow, 11, vehicle.Braking.ToString("0.00"));
                manger.setCellValue(insertRow, 12, vehicle.Reacting.ToString("0.00"));
                manger.setCellValue(insertRow, 13, vehicle.Area.ToString("0.00"));
                manger.setCellValue(insertRow, 14, vehicle.Lane.ToString("0.00"));
                manger.setCellValue(insertRow, 15, vehicle.TrafficLight.ToString("0.00"));
                insertRow++;
            }
        }
    }
}
