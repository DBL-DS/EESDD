using EESDD.Control.DataModel;
using EESDD.Control.Operation;
using EESDD.Control.User;
using EESDD.Public;
using EESDD.Widgets.Buttons;
using EESDD.Widgets.Chart;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
        private int currentScene;
        private User user;
        public EvaluationPage()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            currentScene = UserSelections.ScenePractice;
            currentSceneButton = LittleOne;
            user = PageList.Main.User;
            MainChartChange(speed, new EventArgs());
            GUISet();
        }

        private void GUISet()
        {
            NormalSample.Stroke = new SolidColorBrush(ColorDef.Normal);
            DistractASample.Stroke = new SolidColorBrush(ColorDef.DistractA);
            DistractBSample.Stroke = new SolidColorBrush(ColorDef.DistractB);
            DistractCSample.Stroke = new SolidColorBrush(ColorDef.DistractC);
            DistractDSample.Stroke = new SolidColorBrush(ColorDef.DistractD);
        }

        public void setTitle(string name)
        {
            titleTip.Text = "欢迎您，" + name + "！请选择一个场景，查看对于您体验过程的记录与评价。";
        }

        private void Little_Enter(object sender, RoutedEventArgs e)
        {
            Button clickBtn = (Button)sender;
            if (clickBtn.Equals(currentSceneButton))
                return;

            currentSceneButton = clickBtn;

            if (clickBtn.Name.Equals("LittleOne"))
            {
                currentScene = UserSelections.ScenePractice;
            }
            else if (clickBtn.Name.Equals("LittleTwo"))
            {
                currentScene = UserSelections.SceneBrake;
            }
            else if (clickBtn.Name.Equals("LittleThree"))
            {
                currentScene = UserSelections.SceneLaneChange;
            }
            else if (clickBtn.Name.Equals("LittleFour"))
            {
                currentScene = UserSelections.SceneIntersection;
            }

            refreshCurrentChart();
        }

        private void MainChartChange(object sender, EventArgs e)
        {
            ChartSelectionButton select = (ChartSelectionButton)sender;

            ChangeButtonChosen(select);

            if (select.Name.Equals("speed"))
            {
                ChangeMainChartTitle("Speed-Time");
                ChangeMainChart(SpeedChart);
            }
            else if (select.Name.Equals("acc"))
            {
                ChangeMainChartTitle("Acceleration-Time");
                ChangeMainChart(AccelerationChart);
            }
            else if (select.Name.Equals("brake"))
            {
                ChangeMainChartTitle("Brake-Time");
                ChangeMainChart(BrakeChart);
            }
            else if (select.Name.Equals("offset"))
            {
                ChangeMainChartTitle("Offset Middle Line-Time");
                ChangeMainChart(OffsetChart);
            }
            else if (select.Name.Equals("follow"))
            {
                ChangeMainChartTitle("Following Distance-Time");
                ChangeMainChart(FollowChart);
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
        private void ChangeMainChartTitle(string Title)
        {
            MainChartTitle.Text = Title;
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
            clearLines();

            int normalIndex = UserSelections.getIndex(currentScene, UserSelections.NormalMode);
            if (normalIndex != -1 && user.Index[normalIndex] != -1)
                plotExperienceLine(UserSelections.NormalMode, normalIndex);

            int distractAIndex = UserSelections.getIndex(currentScene, UserSelections.DistractAMode);
            if (distractAIndex != -1 && user.Index[distractAIndex] != -1)
                plotExperienceLine(UserSelections.DistractAMode, distractAIndex);

            int distractBIndex = UserSelections.getIndex(currentScene, UserSelections.DistractBMode);
            if (distractBIndex != -1 && user.Index[distractBIndex] != -1)
                plotExperienceLine(UserSelections.DistractBMode, distractBIndex);
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

            SpeedChart.Init.AppendAsync(dispatcher, new Point(unit.Right.SimulationTime, 0));
            OffsetChart.Init.AppendAsync(dispatcher, new Point(unit.Right.SimulationTime, 0));
            AccelerationChart.Init.AppendAsync(dispatcher, new Point(unit.Right.SimulationTime, 0));
            BrakeChart.Init.AppendAsync(dispatcher, new Point(unit.Right.SimulationTime, 0));
            FollowChart.Init.AppendAsync(dispatcher, new Point(unit.Right.SimulationTime, 0));

            if (_mode == UserSelections.NormalMode)
            {
                foreach (SimulatedVehicle vehicle in list)
                {
                    SpeedChart.Normal.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Speed));
                    OffsetChart.Normal.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Offset));
                    AccelerationChart.Normal.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Acceleration));
                    BrakeChart.Normal.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.BrakePedal));
                    FollowChart.Normal.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.DistanceToNext));
                }
            }
            else if (_mode == UserSelections.DistractAMode)
            {
                foreach (SimulatedVehicle vehicle in list)
                {
                    SpeedChart.DistractA.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Speed));
                    OffsetChart.DistractA.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Offset));
                    AccelerationChart.DistractA.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Acceleration));
                    BrakeChart.DistractA.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.BrakePedal));
                    FollowChart.DistractA.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.DistanceToNext));
                }
            }
            else if (_mode == UserSelections.DistractBMode)
            {
                foreach (SimulatedVehicle vehicle in list)
                {
                    SpeedChart.DistractB.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Speed));
                    OffsetChart.DistractB.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Offset));
                    AccelerationChart.DistractB.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.Acceleration));
                    BrakeChart.DistractB.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.BrakePedal));
                    FollowChart.DistractB.AppendAsync(dispatcher, new Point(vehicle.SimulationTime, vehicle.DistanceToNext));
                }
            }
        }

        private void plotBarChart() {
            clearBars();
            switch (currentScene)
            {
                case UserSelections.SceneBrake:
                    foreach (BarDetail detail in BarChoice.SceneBrake)
                        plotExperienceBar(detail);
                    break;
                case UserSelections.SceneLaneChange:
                    foreach (BarDetail detail in BarChoice.SceneLaneChange)
                        plotExperienceBar(detail);
                    break;
                case UserSelections.SceneIntersection:
                    foreach (BarDetail detail in BarChoice.SceneIntersection)
                        plotExperienceBar(detail);
                    break;
                default:
                    break;
            }
        }

        private void plotExperienceBar(BarDetail detail)
        {
            BarChart bar = new BarChart();
            bars.Children.Add(bar);
            bar.setBarFromBarDetail(detail);
            bar.MinWidth = 150;

            float normalValue, distractAValue, distractBValue, distractCValue, distractDValue;

            normalValue = getBarChartValue(currentScene, UserSelections.NormalMode, detail.DataName);
            distractAValue = getBarChartValue(currentScene, UserSelections.DistractAMode, detail.DataName);
            distractBValue = getBarChartValue(currentScene, UserSelections.DistractBMode, detail.DataName);
            distractCValue = getBarChartValue(currentScene, UserSelections.DistractCMode, detail.DataName);
            distractDValue = getBarChartValue(currentScene, UserSelections.DistractDMode, detail.DataName);


            bar.setValue(normalValue, distractAValue, distractBValue, distractCValue, distractDValue);
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
            plotLineChart();
            plotBarChart();
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
            if (bars != null && bars.Children != null)
            {
                foreach (BarChart chart in bars.Children)
                {
                    if (chart != null)
                    {
                        chart.setBarVisible(mode, ((CheckBox)sender).IsChecked == true ? true : false);
                    }
                }
            }
        }
    }
}
