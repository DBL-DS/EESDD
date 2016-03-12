using EESDD.Control.DataModel;
using EESDD.Control.Operation;
using EESDD.Control.User;
using EESDD.Data.Report;
using EESDD.Public;
using EESDD.Widgets.Buttons;
using EESDD.Widgets.Chart;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace EESDD.Pages
{
    /// <summary>
    /// EvaluationPage.xaml 的交互逻辑
    /// </summary>
    public partial class EvaluationPage : Page
    {
        private LinePlotter currentLine;
        private ChartSelectionButton currentChartButton;
        private Button currentSceneButton;
        private Border currentBorder;
        private int currentScene;
        private User user;
        private int axis;   // 0 - time,  1 - distance

        private 

        Thread plotCharts;
        public EvaluationPage()
        {
            InitializeComponent();
        }

        public void init()
        {
            clearLines();
            clearBars();
            currentScene = UserSelections.ScenePractice;
            currentBorder = LittleOneBorder;
            currentSceneButton = LittleOne;
            user = PageList.Main.User;
            MainChartChange(speed, new EventArgs());
            GUISet();
            ChartSet();
            AxisxSet();
            changeBorder(currentBorder);

            PlotCharts();
        }

        private void GUISet()
        {
            NormalSample.Stroke = new SolidColorBrush(ColorDef.Normal);
            DistractASample.Stroke = new SolidColorBrush(ColorDef.DistractA);
            DistractBSample.Stroke = new SolidColorBrush(ColorDef.DistractB);
            DistractCSample.Stroke = new SolidColorBrush(ColorDef.DistractC);
            DistractDSample.Stroke = new SolidColorBrush(ColorDef.DistractD);

            ScreenShotChart.changeToPrintMode();
        }

        private void ChartSet()
        {
            normalCheck.IsChecked = true;
            distractACheck.IsChecked = true;
            distractBCheck.IsChecked = true;
            distractCCheck.IsChecked = true;
            distractDCheck.IsChecked = true;
        }

        private void AxisxSet()
        {
            timeCheck.IsChecked = true;
            axis = 0;
        }

        public void setTitle(string name)
        {
            titleTip.Text = "欢迎您，" + name + "！请选择一个场景，查看对于您体验过程的记录与评价。";
        }

        private void PlotCharts()
        {
            if (plotCharts != null && plotCharts.IsAlive)
            {
                plotCharts.Abort();
            }

            titleTip.Text = "场景数据加载中...";

            plotCharts = new Thread(refreshCurrentChart);
            plotCharts.Start();
        }

        private void Little_Enter(object sender, RoutedEventArgs e)
        {
            Button clickBtn = (Button)sender;
            if (clickBtn.Equals(currentSceneButton))
                return;

            currentSceneButton = clickBtn;

            if (clickBtn.Name.Equals("LittleOne"))
            {
                changeBorder(LittleOneBorder);
                currentScene = UserSelections.ScenePractice;
            }
            else if (clickBtn.Name.Equals("LittleTwo"))
            {
                changeBorder(LittleTwoBorder);
                currentScene = UserSelections.SceneBrake;
            }
            else if (clickBtn.Name.Equals("LittleThree"))
            {
                changeBorder(LittleThreeBorder);
                currentScene = UserSelections.SceneLaneChange;
            }
            else if (clickBtn.Name.Equals("LittleFour"))
            {
                changeBorder(LittleFourBorder);
                currentScene = UserSelections.SceneIntersection;
            }

            PlotCharts();
        }

        private void changeBorder(Border toChange)
        {
            if (!toChange.Equals(currentBorder))
            {
                currentBorder.BorderBrush = new SolidColorBrush(ColorDef.SceneButtonBorderNormal);
                currentBorder = toChange;
                currentBorder.BorderBrush = new SolidColorBrush(ColorDef.SceneButtonBorderSelected);
            }
        }

        private void MainChartChange(object sender, EventArgs e)
        {
            ChartSelectionButton select = (ChartSelectionButton)sender;

            ChangeButtonChosen(select);
            refreshMainChart();
        }

        private void refreshMainChart()
        {
            string currentChartName = currentChartButton.Name;
            switch (currentChartName)
            {
                case "speed":
                    ChangeMainChartTitle("Speed");
                    ChangeMainChart(SpeedChart);
                    break;
                case "acc":           
                    ChangeMainChartTitle("Acceleration");
                    ChangeMainChart(AccelerationChart);
                    break;
                case "brake":
                    ChangeMainChartTitle("Brake");
                    ChangeMainChart(BrakeChart);
                    break;
                case "offset":
                    ChangeMainChartTitle("Offset Middle Line");
                    ChangeMainChart(OffsetChart);
                    break;
                case "follow":
                    ChangeMainChartTitle("Following Distance");
                    ChangeMainChart(FollowChart);
                    break;
                default:
                    return;
            }
        }

        private void ChangeButtonChosen(ChartSelectionButton toChange)
        {
            if (currentChartButton == null || !currentChartButton.Equals(toChange))
            {
                if (currentChartButton != null)
                    currentChartButton.Chosen = false;
                currentChartButton = toChange;
                currentChartButton.Chosen = true;
            }
        }
        private void ChangeMainChartTitle(string TitleX)
        {
            string TitleY;
            switch (axis)
            {
                case 0:
                    TitleY = " - Time";
                    break;
                case 1:
                    TitleY = " - Distance";
                    break;
                default:
                    return;
            }
            MainChartTitle.Text = TitleX + TitleY;
        }

        private void ChangeMainChart(LinePlotter toChange)
        {
            if (currentLine == null || !currentLine.Equals(toChange))
            {
                if (currentLine != null)
                    currentLine.Visibility = System.Windows.Visibility.Hidden;
                currentLine = toChange;
                currentLine.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void plotLineChart()
        {
            int normalIndex = UserSelections.getIndex(currentScene, UserSelections.NormalMode);
            if (normalIndex != -1 && user.Index[normalIndex] != -1)
            {
                plotExperienceLine(UserSelections.NormalMode, normalIndex);
            }

            int distractAIndex = UserSelections.getIndex(currentScene, UserSelections.DistractAMode);
            if (distractAIndex != -1 && user.Index[distractAIndex] != -1)
            {
                plotExperienceLine(UserSelections.DistractAMode, distractAIndex);
            }

            int distractBIndex = UserSelections.getIndex(currentScene, UserSelections.DistractBMode);
            if (distractBIndex != -1 && user.Index[distractBIndex] != -1)
            {
                plotExperienceLine(UserSelections.DistractBMode, distractBIndex);
            }

            int distractCIndex = UserSelections.getIndex(currentScene, UserSelections.DistractCMode);
            if (distractCIndex != -1 && user.Index[distractCIndex] != -1)
            {
                plotExperienceLine(UserSelections.DistractCMode, distractCIndex);
            }

            int distractDIndex = UserSelections.getIndex(currentScene, UserSelections.DistractDMode);
            if (distractDIndex != -1 && user.Index[distractDIndex] != -1)
            {
                plotExperienceLine(UserSelections.DistractDMode, distractDIndex);
            }
        }

        private void plotExperienceLine(int _mode, int indexOfSelection)
        {
            Dispatcher dispatcher = PageList.Main.Dispatcher;
            ExperienceUnit unit = user.Experiences[user.Index[indexOfSelection]];
            List<SimulatedVehicle> list = unit.Vehicles;

            SpeedChart.Init.AppendAsync(dispatcher, new Point(0, unit.Top.Speed));
            OffsetChart.Init.AppendAsync(dispatcher, new Point(0, unit.Top.Offset));
            AccelerationChart.Init.AppendAsync(dispatcher, new Point(0, unit.Top.Acceleration));
            BrakeChart.Init.AppendAsync(dispatcher, new Point(0, unit.Top.BrakePedal));
            FollowChart.Init.AppendAsync(dispatcher, new Point(0, unit.Top.DistanceToNext));

            SpeedChart.Init.AppendAsync(dispatcher, new Point(0, unit.Bottom.Speed));
            OffsetChart.Init.AppendAsync(dispatcher, new Point(0, unit.Bottom.Offset));
            AccelerationChart.Init.AppendAsync(dispatcher, new Point(0, unit.Bottom.Acceleration));
            BrakeChart.Init.AppendAsync(dispatcher, new Point(0, unit.Bottom.BrakePedal));
            FollowChart.Init.AppendAsync(dispatcher, new Point(0, unit.Bottom.DistanceToNext));

            switch (axis)
            {
                case 0:
                    SpeedChart.Init.AppendAsync(dispatcher, new Point(unit.Right.SimulationTime, 0));
                    OffsetChart.Init.AppendAsync(dispatcher, new Point(unit.Right.SimulationTime, 0));
                    AccelerationChart.Init.AppendAsync(dispatcher, new Point(unit.Right.SimulationTime, 0));
                    BrakeChart.Init.AppendAsync(dispatcher, new Point(unit.Right.SimulationTime, 0));
                    FollowChart.Init.AppendAsync(dispatcher, new Point(unit.Right.SimulationTime, 0));
                    break;
                case 1:
                    SpeedChart.Init.AppendAsync(dispatcher, new Point(unit.Right.TotalDistance, 0));
                    OffsetChart.Init.AppendAsync(dispatcher, new Point(unit.Right.TotalDistance, 0));
                    AccelerationChart.Init.AppendAsync(dispatcher, new Point(unit.Right.TotalDistance, 0));
                    BrakeChart.Init.AppendAsync(dispatcher, new Point(unit.Right.TotalDistance, 0));
                    FollowChart.Init.AppendAsync(dispatcher, new Point(unit.Right.TotalDistance, 0));
                    break;
                default:
                    return;
            }         


            foreach (SimulatedVehicle vehicle in list)
            {
                float x;

                switch (axis)
                {
                    case 0:
                        x = vehicle.SimulationTime;
                        break;
                    case 1:
                        x = vehicle.TotalDistance;
                        break;
                    default:
                        return;
                }

                SpeedChart.addRealTimePoint(_mode, new Point(x, vehicle.Speed));
                OffsetChart.addRealTimePoint(_mode, new Point(x, vehicle.Offset));
                AccelerationChart.addRealTimePoint(_mode, new Point(x, vehicle.Acceleration));
                BrakeChart.addRealTimePoint(_mode, new Point(x, vehicle.BrakePedal));
                FollowChart.addRealTimePoint(_mode, new Point(x, vehicle.DistanceToNext));
            }

        }

        private void plotExperienceLine(int scene, int mode, LinePlotter plotter, string variable, int xAxis)
        {
            int index = UserSelections.getIndex(scene, mode);
            if (index == -1)
                return;

            Dispatcher dispatcher = PageList.Main.Dispatcher;
            ExperienceUnit unit = user.Experiences[user.Index[index]];
            List<SimulatedVehicle> list = unit.Vehicles;

            plotter.Init.AppendAsync(dispatcher, 
                new Point(0, (float)unit.Top.GetType().GetProperty(variable).GetValue(unit.Top)));
            plotter.Init.AppendAsync(dispatcher, 
                new Point(0, (float)unit.Bottom.GetType().GetProperty(variable).GetValue(unit.Bottom)));

            switch (xAxis)
            {
                case 0:
                    plotter.Init.AppendAsync(dispatcher, new Point(unit.Right.SimulationTime, 0));
                    break;
                case 1:
                    plotter.Init.AppendAsync(dispatcher, new Point(unit.Right.TotalDistance, 0));
                    break;
                default:
                    return;
            }


            foreach (SimulatedVehicle vehicle in list)
            {
                float x;

                switch (xAxis)
                {
                    case 0:
                        x = vehicle.SimulationTime;
                        break;
                    case 1:
                        x = vehicle.TotalDistance;
                        break;
                    default:
                        return;
                }

                plotter.addRealTimePoint(mode, 
                    new Point(x, (float)vehicle.GetType().GetProperty(variable).GetValue(vehicle)));
            }

        }

        private void plotBarChart() {
            switch (currentScene)
            {
                case UserSelections.SceneBrake:
                    foreach (BarDetail detail in BarChoice.SceneBrake)
                        plotExperienceBar(currentScene, detail);
                    break;
                case UserSelections.SceneLaneChange:
                    foreach (BarDetail detail in BarChoice.SceneLaneChange)
                        plotExperienceBar(currentScene, detail);
                    break;
                case UserSelections.SceneIntersection:
                    foreach (BarDetail detail in BarChoice.SceneIntersection)
                        plotExperienceBar(currentScene, detail);
                    break;
                default:
                    break;
            }
        }

        private BarChart plotExperienceBar(int scene, BarDetail detail)
        {
            BarChart bar = new BarChart();
            bars.Children.Add(bar);
            bar.setBarFromBarDetail(detail);
            bar.MinWidth = 150;

            float normalValue, distractAValue, distractBValue, distractCValue, distractDValue;
             
            normalValue = getBarChartValue(scene, UserSelections.NormalMode, detail.DataName);
            distractAValue = getBarChartValue(scene, UserSelections.DistractAMode, detail.DataName);
            distractBValue = getBarChartValue(scene, UserSelections.DistractBMode, detail.DataName);
            distractCValue = getBarChartValue(scene, UserSelections.DistractCMode, detail.DataName);
            distractDValue = getBarChartValue(scene, UserSelections.DistractDMode, detail.DataName);

            bar.setValue(normalValue, distractAValue, distractBValue, distractCValue, distractDValue);
            return bar;
        }

        private float getBarChartValue(int scene, int mode, string dataName)
        {
            int index = UserSelections.getIndex(scene, mode);
            float result;

            if (index != -1 && user.Index[index] != -1)
            {
                ExperienceUnit unit = user.Experiences[user.Index[index]];
                result = (float)unit.Evaluation.GetType().GetProperty(dataName).GetValue(unit.Evaluation);
            }
            else
                result = 0;

            return result;
        }

        public void refreshCurrentChart()
        {
            refreshChart();
            plotLineChart();
            this.Dispatcher.BeginInvoke((Action)delegate()
            {
                titleTip.Text = "数据加载完毕。";
            });
        }

        private void refreshChart()
        {
            this.Dispatcher.BeginInvoke((Action)delegate()
            {
                clearLines();
                clearBars();
                plotBarChart();
                scanChecks();
            });
        }

        private void clearLines()
        {
            SpeedChart.clearData();
            OffsetChart.clearData();
            AccelerationChart.clearData();
            BrakeChart.clearData();
            FollowChart.clearData();
        }

        private void clearBars()
        {
            bars.Children.Clear();
        }

        private void chartSelect(object sender, RoutedEventArgs e)
        {
            string name = ((CheckBox)sender).Name;
            int mode = 0;
            bool? check = ((CheckBox)sender).IsChecked;

            switch (name)
            {
                case "normalCheck":
                    mode = UserSelections.NormalMode;
                    break;
                case "distractACheck":
                    mode = UserSelections.DistractAMode;
                    break;
                case "distractBCheck":
                    mode = UserSelections.DistractBMode;
                    break;
                case "distractCCheck":
                    mode = UserSelections.DistractCMode;
                    break;
                case "distractDCheck":
                    mode = UserSelections.DistractDMode;
                    break;
                default:
                    return;
            }

            changeChartsVisible(mode, check == true);
        }

        private void changeChartsVisible(int chartMode, bool visible)
        {
            if (bars != null && bars.Children != null)
            {
                foreach (BarChart chart in bars.Children)
                {
                    if (chart != null)
                    {
                        chart.setBarVisible(chartMode, visible);
                    }
                }
            }

            SpeedChart.setLineVisible(chartMode, visible);
            OffsetChart.setLineVisible(chartMode, visible);
            AccelerationChart.setLineVisible(chartMode, visible);
            BrakeChart.setLineVisible(chartMode, visible);
            FollowChart.setLineVisible(chartMode, visible);

        }

        private void scanChecks()
        {
            setDefaultChartsCheck();

            changeChartsVisible(UserSelections.NormalMode, normalCheck.IsChecked == true);
            changeChartsVisible(UserSelections.DistractAMode, distractACheck.IsChecked == true);
            changeChartsVisible(UserSelections.DistractBMode, distractBCheck.IsChecked == true);
            changeChartsVisible(UserSelections.DistractCMode, distractCCheck.IsChecked == true);
            changeChartsVisible(UserSelections.DistractDMode, distractDCheck.IsChecked == true);
        }

        private void setDefaultChartsCheck()
        {
            if (currentScene == UserSelections.ScenePractice)
            {
                distractACheck.IsChecked = false;
                distractBCheck.IsChecked = false;
                distractCCheck.IsChecked = false;
                distractDCheck.IsChecked = false;
            }
            else if (currentScene == UserSelections.SceneIntersection)
            {
                distractDCheck.IsChecked = false;
            }
        }

        private void AxisCheck(object sender, RoutedEventArgs e)
        {
            string axisName = ((RadioButton)sender).Name;

            switch (axisName)
            {
                case "timeCheck":
                    axis = 0;
                    break;
                case "distanceCheck":
                    axis = 1;
                    break;
                default:
                    return;
            }

            refreshMainChart();
            PlotCharts();
        }

        private void ShowDistract(object sender, RoutedEventArgs e)
        {
            currentLine.Visibility = System.Windows.Visibility.Hidden;
            ScreenShotChart.Visibility = System.Windows.Visibility.Visible;
            plotExperienceLine(UserSelections.SceneBrake, UserSelections.NormalMode, ScreenShotChart, "Speed", 1);
            ImageMaker.ViewToPng(LineChart, DirectoryDef.PictureTempPath);
        }

        private void ExportPdf(object sender, RoutedEventArgs e)
        {
            ReportGenerator generator = new ReportGenerator();
            Thread exportPdfThread = new Thread(generator.generate);
            exportPdfThread.Start();
        }

        private void SaveScreenShots()
        {
            
        }

        public void SaveLineScreenShot(int scene, string variable, string filePath, int xAsix)
        {
            ScreenShotChart.clearData();

            plotExperienceLine(scene, UserSelections.NormalMode, ScreenShotChart, variable, xAsix);
            plotExperienceLine(scene, UserSelections.DistractAMode, ScreenShotChart, variable, xAsix);
            plotExperienceLine(scene, UserSelections.DistractBMode, ScreenShotChart, variable, xAsix);
            plotExperienceLine(scene, UserSelections.DistractCMode, ScreenShotChart, variable, xAsix);
            plotExperienceLine(scene, UserSelections.DistractDMode, ScreenShotChart, variable, xAsix);

            Thread.Sleep(5000);
            ImageMaker.ViewToPng(ScreenShotChart, filePath);
        }

        public void SaveBarScreenShot(int scene, BarDetail detail, string filePath)
        {
            this.Dispatcher.BeginInvoke((Action)delegate()
            {
                clearBars();

                BarChart bar = plotExperienceBar(scene, detail);
                bar.changeToPrintMode();
                
            });
            ImageMaker.ViewToPng(bars, filePath);

        }

        public void reportPrinting()
        {
            reportProgress(0);
            this.Dispatcher.BeginInvoke((Action)delegate()
            {
                currentLine.Visibility = System.Windows.Visibility.Hidden;
                ScreenShotChart.Visibility = System.Windows.Visibility.Visible;
                MainPanel.IsEnabled = false;
            });
        }
        public void reportProgress(int progress)
        {
            this.Dispatcher.BeginInvoke((Action)delegate()
            {
                titleTip.Text = "评估报告生成中..." + progress + "%";
            });
        }
        public void reportPrinted()
        {
            this.Dispatcher.BeginInvoke((Action)delegate()
            {
                titleTip.Text = "评估报告生成成功";
                MainPanel.IsEnabled = true;

                clearBars();
                plotBarChart();
                ScreenShotChart.Visibility = System.Windows.Visibility.Hidden;
                currentLine.Visibility = System.Windows.Visibility.Visible;
            });
        }
    }
}
