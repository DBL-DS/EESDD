using EESDD.Control.DataModel;
using EESDD.Control.Operation;
using EESDD.Control.User;
using EESDD.VISSIM;
using EESDD.Widgets.Buttons;
using EESDD.Widgets.Chart;
using EESDD.Widgets.Menu;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace EESDD.Pages
{
    /// <summary>
    /// ExperiencePage.xaml 的交互逻辑
    /// </summary>
    public partial class ExperiencePage : Page
    {
        private LinePlotter currentLine;
        private ChartSelectionButton currentBtn;
        private bool used;
        public ExperiencePage()
        {
            InitializeComponent();
            init();
            bindChartSource();
        }

        private void init() {
            ChangeButtonChosen(speed);
            ChangeMainChart(SpeedChart);
            ChangeMainChartTitle("Speed-Time");
            used = false;            
        }

        public void initChart()
        {
           List<ExperienceUnit> exps = PageList.Main.User.Experiences;
            if (exps.Count != 0) {
                foreach (ExperienceUnit unit in exps)
                {
                    if (unit.SceneID != PageList.Main.Selection.SceneSelect)
                        continue;
                    else if (unit.Mode == PageList.Main.Selection.ModeSelect)
                        continue;
                    else {
                        switch (unit.Mode) { 
                            case UserSelections.NormalMode:
                                plotNormalChart(unit);
                                break;
                        }
                    }
                }
            }
        }
        private void plotNormalChart(ExperienceUnit unit) {
            List<SimulatedVehicle> vehicles = unit.Vehicles;
            ObservableDataSource<Point> normalData = new ObservableDataSource<Point>();
            Dispatcher dispatcher = PageList.Main.Dispatcher;

            foreach (SimulatedVehicle v in vehicles)
            {
                normalData.AppendAsync(dispatcher, new Point(v.SimulationTime,v.Speed));
            }
            SpeedChart.drawNormalLine(normalData);
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
        }

        private void ChangeButtonChosen(ChartSelectionButton toChange)
        {
            if (currentBtn == null || !currentBtn.Equals(toChange))
            {
                if (currentBtn != null)
                    currentBtn.Chosen = false;
                currentBtn = toChange;
                currentBtn.Chosen = true;
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

        private void bindChartSource()
        {
            SpeedChart.drawNormalLine(PageList.Main.Player.SpeedPoints);
            LittleSpeed.drawNormalLine(PageList.Main.Player.SpeedPoints);

            AccelerationChart.drawNormalLine(PageList.Main.Player.AcceleratePoints);
            LittleAcc.drawNormalLine(PageList.Main.Player.AcceleratePoints);

            OffsetChart.drawNormalLine(PageList.Main.Player.OffsetPoints);
            LittleOffset.drawNormalLine(PageList.Main.Player.OffsetPoints);

            BrakeChart.drawNormalLine(PageList.Main.Player.BrakePoints);
            LittleBrake.drawNormalLine(PageList.Main.Player.BrakePoints);
        }

        public void refreshTextBlocks()
        {
            this.Dispatcher.BeginInvoke((Action)delegate() {
                TimeDisplay.Text = PageList.Main.Player.Time;
                RealSpeed.Text = PageList.Main.Player.Speed;
                RealAcceleration.Text = PageList.Main.Player.Acceleration;
                DistanceDisplay.Text = PageList.Main.Player.TotalDistance;
                RealBrakeDistance.Text = PageList.Main.Player.BrakeDistance;
                BrakeDistanceStart.Text = PageList.Main.Player.BrakeDistanceStart;
                BrakeDistanceEnd.Text = PageList.Main.Player.BrakeDistanceEnd;
                RealReaction.Text = PageList.Main.Player.ReactTime;
                ReactTimeStart.Text = PageList.Main.Player.ReactTimeStart;
                ReactTimeEnd.Text = PageList.Main.Player.ReactTimeEnd;
            });
        }
        public void startRefresh()
        {
            used = true;
            Thread refreshData = new Thread(PageList.Main.Player.refreshDataSource);
            refreshData.Start();

            int scene = PageList.Main.Selection.SceneSelect;
            if (scene != UserSelections.SceneLaneChange)
            {
                PageList.Main.Player.initVissim();
                Thread vissimRun = new Thread(PageList.Main.Player.UseVissim);
                vissimRun.Start();
            }
        }



        public void endRefresh(bool state)
        {
            PageList.Main.Player.endRefreshDataSource(state);
        }
        
        private void ShutDown_Click(object sender, RoutedEventArgs e)
        {
            endRefresh(CustomMessageBox.Show("提示","是否保存数据？") == true ? true : false);
            PageList.Main.setPage(PageList.SceneSelect);
            clearChart();
        }

        public bool Used
        {
            get { return used; }
        }

        private void clearChart() {
            SpeedChart.clear();
            LittleSpeed.clear();

            AccelerationChart.clear();
            LittleAcc.clear();

            OffsetChart.clear();
            LittleOffset.clear();

            BrakeChart.clear();
            LittleBrake.clear();
        }
    }
}
