using System;
using System.Windows;
using System.Windows.Controls;
using EESDD.Pages;
using EESDD.Control.Player;
using EESDD.UDP;
using System.Threading;
using EESDD.Control.Operation;
using EESDD.Control.User;
using System.Windows.Input;
using EESDD.Widgets.Buttons;

namespace EESDD
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        UDPController udpControl;
        VehicleUDP udp;
        UserSelections selection;
        User user;
        Player player;
        bool refreshing;

        public MainWindow()
        {
            InitializeComponent();
            init();
            setPage(PageList.SceneSelect);
        }
        
        internal Player Player
        {
            get { return player; }
            set { player = value; }
        }
        internal User User
        {
            get { return user; }
            set { user = value; }
        }

        internal UserSelections Selection
        {
            get { return selection; }
            set { selection = value; }
        }


        void init() {
            udpControl = new UDPController();
            selection = new UserSelections();
            user = new User();
            player = new Player();
        }
        public void setPage(Page page) {

            if (page.Equals(PageList.SceneSelect) || page.Equals(PageList.ModeSelect) 
                || page.Equals(PageList.GetReady) || page.Equals(PageList.Experience)){
                PageList.CurrentExperience = page;
            }
            
            MainFrame.Content = page;
        }

        public bool testConnection() {
            UDPTest test = new UDPTest(udpControl.Port);
            Thread thread = new Thread(test.test);
            thread.Start();
            if (thread.Join(TimeSpan.FromSeconds(2))) {
                thread.Abort();
            }
            test.close();
            return test.Connected;
        }

        //private void MinButton_Click(object sender, RoutedEventArgs e)
        //{
        //    this.WindowState = System.Windows.WindowState.Minimized;
        //}

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (CustomMessageBox.Show( "Confirmation","Do you want to close this window?")
                == true)
            {
                Application.Current.Shutdown();
            }
        }

        //private void MaxButton_Click(object sender, RoutedEventArgs e)
        //{
        //    this.WindowState = this.WindowState == System.Windows.WindowState.Normal ? 
        //        System.Windows.WindowState.Maximized : System.Windows.WindowState.Normal;
        //    //maxBtn.ToolTip = this.WindowState == System.Windows.WindowState.Normal ?
        //    //    "最大化" : "恢复";
        //}

        //private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    if (e.ChangedButton == MouseButton.Left)
        //        this.DragMove();
        //}

        public void refreshDataSource()
        {
            initRefresh();
            while (refreshing)
            {
                player.play(udp.getData());
                PageList.Experience.refreshTextBlocks();
            }
        }
        public void endRefreshDataSource(bool state)
        {
            refreshing = false;
            udp.close();
            udp = null;

            if (state)
            {
                ExperienceUnit unit = new ExperienceUnit();
                unit.SceneID = selection.SceneSelect;
                unit.Mode = selection.ModeSelect;
                unit.Vehicles = player.Vehicles;
                Evaluation evaluation = new Evaluation();
                unit.Evaluation = evaluation;

                user.addExpUnit(unit);
            }

        }
        private void initRefresh()
        {
            udp = new VehicleUDP(udpControl.Port);
            if (player.Vehicles.Count != 0)
            {
                player.reset();
            }
            refreshing = true;
        }

        private void click() {
            exp.Chosen = true;
        }

        private void exp_BtnClick(object sender, EventArgs e)
        {
            if (user.Name != null)
            {            
                setChosen((TabsButton)sender);
                PageList.Main.setPage(PageList.ModeSelect);
            }
            else
            {
                CustomMessageBox.Show("提示","请登录后查看！");
            }
        }

        private void eva_BtnClick(object sender, EventArgs e)
        {
            if (user.Name != null)
            {
                setChosen((TabsButton)sender);
                PageList.Main.setPage(PageList.Evaluation);
            }
            else
            {
                CustomMessageBox.Show("提示", "请登录后查看！");
            }
        }

        private void data_BtnClick(object sender, EventArgs e)
        {
            if (user.Name != null)
            {
                setChosen((TabsButton)sender);
                PageList.Main.setPage(PageList.Authentication);
            }
            else
            {
                CustomMessageBox.Show("提示", "请登录后查看！");
            }
        }

        private void setChosen(TabsButton t)
        {
            t.Chosen = true;

            if (!t.Equals(exp))
                exp.Chosen = false;
            if (!t.Equals(eva))
                eva.Chosen = false;
            if (!t.Equals(data))
                data.Chosen = false;
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }

    public static class PageList
    {
        static MainWindow main = (MainWindow)Application.Current.MainWindow;
        static WelcomePage welcome;
        static LoginPage login;
        static EvaluationPage evaluation;
        static SceneSelectPage sceneSelect;
        static ModeSelectPage modeSelect;
        static GetReadyPage getReady;
        static ExperiencePage experience;
        static DataExportAuthenticationPage authentication;
        static DataExportPage dataExport;
        static AboutPage about;

        static Page currentExperience;
        static Page currentEvaluation;
        static Page currentData;
        static Page currentAbout;

        public static MainWindow Main
        {
            get { return main; }
        }
        public static Page Welcome
        {
            get {
                if (welcome == null) {
                    welcome = new WelcomePage();
                }
                return welcome;
            }
        }
        public static LoginPage Login
        {
            get {
                if (login == null)
                {
                    login = new LoginPage();
                }
                return login; 
            }
        }
        public static EvaluationPage Evaluation
        {
            get {
                if (evaluation == null)
                {
                    evaluation = new EvaluationPage();
                } 
                return evaluation;
            }
        }
        public static SceneSelectPage SceneSelect
        {
            get {
                if (sceneSelect == null)
                {
                    sceneSelect = new SceneSelectPage();
                }
                return sceneSelect;
            }
        }
        public static ModeSelectPage ModeSelect
        {
            get
            {
                if (modeSelect == null)
                {
                    modeSelect = new ModeSelectPage();
                }
                return modeSelect;
            }
        }
        public static GetReadyPage GetReady
        {
            get
            {
                if (getReady == null)
                {
                    getReady = new GetReadyPage();
                }
                return getReady;
            }
        }
        public static ExperiencePage Experience
        {
            get
            {
                if (experience == null)
                {
                    experience = new ExperiencePage();
                }
                return experience;
            }
        }
        public static DataExportAuthenticationPage Authentication
        {
            get
            {
                if (authentication == null)
                {
                    authentication = new DataExportAuthenticationPage();
                }
                return authentication;
            }
        }
        public static DataExportPage DataExport
        {
            get
            {
                if (dataExport == null)
                {
                    dataExport = new DataExportPage();
                }
                return dataExport;
            }
        }
        public static AboutPage About
        {
            get
            {
                if (about == null)
                {
                    about = new AboutPage();
                }
                return about;
            }
        }

        public static Page CurrentExperience
        {
            get {
                if (currentExperience == null)
                {
                    currentExperience = PageList.SceneSelect;
                }
                return PageList.currentExperience; 
            }
            set { PageList.currentExperience = value; 
            }
        }

        public static Page CurrentEvaluation
        {
            get {
                if (currentEvaluation == null)
                {
                    currentEvaluation = Evaluation;
                }
                return PageList.currentEvaluation; 
            }
            set { PageList.currentEvaluation = value;
            }
        }
        
        public static Page CurrentData
        {
            get {
                if (currentData == null) {
                    currentData = Authentication;
                } 
                return PageList.currentData;
            }
            set { PageList.currentData = value; 
            }
        }

        public static Page CurrentAbout
        {
            get { 
                if (currentAbout == null) {
                    currentAbout = About;
                }
                return PageList.currentAbout;
            }
            set { PageList.currentAbout = value; }
        }
    }
}
