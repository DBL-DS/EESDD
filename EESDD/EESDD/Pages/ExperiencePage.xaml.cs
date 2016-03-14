using EESDD.Control.Operation;
using EESDD.Data.Database;
using EESDD.Widgets.Buttons;
using EESDD.Widgets.Chart;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            setDataSource();
        }

        private void init() {
            ChangeButtonChosen(speed);
            ChangeMainChart(SpeedChart);
            ChangeMainChartTitle("Speed-Time");
            used = false;            
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
                ChangeMainChartTitle("Following Distance Line-Time");
                ChangeMainChart(FollowChart);
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
            setTitle();
            setImage();
            used = true;

            Thread refreshData = new Thread(PageList.Main.Player.refreshDataSource);
            refreshData.Start();

            // The Scene LaneChange & Scene Navigator don't need vissim to record
            int scene = PageList.Main.Selection.SceneSelect;
            if (scene != UserSelections.SceneLaneChange)
            {
                try
                {
                    PageList.Main.Player.initVissim();
                }
                catch (System.Exception)
                {
                    CustomMessageBox.Show("Warnning", "请确保VISSIM能够正常运行");
                    endRefresh(false);
                    PageList.Main.setPage(PageList.SceneSelect);
                    return;
                }
                Thread vissimRun = new Thread(PageList.Main.Player.UseVissim);
                vissimRun.Start();
            }
        }

        private void setTitle()
        {
            switch (PageList.Main.Selection.SceneSelect)
            {
                case UserSelections.ScenePractice:
                    MapTitle.Text = "练习场景";
                    break;
                case UserSelections.SceneBrake:
                    MapTitle.Text = "跟驰刹车";
                    break;
                case UserSelections.SceneLaneChange:
                    MapTitle.Text = "前车并线";
                    break;
                case UserSelections.SceneIntersection:
                    MapTitle.Text = "路口等灯";
                    break;
            }

            switch (PageList.Main.Selection.ModeSelect)
            {
                case UserSelections.NormalMode:
                    if (PageList.Main.Selection.SceneSelect != UserSelections.ScenePractice)
                        MapTitle.Text += "-正常模式";
                    break;
                case UserSelections.DistractAMode:
                    MapTitle.Text += "-分心模式A（微信语音）";
                    break;
                case UserSelections.DistractBMode:
                    MapTitle.Text += "-分心模式B（微信短信）";
                    break;
                case UserSelections.DistractCMode:
                    MapTitle.Text += "-分心模式C（调节收音机）";
                    break;
                case UserSelections.DistractDMode:
                    MapTitle.Text += "-分心模式D（行车导航）";
                    break;
            }
        }

        private void setImage()
        {
            mapImageOne.Visibility = System.Windows.Visibility.Hidden;
            mapImageTwo.Visibility = System.Windows.Visibility.Hidden;
            mapImageThree.Visibility = System.Windows.Visibility.Hidden;
            mapImageFour.Visibility = System.Windows.Visibility.Hidden;
            switch (PageList.Main.Selection.SceneSelect)
            {
                case UserSelections.ScenePractice:
                    mapImageOne.Visibility = System.Windows.Visibility.Visible;
                    break;
                case UserSelections.SceneBrake:
                    mapImageTwo.Visibility = System.Windows.Visibility.Visible;
                    break;
                case UserSelections.SceneLaneChange:
                    mapImageThree.Visibility = System.Windows.Visibility.Visible;
                    break;
                case UserSelections.SceneIntersection:
                    mapImageFour.Visibility = System.Windows.Visibility.Visible;
                    break;
            }
        }

        public void endRefresh(bool state)
        {
            PageList.Main.Player.endRefreshDataSource(state);
        }
        
        private void ShutDown_Click(object sender, RoutedEventArgs e)
        {
            bool? result = CustomMessageBox.Show("提示", "是否保存数据？");
            if (result != null)
            {
                endRefresh((bool)result);
                PageList.Main.setPage(PageList.SceneSelect);
                clearChart();
                UserInfoManger.saveUserInfo(PageList.Main.User);          
            }
        }

        public bool Used
        {
            get { return used; }
        }

        private void clearChart() {
            SpeedChart.clearData();
            LittleSpeed.clearData();

            AccelerationChart.clearData();
            LittleAcc.clearData();

            OffsetChart.clearData();
            LittleOffset.clearData();

            BrakeChart.clearData();

            FollowChart.clearData();
            LittleFollow.clearData();
        }
     
        private void setDataSource() {
            LittleSpeed.setLinesData(SpeedChart);
            LittleAcc.setLinesData(AccelerationChart);
            LittleOffset.setLinesData(OffsetChart);
            LittleFollow.setLinesData(FollowChart);

            double thickness = 1.0;
            LittleSpeed.clearLine();
            LittleSpeed.drawLine(thickness); 
            LittleAcc.clearLine();
            LittleAcc.drawLine(thickness);
            LittleOffset.clearLine();
            LittleOffset.drawLine(thickness);
            LittleFollow.clearLine();
            LittleFollow.drawLine(thickness);
        }
    }
}
