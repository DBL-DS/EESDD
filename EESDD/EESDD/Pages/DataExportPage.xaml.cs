using EESDD.Control.DataModel;
using EESDD.Control.Operation;
using EESDD.Control.SceneCheck;
using EESDD.Control.User;
using EESDD.Data.Database;
using EESDD.Data.Export;
using EESDD.Public;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EESDD.Pages
{
    /// <summary>
    /// DataExportPage.xaml 的交互逻辑
    /// </summary>
    public partial class DataExportPage : Page
    {
        public DataExportPage()
        {
            InitializeComponent();
            init();
        }

        private Thread exportDataThread;

        private List<SceneCheck> SceneChecks;
        private List<Scene> Scenes;
        private List<Mode> Modes;

        private void init()
        {
            SceneChecks = new List<SceneCheck>();
        }

        public void loadInfo()
        {
            User user = PageList.Main.User;
            LoginName.Text = user.LoginName;
            RealName.Text = user.RealName;
            Age.Text = user.Age + "";
            Career.Text = user.Career;
            DrivingAge.Text = user.DrivingAge + "";
            Height.Text = user.Height + "";
            Weight.Text = user.Weight + "";
            Contract.Text = user.Contact;
            Gender.Source = user.Gender == "男" ?
                new BitmapImage(new Uri(@"/EESDD;component/Images/DataExport/baby-boy-icon.png", UriKind.Relative))
                : new BitmapImage(new Uri(@"/EESDD;component/Images/DataExport/baby-girl-icon.png", UriKind.Relative));
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var grid = sender as DataGrid;
            grid.ItemsSource = SceneChecks;
        }

        public void refreshData()
        {
            SceneChecks.Clear();
            addScene(TextDef.SceneBrake, UserSelections.SceneBrake);
            addScene(TextDef.SceneLaneChange, UserSelections.SceneLaneChange);
            addScene(TextDef.SceneIntersection, UserSelections.SceneIntersection);
            ScenesGrid.Items.Refresh();
        }

        private void addScene(string sceneName, int scene)
        {
            SceneChecks.Add(new SceneCheck(
                sceneName,
                getCheck(scene, UserSelections.NormalMode),
                getCheck(scene, UserSelections.DistractAMode),
                getCheck(scene, UserSelections.DistractBMode),
                getCheck(scene, UserSelections.DistractCMode),
                getCheck(scene, UserSelections.DistractDMode)
                ));
        }

        private string getCheck(int scene, int mode)
        {
            int index = UserSelections.getIndex(scene, mode);
            return PageList.Main.User.Index[index] == -1 ? "" : "√";
        }

        private void DeleteDataBtn_Click(object sender, RoutedEventArgs e)
        {
            DataDeleteChooseGrid.Visibility = System.Windows.Visibility.Visible;
            ConfirmDeleteDataBtn.Visibility = System.Windows.Visibility.Visible;
        }

        private void ExportDataBtn_Click(object sender, RoutedEventArgs e)
        {
            exportDataThread = new Thread(DataExporter.exportExcel);
            Thread finishExportThread = new Thread(finishExport);
            exportDataThread.Start();
            finishExportThread.Start();
            StopExportBtn.Visibility = System.Windows.Visibility.Visible;
        }

        private void finishExport()
        {
            if (exportDataThread != null && exportDataThread.IsAlive)
            {
                exportDataThread.Join();
                this.Dispatcher.BeginInvoke((Action)delegate()
                {
                    StopExportBtn.Visibility = System.Windows.Visibility.Hidden;
                    ExportTip.Text = "数据导出结束";
                });                
            }
        }
        private void OpenDataFolder(object sender, RoutedEventArgs e)
        {
            Process.Start(DirectoryDef.dataExportPath);
        }

        private void StopExportBtn_Click(object sender, RoutedEventArgs e)
        {
            if (exportDataThread != null && exportDataThread.IsAlive)
            {
                exportDataThread.Abort();
            }
            StopExportBtn.Visibility = System.Windows.Visibility.Hidden;
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            setComboBoxList();

            SceneComboBox.ItemsSource = Scenes;
            SceneComboBox.DisplayMemberPath = "SceneName";
            SceneComboBox.SelectedValuePath = "SceneValue";

            ModeComboBox.ItemsSource = Modes;
            ModeComboBox.DisplayMemberPath = "ModeName";
            ModeComboBox.SelectedValuePath = "ModeValue";
        }

        private void setComboBoxList()
        {
            Scenes = new List<Scene>();
            Scenes.Add(new Scene("请选择场景", -1));
            Scenes.Add(new Scene(TextDef.SceneBrake, UserSelections.SceneBrake));
            Scenes.Add(new Scene(TextDef.SceneLaneChange, UserSelections.SceneLaneChange));
            Scenes.Add(new Scene(TextDef.SceneIntersection, UserSelections.SceneIntersection));

            Modes = new List<Mode>();
            Modes.Add(new Mode("请选择模式", -1));
            Modes.Add(new Mode(TextDef.NormalMode, UserSelections.NormalMode));
            Modes.Add(new Mode(TextDef.DistractAMode, UserSelections.DistractAMode));
            Modes.Add(new Mode(TextDef.DistractBMode, UserSelections.DistractBMode));
            Modes.Add(new Mode(TextDef.DistractCMode, UserSelections.DistractCMode));
            Modes.Add(new Mode(TextDef.DistractDMode, UserSelections.DistractDMode));
        }

        private void ConfirmDeleteDataBtn_Click(object sender, RoutedEventArgs e)
        {
            Scene selectedScene = SceneComboBox.SelectedItem as Scene;
            Mode selectedMode = ModeComboBox.SelectedItem as Mode;

            if (selectedScene.SceneValue == -1 || selectedMode.ModeValue == -1)
            {
                DeleteTip.Text = "请选择正确的场景和模式";
                return;
            }

            bool? confirmResult = CustomMessageBox.Show
                ("Confirmation","确定删除该条数据吗？ " + selectedScene.SceneName + " " + selectedMode.ModeName);
            if (confirmResult == true)
            {
                bool deleteResult = 
                    deleteData(UserSelections.getIndex(selectedScene.SceneValue, selectedMode.ModeValue));
                DeleteTip.Text = deleteResult ? "记录已成功删除" : "没有该条记录";
                refreshData();
            }
        }

        private bool deleteData(int index)
        {
            int value = PageList.Main.User.Index[index];
            if (value != -1)
            {
                PageList.Main.User.Index[index] = -1;
                UserInfoManger.saveUserInfo(PageList.Main.User);
                return true;
            }
            return false;
        }

        private void HideChooseGrid(object sender, RoutedEventArgs e)
        {
            DataDeleteChooseGrid.Visibility = System.Windows.Visibility.Hidden;
            ConfirmDeleteDataBtn.Visibility = System.Windows.Visibility.Hidden;
            DeleteTip.Text = "";
        }
    }
}
